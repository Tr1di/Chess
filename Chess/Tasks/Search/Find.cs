using Chess.Desk;
using Chess.Pieces;

namespace Chess.Tasks.Search
{
    public class Find : IBoardAction<Cell>
    {
        private readonly IPiece _piece;

        public Cell Result { get; private set; }

        public Find(IPiece piece)
        {
            _piece = piece;
        }

        public void Visit(Board board)
        {}

        public void Visit(Cell cell)
        {
            if (Result != null) return;
            
            if (cell.Piece == _piece)
            {
                Result = cell;
            }
        }

        public void Accept(IBoardAction action)
        {
            Result.Accept(action);
        }
    }
}