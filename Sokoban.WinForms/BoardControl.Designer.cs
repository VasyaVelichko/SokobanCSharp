namespace Alteridem.Sokoban.WinForms
{
   partial class BoardControl
   {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose( bool disposing )
      {
         if ( disposing && ( components != null ) )
         {
            components.Dispose();
         }
         base.Dispose( disposing );
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoardControl));
         this._imageList = new System.Windows.Forms.ImageList(this.components);
         this.SuspendLayout();
         // 
         // _imageList
         // 
         this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
         this._imageList.TransparentColor = System.Drawing.Color.Transparent;
         this._imageList.Images.SetKeyName(0, "wall.png");
         this._imageList.Images.SetKeyName(1, "player.png");
         this._imageList.Images.SetKeyName(2, "player_on_goal.png");
         this._imageList.Images.SetKeyName(3, "box.png");
         this._imageList.Images.SetKeyName(4, "box_on_goal.png");
         this._imageList.Images.SetKeyName(5, "goal.png");
         // 
         // BoardControl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(23F, 43F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.Color.White;
         this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
         this.Name = "BoardControl";
         this.Size = new System.Drawing.Size(685, 493);
         this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.ImageList _imageList;
   }
}
