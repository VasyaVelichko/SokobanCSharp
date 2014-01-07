using System;
using System.Linq;
using System.Text;

namespace Alteridem.Sokoban
{
   public class Board
   {
      private const char WALL = '#';
      private const char PLAYER = '@';
      private const char PLAYER_ON_GOAL = '+';
      private const char BOX = '$';
      private const char BOX_ON_GOAL = '*';
      private const char GOAL = '.';
      private const char FLOOR = ' ';

      // The man's position
      private Position _player;

      // The moves that have been made
      private readonly StringBuilder _moveList = new StringBuilder();

      public char[][] Squares { get; private set; }

      /// <summary>
      /// Gets the moves as a string.
      /// </summary>
      public string MovesList { get { return _moveList.ToString( ); } }


      /// <summary>
      /// Gets the number of moves.
      /// </summary>
      public int Moves { get; private set; }

      /// <summary>
      /// Gets the number of pushes
      /// </summary>
      public int Pushes { get; private set; }

      public Board()
      {
         Moves = 0;
         Pushes = 0;
      }

      public void Load( string board )
      {
         board = board.Replace( "\r", "" );

         // TODO: Modify for RLE

         string[] rows = board.Split( new[] { '\n' } );
         Squares = new char[rows.Length][];
         for ( int r = 0; r < rows.Length; r++ )
         {
            Squares[r] = rows[r].ToCharArray();
         }
         _player = FindPlayer();
      }

      /// <summary>
      /// Determines whether the move is valid.
      /// </summary>
      /// <param name="move"></param>
      /// <returns></returns>
      public bool IsMoveValid( Move move )
      {
         var newPlayer = _player.MakeMove( move );

         // Are we off the board? This includes one in from each edge.
         if ( newPlayer.Row <= 0 ||
              newPlayer.Row >= Squares.Length - 1 ||
              newPlayer.Column <= 0 ||
              newPlayer.Column >= Squares[newPlayer.Row].Length - 1
            )
         {
            return false;
         }

         char square = Squares[newPlayer.Row][newPlayer.Column];
         // Is the new position a wall?
         if ( square == WALL )
            return false;

         // Is the new position floor or a goal?
         if ( square == FLOOR || square == GOAL )
            return true;

         // Is the new position a box? Can we move the box?
         if ( square == BOX || square == BOX_ON_GOAL )
         {
            var box = newPlayer.MakeMove( move );

            // Are we off the board?
            if ( box.Row < 0 ||
                 box.Row >= Squares.Length ||
                 box.Column < 0 ||
                 box.Column >= Squares[box.Row].Length
               )
            {
               return false;
            }

            // You can move a box onto the floor, or onto a goal
            char boxSquare = Squares[box.Row][box.Column];
            if ( boxSquare == FLOOR || boxSquare == GOAL )
            {
               return true;
            }
         }
         return false;
      }

      /// <summary>
      /// Applies the given move to the current board and returns a new board
      /// </summary>
      /// <param name="move">The move to make</param>
      /// <returns>The transformed board, or null if the move is invalid</returns>
      public bool MakeMove( Move move )
      {
         if ( !IsMoveValid( move ) )
            return false;

         char moveLetter = GetMoveLetter( move );

         // Transform the old square
         char oldSquare = Squares[_player.Row][_player.Column];
         if ( oldSquare == PLAYER )
         {
            Squares[_player.Row][_player.Column] = FLOOR;
         }
         else // PLAYER_ON_GOAL
         {
            Squares[_player.Row][_player.Column] = GOAL;
         }

         // Transform the new square
         var newPlayer = _player.MakeMove( move );
         char newSquare = Squares[newPlayer.Row][newPlayer.Column];
         if ( newSquare == FLOOR || newSquare == BOX )
         {
            Squares[newPlayer.Row][newPlayer.Column] = PLAYER;
         }
         else if ( newSquare == GOAL || newSquare == BOX_ON_GOAL )
         {
            Squares[newPlayer.Row][newPlayer.Column] = PLAYER_ON_GOAL;
         }

         // Move boxes
         if ( newSquare == BOX || newSquare == BOX_ON_GOAL )
         {
            Pushes++;
            moveLetter = Char.ToUpperInvariant( moveLetter );
            var box = newPlayer.MakeMove( move );

            char boxSquare = Squares[box.Row][box.Column];
            if ( boxSquare == FLOOR )
            {
               Squares[box.Row][box.Column] = BOX;
            }
            else // GOAL
            {
               Squares[box.Row][box.Column] = BOX_ON_GOAL;
            }
         }
         else
         {
            Moves++;
         }
         _moveList.Append( moveLetter );
         return true;
      }

      private char GetMoveLetter( Move move )
      {
         switch ( move )
         {
            case Move.Down:
               return 'd';
            case Move.Left:
               return 'l';
            case Move.Right:
               return 'r';
            case Move.Up:
            default:
               return 'u';
         }
      }

      private Position FindPlayer()
      {
         for ( int r = 0; r < Squares.Length; r++ )
         {
            for ( int c = 0; c < Squares[r].Length; c++ )
            {
               if ( Squares[r][c] == PLAYER || Squares[r][c] == PLAYER_ON_GOAL )
               {
                  return new Position( r, c );
               }
            }
         }
         return new Position( -1, -1 );
      }

      #region Overrides of Object

      /// <summary>
      /// Returns a string that represents the current object.
      /// </summary>
      /// <returns>
      /// A string that represents the current object.
      /// </returns>
      public override string ToString()
      {
         var builder = new StringBuilder();
         foreach ( char[] row in Squares )
         {
            builder.AppendLine( new string( row ) );
         }
         if ( builder.Length >= 2 )
         {
            builder.Remove( builder.Length - 2, 2 );
         }
         return builder.ToString();
      }

      #endregion
   }
}