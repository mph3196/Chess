using Chess.Enums;
using Chess.Interfaces;
using Chess.Model.Factories;
using Chess.Model.Pieces;

namespace Chess.Model;

public class BoardModel : Subject<IStateChangedObserver>
{
    private List<Square> _squares;
    private List<Piece> _pieces;
    private PieceColor _currentTurn;
    private int _turnNumber;
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

    }
    
    public void Initialise(IPieceFactory pieceFactory)
    {
        _pieces = pieceFactory.CreatePieces();
        PlacePieces();
        NotifyObservers(observer => observer.OnModelStateChanged());
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
        List<Square> selectedSquares = new List<Square>();
        foreach (Square s in Squares)
        {
            if (s.Rank == rank && s.File == file)
            {
                selectedSquares.Add(s);
                if (_availableMoves != null)
                {
                    foreach (Move move in _availableMoves)
                    {
                        if (move.ToRank == rank && move.ToFile == file)
                        {
                            ExecuteMove(move);     
                            foreach (Square sq in Squares)
                            {
                                sq.Selected = false;
                            }
                            _availableMoves = null;
                            NotifyObservers(observer => observer.OnModelStateChanged());
                            return;
                        }
                    }
                    _availableMoves = null;

                }
                else if (s.Occupied)
                {
                    Console.WriteLine($"Occupant: {s.Occupant.Color} {s.Occupant.Type}");
                    _availableMoves = s.Occupant.GetLegalMoves(rank, file, Squares);
                    foreach (Move move in _availableMoves)
                    {
                        int targetRank = move.ToRank;
                        BoardFile targetFile = move.ToFile;
                        foreach (Square sq in Squares)
                        {
                            if (sq.Rank == targetRank && sq.File == targetFile)
                            {
                                selectedSquares.Add(sq);
                            }
                        }
                    }
                }
                else
                {
                    _availableMoves = null;
                }
            }
            s.Selected = false;
        }
        if (selectedSquares != null)
        {
            foreach (Square s in selectedSquares)
            {
                s.Selected = true;
            }
        }
        NotifyObservers(observer => observer.OnModelStateChanged());
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

    
}