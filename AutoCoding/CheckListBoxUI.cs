/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-7-18
 * Time: 14:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AutoCoding
{
	/// <summary>
	/// Description of CheckListBoxUI.
	/// </summary>
	public partial class CheckListBoxUI : UserControl
	{
		public CheckListBoxUI()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			checkedListBox.CheckOnClick = true;
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public void SetValue(string strCheckedCols)
		{
			checkedListBox.Items.Clear();
			if (MainForm.staticInfo.CurTableName == "" || MainForm.staticInfo.CurTableName == null) 
			{
				return;
			}
			if (MainForm.staticInfo.Tables.ContainsKey(MainForm.staticInfo.CurTableName))
			{				
				List<string> strs = new List<string>(strCheckedCols.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries));
				//string[] strs = strCheckedCols.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string str in MainForm.staticInfo.Tables[MainForm.staticInfo.CurTableName].Columns.Keys)
				{
					
					checkedListBox.Items.Add(str, strs.Contains(str));
				}
			}
		}
		
		public object GetValue()
		{
			string strRet = "";
			foreach (object objItem in checkedListBox.CheckedItems)
			{
				if (strRet.Length > 0)
				{
					strRet += ",";					
				}
				strRet += objItem.ToString();
			}
			return strRet;
		}
		
		void Btn_SelectAllClick(object sender, EventArgs e)
		{
			for (int i = 0; i < checkedListBox.Items.Count; i++) 
			{
				checkedListBox.SetItemChecked(i, true);
			}
		}
		
		void Btn_SelectReverseClick(object sender, EventArgs e)
		{
			for (int i = 0; i < checkedListBox.Items.Count; i++) 
			{
				checkedListBox.SetItemChecked(i, !checkedListBox.GetItemChecked(i));
			}
		}
		
		void Btn_KeyClick(object sender, EventArgs e)
		{
			if (MainForm.staticInfo.CurTableName == "" || MainForm.staticInfo.CurTableName == null) 
			{
				return;
			}
			if (MainForm.staticInfo.Tables.ContainsKey(MainForm.staticInfo.CurTableName))
			{				
				for (int i = 0; i < checkedListBox.Items.Count; i++) 
				{
					if (MainForm.staticInfo.Tables[MainForm.staticInfo.CurTableName].PrimaryKeys.Contains(checkedListBox.Items[i].ToString()))
					{
						checkedListBox.SetItemChecked(i, true);
					}
					else
					{
						checkedListBox.SetItemChecked(i, false);
					}
				}
			}
		}
	}
}
