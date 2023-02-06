using System;
using System.Drawing;
using Chess.Tasks.Movement;

namespace Chess.Pieces
{
    public class Bishop : IPiece
    {
        public Side Side { get; internal set; }

        public Predicate<Point> MovePattern => direction => Math.Abs(direction.X) == Math.Abs(direction.Y);
        
        public MoveSelector MakeSelector()
        {
            return new MoveSelector(this);
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