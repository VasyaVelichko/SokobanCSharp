namespace Alteridem.Sokoban
{
   /// <summary>
   /// Represents a position on the board
   /// </summary>
   public struct Position
   {
      private readonly int m_row;
      private readonly int m_column;

      public int Row { get { return m_row; } }
      public int Column { get { return m_column; } }

      public Position( int row, int column )
      {
         m_row = row;
         m_column = column;
      }

      public bool Valid()
      {
         return m_row >= 0 && m_column >= 0;
      }

      public Position MakeMove( Move move )
      {
         int r = 0;
         int c = 0;

         switch ( move )
         {
            case Move.Up:
               r = -1;
               break;
            case Move.Down:
               r = 1;
               break;
            case Move.Left:
               c = -1;
               break;
            case Move.Right:
               c = 1;
               break;
         }
         return new Position( m_row + r, m_column + c );
      }
   }
}
