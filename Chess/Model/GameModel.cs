using Chess.Enums;
using Chess.Interfaces;
using Chess.Model.Factories;
using Chess.Model.Pieces;

namespace Chess.Model;

public class GameModel : Subject<IStateChangedObserver>
{
    private Board _board;
    private MoveValidator _moveValidator;
    private PieceColor _currentTurn;
    private int _turnNumber;
    private int _fullMoveCounter;
    private bool _isCheck;
    private bool _isCheckmate;
    private bool _isStalemate;
    private List<Move>? _availableMoves;


    public GameModel()
    {
        _board = new Board();
        _moveValidator = new MoveValidator(_board);  
        _turnNumber = 0;
        _fullMoveCounter = 1;
        _isCheck = false;
        _isCheckmate = false;
        _isStalemate = false;
        _availableMoves = null;
        Initialise(new StandardPieceFactory());
    }

    public void Initialise(IPieceFactory pieceFactory)
    {
        foreach (Square s in _board.Squares)
        {
            s.Occupant = null;
            s.Selected = false;
        }
        _board.PlacePieces(pieceFactory.CreatePieces());
        _currentTurn = PieceColor.WHITE;
        _turnNumber = 0;
        _fullMoveCounter = 1;
        _isCheck = false;
        _isCheckmate = false;
        _isStalemate = false;
        _availableMoves = null;
        NotifyObservers(observer => observer.OnModelStateChanged());
    }
    public void SquareClicked(int rank, BoardFile file)
    {
        Square clickedSquare = _board.GetSquare(rank, file);
        if (clickedSquare == null) return;

        if (_availableMoves == null)
        {
            if (clickedSquare.Occupied && clickedSquare.Occupant?.Color == _currentTurn)
            {
                ClearSelection();
                SelectPiece(clickedSquare);
            }
            else
            {
                ClearSelection();
            }
        }
        else
        {
            if (!TryExecuteMove(clickedSquare))
            {
                ClearSelection();
            }
        }

        NotifyObservers(observer => observer.OnModelStateChanged());
    }

    private void ClearSelection()
    {
        _board.ClearSelection();
        _availableMoves = [];
    }

    private void SelectPiece(Square s)
    {
        List<Move> getMoves = s.Occupant!.GetLegalMoves(s.Rank, s.File,_board.Squares);
        _availableMoves = _moveValidator.RemoveIllegalMoves(getMoves);
        s.Selected = true;
        foreach (Move move in _availableMoves)
        {
            Square target = _board.GetSquare(move.ToRank, move.ToFile);
            target.Selected = true;
        }
    }

    private bool TryExecuteMove(Square clickedSquare)
    {
        bool moveExecuted = false;
        foreach (Move move in _availableMoves)
        {
            if (move.ToRank == clickedSquare.Rank && move.ToFile == clickedSquare.File)
            {
                ExecuteMove(move);
                AdvanceTurn();
                ClearSelection();
                NotifyObservers(observer => observer.OnModelStateChanged());
                moveExecuted = true;
            }
        }
        return moveExecuted;
    }

    public void ExecuteMove(Move move)
    {
        Square? fromSquare = null;
        Square? toSquare = null;

        foreach (Square s in _board.Squares)
        {
            if (s.Rank == move.FromRank && s.File == move.FromFile)
                fromSquare = s;
            if (s.Rank == move.ToRank && s.File == move.ToFile)
                toSquare = s;
        }

        toSquare!.Occupant = fromSquare!.Occupant;
        toSquare.Occupant!.HasMoved = true;
        fromSquare.Occupant = null;

        if (toSquare.Occupant.Type == PieceType.PAWN)
        {
            int backRank = toSquare.Occupant.Color == PieceColor.WHITE ? 8 : 1;
            if (toSquare.Rank == backRank)
            {
                toSquare.Occupant = new Queen(toSquare.Occupant.Color);
            }
        }
    }

    public void AdvanceTurn()
    {
        if (_currentTurn == PieceColor.WHITE)
        {
            _currentTurn = PieceColor.BLACK;
        }
        else
        {
            _currentTurn = PieceColor.WHITE;
            _fullMoveCounter++;
        }
        _turnNumber++;

        _isCheck = _moveValidator.IsKingInCheck(_currentTurn);
        CheckmateOrStalemate();
    }

    private void CheckmateOrStalemate()
    {
        bool hasMoves = false;

        foreach (Square s in _board.Squares)
        {
            if (s.Occupied && s.Occupant!.Color == _currentTurn)
            {
                List<Move> moves = s.Occupant.GetLegalMoves(s.Rank, s.File, _board.Squares);
                moves = _moveValidator.RemoveIllegalMoves(moves);
                if (moves.Count > 0)
                {
                    hasMoves = true;
                    break;
                }
            }
        }
    
        if (!hasMoves)
        {
            if (_isCheck)
            {
                _isCheckmate = true;
            }
            else
            {
                _isStalemate = true;
            }
        }
    }

    public BoardState GetBoardState()
    {
        BoardState state;
        List<SquareState> squares = new List<SquareState>();

        foreach (Square s in _board.Squares)
        {
            PieceInfo? pieceState = null;
            if (s.Occupied)
            {
                Piece piece = s.Occupant!;
                pieceState = new PieceInfo(piece.Color, piece.Type);
            }
            SquareState squareState = new SquareState(s.Rank, s.File, s.Selected, pieceState);
            squares.Add(squareState);
        }
        state = new BoardState(squares, _currentTurn, _isCheck, _isCheckmate, _turnNumber);
        return state;
    }

    public string ToFEN()
    {
        FENEncoder encoder = new FENEncoder();
        char sideToMove = _currentTurn == PieceColor.WHITE ? 'w' : 'b';
        string castlingAbility = "-";
        string enPassant = "-";
        int halfMoveClock = _turnNumber;
        int fullMoveCounter = _fullMoveCounter;
        return encoder.Encode(sideToMove, castlingAbility, enPassant, halfMoveClock, fullMoveCounter, _board.Squares);
    }

    public PieceColor CurrentTurn
    {
        get { return _currentTurn; }
    }
    
}