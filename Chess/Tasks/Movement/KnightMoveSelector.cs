using System;
using System.Drawing;
using Chess.Pieces;

namespace Chess.Tasks.Movement
{
    public class KnightMoveSelector : MoveSelector
    {
        public KnightMoveSelector(IPiece piece, Predicate<Point> movePattern) 
            : base(piece, movePattern)
        { }
    }
}