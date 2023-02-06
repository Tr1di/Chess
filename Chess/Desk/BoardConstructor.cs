using System.Drawing;
using Chess.Game;

namespace Chess.Desk
{
    public interface IBoardConstructor
    {
        IBoardConstructor SpawnLayout(IBoardLayout layout);
        IBoardConstructor Resize(Size size);
        IBoardConstructor SetOwnership(GameSession owningSession);
        Board Confirm();
    }
    
    public class DefaultBoardConstructor : IBoardConstructor
    {
        protected Board Board { get; }

        public DefaultBoardConstructor()
        {
            Board = new Board(Board.DefaultSize);
        }

        public IBoardConstructor SpawnLayout(IBoardLayout layout)
        {
            layout.SetUp(Board);
            return this;
        }

        public IBoardConstructor Resize(Size size)
        {
            return this;
        }

        public IBoardConstructor SetOwnership(GameSession owningSession)
        {
            Board.GameSession = owningSession;
            return this;
        }

        public Board Confirm()
        {
            return Board;
        }
    }
}