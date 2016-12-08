/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-7-18
 * Time: 14:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace AutoCoding
{
	partial class CheckListBoxUI
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.checkedListBox = new System.Windows.Forms.CheckedListBox();
			this.btn_SelectAll = new System.Windows.Forms.Button();
			this.btn_Key = new System.Windows.Forms.Button();
			this.btn_SelectReverse = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// checkedListBox
			// 
			this.checkedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBox.FormattingEnabled = true;
			this.checkedListBox.Location = new System.Drawing.Point(0, 0);
			this.checkedListBox.Name = "checkedListBox";
			this.checkedListBox.Size = new System.Drawing.Size(174, 260);
			this.checkedListBox.TabIndex = 0;
			// 
			// btn_SelectAll
			// 
			this.btn_SelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btn_SelectAll.Location = new System.Drawing.Point(0, 266);
			this.btn_SelectAll.Name = "btn_SelectAll";
			this.btn_SelectAll.Size = new System.Drawing.Size(53, 33);
			this.btn_SelectAll.TabIndex = 1;
			this.btn_SelectAll.Text = "全选";
			this.btn_SelectAll.UseVisualStyleBackColor = true;
			this.btn_SelectAll.Click += new System.EventHandler(this.Btn_SelectAllClick);
			// 
			// btn_Key
			// 
			this.btn_Key.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btn_Key.Location = new System.Drawing.Point(59, 266);
			this.btn_Key.Name = "btn_Key";
			this.btn_Key.Size = new System.Drawing.Size(53, 33);
			this.btn_Key.TabIndex = 1;
			this.btn_Key.Text = "主键";
			this.btn_Key.UseVisualStyleBackColor = true;
			this.btn_Key.Click += new System.EventHandler(this.Btn_KeyClick);
			// 
			// btn_SelectReverse
			// 
			this.btn_SelectReverse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btn_SelectReverse.Location = new System.Drawing.Point(118, 266);
			this.btn_SelectReverse.Name = "btn_SelectReverse";
			this.btn_SelectReverse.Size = new System.Drawing.Size(53, 33);
			this.btn_SelectReverse.TabIndex = 1;
			this.btn_SelectReverse.Text = "反选";
			this.btn_SelectReverse.UseVisualStyleBackColor = true;
			this.btn_SelectReverse.Click += new System.EventHandler(this.Btn_SelectReverseClick);
			// 
			// CheckListBoxUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.btn_SelectReverse);
			this.Controls.Add(this.btn_Key);
			this.Controls.Add(this.btn_SelectAll);
			this.Controls.Add(this.checkedListBox);
			this.Name = "CheckListBoxUI";
			this.Size = new System.Drawing.Size(174, 305);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button btn_SelectReverse;
		private System.Windows.Forms.Button btn_Key;
		private System.Windows.Forms.Button btn_SelectAll;
		private System.Windows.Forms.CheckedListBox checkedListBox;
	}
}
