using Chess.Enums;
using Chess.Interfaces;
using Chess.Model.Factories;
using Chess.Model.Pieces;

namespace Chess.Model;

public class BoardModel : Subject<IStateChangedObserver>
{
    private FENEncoder _encoder;
    private SquareLookup _squareLookup;
    private List<Square> _squares;
    private List<Piece> _pieces;
    private PieceColor _currentTurn;
    private int _turnNumber;
    private int _fullMoveCounter;
    private bool _isCheck;
    private bool _isCheckmate;
    private bool _isStalemate;
    private List<Move> _availableMoves;


    public BoardModel()
    {        
        _turnNumber = 0;
        _fullMoveCounter = 1;
        _squares = new List<Square>(64);

        for (int rank = 1; rank <= 8; rank++)
        {
            for (BoardFile file = BoardFile.A; file <= BoardFile.H; file++)
            {
                _squares.Add(new Square(rank, file));
            }
        }

        _encoder = new FENEncoder(_squares);
        _squareLookup = new SquareLookup(_squares);
        Initialise(new StandardPieceFactory());
    }

    public PieceColor CurrentTurn
    {
        get { return _currentTurn; }
    }

    public List<Square> Squares
    {
        get { return _squares; }
    }
    
    public void Initialise(IPieceFactory pieceFactory)
    {
        foreach (Square s in _squares)
        {
            s.Occupant = null;
            s.Selected = false;
        }
        _pieces = pieceFactory.CreatePieces();
        PlacePieces();
        _currentTurn = PieceColor.WHITE;
        _turnNumber = 0;
        _fullMoveCounter = 1;
        _isCheck = false;
        _isCheckmate = false;
        _isStalemate = false;
        NotifyObservers(observer => observer.OnModelStateChanged());
    }

    private void PlacePieces()
    {
        int i = 0;

        // White back rank
        for (BoardFile f = BoardFile.A; f <= BoardFile.H; f++)
        {
            Square? s = _squareLookup.GetSquare(1, f);
            s!.Occupant = _pieces[i++];
        }
        // White front rank
        for (BoardFile f = BoardFile.A; f <= BoardFile.H; f++)
        {
            Square? s = _squareLookup.GetSquare(2, f);
            s!.Occupant = _pieces[i++];
        }
        // Black front rank
        for (BoardFile f = BoardFile.A; f <= BoardFile.H; f++)
        {
            Square? s = _squareLookup.GetSquare(7, f);
            s!.Occupant = _pieces[i++];
        }
        // Black back rank
        for (BoardFile f = BoardFile.A; f <= BoardFile.H; f++)
        {
            Square? s = _squareLookup.GetSquare(8, f);
            s!.Occupant = _pieces[i++];
        }                                          
    }

    public void SquareClicked(int rank, BoardFile file)
    {
        Square clickedSquare = _squareLookup.GetSquare(rank, file);
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
        List<Move> getMoves = s.Occupant.GetLegalMoves(s.Rank, s.File, _squares, _squareLookup);
        _availableMoves = RemoveIllegalMoves(getMoves);
        s.Selected = true;
        foreach (Move move in _availableMoves)
        {
            Square target = _squareLookup.GetSquare(move.ToRank, move.ToFile);
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

        _isCheck = IsKingInCheck(_currentTurn);
        CheckmateOrStalemate();
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

        foreach (Square s in _squares)
        {
            if (s.Rank == move.FromRank && s.File == move.FromFile)
                fromSquare = s;
            if (s.Rank == move.ToRank && s.File == move.ToFile)
                toSquare = s;
        }

        toSquare.Occupant = fromSquare.Occupant;
        toSquare.Occupant.HasMoved = true;
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

    public string ToFEN()
    {
        char sideToMove = _currentTurn == PieceColor.WHITE ? 'w' : 'b';
        string castlingAbility = "-";
        string enPassant = "-";
        int halfMoveClock = _turnNumber;
        int fullMoveCounter = _fullMoveCounter;
        return _encoder.Encode(sideToMove, castlingAbility, enPassant, halfMoveClock, fullMoveCounter, _squareLookup);
    }

    public bool IsSquareAttacked(int rank, BoardFile file, PieceColor attackingColor)
    {
        foreach (Square s in _squares)
        {
            if (!s.Occupied || s.Occupant?.Color != attackingColor)
            {
                continue;
            }

            List<Move> moves = s.Occupant.GetLegalMoves(s.Rank, s.File, _squares, _squareLookup);
            foreach (Move move in moves)
            {
                if (move.ToRank == rank && move.ToFile == file)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool IsKingInCheck(PieceColor color)
    {
        foreach (Square s in _squares)
        {
            if (s.Occupied && s.Occupant.Type == PieceType.KING && s.Occupant.Color == color)
            {
                PieceColor opponent = color == PieceColor.WHITE ? PieceColor.BLACK : PieceColor.WHITE;
                return IsSquareAttacked(s.Rank, s.File, opponent) ;
            }
        }
        return false;
    }

    private bool MoveLeavesKingChecked(Move move, PieceColor movingColor)
    {
        Square from = _squareLookup.GetSquare(move.FromRank, move.FromFile);
        Square to = _squareLookup.GetSquare(move.ToRank, move.ToFile);
        Piece captured = to.Occupant;
        Piece movedPiece = from.Occupant;

        to.Occupant = movedPiece;
        from.Occupant = null;

        bool kingInCheck = IsKingInCheck(movingColor);

        from.Occupant = movedPiece;
        to.Occupant = captured;

        return kingInCheck;
    }

    public List<Move> RemoveIllegalMoves(List<Move> moves)
    {
        List<Move> legalMoves = new List<Move>();

        foreach (Move m in moves)
        {
            Square s = _squareLookup.GetSquare(m.FromRank, m.FromFile);
            PieceColor occupantColor = s.Occupant.Color;
            if (!MoveLeavesKingChecked(m, occupantColor))
            {
                legalMoves.Add(m);
            }
        }
        return legalMoves;
    }

    public void CheckmateOrStalemate()
    {
        bool hasMoves = false;

        foreach (Square s in _squares)
        {
            if (s.Occupied && s.Occupant.Color == _currentTurn)
            {
                List<Move> moves = s.Occupant.GetLegalMoves(s.Rank, s.File, _squares, _squareLookup);
                moves = RemoveIllegalMoves(moves);
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
}