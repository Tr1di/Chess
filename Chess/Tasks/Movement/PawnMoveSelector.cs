using System;
using System.Drawing;
using Chess.Desk;
using Chess.Pieces;

namespace Chess.Tasks.Movement
{
    public class PawnMoveSelector : MoveSelector
    {
        private readonly Pawn _pawn;
        private readonly Predicate<Point> _firstMovePattern;
        private bool HasMoved => Board?.GameSession?.HasMoved(_pawn) ?? false;

        public PawnMoveSelector(IPiece piece, 
            Predicate<Point> movePattern, 
            Predicate<Point> killPattern = null, 
            Predicate<Point> firstMovePattern = null) 
            : base(piece, movePattern, killPattern)
        {
            _pawn = piece as Pawn ?? throw new ArgumentException();
            _firstMovePattern = firstMovePattern;
        }

        protected override bool IsSatisfiesMovePattern(Cell toward, Point relative)
        {
            return base.IsSatisfiesMovePattern(toward, relative)
                   || (!HasMoved && _firstMovePattern(relative));
        }
    }
}