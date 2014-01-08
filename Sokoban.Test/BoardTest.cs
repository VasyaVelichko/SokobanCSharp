using NUnit.Framework;

namespace Alteridem.Sokoban.Test
{
   [TestFixture]
   public class BoardTest
   {
      private const string DOWN_BOARD = "###\r\n#@#\r\n#$#\r\n#.#\r\n###";
      private const string SIMPLE_BOARD = "#####\r\n#@$.#\r\n#####";
      private const string MEDIUM_BOARD = "#######\r\n#.@ # #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######";
      private const string LARGE_BOARD = "#######\r\n#     #\r\n#     #\r\n#. #  #\r\n#. $$ #\r\n#.$$  #\r\n#.#  @#\r\n#######";
      private const string LARGE_BOARD_SOLUTION = "ulULLulDDurrrddlULrruLLrrUruLLLulD";

      [TestCase( "", "" )]
      [TestCase( SIMPLE_BOARD, SIMPLE_BOARD )]
      [TestCase( MEDIUM_BOARD, MEDIUM_BOARD )]
      [TestCase( DOWN_BOARD, DOWN_BOARD )]
      [TestCase( "#####\n#@$.#\n#####", SIMPLE_BOARD )]
      public void TestLoad( string boardStr, string expected )
      {
         var board = new Board();
         board.Load(boardStr);
         Assert.AreEqual( expected, board.ToString() );
      }

      [TestCase( "", Move.Down, false, "" )]
      [TestCase( SIMPLE_BOARD, Move.Down, false, "" )]
      [TestCase( SIMPLE_BOARD, Move.Up, false, "" )]
      [TestCase( SIMPLE_BOARD, Move.Right, true, "#####\r\n# @*#\r\n#####" )]
      [TestCase( SIMPLE_BOARD, Move.Left, false, "" )]
      [TestCase( MEDIUM_BOARD, Move.Up, false, "" )]
      [TestCase( MEDIUM_BOARD, Move.Down, true, "#######\r\n#.  # #\r\n#$+ $ #\r\n# $ $ #\r\n# ..  #\r\n#  *  #\r\n#######" )]
      [TestCase( MEDIUM_BOARD, Move.Right, true, "#######\r\n#. @# #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######" )]
      [TestCase( MEDIUM_BOARD, Move.Left, true, "#######\r\n#+  # #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######" )]
      [TestCase( DOWN_BOARD, Move.Down, true, "###\r\n# #\r\n#@#\r\n#*#\r\n###" )]
      public void TestMove( string boardStr, Move move, bool expectedValid, string expected )
      {
         var board = new Board();
         board.Load( boardStr );
         bool valid = board.MakeMove( move );
         Assert.AreEqual( expectedValid, valid );
         if (valid)
         {
            Assert.AreEqual(expected, board.ToString());
         }
         else
         {
            // Make sure it hasn't changed
            Assert.AreEqual(boardStr, board.ToString());
         }
      }

      [TestCase( SIMPLE_BOARD, false )]
      [TestCase( MEDIUM_BOARD, false )]
      [TestCase( "#####\r\n# @*#\r\n#####", true )]
      [TestCase( "#######\r\n#@  # #\r\n#** * #\r\n#   * #\r\n# ..  #\r\n#  *  #\r\n#######", true )]
      [TestCase( "###\r\n# #\r\n#@#\r\n#*#\r\n###", true )]
      public void TestSolved(string boardStr, bool isSolved)
      {
         var board = new Board();
         board.Load( boardStr );
         Assert.AreEqual( isSolved, board.IsSolved() );
      }

      [TestCase( SIMPLE_BOARD, new[] { Move.Right }, 0, 1 )]
      [TestCase( DOWN_BOARD, new[] { Move.Down }, 0, 1 )]
      [TestCase( MEDIUM_BOARD, new[] { Move.Right, Move.Down, Move.Down, Move.Left, Move.Left, Move.Up, Move.Down, Move.Down, Move.Right, Move.Right, Move.Right, Move.Right, Move.Up }, 12, 1 )]
      [TestCase( LARGE_BOARD, new[] { Move.Up, Move.Left, Move.Up, Move.Left, Move.Left, Move.Up, Move.Left, Move.Down, Move.Down, Move.Up, Move.Right, Move.Right, Move.Right, Move.Down, Move.Down, Move.Left, Move.Up, Move.Left, Move.Right, Move.Right, Move.Up, Move.Left, Move.Left, Move.Right, Move.Right, Move.Up, Move.Right, Move.Up, Move.Left, Move.Left, Move.Left, Move.Up, Move.Left, Move.Down }, 20, 14 )]
      public void TestCountMoves(string boardStr, Move[] moveList, int moves, int pushes)
      {
         var board = PlaybackMoves(boardStr, moveList);
         Assert.AreEqual( moves, board.Moves );
         Assert.AreEqual( pushes, board.Pushes );
      }

      [TestCase( SIMPLE_BOARD, new[] { Move.Right } )]
      [TestCase( DOWN_BOARD, new[] { Move.Down } )]
      [TestCase( LARGE_BOARD, new[] { Move.Up, Move.Left, Move.Up, Move.Left, Move.Left, Move.Up, Move.Left, Move.Down, Move.Down, Move.Up, Move.Right, Move.Right, Move.Right, Move.Down, Move.Down, Move.Left, Move.Up, Move.Left, Move.Right, Move.Right, Move.Up, Move.Left, Move.Left, Move.Right, Move.Right, Move.Up, Move.Right, Move.Up, Move.Left, Move.Left, Move.Left, Move.Up, Move.Left, Move.Down } )]
      public void TestSolving( string boardStr, Move[] moveList )
      {
         var board = PlaybackMoves( boardStr, moveList );
         Assert.IsTrue( board.IsSolved() );
      }

      [TestCase( SIMPLE_BOARD, new[] { Move.Right }, "R" )]
      [TestCase( DOWN_BOARD, new[] { Move.Down }, "D" )]
      [TestCase( MEDIUM_BOARD, new[] { Move.Right, Move.Down, Move.Down, Move.Left, Move.Left, Move.Up, Move.Down, Move.Down, Move.Right, Move.Right, Move.Right, Move.Right, Move.Up }, "rddllUddrrrru" )]
      [TestCase( LARGE_BOARD, new[] { Move.Up, Move.Left, Move.Up, Move.Left, Move.Left, Move.Up, Move.Left, Move.Down, Move.Down, Move.Up, Move.Right, Move.Right, Move.Right, Move.Down, Move.Down, Move.Left, Move.Up, Move.Left, Move.Right, Move.Right, Move.Up, Move.Left, Move.Left, Move.Right, Move.Right, Move.Up, Move.Right, Move.Up, Move.Left, Move.Left, Move.Left, Move.Up, Move.Left, Move.Down }, LARGE_BOARD_SOLUTION )]
      public void TestMoveList(string boardStr, Move[] moveList, string expected)
      {
         var board = PlaybackMoves( boardStr, moveList );
         Assert.AreEqual( expected, board.MoveList );
      }

      private static Board PlaybackMoves(string boardStr, Move[] moveList)
      {
         var board = new Board();
         board.Load(boardStr);
         foreach (Move move in moveList)
         {
            bool valid = board.MakeMove(move);
            Assert.IsTrue(valid);
         }
         return board;
      }
   }
}
