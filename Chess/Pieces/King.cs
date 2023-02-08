using System;
using System.Drawing;
using Chess.Tasks.Movement;

namespace Chess.Pieces
{
    public class King : IPiece
    {
        public Side Side { get; internal set; }
        
        public Predicate<Point> MovePattern => direction => Math.Abs(direction.X) < 2 && Math.Abs(direction.Y) < 2;
        public Predicate<Point> KillPattern => MovePattern;
        public Predicate<Point> CastlePattern => direction => direction.X == 0 && Math.Abs(direction.Y) == 2;
        
        public MoveSelector Selector => new KingMoveSelector(this, MovePattern, CastlePattern);

        public override string ToString()
        {
            return $"{ Side } { GetType().Name }";
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