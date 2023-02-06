using System.Collections.Generic;
using Chess.Desk;
using Chess.Pieces;

namespace Chess.Tasks.Search
{
    public class FindAll<TPiece> : IBoardAction<IEnumerable<Cell>> where TPiece : IPiece
    {
        private readonly List<Cell> _result;

        public IEnumerable<Cell> Result => _result;

        public FindAll()
        {
            _result = new List<Cell>();
        }

        public void Visit(Board board)
        {}

        public void Visit(Cell cell)
        {
            if (!(cell.Piece is TPiece)) return;
            _result.Add(cell);
        }

        public void Accept(IBoardAction action)
        {
            _result.ForEach(cell => cell.Accept(action));
        }
    }
}