using Chess.Desk;
using Chess.Pieces;

namespace Chess.Tasks
{
    public class InvertDesk : IBoardAction
    {
        private Board _board;

        public void Visit(Board board)
        {
            _board = board;
        }

        public void Visit(Cell cell)
        {
            var index = _board.GetCellIndex(cell);
            
            if (index >= _board.Length / 2) return;
            
            var length = _board.Length;
            var toward = _board[length - index];
            
            Swap(cell, toward);
        }

        private void Swap(Cell from, Cell to)
        {
            (to.Piece, from.Piece) = (from.Piece, to.Piece);
        }

        public void Accept(IBoardAction action)
        {
            throw new System.NotImplementedException();
        }
    }
}