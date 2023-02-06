using System;
using System.Collections.Generic;
using System.Drawing;
using Chess.Pieces;
using Game.Properties;

namespace Game
{
    public struct SelectionColor
    {
        public static SelectionColor Default => new SelectionColor
        {
            White = Color.White,
            Black = Color.Black,
            Selected = Color.FromArgb(41, 255, 101),
            Marked = Color.FromArgb(41, 172, 255),
            Threat = Color.FromArgb(255, 67, 67),
        };

        public Color White { get; private set; }
        public Color Black { get; private set; }
        public Color Selected { get; private set; }
        public Color Marked { get; private set; }
        public Color Threat { get; private set; }
        
        public SelectionColor(Color white, Color black, Color selected, Color marked, Color threat)
        {
            White = white;
            Black = black;
            Selected = selected;
            Marked = marked;
            Threat = threat;
        }
    }
    
    public struct PieceImage
    {
        public static PieceImage Pawn => new PieceImage(new Dictionary<Side, Image>
        {
            { Side.White, Resources.WhitePawn },
            { Side.Black, Resources.BlackPawn }
        });
        
        public static PieceImage Rook => new PieceImage(new Dictionary<Side, Image>
        {
            { Side.White, Resources.WhiteRook },
            { Side.Black, Resources.BlackRook }
        });
        
        public static PieceImage Bishop => new PieceImage(new Dictionary<Side, Image>
        {
            { Side.White, Resources.WhiteBishop },
            { Side.Black, Resources.BlackBishop }
        });
        
        public static PieceImage Knight => new PieceImage(new Dictionary<Side, Image>
        {
            { Side.White, Resources.WhiteKnight },
            { Side.Black, Resources.BlackKnight }
        });
        
        public static PieceImage Queen => new PieceImage(new Dictionary<Side, Image>
        {
            { Side.White, Resources.WhiteQueen },
            { Side.Black, Resources.BlackQueen }
        });
        
        public static PieceImage King => new PieceImage(new Dictionary<Side, Image>
        {
            { Side.White, Resources.WhiteKing },
            { Side.Black, Resources.BlackKing }
        });
        
        private Dictionary<Side, Image> _images;

        public PieceImage(Dictionary<Side, Image> images)
        {
            _images = images;
        }

        public Image Get(Side side)
        {
            return _images[side];
        }
    }
    
    public class VisualStyle
    {
        public static VisualStyle Default => new VisualStyle (new Dictionary<Type, PieceImage>
            {
                { typeof(Pawn  ), PieceImage.Pawn   },
                { typeof(Rook  ), PieceImage.Rook   },
                { typeof(Bishop), PieceImage.Bishop },
                { typeof(Knight), PieceImage.Knight },
                { typeof(Queen ), PieceImage.Queen  },
                { typeof(King  ), PieceImage.King   }
            }, 
            SelectionColor.Default);

        public static VisualStyle JekaStyle => throw new NotImplementedException();
        public static VisualStyle HStyle => throw new NotImplementedException();

        private readonly Dictionary<Type, PieceImage> _pieces;
        private readonly SelectionColor _colors;

        public SelectionColor Colors => _colors;

        public VisualStyle(Dictionary<Type, PieceImage> pieces, SelectionColor colors)
        {
            _pieces = pieces;
            _colors = colors;
        }
        
        public Image Get(IPiece piece)
        {
            return Get(piece.GetType(), piece.Side);
        }
        
        public Image Get<TPiece>(Side side)
        {
            return Get(typeof(TPiece), side);
        }
        
        public Image Get(Type piece, Side side)
        {
            return _pieces[piece].Get(side);
        }
    }
}