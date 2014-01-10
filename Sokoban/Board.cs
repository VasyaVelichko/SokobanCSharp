using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Alteridem.Sokoban
{
   public class Board
   {
      #region Defines

      public const char WALL = '#';
      public const char PLAYER = '@';
      public const char PLAYER_ON_GOAL = '+';
      public const char BOX = '$';
      public const char BOX_ON_GOAL = '*';
      public const char GOAL = '.';
      public const char FLOOR = ' ';

      #endregion

      #region Private Members

      // The man's position
      private Position _player;

      // The moves that have been made
      private readonly StringBuilder _moveList = new StringBuilder();

      #endregion

      #region Properties

      public char[][] Squares { get; private set; }

      /// <summary>
      /// Gets the moves as a string.
      /// </summary>
      public string MoveList
      {
         get { return _moveList.ToString(); }
      }

      /// <summary>
      /// Gets the number of moves.
      /// </summary>
      public int Moves { get; private set; }

      /// <summary>
      /// Gets the number of pushes
      /// </summary>
      public int Pushes { get; private set; }

      /// <summary>
      /// The number of rows
      /// </summary>
      public int Rows { get; private set; }

      /// <summary>
      /// The number of columns
      /// </summary>
      public int Columns { get; private set; }

      #endregion

      #region Construction

      public Board()
      {
      }

      public void Load( string board )
      {
         Moves = 0;
         Pushes = 0;
         _moveList.Clear();

         board = board.Replace( "\r", "" );

         // Modify from RLE
         board = ConvertFromRunLengthEncoded( board );

         string[] rows = board.Split( new[] { '\n' } );
         Squares = new char[rows.Length][];
         Rows = rows.Length;
         for ( int r = 0; r < rows.Length; r++ )
         {
            if ( rows[r].Length > Columns )
               Columns = rows[r].Length;

            Squares[r] = rows[r].ToCharArray();
         }
         _player = FindPlayer();

         if ( Columns == 0 )
            Rows = 0;
      }

      #endregion

      #region Public Methods

      /// <summary>
      /// Applies the given move to the current board
      /// </summary>
      /// <param name="move">The move to make</param>
      /// <returns>True if the player can make that move, false otherwise</returns>
      public bool MakeMove( Move move )
      {
         if ( !IsMoveValid( move ) )
            return false;

         if ( IsSolved() )
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
         _player = newPlayer;
         _moveList.Append( moveLetter );
         return true;
      }

      /// <summary>
      /// Determines whether this instance is solved.
      /// </summary>
      /// <returns></returns>
      public bool IsSolved()
      {
         return Squares.All( row => !row.Any( c => c == BOX ) );
      }

      /// <summary>
      /// Gets a RLE'd version of the board
      /// </summary>
      /// <returns></returns>
      public string ToRunLengthEncodedString()
      {
         return ConverToRunLengthEncoded( ToString() );
      }

      #endregion

      #region Private Methods

      private string ConverToRunLengthEncoded( string board )
      {
         if ( string.IsNullOrEmpty( board ) )
            return string.Empty;

         board = board.Replace( "\r\n", "|" )
                      .Replace( ' ', '-' );

         var sb = new StringBuilder();
         int count = 1;
         char current = board[0];
         for ( int i = 1; i < board.Length; i++ )
         {
            if ( current == board[i] )
            {
               count++;
            }
            else
            {
               OutputRunLengthEncoded( count, sb, current );
               count = 1;
               current = board[i];
            }
         }
         OutputRunLengthEncoded( count, sb, current );
         return sb.ToString();
      }

      private static void OutputRunLengthEncoded( int count, StringBuilder sb, char current )
      {
         if ( count == 1 )
            sb.Append( current );
         else
            sb.AppendFormat( "{0}{1}", count, current );
      }

      private string ConvertFromRunLengthEncoded( string board )
      {
         board = board.Replace( "|", "\n" )
                      .Replace( '-', ' ' )
                      .Replace( '_', ' ' );

         board = ExpandRunLengthEncoding( board );

         return board;
      }

      private string ExpandRunLengthEncoding( string board )
      {
         var rle = string.Empty;
         var sb = new StringBuilder();
         foreach ( char c in board )
         {
            if ( Char.IsDigit( c ) )
            {
               rle += c;
            }
            else
            {
               if ( rle == string.Empty )
               {
                  sb.Append( c );
               }
               else
               {
                  int count = Int32.Parse( rle );
                  rle = string.Empty;
                  for ( int x = 0; x < count; x++ )
                  {
                     sb.Append( c );
                  }
               }
            }
         }
         return sb.ToString();
      }

      /// <summary>
      /// Determines whether the move is valid.
      /// </summary>
      /// <param name="move"></param>
      /// <returns></returns>
      private bool IsMoveValid( Move move )
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

      #endregion

      #region Overrides

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