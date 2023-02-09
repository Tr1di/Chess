using Chess.Desk;

namespace Chess.Tasks
{
    public interface IBoardAction : IBoardUnit
    {
        void Visit(Board board);
        void Visit(Cell cell);
    }

    public interface IBoardAction<out TResult> : IBoardAction
    {
        TResult Result { get; }
    }
}