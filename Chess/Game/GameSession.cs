using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Desk;
using Chess.Pieces;
using Chess.Tasks;
using Chess.Tasks.Check;

namespace Chess.Game
{
    public enum GameState
    {
        Running,
        Paused,
        Over
    }

    public class GameSession
    {
        private Side _winner;
        
        public event Action OnTurnConfirmed;
        public event Action OnGameOver;

        public IGameMode GameMode { get; }
        public Board Board { get; }

        private Side _checkedSide = Side.None;
        private Cell _checkedFrom;
        
        public bool IsInCheck => _checkedSide != Side.None;
        public GameState GameState { get; private set; }
        public bool IsOver => GameState == GameState.Over;
        public Side Turn { get; private set; }
        private Side SwitchedTurn => Turn == Side.Black ? Side.White : Side.Black;
        public Side Winner => IsOver ? _winner : Side.None;

        public bool CanCastle => GameMode.IsCastlingAllowed;
        
        private List<Move> _moves;
        
        public GameSession(IGameMode gameMode)
        {
            GameMode = gameMode;
            Board = GameMode.BoardConstructor
                .SetOwnership(this)
                .SpawnLayout(GameMode.BoardLayout)
                .Confirm();
            
            _moves = new List<Move>();
            
            Turn = Side.White;
            
            //OnTurnConfirmed += CheckCheck;
            OnTurnConfirmed += SwitchTurn;
            //OnTurnConfirmed += CheckGameOver;
        }

        public bool HasMoved(IPiece piece)
        {
            return _moves.Exists(move => move.Piece == piece);
        }

        public bool JustMoved(IPiece piece)
        {
            return _moves.LastOrDefault().Piece == piece;
        }
        
        public virtual void Start()
        {
            
        }
        
        public virtual void Pause()
        {
            
        }
        
        public virtual void Stop()
        {
            
        }

        private void CheckGameOver()
        {
            if (GameMode.IsGameOver(this))
            {
                if (IsInCheck)
                {
                    _winner = SwitchedTurn;
                    // OnGameOver
                }
            }
        }

        public void ConfirmMove(MoveExecutor executor)
        {
            if (executor.From.Piece.Side != Turn) 
                throw new ArgumentException($"Try move { executor.From.Piece } at { Turn } turn!");
            
            _moves.AddRange(executor.Execute());
            OnTurnConfirmed?.Invoke();
        }

        private void SwitchTurn()
        {
            Turn = SwitchedTurn;
        }

        public bool CanEnPas()
        {
            return false;
        }
        
        protected virtual void CheckCheck()
        {
            var pieces = Board.FindAll<IPiece>().Result.ToList();
            var kings = pieces.Where(x => x.Piece is King).ToList();

            foreach (var king in kings)
            {
                foreach (var piece in pieces.Where(x => x.Piece.Side != king.Piece.Side))
                {
                    var canMove = new HasDirectKillMove(piece.Piece, king.Location, piece.Piece.KillPattern);
                    Board.Accept(canMove);
                    
                    if (canMove.Result)
                    {
                        _checkedSide = king.Piece.Side;
                        _checkedFrom = piece;
                        return;
                    }
                }
            }
            
            _checkedSide = Side.None;
            _checkedFrom = null;
        }
    }
}