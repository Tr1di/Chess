using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Desk;
using Chess.Pieces;

namespace Chess.Tasks.Movement
{
    public class KingMoveSelector : MoveSelector
    {
        private readonly King _king;
        private bool HasMoved => Board?.GameSession?.HasMoved(_king) ?? false;
        private List<Cell> _rooksToCastle;
        
        public KingMoveSelector(IPiece piece) : base(piece)
        {
            _king = piece as King ?? throw new ArgumentException();
            _rooksToCastle = new List<Cell>();
        }

        protected override bool CanPerformMove(Cell cell)
        {
            //TODO: Is safe on castle way
            return base.CanPerformMove(cell);
        }

        private bool CanCastle(Point point)
        {
            if (!Board.GameSession.CanCastle) return false;
            if (HasMoved) return false;
            if (!_king.CastlePattern(point)) return false;

            return _rooksToCastle.Exists(rook =>
            {
                var relativeNormalized = rook.Location.Relative(From.Location).Normalize();
                return relativeNormalized.NearlyEquals(point.Normalize());
            });
        }
        
        protected override bool IsSatisfiesMovePattern(Cell toward, Point relative)
        {
            return _king.UsualMovePattern(relative) || CanCastle(relative);
        }

        protected override MoveExecutor CreateMoveExecutor(Cell cell)
        {
            var exec = base.CreateMoveExecutor(cell);
            if (false)
            {
                // exec.AddSubmove(new MoveExecutor(From, cell, ));
            }
            return exec;
        }
        
        public override void Visit(Board board)
        {
            base.Visit(board);
            _rooksToCastle = Board.FindAll<Rook>().Result
                .Where(x => x.Piece.Side == _king.Side)
                .Where(x => !Board.GameSession.HasMoved(x.Piece))
                .ToList();
        }
    }
}