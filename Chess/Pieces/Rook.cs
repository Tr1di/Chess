using System;
using System.Drawing;
using Chess.Tasks.Movement;

namespace Chess.Pieces
{
    public class Rook : IPiece
    {
        public Predicate<Point> MovePattern => direction => direction.X == 0 || direction.Y == 0;
        public Predicate<Point> KillPattern => MovePattern;

        public Side Side { get; internal set; }

        public MoveSelector Selector => new MoveSelector(this, MovePattern);

        public override string ToString()
        {
            return $"{ Side } { GetType().Name }";
        }
    }
    
    public class RookFabric : IPieceFabric
    {
        public IPiece CreatePiece(Side side)
        {
            return new Rook
            {
                Side = side
            };
        }
    }
}