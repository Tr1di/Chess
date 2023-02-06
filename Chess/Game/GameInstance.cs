using System.Collections.Generic;
using System.Linq;

namespace Chess.Game
{
    public class GameInstance
    {
        private IList<GameSession> Sessions { get; }

        public GameInstance()
        {
            Sessions = new List<GameSession>();
        }
        
        public GameSession BeginNewSession(IGameMode gameMode)
        {
            Sessions.Add(new GameSession(gameMode));
            return Sessions.Last();
        }

        public bool IsGameOver(GameSession session)
        {
            return session.IsOver;
        }
    }
}