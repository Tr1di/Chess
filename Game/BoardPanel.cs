using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Chess.Desk;

namespace Game
{
    public partial class BoardPanel : FlowLayoutPanel
    {
        private VisualStyle _style = VisualStyle.Default;
        
        public Board Board { get; private set; }
        private int CellSize => Height / Board.Height;
        
        public VisualStyle Style
        {
            get => _style;
            set
            {
                _style = value;
                OnUpdate(Board);
            }
        }

        public BoardPanel()
        {
            InitializeComponent();
        }

        public BoardPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Initialize(Board board)
        {
            Board = board;
            Board.OnUpdate += OnUpdate;
            Board.ForEachCell(AddCell);
            Width = Board.Width * CellSize;
        }

        private void OnUpdate(Board board)
        {
            foreach (var cell in Controls.OfType<CellPanel>())
            {
                if (Board.AllowedMoves.Contains(cell.Cell)) cell.Marked();
                else if (Board.SelectedCell == cell.Cell)   cell.Selected();
                else                                        cell.Unmarked();
            }
        }
        
        private void AddCell(Cell cell)
        {
            var panel = new CellPanel(cell, this);
            panel.Width = CellSize;
            panel.Height = CellSize;

            var index = Board.GetCellIndex(cell);

            panel.IdleColor = (index / Board.Width) % 2 == index % 2 ? SelectionColor.Default.White : SelectionColor.Default.Black;
            panel.Unmarked();

            panel.Margin = new Padding(0);
            
            Controls.Add(panel);
        }
    }
}