/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-6-29
 * Time: 14:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AutoCoding
{
	public struct ST_COL
	{
		public ST_COL(string sColName, string sColType, string sDefValue, string sColComment, string sOtherInfo)
		{
			strColName = sColName;
			strColType = sColType;
			strDefValue = sDefValue;
			strColComment = sColComment;
			strOtherInfo = sOtherInfo;
		}
		public string strColName;
		public string strColType;
		public string strDefValue;
		public string strColComment;
		public string strOtherInfo;
	}
	
	public struct ST_TABLE
	{
		public string strTableName;
		public string strTableComment;
		public string strObjectNo;
		public string strSSMT;
		public string strTableSpace;
		public string strInstruction;
		public string strIndexTableSapce;
		public string strDataSource;
		public string strPrimaryKey;
		public List<ST_COL> colList;
	}
	/// <summary>
	/// Description of TableStructure2Sql.
	/// </summary>
	public class TableStructure2Sql
	{
		protected string _TableStructure;
		protected string _TableName;
		protected string _TableComment;
		protected string _ObjectNo;
		protected string _SSMT;
		protected string _TableSpace;
		protected string _Instruction;
		protected string _IndexTableSpace;
		protected string _DataSource;
		protected string _Columns;
		protected string _PrimaryKey;
		protected string _IndexInfo;
		protected List<ST_COL> _ColList;
		protected List<ST_TABLE> _TableList;
		protected List<string> _IndexList;
		public TableStructure2Sql()
		{
			_TableStructure = "";
			_ColList = new List<ST_COL>();
			_TableList = new List<ST_TABLE>();
			_IndexList = new List<string>();
		}
		public TableStructure2Sql(string strTableStructure)
		{
			_TableStructure = strTableStructure;
			_ColList = new List<ST_COL>();
			_TableList = new List<ST_TABLE>();
			_IndexList = new List<string>();
		}
		
		public string GetSqlString()
		{
			string strSql = "";
			Analyse();
			if (_TableName == null || _TableName == "") 
			{
				return "";
			}
			strSql += GetComment();
			strSql += GetCreateTableString();
			strSql += "in " + _TableSpace + "\r\n";
			strSql += "index in " + _IndexTableSpace + ";\r\n";
			strSql += GetCommentSqlString();
			strSql += "\r\n";
			//strSql += GetCreateIndexString();
			return strSql;
		}
		
		protected string GetComment()
		{
			string strComment = "";
			strComment += "--==============================================================\r\n"
						+ "-- 表  名: " + _TableName + "\r\n"
						+ "-- 表说明: " + _TableComment + "\r\n"
						+ "-- 对象号: " + _ObjectNo + "\r\n"
						+ "-- 内存表: " + _SSMT + "\r\n"
						+ "-- 使用说明: " + _Instruction + "\r\n"
						+ "-- 数据来源: " + _DataSource + "\r\n"
						+ "--==============================================================\r\n";
			return strComment;
		}
		
		protected string GetCreateTableString()
		{
			string strCreateTable = "";			
			strCreateTable += "CREATE TABLE " + _TableName.ToUpper() + "\r\n"
				+ "(\r\n";
			string strDefValue = "";
			foreach (ST_COL col in _ColList) 
			{
				strDefValue = GetDefValue(col.strDefValue);
				strCreateTable += "    " + FormatString(col.strColName.ToUpper(), 25) 
					+ FormatString(col.strColType.ToUpper(), 23)
					+ "NOT NULL    WITH DEFAULT " + strDefValue + ",\r\n";
			}
			strCreateTable += "    CONSTRAINT P_Key_1 PRIMARY KEY(" + _PrimaryKey  + ")\r\n";
			strCreateTable += ")\r\n";
			return strCreateTable;
		}
		
		protected string GetDefValue(string strDefValue)
		{
			string strRet = strDefValue;
			int nCount = 0;
			if (strRet.Contains("个0"))
			{
				strRet = strRet.Trim('\'');
				strRet = strRet.Replace("个0","");
				nCount = Convert.ToInt32(strRet);
				strRet = "'";
				for (int i = 0; i < nCount; i++) 
				{
					strRet += "0";
				}
				strRet += "'";
			}
			return strRet;
		}
		
		protected string GetCreateIndexString()
		{
			string strCreateIndex = "";
			foreach (string str in _IndexList) 
			{
				//strCreateIndex += 
			}
			return strCreateIndex;
		}
		
		protected string FormatString(string strToFormat, int nLength)
		{			
			string strRet = strToFormat;
			strRet = strRet.Trim('\n');
			if (nLength > strRet.Length) 
			{				
				for (int i = strRet.Length; i < nLength; i++) 
				{
					strRet += " ";
				}
				return strRet;
			} 
			else 
			{
				return strRet + " ";
			}
		}
		
		protected string GetCommentSqlString()
		{
			string strCommentSql = "";
			strCommentSql += "COMMENT ON TABLE " + _TableName.ToUpper() + " IS '" + _TableComment + "';\r\n";
			foreach (ST_COL col in _ColList) 
			{
				strCommentSql += "COMMENT ON COLUMN " + _TableName + "." + col.strColName + " IS '" + col.strColComment + "';\r\n";
			}
			return strCommentSql;
		}
		
		protected void AddTableToList(Match m)
		{
			
		}
		
		protected void Analyse()
		{
			try 
			{
				string strRegex = @"表名\s+(?<TableName>\S+)\s+表说明\s+(?<TableComment>\S*)\s+"
					+ @"对象号\s+(?<ObjectNo>\S*)\s+内存表\s+(?<SSMT>\S*)\s+"
					+ @"数据表空间\s+(?<TableSpace>\S+)\s+使用说明\s+(?<Instruction>\S*)\s+"
					+ @"索引表空间\s+(?<IndexTableSpace>\S+)\s+数据来源\s*(?<DataSource>\S*)\s*$"
					+ @".*?"
					+ @"字段名(?<WhatInfo>.*?)$"
					+ @"(?<Columns>.*?)"
					+ @"索引名称.*?$"
					+ @"\s+主键\s+(?<PrimaryKey>.*?)\s+$"
					+ @"(?<IndexInfo>.*)";
				Regex regFront = new Regex(strRegex, RegexOptions.Multiline | RegexOptions.Singleline);
				Match m = regFront.Match(_TableStructure);
				string strIndex = "";
				if (m.Success) 
				{
					_TableName = m.Groups["TableName"].Value;
					_TableComment = m.Groups["TableComment"].Value;
					_ObjectNo = m.Groups["ObjectNo"].Value;
					_SSMT = m.Groups["SSMT"].Value;
					_TableSpace = m.Groups["TableSpace"].Value;
					_Instruction = m.Groups["Instruction"].Value;
					_IndexTableSpace = m.Groups["IndexTableSpace"].Value;
					_DataSource = m.Groups["DataSource"].Value;
					_Columns = m.Groups["Columns"].Value;
					_PrimaryKey = m.Groups["PrimaryKey"].Value;
					_PrimaryKey = _PrimaryKey.Replace("、",",");
					_PrimaryKey = _PrimaryKey.Replace("，",",");
					strIndex = m.Groups["IndexInfo"].Value;
					if (_Columns != "") 
					{
						string[] sp = {"\r\n"};
						string[] cols = _Columns.Split(sp, StringSplitOptions.None);
						foreach (string str in cols) 
						{
							if (str != "") 
							{
								string[] items = str.Split(new string[]{"\t"}, StringSplitOptions.None);
								_ColList.Add(new ST_COL(items[0].Replace("\n",""),items[1],items[2],items[3],items[4]));
								/*if (str.IndexOf("CURRENT TIMESTAMP") != -1)
								{
									str.Replace("CURRENT TIMESTAMP","");
									Regex regCol = new Regex(@"(?<ColName>\S+)\s+(?<ColType>\S+)\s+(?<DefValue>\S*)\s+(?<ColComment>\S*)\s+(?<OtherInfo>\S*)$", RegexOptions.Multiline | RegexOptions.Singleline);
									Match mat = regCol.Match(str);
									if (mat.Success) 
									{
										_ColList.Add(new ST_COL(mat.Groups["ColName"].Value, mat.Groups["ColType"].Value,
										                        "CURRENT TIMESTAMP", mat.Groups["ColComment"].Value, 
										                        mat.Groups["OtherInfo"].Value));
									}
								} 
								else 
								{
									Regex regCol = new Regex(@"(?<ColName>\S+)\s+(?<ColType>\S+)\s+(?<DefValue>\S+)\s+(?<ColComment>\S*)\s+(?<OtherInfo>\S*)$", RegexOptions.Multiline | RegexOptions.Singleline);
									Match mat = regCol.Match(str);
									if (mat.Success) 
									{
										_ColList.Add(new ST_COL(mat.Groups["ColName"].Value, mat.Groups["ColType"].Value,
										                        mat.Groups["DefValue"].Value, mat.Groups["ColComment"].Value, 
										                        mat.Groups["OtherInfo"].Value));
									}									
								}*/
							}
						}
					}
					if(strIndex != "")
					{
						string[] indices = strIndex.Split(new string[]{"\r\n"}, StringSplitOptions.None);
						foreach (string str in indices) 
						{
							if (str != "") 
							{
								Regex regIndex = new Regex(@"\s+索引\s+(?<Index>.*?)\s+$");
								MatchCollection mc = regIndex.Matches(strIndex);
								foreach (Match mt in mc) 
								{
									_IndexList.Add(mt.Groups["Index"].Value);
								}
							}
						}
					}
				}
			} 
			catch (Exception) 
			{
				
				throw;
			}			
		}
	}
}

