using System;
using System.Drawing;
using Chess.Tasks.Movement;

namespace Chess.Pieces
{
    public enum Side
    {
        None,
        Black,
        White,
        Max
    }
    
    public interface IPiece
    {
        Side Side { get; }
        Predicate<Point> MovePattern { get; }
        Predicate<Point> KillPattern { get; }
        MoveSelector Selector { get; }
    }
}
