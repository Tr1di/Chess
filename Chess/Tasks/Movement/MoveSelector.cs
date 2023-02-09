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
        private static Func<MoveSelector, Cell, bool> HasDirectMove => (selector, cell) =>
        {
            var canMove = new HasDirectMove(selector._piece, 
                selector._from.Location, cell.Location, 
                selector._locked, selector._ignored);
            
            selector.Accept(canMove);
            return canMove.Result;
        };

        private static Func<MoveSelector, Cell, bool> CanKill => (selector, cell) =>
        {
            if (cell.Piece == null) return true;
            if (cell.Piece.Side == selector._piece.Side) return false;
            
            var toward = selector.MakeRelativeToPiece(cell.Location);
                
            return cell.Piece.KillPattern(toward);
        };
        
        private static Func<MoveSelector, Cell, bool> CanProtectKing => (selector, cell) =>
        {
            return !selector._board.FindAll<IPiece>()
                .Result
                .Where(x => x.Piece.Side != selector._piece.Side)
                .ToList()
                .Exists(piece =>
                {
                    var canMove = piece.Piece.Selector;
                    canMove._filters.RemoveAll(x => x == CanProtectKing);

                    canMove._locked.Add(cell.Location);
                    canMove._ignored.Add(selector._from.Location);

                    canMove.Filter((s, c) =>
                    {
                        if (c.Piece == null) return false;
                        
                        var toward = s.MakeRelativeToPiece(cell.Location);
                
                        return c.Piece.KillPattern(toward);
                    });
                    canMove.Filter((s, c) => c.Piece is King king && king.Side == selector._piece.Side);

                    selector.Accept(canMove);
                    return canMove.Result.Any();
                });
        };
        
        private readonly List<Point> _locked;
        private readonly List<Point> _ignored;
        
        private readonly IPiece _piece;
        private readonly Predicate<Point> _movePattern;
        private readonly List<Func<MoveSelector, Cell, bool>> _filters;
        private readonly List<Func<MoveExecutor, MoveExecutor>> _executorModifiers;
        private readonly List<Cell> _result;
        
        private Board _board;
        private Cell _from;
        
        public Cell From => _from;
        public Board Board => _board;
        
        public IEnumerable<MoveExecutor> Result => _result
            .Where(cell => _filters.TrueForAll(pred => pred(this, cell)))
            .Select(CreateMoveExecutor);

        public MoveSelector(IPiece piece, Predicate<Point> movePattern)
        {
            _piece = piece;
            _movePattern = movePattern;
            _result = new List<Cell>();
            _filters = new List<Func<MoveSelector, Cell, bool>>();
            _executorModifiers = new List<Func<MoveExecutor, MoveExecutor>>();
            
            _locked = new List<Point>();
            _ignored = new List<Point>();
            
            Filter(HasDirectMove);
            Filter(CanKill);
            Filter(CanProtectKing);
        }
        
        public void Visit(Board board)
        {
            _board = board;
            _from = board.Find(_piece);
            _result.Clear();
        }

        protected virtual MoveExecutor CreateMoveExecutor(Cell cell)
        {
            var exec = new MoveExecutor(_from, cell);
            _executorModifiers.ForEach(x => exec = x(exec));
            return exec;
        }

        protected virtual bool IsSatisfiesMovePattern(Cell toward, Point relative)
        {
            return _movePattern(relative);
        }
        
        public void Visit(Cell cell)
        {
            var toward = MakeRelativeToPiece(cell.Location);

            if (IsSatisfiesMovePattern(cell, toward))
            {
                _result.Add(cell);
            }
        }

        public void Accept(IBoardAction action)
        {
            action.Visit(_board);
            _result.ForEach(x => x.Accept(action));
        }

        public MoveSelector Filter(Func<MoveSelector, Cell, bool> filter)
        {
            _filters.Add(filter);
            return this;
        }

        public MoveSelector ModifyExecutor(Func<MoveExecutor, MoveExecutor> modifier)
        {
            _executorModifiers.Add(modifier);
            return this;
        }

        public Point MakeRelativeToPiece(Point direction)
        {
            direction = direction.Relative(_from.Location);
            return _piece.Side == Side.White ? direction.Invert() : direction;
        }
    }
}