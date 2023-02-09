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
        
        private readonly Point _from;
        private readonly Point _towardRelative;
        private readonly PointF _towardNormalized;

        private Board _board;
        
        private readonly List<Point> _lockedPoint;
        private readonly List<Point> _ignoredCells;

        public IPiece Piece => _piece;
        public Point Toward => _toward;
        
        public bool Result { get; private set; }

        public HasDirectMove(IPiece piece, Point from, Point toward, List<Point> locked = null, List<Point> ignored = null)
        {
            _piece = piece;
            _toward = toward;
            
            _from = from;
            _towardRelative = MakeRelative(_toward);
            _towardNormalized = _towardRelative.Normalize();
            
            _lockedPoint = locked ?? new List<Point>();
            _ignoredCells = ignored ?? new List<Point>();
        }

        public void Visit(Board board)
        {
            _board = board;
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
            action.Visit(_board);
        }

        private Point MakeRelative(Point direction)
        {
            direction = direction.Relative(_from);
            return _piece.Side == Side.White ? direction.Invert() : direction;
        }
    }
}