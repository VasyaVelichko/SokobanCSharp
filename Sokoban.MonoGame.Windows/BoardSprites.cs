using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alteridem.Sokoban.MonoGame.Windows
{
   /// <summary>
   /// This class handles the drawing of the Sokoban board
   /// </summary>
   internal class BoardSprites
   {
      private readonly Texture2D _box;
      private readonly Texture2D _boxOnGoal;
      private readonly Texture2D _goal;
      private readonly Texture2D _player;
      private readonly Texture2D _playerOnGoal;
      private readonly Texture2D _wall;

      public BoardSprites( Game game )
      {
         _box = game.Content.Load<Texture2D>( "box" );
         _boxOnGoal = game.Content.Load<Texture2D>( "box_on_goal" );
         _goal = game.Content.Load<Texture2D>( "goal" );
         _player = game.Content.Load<Texture2D>( "player" );
         _playerOnGoal = game.Content.Load<Texture2D>( "player_on_goal" );
         _wall = game.Content.Load<Texture2D>( "wall" );
      }

      public void Draw( SpriteBatch spriteBatch, Board board, Rectangle bounds )
      {
         int width = board.Columns * 48;
         int height = board.Rows * 48;
         int xOffset = ( bounds.Width - width ) / 2 + bounds.X;
         int yOffset = ( bounds.Height - height ) / 2 + bounds.Y;

         for ( int r = 0; r < board.Squares.Length; r++ )
         {
            for ( int c = 0; c < board.Squares[r].Length; c++ )
            {
               var position = new Vector2( c * 48 + xOffset, r * 48 + yOffset );
               switch ( board.Squares[r][c] )
               {
                  case Board.WALL:
                     spriteBatch.Draw( _wall, position, Color.White );
                     break;
                  case Board.PLAYER:
                     spriteBatch.Draw( _player, position, Color.White );
                     break;
                  case Board.PLAYER_ON_GOAL:
                     spriteBatch.Draw( _playerOnGoal, position, Color.White );
                     break;
                  case Board.BOX:
                     spriteBatch.Draw( _box, position, Color.White );
                     break;
                  case Board.BOX_ON_GOAL:
                     spriteBatch.Draw( _boxOnGoal, position, Color.White );
                     break;
                  case Board.GOAL:
                     spriteBatch.Draw( _goal, position, Color.White );
                     break;
               }
            }
         }
      }
   }
}