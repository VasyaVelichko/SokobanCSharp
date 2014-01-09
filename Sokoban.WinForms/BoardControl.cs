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

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);

         for (int r = 0; r < _board.Squares.Length; r++)
         {
            for (int c = 0; c < _board.Squares[r].Length; c++)
            {
               switch (_board.Squares[r][c])
               {
                  case Board.WALL:
                     _imageList.Draw(e.Graphics, c*48, r*48, 0);
                     break;
                  case Board.PLAYER:
                     _imageList.Draw( e.Graphics, c * 48, r * 48, 1 );
                     break;
                  case Board.PLAYER_ON_GOAL:
                     _imageList.Draw( e.Graphics, c * 48, r * 48, 2 );
                     break;
                  case Board.BOX:
                     _imageList.Draw( e.Graphics, c * 48, r * 48, 3 );
                     break;
                  case Board.BOX_ON_GOAL:
                     _imageList.Draw( e.Graphics, c * 48, r * 48, 4 );
                     break;
                  case Board.GOAL:
                     _imageList.Draw( e.Graphics, c * 48, r * 48, 5 );
                     break;
               }
            }
         }
      }

      protected override void OnPaintBackground(PaintEventArgs e)
      {
         base.OnPaintBackground(e);
      }

      private string ConvertBoardToFont()
      {
         return
            _board.ToString( )
               .Replace( Board.WALL, '█' )
               .Replace( Board.PLAYER, '☺' )
               .Replace( Board.PLAYER_ON_GOAL, '☻' )
               .Replace( Board.BOX, '□' )
               .Replace( Board.BOX_ON_GOAL, '■' )
               .Replace( Board.GOAL, '●' );
      }
   }
}
