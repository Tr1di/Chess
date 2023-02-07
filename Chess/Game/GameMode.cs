using System.Linq;
using Chess.Desk;
using Chess.Pieces;
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
            var pieces = session.Board.FindAll<IPiece>().Result.Select(x => x.Piece).Where(x => x.Side == session.Turn);

            foreach (var piece in pieces)
            {
                var hasMoves = piece.MakeSelector();
                session.Board.Accept(hasMoves);
                if (hasMoves.Result.Any())
                {
                    return false;
                }
            }

            return true;
        }

        public bool GetWinner(GameSession session)
        {
            throw new System.NotImplementedException();
        }
    }
}