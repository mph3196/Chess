using SplashKitSDK;

using Chess.Enums;
using Chess.Interfaces;
using Chess.Model;
using Chess.View;
using Chess.Model.Factories;

namespace Chess.Controller;

public class GameController : IScreenClickedObserver, IStateChangedObserver
{
    private readonly GameModel _model;
    private readonly GameView _view;
    private BoardState? _boardState;
    private readonly StockfishHTTP _stockfish;
    private Player _whitePlayer = Player.HUMAN;
    private Player _blackPlayer = Player.AI;
    private Player _currentPlayer;

    public GameController(GameModel model, GameView view)
    {
        _model = model;
        _view = view;
        _stockfish = new StockfishHTTP();
        _model.Subscribe(this);
        _view.Subscribe(this);
        OnModelStateChanged();
    }

    public void Run()
    {
        
        while (!_view.Window.CloseRequested)
        {
            SplashKit.ProcessEvents();
            _view.Update();
            if (_currentPlayer == Player.AI)
            {
                StockfishMove();                
            }
        }
    }

    private void StockfishMove()
    {
        Thread.Sleep(2000);
        try
        {
            string fen = _model.ToFEN();
            string uci = _stockfish.GetMoveFromResponse(fen);
            if (uci != null)
            {
                Move move = DecodeUCI(uci);
                Console.WriteLine("UCI decoded");
                _model.ExecuteMove(move);
                Console.WriteLine($"AI moved: {uci}");
                _model.AdvanceTurn();
                OnModelStateChanged();
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Network error: {ex.Message}\nChanging to human players");
            _whitePlayer = Player.HUMAN;
            _blackPlayer = Player.HUMAN;
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Error during AI turn: {ex.Message}\nChanging to human players");
            _whitePlayer = Player.HUMAN;
            _blackPlayer = Player.HUMAN;
        }
        catch (TaskCanceledException ex)
        {
            Console.WriteLine($"Request timed out: {ex.Message}");
            _whitePlayer = Player.HUMAN;
            _blackPlayer = Player.HUMAN;
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Too many exceptions! {ex.Message}");
            _whitePlayer = Player.HUMAN;
            _blackPlayer = Player.HUMAN;
        }
        finally
        {
            _currentPlayer = _model.CurrentTurn == PieceColor.WHITE  ? _whitePlayer : _blackPlayer;
        }
    }

    public void OnModelStateChanged()
    {
        Console.WriteLine("Controller: Model changed");
        _currentPlayer = _model.CurrentTurn == PieceColor.WHITE  ? _whitePlayer : _blackPlayer;
        _boardState = _model.GetBoardState();
        _boardState.UpdateStateFromController(_stockfish.GetDifficulty(), _whitePlayer.ToString(), _blackPlayer.ToString());
        _view.UpdateDisplay(_boardState);
    }


   public void OnSquareClicked(int rank, BoardFile file)
    {
        Console.WriteLine($"Controller: Received click at {file}{rank}");
        _model.SquareClicked(rank, file);
    }

    public void OnButtonClicked(string action)
    {
        switch (action)
        {
            case "newGame":
                Console.WriteLine("Starting new game");
                _model.Initialise(new StandardPieceFactory());
                break;
            case "swapWhite":
                Console.WriteLine("Swapping white player");
                _whitePlayer = _whitePlayer == Player.HUMAN ? Player.AI : Player.HUMAN;
                break;
            case "swapBlack":
                Console.WriteLine("Swapping black player");
                _blackPlayer = _blackPlayer == Player.HUMAN ? Player.AI : Player.HUMAN;
                break;
            case "lowerDifficulty":
                Console.WriteLine("Lowering difficulty");
                _stockfish.ChangeDifficulty(-5); break;
            case "increaseDifficulty":
                Console.WriteLine("Increasing difficulty");
                _stockfish.ChangeDifficulty(5);
                break;
            case "queens":
                _model.Initialise(new QueenFactory());
                _whitePlayer = Player.HUMAN;
                _blackPlayer = Player.HUMAN;
                break;
            default:
                Console.WriteLine($"Unknown action: {action}");
                break;
        }
        OnModelStateChanged();
    }

    private Move DecodeUCI(string uci)
    {
        char fromFileChar = uci[0];
        char fromRankChar = uci[1];
        char toFileChar   = uci[2];
        char toRankChar   = uci[3];

        BoardFile fromFile = (BoardFile)(char.ToLower(fromFileChar) - 'a');
        int      fromRank  = fromRankChar - '0';
        BoardFile toFile   = (BoardFile)(char.ToLower(toFileChar) - 'a');
        int      toRank    = toRankChar - '0';

        return new Move(fromRank, fromFile, toRank, toFile);
    }

}

