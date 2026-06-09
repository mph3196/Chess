using Chess.Enums;
using Chess.Interfaces;
using Chess.Model.Factories;
using Chess.Model.Pieces;

namespace Chess.Model;

public class BoardModel : Subject<IStateChangedObserver>
{
    private FENEncoder _encoder;
    private List<Square> _squares;
    private List<Piece> _pieces;
    private PieceColor _currentTurn;
    private int _turnNumber;
    private int _fullMoveCounter;
    private bool _isCheck;
    private bool _isCheckmate;
    private List<Move> _availableMoves;


    public BoardModel()
    {
        _squares = new List<Square>(64);
        for (int rank = 1; rank <= 8; rank++)
        {
            for (BoardFile file = BoardFile.A; file <= BoardFile.H; file++)
            {
                _squares.Add(new Square(rank, file));
            }
        }
        _pieces = new List<Piece>();
        _turnNumber = 0;
        _fullMoveCounter = 1;
        _encoder = new FENEncoder(_squares);
    }
    
    public void Initialise(IPieceFactory pieceFactory)
    {
        _pieces = pieceFactory.CreatePieces();
        PlacePieces();
        NotifyObservers(observer => observer.OnModelStateChanged());
        _currentTurn = PieceColor.WHITE;
        _turnNumber = 1;
    }

    private void PlacePieces()
    {
        int i = 0;

        // White back rank
        for (BoardFile f = BoardFile.A; f <= BoardFile.H; f++)
        {
            Square? s = GetSquare(1, f);
            s!.Occupant = _pieces[i++];
        }
        // White pawns
        for (BoardFile f = BoardFile.A; f <= BoardFile.H; f++)
        {
            Square? s = GetSquare(2, f);
            s!.Occupant = _pieces[i++];
        }
        // Black pawns
        for (BoardFile f = BoardFile.A; f <= BoardFile.H; f++)
        {
            Square? s = GetSquare(7, f);
            s!.Occupant = _pieces[i++];
        }
        // Black back rankk
        for (BoardFile f = BoardFile.A; f <= BoardFile.H; f++)
        {
            Square? s = GetSquare(8, f);
            s!.Occupant = _pieces[i++];
        }                                          
    }

    public Square? GetSquare(int rank, BoardFile file)
    {
        Square square = null;
        foreach (Square s in _squares)
        {
            if (s.AreYou(rank, file))
            {
                square = s;
            }
        }
        return square;
    }

    public List<Square> Squares
    {
        get { return _squares; }
    }

    public void SquareClicked(int rank, BoardFile file)
    {
        Square clickedSquare = GetSquare(rank, file);
        if (clickedSquare == null) return;

        if (_availableMoves == null)
        {
            if (clickedSquare.Occupied && clickedSquare.Occupant.Color == _currentTurn)
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
        foreach (Square s in _squares)
        {
            s.Selected = false;
        }
        _availableMoves = null;
    }

    private void SelectPiece(Square s)
    {
        var lookup = new SquareLookup(_squares);
        _availableMoves = s.Occupant.GetLegalMoves(s.Rank, s.File, _squares, lookup);
        s.Selected = true;
        foreach (Move move in _availableMoves)
        {
            Square target = GetSquare(move.ToRank, move.ToFile);
            target.Selected = true;
        }
    }

    private bool TryExecuteMove(Square clickedSquare)
    {
        foreach (Move move in _availableMoves)
        {
            if (move.ToRank == clickedSquare.Rank && move.ToFile == clickedSquare.File)
            {
                ExecuteMove(move);
                AdvanceTurn();
                ClearSelection();
                NotifyObservers(observer => observer.OnModelStateChanged());
                return true;
            }
        }
        return false;
    }

    private void AdvanceTurn()
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
    }

    public BoardState GetBoardState()
    {
        BoardState state;
        List<SquareState> squares = new List<SquareState>();

        foreach (Square s in _squares)
        {
            PieceInfo? pieceState = null;
            if (s.Occupied)
            {
                Piece piece = s.Occupant;
                pieceState = new PieceInfo(piece.Color, piece.Type);
            }
            SquareState squareState = new SquareState(s.Rank, s.File, s.Selected, pieceState);
            squares.Add(squareState);
        }
        state = new BoardState(squares, _currentTurn, _isCheck, _isCheckmate, _turnNumber);
        return state;
    }

    public void ExecuteMove(Move move)
    {
        Square fromSquare = null;
        Square toSquare = null;
        foreach (Square s in Squares)
        {
            if (s.Rank == move.FromRank && s.File == move.FromFile)
            {
                fromSquare = s;
            }
            if (s.Rank == move.ToRank && s.File == move.ToFile)
            {
                toSquare = s;
            }
        }
        toSquare.Occupant = fromSquare.Occupant;
        toSquare.Occupant.HasMoved = true;
        fromSquare.Occupant = null;
        
    }

    public string ToFEN()
    {
        char sideToMove = _currentTurn == PieceColor.WHITE ? 'w' : 'b';
        string castlingAbility = "-";
        string enPassant = "-";
        int halfMoveClock = _turnNumber;
        int fullMoveCounter = _fullMoveCounter;
        return _encoder.Encode(sideToMove, castlingAbility, enPassant, halfMoveClock, fullMoveCounter);
    }

    public PieceColor CurrentTurn
    {
        get { return _currentTurn; }
    }
}