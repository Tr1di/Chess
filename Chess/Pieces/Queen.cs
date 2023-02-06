using System;
using System.Drawing;
using Chess.Tasks.Movement;

namespace Chess.Pieces
{
    public class Queen : IPiece
    {
        public Side Side { get; internal set; }

        public Predicate<Point> MovePattern => direction => Math.Abs(direction.X) == Math.Abs(direction.Y) 
                                                            || direction.X == 0 
                                                            || direction.Y == 0;
        
        public MoveSelector MakeSelector()
        {
            return new MoveSelector(this);
        }
    }
    
    public class QueenFabric : IPieceFabric
    {
        public IPiece CreatePiece(Side side)
        {
            return new Queen
            {
                Side = side
            };
        }
    }
}