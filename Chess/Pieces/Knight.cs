using System;
using System.Drawing;
using Chess.Tasks.Movement;

namespace Chess.Pieces
{
    public class Knight : IPiece
    {
        private static readonly float MoveLength = new Point(2, 1).Length();
        
        public Side Side { get; internal set; }

        public Predicate<Point> MovePattern => direction => direction.Length().Equals(MoveLength);
        public Predicate<Point> KillPattern => MovePattern;

        public MoveSelector Selector => new KnightMoveSelector(this, MovePattern);

        public override string ToString()
        {
            return GetType().Name;
        }
    }
    
    public class KnightFabric : IPieceFabric
    {
        public IPiece CreatePiece(Side side)
        {
            return new Knight
            {
                Side = side
            };
        }
    }
}