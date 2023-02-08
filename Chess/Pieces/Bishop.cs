using System;
using System.Drawing;
using Chess.Tasks.Movement;

namespace Chess.Pieces
{
    public class Bishop : IPiece
    {
        public Predicate<Point> MovePattern => direction => Math.Abs(direction.X) == Math.Abs(direction.Y);
        public Predicate<Point> KillPattern => MovePattern;
        public Side Side { get; internal set; }
        
        public MoveSelector Selector => new MoveSelector(this, MovePattern);

        public override string ToString()
        {
            return $"{ Side } { GetType().Name }";
        }
    }
    
    public class BishopFabric : IPieceFabric
    {
        public IPiece CreatePiece(Side side)
        {
            return new Bishop
            {
                Side = side
            };
        }
    }
}