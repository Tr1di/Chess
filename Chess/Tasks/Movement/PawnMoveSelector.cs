using System;
using System.Drawing;
using Chess.Desk;
using Chess.Pieces;

namespace Chess.Tasks.Movement
{
    public class PawnMoveSelector : MoveSelector
    {
        private readonly Pawn _pawn;
        private bool HasMoved => Board?.GameSession?.HasMoved(_pawn) ?? false;

        public PawnMoveSelector(IPiece piece) : base(piece)
        {
            _pawn = piece as Pawn ?? throw new ArgumentException();
        }

        protected override bool IsSatisfiesMovePattern(Cell toward, Point relative)
        {
            return _pawn.UsualMovePattern(relative)
                   || (_pawn.FirstMovePattern(relative) && !HasMoved)
                   || (_pawn.KillPattern(relative) && toward.Piece != null);
        }
    }
}