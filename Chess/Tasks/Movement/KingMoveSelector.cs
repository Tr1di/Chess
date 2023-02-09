using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Desk;
using Chess.Pieces;
using Chess.Tasks.Check;

namespace Chess.Tasks.Movement
{
    public class KingMoveSelector : MoveSelector
    {
        private static Func<MoveExecutor, MoveExecutor> IfCastleMove => executor =>
        {
            return executor;
        };
        
        private readonly King _king;
        private readonly Predicate<Point> _castlePattern;
        private bool HasMoved => Board?.GameSession?.HasMoved(_king) ?? false;
        private List<Cell> RooksToCastle => Board.FindAll<Rook>().Result
            .Where(x => x.Piece.Side == _king.Side)
            .Where(x => !Board.GameSession.HasMoved(x.Piece))
            .Where(x =>
            {
                var canMove = new HasDirectMove(x.Piece, From.Location, x.Location);
                Board.Accept(canMove);
                return canMove.Result;
            })
            .ToList();

        public KingMoveSelector(IPiece piece, Predicate<Point> movePattern, Predicate<Point> castlePattern) : base(piece, movePattern)
        {
            _king = piece as King ?? throw new ArgumentException();
            _castlePattern = castlePattern;
            ModifyExecutor(IfCastleMove);
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
            
            return RooksToCastle.FirstOrDefault(rook =>
                {
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
            toward = _king.Side == Side.White ? toward.Invert() : toward;

            var rook = GetRookForCastlingByDirection(toward);
            if (rook != null)
            {
                // exec.AddSubmove(new MoveExecutor(rook, cell, ));
            }
            return exec;
        }
    }
}