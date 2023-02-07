using System;
using System.Collections.Generic;
using System.Drawing;
using Chess.Desk;
using Chess.Pieces;

namespace Chess.Tasks
{
    public struct Move
    {
        public IPiece Piece { get; internal set; }
        public Point Toward { get; internal set; }
        public Point From { get; internal set; }
    }

    public class MoveExecutor
    {
        private readonly Cell _from;
        private readonly Cell _toward;
        private List<MoveExecutor> _submoves;

        public Cell From => _from;
        public Cell Toward => _toward;

        public MoveExecutor(Cell from, Cell toward, List<MoveExecutor> submoves = null)
        {
            _from = from;
            _toward = toward;
            _submoves = submoves ?? new List<MoveExecutor>();
        }
        
        public virtual List<Move> Execute()
        {
            var result = new List<Move>
            {
                new Move
                {
                    Piece = From.Piece,
                    From = From.Location,
                    Toward = Toward.Location
                }
            };

            // Toward.Piece?.Killed()
            Toward.Piece = From.Piece;
            From.Piece = null;
            
            foreach (var submove in _submoves)
            {
                result.AddRange(submove.Execute());
            }
            
            return result;
        }

        public void AddSubmove(MoveExecutor submove)
        {
            _submoves.Add(submove);
        }
    }

    public class CastleExecutor : MoveExecutor
    {
        private Cell _rookToward;
        
        public CastleExecutor(Cell from, Cell toward, Cell rookToward) : base(from, toward)
        {
            _rookToward = rookToward;
        }
    }
}