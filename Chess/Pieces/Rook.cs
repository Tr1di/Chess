using System;
using System.Drawing;
using Chess.Tasks.Movement;

namespace Chess.Pieces
{
    public class Rook : IPiece
    {
        public Side Side { get; internal set; }

        public Predicate<Point> MovePattern => direction => direction.X == 0 || direction.Y == 0;
        
        public MoveSelector MakeSelector()
        {
            return new MoveSelector(this);
        }
        
        public override string ToString()
        {
            return GetType().Name;
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