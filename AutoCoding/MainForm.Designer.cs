/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-6-18
 * Time: 22:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace AutoCoding
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
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
			this.tabControl_main = new System.Windows.Forms.TabControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.btn_ReRead = new System.Windows.Forms.Button();
			this.propertyGrid_Func = new System.Windows.Forms.PropertyGrid();
			this.btn_GenCode = new System.Windows.Forms.Button();
			this.richTextBox_Demo = new System.Windows.Forms.RichTextBox();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.btn_CopyBatch = new System.Windows.Forms.Button();
			this.comboBox_Batch = new System.Windows.Forms.ComboBox();
			this.btn_Batch = new System.Windows.Forms.Button();
			this.richTextBox_BatchOut = new System.Windows.Forms.RichTextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.propertyGrid_Config = new System.Windows.Forms.PropertyGrid();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.richTextBox_file = new System.Windows.Forms.RichTextBox();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.richTextBox_struct = new System.Windows.Forms.RichTextBox();
			this.tabPage9 = new System.Windows.Forms.TabPage();
			this.btn_CopySqlResult = new System.Windows.Forms.Button();
			this.btn_ClearAllText = new System.Windows.Forms.Button();
			this.btn_ToSql = new System.Windows.Forms.Button();
			this.textBox_sql = new System.Windows.Forms.TextBox();
			this.textBox_tableStructure = new System.Windows.Forms.TextBox();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.btn_RelAnaClearAll = new System.Windows.Forms.Button();
			this.textBox_Src2 = new System.Windows.Forms.TextBox();
			this.textBox_Src1 = new System.Windows.Forms.TextBox();
			this.btn_comp = new System.Windows.Forms.Button();
			this.richTextBox_CompRes = new System.Windows.Forms.RichTextBox();
			this.comboBox_inCols = new System.Windows.Forms.ComboBox();
			this.tabPage8 = new System.Windows.Forms.TabPage();
			this.tabControl_main.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.tabPage9.SuspendLayout();
			this.tabPage6.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl_main
			// 
			this.tabControl_main.Controls.Add(this.tabPage2);
			this.tabControl_main.Controls.Add(this.tabPage4);
			this.tabControl_main.Controls.Add(this.tabPage3);
			this.tabControl_main.Controls.Add(this.tabPage1);
			this.tabControl_main.Controls.Add(this.tabPage5);
			this.tabControl_main.Controls.Add(this.tabPage9);
			this.tabControl_main.Controls.Add(this.tabPage6);
			this.tabControl_main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl_main.Location = new System.Drawing.Point(0, 0);
			this.tabControl_main.Name = "tabControl_main";
			this.tabControl_main.SelectedIndex = 0;
			this.tabControl_main.Size = new System.Drawing.Size(957, 563);
			this.tabControl_main.TabIndex = 1;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.btn_ReRead);
			this.tabPage2.Controls.Add(this.propertyGrid_Func);
			this.tabPage2.Controls.Add(this.btn_GenCode);
			this.tabPage2.Controls.Add(this.richTextBox_Demo);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(949, 537);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "AutoCoding";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// btn_ReRead
			// 
			this.btn_ReRead.Location = new System.Drawing.Point(420, 3);
			this.btn_ReRead.Name = "btn_ReRead";
			this.btn_ReRead.Size = new System.Drawing.Size(75, 23);
			this.btn_ReRead.TabIndex = 34;
			this.btn_ReRead.Text = "重新读取";
			this.btn_ReRead.UseVisualStyleBackColor = true;
			this.btn_ReRead.Click += new System.EventHandler(this.Btn_ReReadClick);
			// 
			// propertyGrid_Func
			// 
			this.propertyGrid_Func.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left)));
			this.propertyGrid_Func.Location = new System.Drawing.Point(0, 3);
			this.propertyGrid_Func.Name = "propertyGrid_Func";
			this.propertyGrid_Func.Size = new System.Drawing.Size(320, 534);
			this.propertyGrid_Func.TabIndex = 33;
			this.propertyGrid_Func.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGrid_FuncPropertyValueChanged);
			// 
			// btn_GenCode
			// 
			this.btn_GenCode.Location = new System.Drawing.Point(324, 3);
			this.btn_GenCode.Name = "btn_GenCode";
			this.btn_GenCode.Size = new System.Drawing.Size(75, 23);
			this.btn_GenCode.TabIndex = 26;
			this.btn_GenCode.Text = "生成";
			this.btn_GenCode.UseVisualStyleBackColor = true;
			this.btn_GenCode.Click += new System.EventHandler(this.Btn_GenCodeClick);
			// 
			// richTextBox_Demo
			// 
			this.richTextBox_Demo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox_Demo.HideSelection = false;
			this.richTextBox_Demo.Location = new System.Drawing.Point(324, 32);
			this.richTextBox_Demo.Name = "richTextBox_Demo";
			this.richTextBox_Demo.Size = new System.Drawing.Size(623, 500);
			this.richTextBox_Demo.TabIndex = 0;
			this.richTextBox_Demo.Text = "";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.btn_CopyBatch);
			this.tabPage4.Controls.Add(this.comboBox_Batch);
			this.tabPage4.Controls.Add(this.btn_Batch);
			this.tabPage4.Controls.Add(this.richTextBox_BatchOut);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(949, 537);
			this.tabPage4.TabIndex = 5;
			this.tabPage4.Text = "BatchCoding";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// btn_CopyBatch
			// 
			this.btn_CopyBatch.Location = new System.Drawing.Point(394, 2);
			this.btn_CopyBatch.Name = "btn_CopyBatch";
			this.btn_CopyBatch.Size = new System.Drawing.Size(75, 28);
			this.btn_CopyBatch.TabIndex = 4;
			this.btn_CopyBatch.Text = "拷贝结果";
			this.btn_CopyBatch.UseVisualStyleBackColor = true;
			this.btn_CopyBatch.Click += new System.EventHandler(this.Btn_CopyBatchClick);
			// 
			// comboBox_Batch
			// 
			this.comboBox_Batch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_Batch.FormattingEnabled = true;
			this.comboBox_Batch.Location = new System.Drawing.Point(8, 6);
			this.comboBox_Batch.Name = "comboBox_Batch";
			this.comboBox_Batch.Size = new System.Drawing.Size(289, 20);
			this.comboBox_Batch.TabIndex = 3;
			// 
			// btn_Batch
			// 
			this.btn_Batch.Location = new System.Drawing.Point(303, 3);
			this.btn_Batch.Name = "btn_Batch";
			this.btn_Batch.Size = new System.Drawing.Size(75, 27);
			this.btn_Batch.TabIndex = 2;
			this.btn_Batch.Text = "生成";
			this.btn_Batch.UseVisualStyleBackColor = true;
			this.btn_Batch.Click += new System.EventHandler(this.Btn_BatchClick);
			// 
			// richTextBox_BatchOut
			// 
			this.richTextBox_BatchOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox_BatchOut.Location = new System.Drawing.Point(3, 32);
			this.richTextBox_BatchOut.Name = "richTextBox_BatchOut";
			this.richTextBox_BatchOut.Size = new System.Drawing.Size(946, 500);
			this.richTextBox_BatchOut.TabIndex = 1;
			this.richTextBox_BatchOut.Text = "";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.propertyGrid_Config);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(949, 537);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "设置";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// propertyGrid_Config
			// 
			this.propertyGrid_Config.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left)));
			this.propertyGrid_Config.Location = new System.Drawing.Point(0, 2);
			this.propertyGrid_Config.Name = "propertyGrid_Config";
			this.propertyGrid_Config.Size = new System.Drawing.Size(320, 533);
			this.propertyGrid_Config.TabIndex = 21;
			this.propertyGrid_Config.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGrid_ConfigPropertyValueChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.richTextBox_file);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(949, 537);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "创建表Sql语句";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// richTextBox_file
			// 
			this.richTextBox_file.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox_file.Location = new System.Drawing.Point(3, 3);
			this.richTextBox_file.Name = "richTextBox_file";
			this.richTextBox_file.Size = new System.Drawing.Size(943, 506);
			this.richTextBox_file.TabIndex = 0;
			this.richTextBox_file.Text = "";
			this.richTextBox_file.TextChanged += new System.EventHandler(this.RichTextBox_fileTextChanged);
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.richTextBox_struct);
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage5.Size = new System.Drawing.Size(949, 537);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "结构体";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// richTextBox_struct
			// 
			this.richTextBox_struct.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox_struct.Location = new System.Drawing.Point(3, 3);
			this.richTextBox_struct.Name = "richTextBox_struct";
			this.richTextBox_struct.Size = new System.Drawing.Size(943, 531);
			this.richTextBox_struct.TabIndex = 0;
			this.richTextBox_struct.Text = "";
			this.richTextBox_struct.TextChanged += new System.EventHandler(this.RichTextBox_structTextChanged);
			// 
			// tabPage9
			// 
			this.tabPage9.Controls.Add(this.btn_CopySqlResult);
			this.tabPage9.Controls.Add(this.btn_ClearAllText);
			this.tabPage9.Controls.Add(this.btn_ToSql);
			this.tabPage9.Controls.Add(this.textBox_sql);
			this.tabPage9.Controls.Add(this.textBox_tableStructure);
			this.tabPage9.Location = new System.Drawing.Point(4, 22);
			this.tabPage9.Name = "tabPage9";
			this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage9.Size = new System.Drawing.Size(949, 537);
			this.tabPage9.TabIndex = 8;
			this.tabPage9.Text = "表结构2Sql语句";
			this.tabPage9.UseVisualStyleBackColor = true;
			// 
			// btn_CopySqlResult
			// 
			this.btn_CopySqlResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_CopySqlResult.Location = new System.Drawing.Point(854, 267);
			this.btn_CopySqlResult.Name = "btn_CopySqlResult";
			this.btn_CopySqlResult.Size = new System.Drawing.Size(90, 33);
			this.btn_CopySqlResult.TabIndex = 4;
			this.btn_CopySqlResult.Text = "拷贝结果";
			this.btn_CopySqlResult.UseVisualStyleBackColor = true;
			this.btn_CopySqlResult.Click += new System.EventHandler(this.Btn_CopySqlResultClick);
			// 
			// btn_ClearAllText
			// 
			this.btn_ClearAllText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_ClearAllText.Location = new System.Drawing.Point(854, 64);
			this.btn_ClearAllText.Name = "btn_ClearAllText";
			this.btn_ClearAllText.Size = new System.Drawing.Size(90, 33);
			this.btn_ClearAllText.TabIndex = 3;
			this.btn_ClearAllText.Text = "清除所有";
			this.btn_ClearAllText.UseVisualStyleBackColor = true;
			this.btn_ClearAllText.Click += new System.EventHandler(this.Btn_ClearAllTextClick);
			// 
			// btn_ToSql
			// 
			this.btn_ToSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_ToSql.Location = new System.Drawing.Point(853, 6);
			this.btn_ToSql.Name = "btn_ToSql";
			this.btn_ToSql.Size = new System.Drawing.Size(90, 33);
			this.btn_ToSql.TabIndex = 2;
			this.btn_ToSql.Text = "生成sql语句";
			this.btn_ToSql.UseVisualStyleBackColor = true;
			this.btn_ToSql.Click += new System.EventHandler(this.Btn_ToSqlClick);
			// 
			// textBox_sql
			// 
			this.textBox_sql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_sql.Location = new System.Drawing.Point(3, 267);
			this.textBox_sql.Multiline = true;
			this.textBox_sql.Name = "textBox_sql";
			this.textBox_sql.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox_sql.Size = new System.Drawing.Size(844, 263);
			this.textBox_sql.TabIndex = 1;
			// 
			// textBox_tableStructure
			// 
			this.textBox_tableStructure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_tableStructure.Location = new System.Drawing.Point(3, 6);
			this.textBox_tableStructure.Multiline = true;
			this.textBox_tableStructure.Name = "textBox_tableStructure";
			this.textBox_tableStructure.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox_tableStructure.Size = new System.Drawing.Size(844, 255);
			this.textBox_tableStructure.TabIndex = 0;
			// 
			// tabPage6
			// 
			this.tabPage6.Controls.Add(this.btn_RelAnaClearAll);
			this.tabPage6.Controls.Add(this.textBox_Src2);
			this.tabPage6.Controls.Add(this.textBox_Src1);
			this.tabPage6.Controls.Add(this.btn_comp);
			this.tabPage6.Controls.Add(this.richTextBox_CompRes);
			this.tabPage6.Location = new System.Drawing.Point(4, 22);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage6.Size = new System.Drawing.Size(949, 537);
			this.tabPage6.TabIndex = 9;
			this.tabPage6.Text = "对应参数";
			this.tabPage6.UseVisualStyleBackColor = true;
			// 
			// btn_RelAnaClearAll
			// 
			this.btn_RelAnaClearAll.Location = new System.Drawing.Point(112, 207);
			this.btn_RelAnaClearAll.Name = "btn_RelAnaClearAll";
			this.btn_RelAnaClearAll.Size = new System.Drawing.Size(75, 23);
			this.btn_RelAnaClearAll.TabIndex = 3;
			this.btn_RelAnaClearAll.Text = "全部清空";
			this.btn_RelAnaClearAll.UseVisualStyleBackColor = true;
			this.btn_RelAnaClearAll.Click += new System.EventHandler(this.Btn_RelAnaClearAllClick);
			// 
			// textBox_Src2
			// 
			this.textBox_Src2.Location = new System.Drawing.Point(489, 7);
			this.textBox_Src2.Multiline = true;
			this.textBox_Src2.Name = "textBox_Src2";
			this.textBox_Src2.Size = new System.Drawing.Size(452, 195);
			this.textBox_Src2.TabIndex = 2;
			// 
			// textBox_Src1
			// 
			this.textBox_Src1.Location = new System.Drawing.Point(9, 7);
			this.textBox_Src1.Multiline = true;
			this.textBox_Src1.Name = "textBox_Src1";
			this.textBox_Src1.Size = new System.Drawing.Size(474, 195);
			this.textBox_Src1.TabIndex = 2;
			// 
			// btn_comp
			// 
			this.btn_comp.Location = new System.Drawing.Point(9, 208);
			this.btn_comp.Name = "btn_comp";
			this.btn_comp.Size = new System.Drawing.Size(96, 23);
			this.btn_comp.TabIndex = 1;
			this.btn_comp.Text = "查看对应关系";
			this.btn_comp.UseVisualStyleBackColor = true;
			this.btn_comp.Click += new System.EventHandler(this.Btn_compClick);
			// 
			// richTextBox_CompRes
			// 
			this.richTextBox_CompRes.Location = new System.Drawing.Point(6, 237);
			this.richTextBox_CompRes.Name = "richTextBox_CompRes";
			this.richTextBox_CompRes.Size = new System.Drawing.Size(937, 294);
			this.richTextBox_CompRes.TabIndex = 0;
			this.richTextBox_CompRes.Text = "";
			// 
			// comboBox_inCols
			// 
			this.comboBox_inCols.FormattingEnabled = true;
			this.comboBox_inCols.Location = new System.Drawing.Point(174, 62);
			this.comboBox_inCols.Name = "comboBox_inCols";
			this.comboBox_inCols.Size = new System.Drawing.Size(180, 20);
			this.comboBox_inCols.TabIndex = 22;
			// 
			// tabPage8
			// 
			this.tabPage8.Location = new System.Drawing.Point(4, 21);
			this.tabPage8.Name = "tabPage8";
			this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage8.Size = new System.Drawing.Size(949, 538);
			this.tabPage8.TabIndex = 8;
			this.tabPage8.Text = "EasyCoding";
			this.tabPage8.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(957, 563);
			this.Controls.Add(this.tabControl_main);
			this.Name = "MainForm";
			this.Text = "AutoCoding - From Sql to Esql V2.0";
			this.tabControl_main.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage5.ResumeLayout(false);
			this.tabPage9.ResumeLayout(false);
			this.tabPage9.PerformLayout();
			this.tabPage6.ResumeLayout(false);
			this.tabPage6.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button btn_RelAnaClearAll;
		private System.Windows.Forms.TextBox textBox_Src1;
		private System.Windows.Forms.TextBox textBox_Src2;
		private System.Windows.Forms.RichTextBox richTextBox_CompRes;
		private System.Windows.Forms.Button btn_comp;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.Button btn_CopyBatch;
		private System.Windows.Forms.ComboBox comboBox_Batch;
		private System.Windows.Forms.Button btn_ClearAllText;
		private System.Windows.Forms.Button btn_CopySqlResult;
		private System.Windows.Forms.Button btn_ToSql;
		private System.Windows.Forms.TextBox textBox_tableStructure;
		private System.Windows.Forms.TextBox textBox_sql;
		private System.Windows.Forms.TabPage tabPage9;
		private System.Windows.Forms.TabPage tabPage8;
		private System.Windows.Forms.Button btn_Batch;
		private System.Windows.Forms.RichTextBox richTextBox_BatchOut;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Button btn_ReRead;
		private System.Windows.Forms.PropertyGrid propertyGrid_Func;
		private System.Windows.Forms.PropertyGrid propertyGrid_Config;
		private System.Windows.Forms.Button btn_GenCode;
		private System.Windows.Forms.RichTextBox richTextBox_struct;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.ComboBox comboBox_inCols;
		private System.Windows.Forms.RichTextBox richTextBox_Demo;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.RichTextBox richTextBox_file;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl_main;
	}
}
