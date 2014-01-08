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
         this._label = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // _label
         // 
         this._label.Dock = System.Windows.Forms.DockStyle.Fill;
         this._label.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._label.Location = new System.Drawing.Point(0, 0);
         this._label.Name = "_label";
         this._label.Size = new System.Drawing.Size(434, 256);
         this._label.TabIndex = 0;
         // 
         // BoardControl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this._label);
         this.Name = "BoardControl";
         this.Size = new System.Drawing.Size(434, 256);
         this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Label _label;
   }
}
