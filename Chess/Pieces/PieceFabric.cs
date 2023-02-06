namespace Chess.Pieces
{
    public interface IPieceFabric
    {
        IPiece CreatePiece(Side side);
    }
}