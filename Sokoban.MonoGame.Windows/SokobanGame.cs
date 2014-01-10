#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace Alteridem.Sokoban.MonoGame.Windows
{
   /// <summary>
   /// This is the main type for your game
   /// </summary>
   public class SokobanGame : Game
   {
      private GraphicsDeviceManager _graphics;
      private SpriteBatch _spriteBatch;
      private Board _board;
      private BoardSprites _boardSprites;
      private KeyboardState _oldState; // Used to determine when a pressed key is released
      private readonly int _hudHeight = 0; // Set this up now to use later

      public SokobanGame()
         : base()
      {
         _graphics = new GraphicsDeviceManager( this );
         Content.RootDirectory = "Content";
      }

      /// <summary>
      /// Allows the game to perform any initialization it needs to before starting to run.
      /// This is where it can query for any required services and load any non-graphic
      /// related content.  Calling base.Initialize will enumerate through any components
      /// and initialize them as well.
      /// </summary>
      protected override void Initialize()
      {
         IsMouseVisible = true;
         Window.Title = "Sokoban";
         Window.IsBorderless = true;

         _oldState = Keyboard.GetState( );

         _board = new Board();
         _board.Load( "7#|#5-#|#5-#|#.-#2-#|#.-2$-#|#.2$2-#|#.#2-@#|7#" );

         base.Initialize();
      }

      /// <summary>
      /// LoadContent will be called once per game and is the place to load
      /// all of your content.
      /// </summary>
      protected override void LoadContent()
      {
         // Create a new SpriteBatch, which can be used to draw textures.
         _spriteBatch = new SpriteBatch( GraphicsDevice );
         _boardSprites = new BoardSprites( this );
      }

      /// <summary>
      /// UnloadContent will be called once per game and is the place to unload
      /// all content.
      /// </summary>
      protected override void UnloadContent()
      {
      }

      /// <summary>
      /// Allows the game to run logic such as updating the world,
      /// checking for collisions, gathering input, and playing audio.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
      protected override void Update( GameTime gameTime )
      {
         if ( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown( Keys.Escape ) )
            Exit();

         var newState = Keyboard.GetState( );

         if ( newState.IsKeyDown( Keys.Up ) && !_oldState.IsKeyDown( Keys.Up ) )
            _board.MakeMove( Move.Up );
         else if ( newState.IsKeyDown( Keys.Down ) && !_oldState.IsKeyDown( Keys.Down ) )
            _board.MakeMove( Move.Down );
         else if ( newState.IsKeyDown( Keys.Right ) && !_oldState.IsKeyDown( Keys.Right ) )
            _board.MakeMove( Move.Right );
         else if ( newState.IsKeyDown( Keys.Left ) && !_oldState.IsKeyDown( Keys.Left ) )
            _board.MakeMove( Move.Left );

         _oldState = newState;

         base.Update( gameTime );
      }

      /// <summary>
      /// This is called when the game should draw itself.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
      protected override void Draw( GameTime gameTime )
      {
         GraphicsDevice.Clear( _board.IsSolved() ? Color.PeachPuff : Color.CornflowerBlue );

         _spriteBatch.Begin();
         var gameBounds = new Rectangle( 0, _hudHeight, Window.ClientBounds.Width, Window.ClientBounds.Height - _hudHeight );
         _boardSprites.Draw( _spriteBatch, _board, gameBounds );
         _spriteBatch.End();

         base.Draw( gameTime );
      }
   }
}
