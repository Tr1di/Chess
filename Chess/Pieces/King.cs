using System;
using System.Drawing;
using Chess.Tasks.Movement;

namespace Chess.Pieces
{
    public class King : IPiece
    {
        public Side Side { get; internal set; }

        public Predicate<Point> MovePattern => direction => UsualMovePattern(direction) || CastlePattern(direction);
        public Predicate<Point> UsualMovePattern => direction => Math.Abs(direction.X) < 2 && Math.Abs(direction.Y) < 2;
        public Predicate<Point> CastlePattern = direction => direction.X == 0 && Math.Abs(direction.Y) == 2;

        public MoveSelector MakeSelector()
        {
            return new KingMoveSelector(this);
        }
        
        public override string ToString()
        {
            return GetType().Name;
        }
        
    }
    
    public class KingFabric : IPieceFabric
    {
        public IPiece CreatePiece(Side side)
        {
            return new King
            {
                Side = side
            };
        }
    }
}