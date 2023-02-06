using Chess.Pieces;

namespace Chess.Tasks.Movement
{
    public class KnightMoveSelector : MoveSelector
    {
        public KnightMoveSelector(IPiece piece) : base(piece)
        {}
    }
}