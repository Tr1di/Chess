using System;
using Chess.Pieces;
using FluentNHibernate.Mapping;

namespace DataBase
{
    internal class PieceMap : ClassMap<IPiece>
    {
        public PieceMap()
        {
            Id().GeneratedBy.Guid();
            // Map(x => Enum.GetName(typeof(Side), x.Side));
        }
    }
}