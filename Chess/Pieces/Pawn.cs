using System;
using System.Drawing;
using Chess.Tasks.Movement;

namespace Chess.Pieces
{
    public class Pawn : IPiece
    {

        public Predicate<Point> MovePattern => direction => direction.X == 1 && direction.Y == 0;
        public Predicate<Point> FirstMovePattern => direction => direction.X < 3 && direction.X > 0 && direction.Y == 0;
        public Predicate<Point> KillPattern => direction => direction.X == 1 && Math.Abs(direction.Y) == 1;
        private Predicate<Point> EnPassPattern => direction => direction.X < 2 && direction.X > 0 && Math.Abs(direction.Y) == 0;
        
        public Side Side { get; internal set; }

        public MoveSelector Selector => new PawnMoveSelector(this, MovePattern, KillPattern, FirstMovePattern);

        public override string ToString()
        {
            return $"{ Side } { GetType().Name }";
        }
    }
    
    public class PawnFabric : IPieceFabric
    {
        public IPiece CreatePiece(Side side)
        {
            return new Pawn
            {
                Side = side
            };
        }
    }
}