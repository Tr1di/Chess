using System;
using System.Collections.Generic;
using System.Drawing;
using Chess.Desk;
using Chess.Pieces;

namespace Chess.Tasks.Check
{
    public class HasDirectKillMove : IBoardAction<bool>
    {
        private readonly HasDirectMove _hasMove;
        private readonly Predicate<Point> _killPattern;
        
        public bool Result { get; set; }

        public HasDirectKillMove(IPiece piece, Point toward, Predicate<Point> killPattern, List<Point> locked = null, List<Point> ignored = null)
        {
            //_hasMove = new HasDirectMove(piece, toward, locked, ignored);
            _killPattern = killPattern;
            Result = false;
        }
        
        public void Visit(Board board)
        {
            Result = false;

            var from = board.Find(_hasMove.Piece).Location;
            var toward = _hasMove.Toward.Relative(from);
            toward = _hasMove.Piece.Side == Side.White ? toward.Invert() : toward;
            
            if (!_killPattern(toward)) return;
            
            board.Accept(_hasMove);
            Result = _hasMove.Result;
        }

        public void Visit(Cell cell)
        {}

        public void Accept(IBoardAction action)
        {
        }

        
    }
}