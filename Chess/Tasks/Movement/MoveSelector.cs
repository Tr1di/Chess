using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Desk;
using Chess.Pieces;
using Chess.Tasks.Check;

namespace Chess.Tasks.Movement
{
    public class MoveSelector : IBoardAction<IEnumerable<MoveExecutor>>
    {
        private readonly List<MoveExecutor> _result;
        private Cell _from;
        
        protected readonly IPiece Piece;
        protected Cell From => _from;
        protected Board Board;

        public IEnumerable<MoveExecutor> Result => FilteredResult();

        public MoveSelector(IPiece piece)
        {
            Piece = piece;
            _result = new List<MoveExecutor>();
        }

        protected virtual bool HasDirectMove(Point toward)
        {
            var canMove = new HasDirectMove(Piece, toward);
            Accept(canMove);
            return canMove.Result;
        }

        protected virtual bool WillProtectKing(Point toward)
        {
            var canProtect = new WillProtectKing(Piece, toward);
            Board.Accept(canProtect);
            return canProtect.Result;
        }
        
        protected virtual bool CanPerformMove(Cell cell)
        {
            if (cell.Piece?.Side == Piece.Side) return false;
            return HasDirectMove(cell.Location)
                && WillProtectKing(cell.Location);
        }

        protected virtual bool IsSatisfiesMovePattern(Point toward)
        {
            return Piece.MovePattern(toward);
        }

        private IEnumerable<MoveExecutor> FilteredResult()
        {
            return _result.FindAll(x => CanPerformMove(x.Toward));
        }

        protected virtual MoveExecutor CreateMoveExecutor(Cell cell)
        {
            return new MoveExecutor(_from, cell);
        }
        
        public virtual void Visit(Board board)
        {
            Board = board;
            _from = board.Find(Piece);
        }

        public void Visit(Cell cell)
        {
            var toward = cell.Location.Relative(_from.Location);
            if (Piece.Side == Side.White) toward = toward.Invert();
            
            if (IsSatisfiesMovePattern(toward))
            {
                _result.Add(CreateMoveExecutor(cell));
            }
        }

        public void Accept(IBoardAction action)
        {
            action.Visit(Board);
            _result.Select(x=> x.Toward).ToList().ForEach(cell => cell.Accept(action));
        }
    }
}