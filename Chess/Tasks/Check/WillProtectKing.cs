using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Desk;
using Chess.Pieces;
using Chess.Tasks.Search;

namespace Chess.Tasks.Check
{
    public class WillProtectKing : IBoardAction<bool>
    {
        private Board _board;
        private IPiece _piece;
        private Point _move;
        private Point _moveFrom;
        private Point _king;
        private Side _side;
        private bool _result;

        public Board Board => _board;
        public bool Result => _result;

        public WillProtectKing(IPiece piece, Point move)
        {
            _move = move;
            _piece = piece;
            _side = piece.Side;
        }

        public void Visit(Board board)
        {
            _board = board;
            _moveFrom = board.Find(_piece).Location;
            _king = board.FindAll<King>().Result.First(cell => cell.Piece.Side == _side).Location;
            _result = true;
        }

        public void Visit(Cell cell)
        {
            if (!_result) return;

            if (cell.Piece == null) return;
            if (cell.Piece?.Side == _side) return;
            
            var canMove = new HasDirectMove(cell.Piece, _king, 
                new List<Point>{ _move }, 
                new List<Point>{ _moveFrom });
            _board.Accept(canMove);
            
            _result = !canMove.Result;
        }

        public void Accept(IBoardAction action)
        {
            throw new NotImplementedException();
        }
    }
}