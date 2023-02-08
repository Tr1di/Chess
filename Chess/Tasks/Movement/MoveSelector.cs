using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Desk;
using Chess.Pieces;
using Chess.Tasks.Check;

namespace Chess.Tasks.Movement
{
    public class MoveSelector : IBoardAction<IEnumerable<MoveExecutor>>
    {
        private readonly List<MoveExecutor> _result;
        private Cell _from;
        private Predicate<Point> _movePattern;
        private Predicate<Point> _killPattern;

        protected readonly IPiece Piece;
        protected Cell From => _from;
        protected Board Board;

        public IEnumerable<MoveExecutor> Result => FilteredResult();

        public MoveSelector(IPiece piece, Predicate<Point> movePattern, Predicate<Point> killPattern = null)
        {
            Piece = piece;
            _result = new List<MoveExecutor>();
            _movePattern = movePattern;
            _killPattern = killPattern ?? movePattern;
        }

        protected virtual bool HasDirectMove(Point toward)
        {
            var canMove = new HasDirectMove(Piece, toward);
            Accept(canMove);
            return canMove.Result;
        }

        protected virtual bool HasDirectKillMove(Point toward)
        {
            var canMove = new HasDirectKillMove(Piece, toward, Piece.KillPattern);
            Board.Accept(canMove);
            return canMove.Result;
        }

        protected virtual bool WillProtectKing(Point toward)
        {
            var canProtect = new WillProtectKing(Piece, toward);
            Board.Accept(canProtect);
            return canProtect.Result;
        }
        
        protected virtual bool CanPerformMove(Cell cell)
        {
            if (cell.Piece?.Side == Piece.Side) return false;
            return (cell.Piece == null ? HasDirectMove(cell.Location) : HasDirectKillMove(cell.Location))
                && WillProtectKing(cell.Location);
        }

        protected virtual bool IsSatisfiesMovePattern(Cell toward, Point relative)
        {
            return _movePattern(relative) || (_killPattern(relative) && toward.Piece != null);
        }

        private IEnumerable<MoveExecutor> FilteredResult()
        {
            return _result.FindAll(x => CanPerformMove(x.Toward));
        }

        protected virtual MoveExecutor CreateMoveExecutor(Cell cell)
        {
            return new MoveExecutor(_from, cell);
        }
        
        public virtual void Visit(Board board)
        {
            Board = board;
            _from = board.Find(Piece);
        }

        public void Visit(Cell cell)
        {
            var toward = cell.Location.Relative(_from.Location);
            if (Piece.Side == Side.White && toward.X != 0) toward = toward.Invert();
            
            if (IsSatisfiesMovePattern(cell, toward))
            {
                _result.Add(CreateMoveExecutor(cell));
            }
        }

        public void Accept(IBoardAction action)
        {
            action.Visit(Board);
            _result.Select(x=> x.Toward).ToList().ForEach(cell => cell.Accept(action));
        }
    }
}