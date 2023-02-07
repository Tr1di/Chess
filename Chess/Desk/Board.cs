using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Chess.Game;
using Chess.Pieces;
using Chess.Tasks;
using Chess.Tasks.Search;

namespace Chess.Desk
{
    public class Board : IBoardUnit
    {
        public static readonly Size DefaultSize = new Size(8, 8);

        private GameSession _gameSession;
        
        public GameSession GameSession
        {
            get => _gameSession;
            internal set
            {
                _gameSession = value;
                _gameSession.OnTurnConfirmed += () => { ClearSelection(); OnUpdate?.Invoke(this); };
            }
        }

        public bool IsInCheck => GameSession.IsInCheck;
        
        private readonly List<Cell> _grid;

        private Size _size;
        
        public int Length => _size.Count();
        public Size Size => _size;
        public int Width => Size.Width;
        public int Height => Size.Height;

        // TODO: Сунуть куда-нибудь в настройки
        private bool SwapOnTurn = false;
        
        public Cell this[int x, int y] => _grid[x * Height + y];
        public Cell this[int index] => _grid[index];
        
        private Cell _selected;
        private readonly List<MoveExecutor> _allowedMoves;

        public Cell SelectedCell => _selected;
        public IEnumerable<Cell> AllowedMoves => _allowedMoves.Select(x => x.Toward);
        public event Action<Board> OnUpdate;
        
        internal Board(Size boardSize)
        {
            _size = boardSize;
            _grid = new List<Cell>();
            _allowedMoves = new List<MoveExecutor>();
            CreateBoard();
        }
        
        public int GetCellIndex(Cell cell)
        {
            return _grid.IndexOf(cell);
        }

        public Point GetLocationOf(Cell cell)
        {
            var index = GetCellIndex(cell);
            var columns = _size.Width;
            
            return new Point(index / columns, index % columns);
        }

        private void CreateBoard()
        {
            for (var i = 0; i < Length; i++)
            {
                var cell = new Cell(this);
                cell.Selected += OnSelected;
                _grid.Add(cell);
            }
        }

        private void ClearSelection()
        {
            _selected = null;
            _allowedMoves?.Clear();
        }
        
        private void OnSelected(Cell cell)
        {
            if (GameSession.IsOver) return;
            
            if (cell == null)
            {
                throw new ArgumentNullException();
            }
            
            if (_allowedMoves != null && _allowedMoves.Count != 0)
            {
                if (AllowedMoves.Contains(cell))
                {
                    GameSession.ConfirmMove(_allowedMoves.Find(x => x.Toward == cell));
                    return;
                }
            }
            
            ClearSelection();
            
            if (cell.Piece?.Side == GameSession.Turn)
            {
                var selector = cell.Piece?.MakeSelector();
                Accept(selector);
                _allowedMoves?.AddRange(selector?.Result ?? new List<MoveExecutor>());
                if (_allowedMoves?.Count > 0) _selected = cell;
            }

            OnUpdate?.Invoke(this);
        }
        
        public void ForEachCell(Action<Cell> action)
        {
            _grid.ForEach(action);
        }

        public Cell Find(IPiece piece)
        {
            var find = new Find(piece);
            Accept(find);
            return find.Result;
        }

        public FindAll<TPiece> FindAll<TPiece>() where TPiece : IPiece
        {
            var find = new FindAll<TPiece>();
            Accept(find);
            return find;
        }
        
        public void Accept(IBoardAction action)
        {
            action.Visit(this);
            ForEachCell(cell => cell.Accept(action));
        }
    }
}