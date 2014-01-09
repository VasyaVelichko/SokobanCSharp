namespace Alteridem.Sokoban.WinForms
{
   partial class GameForm
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
         this._statusStrip = new System.Windows.Forms.StatusStrip();
         this._boardControl = new Alteridem.Sokoban.WinForms.BoardControl();
         this._mainMenu = new System.Windows.Forms.MenuStrip();
         this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
         this.toolStripContainer1.ContentPanel.SuspendLayout();
         this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
         this.toolStripContainer1.SuspendLayout();
         this.SuspendLayout();
         // 
         // toolStripContainer1
         // 
         // 
         // toolStripContainer1.BottomToolStripPanel
         // 
         this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this._statusStrip);
         // 
         // toolStripContainer1.ContentPanel
         // 
         this.toolStripContainer1.ContentPanel.Controls.Add(this._boardControl);
         this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(759, 506);
         this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
         this.toolStripContainer1.Name = "toolStripContainer1";
         this.toolStripContainer1.Size = new System.Drawing.Size(759, 552);
         this.toolStripContainer1.TabIndex = 0;
         this.toolStripContainer1.Text = "toolStripContainer1";
         // 
         // toolStripContainer1.TopToolStripPanel
         // 
         this.toolStripContainer1.TopToolStripPanel.Controls.Add(this._mainMenu);
         // 
         // _statusStrip
         // 
         this._statusStrip.Dock = System.Windows.Forms.DockStyle.None;
         this._statusStrip.Location = new System.Drawing.Point(0, 0);
         this._statusStrip.Name = "_statusStrip";
         this._statusStrip.Size = new System.Drawing.Size(759, 22);
         this._statusStrip.TabIndex = 0;
         // 
         // _boardControl
         // 
         this._boardControl.BackColor = System.Drawing.Color.White;
         this._boardControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this._boardControl.Font = new System.Drawing.Font("Arial Rounded MT Bold", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._boardControl.Location = new System.Drawing.Point(0, 0);
         this._boardControl.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
         this._boardControl.Name = "_boardControl";
         this._boardControl.Size = new System.Drawing.Size(759, 506);
         this._boardControl.TabIndex = 0;
         // 
         // _mainMenu
         // 
         this._mainMenu.Dock = System.Windows.Forms.DockStyle.None;
         this._mainMenu.Location = new System.Drawing.Point(0, 0);
         this._mainMenu.Name = "_mainMenu";
         this._mainMenu.Size = new System.Drawing.Size(759, 24);
         this._mainMenu.TabIndex = 0;
         this._mainMenu.Text = "menuStrip1";
         // 
         // GameForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(759, 552);
         this.Controls.Add(this.toolStripContainer1);
         this.MainMenuStrip = this._mainMenu;
         this.Name = "GameForm";
         this.Text = "Sokoban";
         this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
         this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
         this.toolStripContainer1.ContentPanel.ResumeLayout(false);
         this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
         this.toolStripContainer1.TopToolStripPanel.PerformLayout();
         this.toolStripContainer1.ResumeLayout(false);
         this.toolStripContainer1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.ToolStripContainer toolStripContainer1;
      private System.Windows.Forms.StatusStrip _statusStrip;
      private System.Windows.Forms.MenuStrip _mainMenu;
      private Alteridem.Sokoban.WinForms.BoardControl _boardControl;
   }
}

