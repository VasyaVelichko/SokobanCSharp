using System.Drawing;
using System.Windows.Forms;

namespace Alteridem.Sokoban.WinForms
{
   public partial class BoardControl : UserControl
   {
      private Board _board;

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
         if ( e.Modifiers == Keys.None )
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
      }

      protected override void OnPaint( PaintEventArgs e )
      {
         base.OnPaint( e );
         int width = _board.Columns * 48;
         int height = _board.Rows * 48;
         int xOffset = ( Width - width ) / 2;
         int yOffset = ( Height - height ) / 2;

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

      private string ConvertBoardToFont()
      {
         return
            _board.ToString()
               .Replace( Board.WALL, '█' )
               .Replace( Board.PLAYER, '☺' )
               .Replace( Board.PLAYER_ON_GOAL, '☻' )
               .Replace( Board.BOX, '□' )
               .Replace( Board.BOX_ON_GOAL, '■' )
               .Replace( Board.GOAL, '●' );
      }
   }
}
