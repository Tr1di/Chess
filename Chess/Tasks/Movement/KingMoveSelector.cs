using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Desk;
using Chess.Pieces;

namespace Chess.Tasks.Movement
{
    public class KingMoveSelector : MoveSelector
    {
        private readonly King _king;
        private bool _hasMoved;
        private List<Cell> _rooksToCastle;
        
        public KingMoveSelector(IPiece piece) : base(piece)
        {
            _king = piece as King ?? throw new ArgumentException();
            _hasMoved = false;
            _rooksToCastle = new List<Cell>();
        }

        protected override bool CanPerformMove(Cell cell)
        {
            //TODO: Is safe on castle way
            return base.CanPerformMove(cell);
        }

        private bool CanCastle(Point point)
        {
            if (_hasMoved) return false;
            if (!_king.CastlePattern(point)) return false;
            
            // TODO: не уверен
            point = _king.Side == Side.White ? point.Invert() : point;
            
            return _rooksToCastle.Exists(rook =>
            {
                var relativeNormalized = rook.Location.Relative(From.Location).Normalize();
                return relativeNormalized.NearlyEquals(point.Normalize());
            });
        }
        
        protected override bool IsSatisfiesMovePattern(Point toward)
        {
            return _king.UsualMovePattern(toward) || CanCastle(toward);
        }

        protected override MoveExecutor CreateMoveExecutor(Cell cell)
        {
            return base.CreateMoveExecutor(cell); //new MoveExecutor(From, cell, );
        }
        
        public override void Visit(Board board)
        {
            base.Visit(board);
            _hasMoved = board.GameSession.HasMoved(_king);
            _rooksToCastle = Board.FindAll<Rook>().Result
                .Where(x => x.Piece.Side == _king.Side)
                .Where(x => !Board.GameSession.HasMoved(x.Piece))
                .ToList();
        }
    }
}