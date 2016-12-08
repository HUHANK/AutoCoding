/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-6-18
 * Time: 22:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;
using LuaInterface;

namespace AutoCoding
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private FixConfig fixconfig;
		private CodingConfig funcConfig;
		public static StaticInfo staticInfo;
		public static List<string> autoCodingType;
		private string _FixConfigFilePath;
		private LuaHelper luaHelper;

		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//			
			_FixConfigFilePath = Application.StartupPath + @"\FixConfig.xml";
			
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(FixConfig));
				//Stream reader= new FileStream("FixConfig.xml",FileMode.Open);
				TextReader tr = new StreamReader(_FixConfigFilePath, Encoding.GetEncoding("GB2312"));
				//fixconfig = (FixConfig)serializer.Deserialize(reader);
				fixconfig = (FixConfig)serializer.Deserialize(tr);
				//reader.Close();
				tr.Close();
			} 
			catch (Exception) 
			{
				fixconfig = new FixConfig();
			}
			
			propertyGrid_Config.PropertySort = PropertySort.Categorized;
			propertyGrid_Config.SelectedObject = fixconfig;
			
			try 
			{
				staticInfo = new StaticInfo();
				
				if (fixconfig.CreateTableFilePath.Trim() != "") {
					StreamReader sr = new StreamReader(fixconfig.CreateTableFilePath, Encoding.GetEncoding("GB2312"));
					SqlAnalyse sa = new SqlAnalyse(sr.ReadToEnd());
					sa.Analyse();
					staticInfo.Tables = sa.Tables;
					sr.Close();	
				}
				
				if (fixconfig.TbStructFilePath.Trim() != "") {
					StreamReader sr2 = new StreamReader(fixconfig.TbStructFilePath, Encoding.GetEncoding("GB2312"));
					StructAnalyse sta = new StructAnalyse(sr2.ReadToEnd());
					sta.Analyse();
					staticInfo.Structs = sta.Structs;
					sr2.Close();
				}
				
				if (fixconfig.BuStructFilePath != "") 
				{
					StreamReader sr3 = new StreamReader(fixconfig.BuStructFilePath, Encoding.GetEncoding("GB2312"));
					StructAnalyse sta2 = new StructAnalyse(sr3.ReadToEnd());
					sta2.Analyse();
					foreach (string strKey in sta2.Structs.Keys) 
					{
						if (!staticInfo.Structs.ContainsKey(strKey))
						{
							staticInfo.Structs.Add(strKey, sta2.Structs[strKey]);
						}
					}
					sr3.Close();
				}
			} 
			catch (Exception ex) 
			{
				MessageBox.Show(ex.Message);
			}
			luaHelper = new LuaHelper();
			luaHelper._FixConfig = fixconfig;
			luaHelper.ScriptPath = System.Windows.Forms.Application.StartupPath + "\\scripts\\";
			luaHelper.LoadConfig();
			autoCodingType = luaHelper.GetAutoCodingTypes();
			
			funcConfig = new CodingConfig();
			if (autoCodingType.Count > 0) {
				funcConfig.CodingType = autoCodingType[0];
			}
			
			propertyGrid_Func.PropertySort = PropertySort.Categorized;
			propertyGrid_Func.SelectedObject = funcConfig;
			
			comboBox_Batch.Items.Add("请选择编码类型");
			/*foreach (string str in Enum.GetNames(typeof(BatchCodingType)))
			{
				comboBox_Batch.Items.Add(str);
			}*/
			comboBox_Batch.Items.AddRange(luaHelper.GetBatchCodingTypes().ToArray());
			comboBox_Batch.SelectedIndex = 0;
		}
		
		void Btn_GenCodeClick(object sender, EventArgs e)
		{
			if (!CheckInputValid())
			{
				return;
			}
			/*Coding sc = new Coding();
			sc._FixConfig = fixconfig;
			sc._CodingConfig = funcConfig;
			sc.lua = luaHelper;*/
			
			try {
				luaHelper._CodingConfig = funcConfig;
				luaHelper.SetCurTable(funcConfig.TableName);
				richTextBox_Demo.Text = luaHelper.DoAutoCoding(funcConfig.CodingType);
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
				return;
			}
		}
		
		bool CheckInputValid()
		{
			if (funcConfig.TableName == "") 
			{
				MessageBox.Show("请选择数据库表名");
				return false;
			}
			return true;
		}
		
		void PropertyGrid_ConfigPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			try 
			{
				XmlSerializer ser = new XmlSerializer(typeof(FixConfig));				
		        // A FileStream is used to write the file.
		        //FileStream fs = new FileStream("FixConfig.xml",FileMode.Create);
		        //ser.Serialize(fs,fixconfig);
		        TextWriter tw = new StreamWriter(_FixConfigFilePath, false, Encoding.GetEncoding("GB2312"));
		        ser.Serialize(tw, fixconfig);
		        //fs.Close();
		        tw.Close();
			}
			catch (Exception ex) 
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		void PropertyGrid_FuncPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (staticInfo.CurTableName != funcConfig.TableName) 
			{
				staticInfo.CurTableName = funcConfig.TableName;
				funcConfig.SelectCols = "";
				funcConfig.WhereCols = "";
				funcConfig.InsertCols = "";
				funcConfig.UpdateCols = "";
				funcConfig.UsedCols = "";
				
				funcConfig.BSqlFuncName = "";
				funcConfig.BProFuncName = "";
				funcConfig.FuncComment = "";
				funcConfig.FuncNo = "";
				funcConfig.NotFoundCode = "";
				funcConfig.BSsmtFuncName = "";
				
				string strStName = "";
//				if (staticInfo.CurTableName.Trim().Substring(0, 3).ToUpper() == "KS."
//				    && staticInfo.CurTableName.Trim().Substring(0, 7).ToUpper() != "KS.SOC_")
//				{
					strStName = "ST_" + staticInfo.CurTableName.Substring(staticInfo.CurTableName.IndexOf('.') + 1) + fixconfig.DefStructSuffix;
//				} else {
//					strStName = "ST_" + staticInfo.CurTableName.Substring(staticInfo.CurTableName.IndexOf('.') + 1);
//				}
				
				if (staticInfo.Structs.ContainsKey(strStName)) {
					funcConfig.StructIn = strStName;
					funcConfig.StructOut = strStName;
				} else {
					funcConfig.StructIn = "";
					funcConfig.StructOut = "";
				}
				
				funcConfig.BSqlFile = "";
				funcConfig.BSqlIncFile = "";
				funcConfig.BProFile = "";
				funcConfig.BProIncFile = "";
			}
			if (staticInfo.CurBSqlFile != funcConfig.BSqlFile) 
			{
				staticInfo.CurBSqlFile = funcConfig.BSqlFile;
				if (fixconfig.BSqlFilePrefix == "") {
					MessageBox.Show("请设置BSql层文件名前缀");
					propertyGrid_Func.Refresh();
					return;
				}
				if (funcConfig.BSqlIncFile !="" && funcConfig.BSqlFile.IndexOf(@"\bsql\" + fixconfig.BSqlFilePrefix) < 0) {
					MessageBox.Show(@"路径中没有\bsql\" + fixconfig.BSqlFilePrefix + "请检查文件路径是否有误");
					funcConfig.BSqlIncFile = "";
					funcConfig.BProFile = "";
					funcConfig.BProIncFile = "";
				} else {
					funcConfig.BSqlIncFile = funcConfig.BSqlFile.Replace(@"\bsql\",@"\include\").Replace(".sqc",".h");
					funcConfig.BProFile = funcConfig.BSqlFile.Replace(@"\bsql\" + fixconfig.BSqlFilePrefix,@"\bprocess\" + fixconfig.BProcessFilePrefix).Replace(".sqc",".c");
					funcConfig.BProIncFile = funcConfig.BProFile.Replace(@"\bprocess\",@"\include\").Replace(".c",".h");
				}
			}
			/*if(staticInfo.CurCodingType != funcConfig.CodingType)
			{
				staticInfo.CurCodingType = funcConfig.CodingType;
				funcConfig.FuncComment = "";
			}*/
			propertyGrid_Func.Refresh();
		}
		
		void Btn_ReReadClick(object sender, EventArgs e)
		{
			try 
			{
				staticInfo.Tables.Clear();
				staticInfo.Structs.Clear();
				if (fixconfig.CreateTableFilePath != "") 
				{
					StreamReader sr = new StreamReader(fixconfig.CreateTableFilePath, Encoding.GetEncoding("GB2312"));
					SqlAnalyse sa = new SqlAnalyse(sr.ReadToEnd());
					sa.Analyse();
					staticInfo.Tables = sa.Tables;
					sr.Close();
				}
				
				if (fixconfig.TbStructFilePath != "") 
				{
					StreamReader sr2 = new StreamReader(fixconfig.TbStructFilePath, Encoding.GetEncoding("GB2312"));
					StructAnalyse sta = new StructAnalyse(sr2.ReadToEnd());
					sta.Analyse();
					staticInfo.Structs = sta.Structs;
					sr2.Close();
				}				
				if (fixconfig.BuStructFilePath != "") 
				{
					StreamReader sr3 = new StreamReader(fixconfig.BuStructFilePath, Encoding.GetEncoding("GB2312"));
					StructAnalyse sta = new StructAnalyse(sr3.ReadToEnd());
					sta.Analyse();
					foreach (string strKey in sta.Structs.Keys) 
					{
						if (!staticInfo.Structs.ContainsKey(strKey))
						{
							staticInfo.Structs.Add(strKey, sta.Structs[strKey]);
						}
					}
					sr3.Close();
				}
				
				AddTablesInRichEditBox();
				AddStructsInRichEditBox();
			} 
			catch (Exception ex) 
			{
				
				MessageBox.Show(ex.Message);
			}
			
		}
		
		void AddTablesInRichEditBox()
		{
			try 
			{
				if (richTextBox_file.Text == "") 
				{
					return;
				}
				SqlAnalyse sa = new SqlAnalyse(richTextBox_file.Text);
				sa.Analyse();
				foreach (string str in sa.Tables.Keys) 
				{
					if (staticInfo.Tables.ContainsKey(str))
					{
						staticInfo.Tables[str] = sa.Tables[str];
					} 
					else 
					{
						staticInfo.Tables.Add(str, sa.Tables[str]);
					}
				}
			} 
			catch (Exception ex) 
			{
				
				MessageBox.Show(ex.Message);
			}
		}
		
		void AddStructsInRichEditBox()
		{
			try 
			{
				if (richTextBox_struct.Text == "") 
				{
					return;
				}
				StructAnalyse sa = new StructAnalyse(richTextBox_struct.Text);
				sa.Analyse();
				foreach (string str in sa.Structs.Keys) 
				{
					if (staticInfo.Structs.ContainsKey(str))
					{
						staticInfo.Structs[str] = sa.Structs[str];
					} 
					else 
					{
						staticInfo.Structs.Add(str, sa.Structs[str]);
					}
				}
			} 
			catch (Exception ex) 
			{
				
				MessageBox.Show(ex.Message);
			}
		}
		
		void RichTextBox_fileTextChanged(object sender, EventArgs e)
		{
			AddTablesInRichEditBox();
		}
		
		void RichTextBox_structTextChanged(object sender, EventArgs e)
		{
			AddStructsInRichEditBox();
		}
		
		void Btn_BatchClick(object sender, EventArgs e)
		{
			if (comboBox_Batch.SelectedIndex == 0) 
			{
				MessageBox.Show("请选择编码类型");
				return;
			}
			try {
				richTextBox_BatchOut.Text = luaHelper.DoBatchCoding(comboBox_Batch.Text);
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
		
		void Btn_ToSqlClick(object sender, EventArgs e)
		{
			TableStructure2Sql ts2sql = new TableStructure2Sql(textBox_tableStructure.Text);
			textBox_sql.Text = ts2sql.GetSqlString();
		}
		
		void Btn_ClearAllTextClick(object sender, EventArgs e)
		{
			textBox_tableStructure.Text = "";
			textBox_sql.Text = "";
		}
		
		void Btn_CopySqlResultClick(object sender, EventArgs e)
		{
			textBox_sql.SelectAll();
			textBox_sql.Copy();
		}
		
		
		void Btn_CopyBatchClick(object sender, EventArgs e)
		{
			richTextBox_BatchOut.SelectAll();
			richTextBox_BatchOut.Copy();
		}
		
		void Btn_compClick(object sender, EventArgs e)
		{
			RelationAnalyse ra = new RelationAnalyse(textBox_Src1.Text, textBox_Src2.Text);
			if(ra.Analyse())
			{
				richTextBox_CompRes.Text = ra.GetResult();
			}
			else
			{
				richTextBox_CompRes.Text = "分析失败";
			}
		}
		
		void Btn_RelAnaClearAllClick(object sender, EventArgs e)
		{
			textBox_Src1.Text = "";
			textBox_Src2.Text = "";
			richTextBox_CompRes.Text = "";
		}
		
		void BeginWaitCursor()
		{
			this.Cursor = Cursors.WaitCursor;
		}
		
		void EndWaitCursor()
		{
			this.Cursor = Cursors.Default;
		}
		
		List<string> GetAllTables()
		{
			List<string> tables = new List<string>(staticInfo.Tables.Keys);
			return tables;
		}
		
		void MenuFileExitClick(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
