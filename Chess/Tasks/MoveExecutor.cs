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
        private MoveExecutor _submove;
        
        public Cell From { get; }
        public Cell Toward { get; }

        public MoveExecutor(Cell from, Cell toward, MoveExecutor submove = null)
        {
            _from = from;
            _toward = toward;
            _submove = submove;
        }

        // TODO: Двигаю фигуру
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

            result.AddRange(_submove?.Execute() ?? new List<Move>());
            return result;
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