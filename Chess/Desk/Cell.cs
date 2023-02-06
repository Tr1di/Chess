using System;
using System.Drawing;
using Chess.Pieces;
using Chess.Tasks;

namespace Chess.Desk
{    
    public class Cell : IBoardUnit
    {
        private Board _owner;
        private IPiece _piece;

        public IPiece Piece
        {
            get => _piece;
            set
            {
                _piece = value;
                Updated?.Invoke(this);
            }
        }

        public Point Location => _owner?.GetLocationOf(this) ?? new Point();

        public event Action<Cell> Selected;
        public event Action<Cell> Updated; 

        public Cell(Board owner)
        {
            _owner = owner ?? throw new ArgumentNullException();
        }

        public void Select()
        {
            Selected?.Invoke(this);
        }

        public void Accept(IBoardAction action)
        {
            action.Visit(this);
        }
    }
}