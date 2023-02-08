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
        private readonly Predicate<Point> _castlePattern;
        private bool HasMoved => Board?.GameSession?.HasMoved(_king) ?? false;
        private List<Cell> _rooksToCastle;
        
        public KingMoveSelector(IPiece piece, Predicate<Point> movePattern, Predicate<Point> castlePattern) : base(piece, movePattern)
        {
            _king = piece as King ?? throw new ArgumentException();
            _rooksToCastle = new List<Cell>();
            _castlePattern = castlePattern;
        }

        protected override bool CanPerformMove(Cell cell)
        {
            //TODO: Is safe on castle way
            return base.CanPerformMove(cell);
        }

        private bool CanCastle(Point point)
        {
            return GetRookForCastlingByDirection(point) != null;
        }

        private Cell GetRookForCastlingByDirection(Point point)
        {
            if (!Board.GameSession.CanCastle) return null;
            if (HasMoved) return null;
            if (!_castlePattern(point)) return null;
            
            return _rooksToCastle.First(rook =>
            {
                if (Board.GameSession.HasMoved(rook.Piece)) return false;
                var relativeNormalized = rook.Location.Relative(From.Location).Normalize();
                return relativeNormalized.NearlyEquals(point.Normalize());
            });
        }
        
        protected override bool IsSatisfiesMovePattern(Cell toward, Point relative)
        {
            return base.IsSatisfiesMovePattern(toward, relative) || CanCastle(relative);
        }

        protected override MoveExecutor CreateMoveExecutor(Cell cell)
        {
            var exec = base.CreateMoveExecutor(cell);
            var toward = cell.Location.Relative(From.Location);
            toward = Piece.Side == Side.White ? toward.Invert() : toward;

            var rook = GetRookForCastlingByDirection(toward);
            if (rook != null)
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