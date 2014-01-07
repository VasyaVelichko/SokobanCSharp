using System.Linq;
using System.Security.Cryptography;

namespace Alteridem.Sokoban
{
   public class Game
   {
      private Board _board;
   }

   public class Board
   {
      private const char WALL = '#';
      private const char PLAYER = '@';
      private const char PLAYER_ON_GOAL = '+';
      private const char BOX = '$';
      private const char BOX_ON_GOAL = '*';
      private const char GOAL = '.';
      private const char FLOOR = ' ';

      public char[,] Squares { get; private set; }

      public void Load(string board)
      {
         board = board.Replace("\r", "");

         // TODO: Modify for RLE

         string[] rows = board.Split(new[] {'\n'});

         // Find the max row width
         int columns = rows.Select(row => row.Length).Max();

         Squares = new char[rows.Length, columns];
         for ( int r = 0; r < rows.Length; r++ )
         {
            for (int c = 0; c < rows.Length; c++)
            {
               Squares[r, c] = rows[r][c];
            }
         }
      }

      /// <summary>
      /// Applies the given move to the current board and returns a new board
      /// </summary>
      /// <param name="move">udrl</param>
      /// <returns>The transformed board, or null if the move is invalid</returns>
      public Board Move(char move)
      {
         return null;
      }
   }
}
