using System;
using System.Drawing;
using System.Windows.Forms;

namespace Alteridem.Sokoban.WinForms
{
   public partial class BoardControl : UserControl
   {
      private readonly Board _board;
      private int _headerHeight;

      public BoardControl()
      {
         InitializeComponent();
         SetStyle( ControlStyles.ResizeRedraw |
                   ControlStyles.UserPaint |
                   ControlStyles.AllPaintingInWmPaint |
                   ControlStyles.DoubleBuffer, true );
         _board = new Board();
         _board.Load( "7#|#5-#|#5-#|#.-#2-#|#.-2$-#|#.2$2-#|#.#2-@#|7#" );
         UpdateBoard();
      }

      private void UpdateBoard()
      {
         Invalidate();
      }

      private void OnKeyUp( object sender, KeyEventArgs e )
      {
         switch ( e.KeyCode )
         {
            case Keys.Up:
               if ( _board.MakeMove( Sokoban.Move.Up ) )
                  UpdateBoard();
               break;
            case Keys.Down:
               if ( _board.MakeMove( Sokoban.Move.Down ) )
                  UpdateBoard();
               break;
            case Keys.Right:
               if ( _board.MakeMove( Sokoban.Move.Right ) )
                  UpdateBoard();
               break;
            case Keys.Left:
               if ( _board.MakeMove( Sokoban.Move.Left ) )
                  UpdateBoard();
               break;
         }
      }

      protected override void OnPaint( PaintEventArgs e )
      {
         base.OnPaint( e );

         if (_headerHeight == 0)
         {
            _headerHeight = (int)(e.Graphics.MeasureString("S", Font).Height + 0.5);
         }

         DrawBoard( e );
         DrawMoves(e);
         DrawSolved( e );
      }

      private void DrawBoard( PaintEventArgs e )
      {
         int width = _board.Columns * 48;
         int height = _board.Rows * 48;
         int xOffset = ( Width - width ) / 2;
         int yOffset = ( Height - _headerHeight - height ) / 2 + _headerHeight;

         for ( int r = 0; r < _board.Squares.Length; r++ )
         {
            for ( int c = 0; c < _board.Squares[r].Length; c++ )
            {
               int index = 0;
               switch ( _board.Squares[r][c] )
               {
                  case Board.WALL:
                     index = 0;
                     break;
                  case Board.PLAYER:
                     index = 1;
                     break;
                  case Board.PLAYER_ON_GOAL:
                     index = 2;
                     break;
                  case Board.BOX:
                     index = 3;
                     break;
                  case Board.BOX_ON_GOAL:
                     index = 4;
                     break;
                  case Board.GOAL:
                     index = 5;
                     break;
                  default:
                     continue;
               }
               _imageList.Draw( e.Graphics, c * 48 + xOffset, r * 48 + yOffset, index );
            }
         }
      }

      private void DrawMoves( PaintEventArgs e )
      {
         var moves = string.Format( "P:{0} M:{1}", _board.Pushes, _board.Moves );
         var sf = new StringFormat
         {
            LineAlignment = StringAlignment.Near,
            Alignment = StringAlignment.Near
         };
         e.Graphics.DrawString( moves, Font, Brushes.DarkBlue, ClientRectangle, sf );
      }

      private void DrawSolved( PaintEventArgs e )
      {
         if ( _board.IsSolved() )
         {
            const string solved = "Level Solved!";
            var sf = new StringFormat
            {
               LineAlignment = StringAlignment.Near,
               Alignment = StringAlignment.Center
            };
            e.Graphics.DrawString( solved, Font, Brushes.DarkRed, ClientRectangle, sf );
         }
      }
   }
}
