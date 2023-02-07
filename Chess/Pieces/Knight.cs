using System;
using System.Drawing;
using Chess.Tasks.Movement;

namespace Chess.Pieces
{
    public class Knight : IPiece
    {
        private readonly float _moveLength = new Point(2, 1).Length();
        
        public Side Side { get; internal set; }

        public Predicate<Point> MovePattern => direction => direction.Length().Equals(_moveLength);
        
        public MoveSelector MakeSelector()
        {
            return new KnightMoveSelector(this);
        }
        
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