using System;
using System.Windows.Forms;
using Chess.Game;

namespace Game
{
    public partial class Desk : Form
    {
        private GameInstance Chess { get; }
        private GameSession Session { get; }

        public Desk()
        {
            InitializeComponent();

            Chess = new GameInstance();
            
            Session = Chess.BeginNewSession(new DefaultGameMode());
            
            boardPanel.Initialize(Session.Board);
        }

        private void Desk_Load(object sender, EventArgs e)
        {
            
        }

        private void Desk_ResizeEnd(object sender, EventArgs e)
        {
           
        }

        private void boardPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
