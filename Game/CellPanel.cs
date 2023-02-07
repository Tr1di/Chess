using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Chess.Desk;

namespace Game
{
    public partial class CellPanel : Panel
    {
        private readonly Cell _cell;
        private readonly BoardPanel _owner;
        private Theme Style => _owner.Style;
        public Cell Cell => _cell;

        public Color IdleColor { get; set; }

        public CellPanel()
        {
            InitializeComponent();
        }

        public CellPanel(IContainer container, Cell cell, BoardPanel owner) : this(cell, owner)
        {
            container.Add(this);
        }

        public CellPanel(Cell cell, BoardPanel owner) : this()
        {
            _cell = cell;
            _cell.Updated += Updated;
            _owner = owner;
            Updated(_cell);
        }

        private void Updated(Cell cell)
        {
            if (cell.Piece == null)
            {
                BackgroundImage = null;
                return;
            }

            BackgroundImage = Style.Get(cell.Piece);
        }
        
        public void Marked()
        {
            BackColor = _cell.Piece == null ? Style.Colors.Marked : Style.Colors.Threat;
        }

        public void Unmarked()
        {
            BackColor = IdleColor;
        }

        public void Selected()
        {
            BackColor = Style.Colors.Selected;
        }
        
        private void OnClick(object sender, EventArgs e)
        {
            _cell.Select();
        }
    }
}