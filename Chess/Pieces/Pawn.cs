using System;
using System.Drawing;
using Chess.Tasks.Movement;

namespace Chess.Pieces
{
    public class Pawn : IPiece
    {
        public Side Side { get; internal set; }

        public Predicate<Point> MovePattern => direction => UsualMovePattern(direction)
                                                            || FirstMovePattern(direction)
                                                            || KillPattern(direction)
                                                            || EnPassPattern(direction);

        public Predicate<Point> UsualMovePattern => direction => direction.X == 1 && direction.Y == 0;
        public Predicate<Point> FirstMovePattern => direction => direction.X < 3 && direction.X > 0 && direction.Y == 0;
        public Predicate<Point> KillPattern => direction => direction.X < 2 && direction.X > 0 && Math.Abs(direction.Y) == 1;
        public Predicate<Point> EnPassPattern => direction => direction.X < 2 && direction.X > 0 && Math.Abs(direction.Y) == 0;
        
        public MoveSelector MakeSelector()
        {
            return new PawnMoveSelector(this);
        }

        public override string ToString()
        {
            return GetType().Name;
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