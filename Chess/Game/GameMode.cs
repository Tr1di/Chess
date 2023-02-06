using Chess.Desk;
using Chess.Player;

namespace Chess.Game
{
    public interface IGameMode
    {
        IBoardConstructor BoardConstructor { get; }
        IBoardLayout BoardLayout { get; }
        bool IsCastlingAllowed { get; }
        bool IsEnPasAllowed { get; }
        bool IsGameOver(GameSession session);
        
        // и ещё всяких правил и прочего добавить надо
    }
    
    public class DefaultGameMode : IGameMode
    {
        public IBoardConstructor BoardConstructor => new DefaultBoardConstructor();

        public IBoardLayout BoardLayout => new DefaultLayout();

        public bool IsCastlingAllowed => true;

        public bool IsEnPasAllowed => true;
        
        public bool IsGameOver(GameSession session)
        {
            throw new System.NotImplementedException();
        }

        public bool GetWinner(GameSession session)
        {
            throw new System.NotImplementedException();
        }
    }
}