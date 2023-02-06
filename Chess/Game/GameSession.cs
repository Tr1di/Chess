using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Desk;
using Chess.Pieces;
using Chess.Tasks;

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

        public event Action TurnBegun;
        public event Action TurnEnded;

        public IGameMode GameMode { get; }
        public Board Board { get; }

        public Side CheckedSide = Side.None;
        
        public bool IsInCheck => CheckedSide != Side.None;
        public GameState GameState { get; private set; }
        public bool IsOver => GameState == GameState.Over;
        public Side Turn { get; private set; }
        public Side Winner => IsOver ? _winner : Side.None;

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

        public virtual void GameOver()
        {
            
        }

        public void ConfirmMove(MoveExecutor executor)
        {
            
        }

        public bool CanCastling()
        {
            return false;
        }

        public bool CanEnPas()
        {
            return false;
        }
        
        protected virtual void CheckCheck()
        {
            
        }
    }
}