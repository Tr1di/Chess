using System.Collections.Generic;
using System.Drawing;
using Chess.Desk;
using Chess.Pieces;

namespace Chess.Tasks.Check
{
    public class HasDirectMove : IBoardAction<bool>
    {
        private readonly IPiece _piece;
        private readonly Point _toward;
        private Point _towardRelative;
        private PointF _towardNormalized;
        private Point _from;

        public List<Point> _lockedPoint;
        public List<Point> _ignoredCells;
        private Board _board;

        public IPiece Piece => _piece;
        public Point Toward => _toward;
        public bool Result { get; private set; }

        public HasDirectMove(IPiece piece, Point toward, List<Point> locked = null, List<Point> ignored = null)
        {
            _piece = piece;
            _toward = toward;
            _lockedPoint = locked ?? new List<Point>();
            _ignoredCells = ignored ?? new List<Point>();
        }

        public void Visit(Board board)
        {
            _board = board;
            _from = _board.Find(_piece).Location;
            _towardRelative = MakeRelative(_toward);
            _towardNormalized = _towardRelative.Normalize();
            Result = true;
        }

        public void Visit(Cell cell)
        {
            if (!Result) return;
        
            var relative = MakeRelative(cell.Location);
            
            if (!_towardNormalized.NearlyEquals(relative.Normalize())) return;
            if (_towardRelative.Length() < relative.Length()) return;

            var empty = cell.Piece == null;
            var target = cell.Location == _toward;
            
            var ignored = _ignoredCells.Contains(cell.Location);
            var locked = _lockedPoint.Contains(cell.Location);

            Result = (!locked && empty || target) || ignored;
        }

        public void Accept(IBoardAction action)
        {
            throw new System.NotImplementedException();
        }

        private Point MakeRelative(Point direction)
        {
            direction = direction.Relative(_from);
            return _piece.Side == Side.White ? direction.Invert() : direction;
        }
    }
}