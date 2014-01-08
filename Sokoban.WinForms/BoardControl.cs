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
         _board = new Board(  );
         _board.Load( "7#|#5-#|#5-#|#.-#2-#|#.-2$-#|#.2$2-#|#.#2-@#|7#" );
         UpdateBoard(  );
      }

      private void UpdateBoard()
      {
         _label.ForeColor = _board.IsSolved( ) ? Color.DarkRed : Color.DarkBlue;
         _label.Text = _board.ToString( );
      }

      private void OnKeyUp( object sender, KeyEventArgs e )
      {
         if ( e.Modifiers == Keys.None )
         {
            switch ( e.KeyCode )
            {
               case Keys.Up:
                  if ( _board.MakeMove( Sokoban.Move.Up ) )
                     UpdateBoard( );
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
   }
}
