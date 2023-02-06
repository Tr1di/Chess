using Chess.Desk;

namespace Chess.Tasks
{
    public interface IBoardAction
    {
        void Visit(Board board);
        void Visit(Cell cell);
    }

    public interface IBoardAction<out TResult> : IBoardAction, IBoardUnit
    {
        TResult Result { get; }
    }
}