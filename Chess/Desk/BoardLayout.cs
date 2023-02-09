using System.Collections.Generic;
using System.Drawing;
using Chess.Pieces;

namespace Chess.Desk
{
    public interface IBoardLayout
    {
        void SetUp(Board board);
    }
    
    public class DefaultLayout : IBoardLayout
    {
        public void SetUp(Board board)
        {
            //SpawnPawns(board);
            SpawnRooks(board);
            //SpawnKnights(board);
            SpawnBishops(board);
            //SpawnQueens(board);
            SpawnKings(board);
        }

        private void SpawnPieces(Board board, IPieceFabric fabric, Dictionary<Point, Side> points)
        {
            foreach (var piece in points)
            {
                var cell = piece.Key;
                var side = piece.Value;
                board[cell.X, cell.Y].Piece = fabric.CreatePiece(side);
            }
        }
        
        private void SpawnPawns(Board board)
        {
            var points = new Dictionary<Point, Side>();
            
            for (var side = Side.None + 1; side < Side.Max; side++)
            {
                for (var cell = 0; cell < board.Width; cell++)
                {
                    var row = side == Side.Black ? 1 : 6;
                    points.Add(new Point(row, cell), side);
                }
            }
            
            SpawnPieces(board, new PawnFabric(), points);
        }

        private void SpawnRooks(Board board)
        {
            SpawnPieces(board, new RookFabric(), new Dictionary<Point, Side>
            {
                { new Point(0,0), Side.Black },
                { new Point(0,7), Side.Black },
                { new Point(7,0), Side.White },
                { new Point(7,7), Side.White }
            });
        }

        private void SpawnKnights(Board board)
        {
            SpawnPieces(board, new KnightFabric(), new Dictionary<Point, Side>
            {
                { new Point(0,1), Side.Black },
                { new Point(0,6), Side.Black },
                { new Point(7,1), Side.White },
                { new Point(7,6), Side.White }
            });
        }
        
        private void SpawnBishops(Board board)
        {
            SpawnPieces(board, new BishopFabric(), new Dictionary<Point, Side>
            {
                { new Point(0,2), Side.Black },
                { new Point(0,5), Side.Black },
                { new Point(7,2), Side.White },
                { new Point(7,5), Side.White }
            });
        }

        private void SpawnQueens(Board board)
        {
            SpawnPieces(board, new QueenFabric(), new Dictionary<Point, Side>
            {
                { new Point(0,3), Side.Black },
                { new Point(7,3), Side.White }
            });
        }

        private void SpawnKings(Board board)
        {
            SpawnPieces(board, new KingFabric(), new Dictionary<Point, Side>
            {
                { new Point(0,4), Side.Black },
                { new Point(7,4), Side.White }
            });
        }
    }
}