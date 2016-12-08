/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-6-21
 * Time: 22:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using LuaInterface;
using System.Windows.Forms;

namespace AutoCoding
{
	public enum VaribleType{Struct, Input, Output};
	/// <summary>
	/// Description of Coding.
	/// </summary>
	public class StructCoding
	{
		public FixConfig _FixConfig;
		public CodingConfig _CodingConfig;
		
		protected Table _Table;
		string strStruct = "";
		
		public StructCoding(FixConfig fixConfig, CodingConfig codingConfig)
		{
			_FixConfig = fixConfig;
			_CodingConfig = codingConfig;
			_Table = MainForm.staticInfo.Tables[_CodingConfig.TableName];
		}
		
		public string GetDefStructName()
		{
			return "ST_" + GetTableName().ToUpper() + _FixConfig.DefStructSuffix;
		}
		
		public void AddString(string str)
		{
			strStruct += str;
		}
		
		public string GetTbName()
		{
			return _Table.TableName;
		}
		
		public string GetStructCoding()
		{
			string strStructName = _CodingConfig.StructName;
			if (strStructName == "") 
			{
				strStructName = GetDefStructName();
			}
			
			Lua lua = new Lua();
			lua.RegisterFunction("AddString", this, this.GetType().GetMethod("AddString"));
			lua.RegisterFunction("GetTbName", this, this.GetType().GetMethod("GetTbName"));
			//lua.DoString("GetTbName()");
			lua.DoFile(Application.StartupPath + "\\scripts\\struct.lua");
			strStruct += lua["code"];
			
			strStruct += GetStructComment(strStructName);
			strStruct += "typedef struct " + strStructName.ToLower() + "\r\n";
			strStruct += "{\r\n";
			if (_CodingConfig.UsedCols == "" ) 
			{
				foreach (string strCol in _Table.PrimaryKeys) 
				{
					Column col = _Table.Columns[strCol];
					strStruct += "    " + GetEsqlDefination(col, VaribleType.Struct) + "    /**< (主键)" + col.Comment + " */\r\n";
				}
				foreach (Column col in _Table.Columns.Values) 
				{
					if (!_Table.PrimaryKeys.Contains(col._ColName))
					{
						strStruct += "    " + GetEsqlDefination(col, VaribleType.Struct) + "    /**< " + col.Comment + " */\r\n";
					}
				}
			}
			else
			{
				foreach (Column col in _Table.Columns.Values) 
				{
					if (_CodingConfig.UsedCols.IndexOf(col._ColName) != -1)
					{
						strStruct += "    " + GetEsqlDefination(col, VaribleType.Struct) + "    /**< " + col.Comment + " */\r\n";
					}
				}
			}
			strStruct += "}" + strStructName + ";\r\n";
			return strStruct;
		}
		
		public string GetStructString(List<string> cols)
		{
			string strStructName = GetDefStructName();
			string strStruct = "";
			strStruct += GetStructComment(strStructName);
			strStruct += "typedef struct " + strStructName.ToLower() + "\r\n";
			strStruct += "{\r\n";
			foreach (string strcol in cols) 
			{
				Column col = _Table.Columns[strcol];
				strStruct += "    " + GetEsqlDefination(col, VaribleType.Struct) + "    /**< " + col.Comment + " */\r\n";
			}
			strStruct += "}" + strStructName + ";\r\n";
			return strStruct;
		}
		
		public string GetStructComment(string strStructName)
		{
			string strComment = "";
			strComment += "/**\r\n";
			strComment += " * @brief " + _Table.Comment + "\r\n";
			strComment += " * @details " + "\r\n";
			strComment += " * - 创 建 日 期 : " + DateTime.Now.ToShortDateString() + "\r\n";
			strComment += " * - 程 序 作 者 : " + _FixConfig.Author + "(by AutoCoding)\r\n";
			strComment += " * - 对应数据库表: " + _Table.TableName + "\r\n";
			strComment += " * - 数据库表注释: " + _Table.Comment + "\r\n";
			strComment += " * - 说       明: " + _CodingConfig.StructComment + "\r\n";
			strComment += " */\r\n";
			return strComment;
		}
		
		public string GetTableName()
		{
			int nIndex = _Table.TableName.IndexOf('.');
			return _Table.TableName.Substring(nIndex + 1);
		}
		
		public string GetEsqlVarName(Column col, VaribleType type)
		{
			string strVarName = "";
			string strVarType = GetEsqlVarType(col);
			strVarName += GetEsqlVarNamePrefix(col, type);
			strVarName += col._ColName.ToLower();
			return strVarName;
		}
		
		public string GetEsqlVarNamePrefix(Column col, VaribleType type)
		{
			string strPrefix = "";
			switch (type) 
			{
				case VaribleType.Struct:					
					break;
				case VaribleType.Input:
					strPrefix = "mi_";
					break;
				case VaribleType.Output:
					strPrefix = "mo_";
					break;
				default:
					throw new Exception("Invalid value for CodeType");
			}
			return strPrefix;
		}
		
		public string GetEsqlVarTypeFlag(Column col)
		{
			Dictionary<string, string> type_flags = new Dictionary<string, string>();
			string strType = GetEsqlVarType(col);
			type_flags.Add("double", "d");
			type_flags.Add("int", "i");
			type_flags.Add("short", "sh");
			type_flags.Add("char", "s");

			if (type_flags.ContainsKey(strType))
			{
				return type_flags[strType];
			}
			return "";
		}
		
		public string GetEsqlVarType(Column col)
		{
			if (string.Compare(col._ColType, "char", true) == 0
			 || string.Compare(col._ColType, "varchar", true) == 0
			 || string.Compare(col._ColType, "time", true) == 0
			 || string.Compare(col._ColType, "date", true) == 0
			 || string.Compare(col._ColType, "timestamp", true) == 0
				)
			{
				return "char";
			}
			if (string.Compare(col._ColType, "double", true) == 0
			 || string.Compare(col._ColType, "decimal", true) == 0
				)
			{
				return "double";
			}
			if (string.Compare(col._ColType, "INTEGER", true) == 0
			    || string.Compare(col._ColType, "INT", true) == 0)
			{
				return "int";
			}
			if (string.Compare(col._ColType, "smallint", true) == 0) 
			{
				return "short";
			}
			return "";
		}
		
		public int GetEsqlVarSize(Column col)
		{
			if (string.Compare(col._ColType, "char", true) == 0
			 || string.Compare(col._ColType, "varchar", true) == 0
				)
			{
				return col._ColSize1 + 1;
			}
			if (string.Compare(col._ColType, "time", true) == 0
			 || string.Compare(col._ColType, "date", true) == 0
				)
			{
				return 9;
			}
			if (string.Compare(col._ColType, "timestamp", true) == 0)
			{
				return 27;
			}
			return 0;
		}
		
		public string GetColNameWithBlanks(string strColName)
		{
			int nTotalSize = 20;
			string strTabs = "";
			int nTabCount = (nTotalSize - strColName.Length) / 4;
			if (nTotalSize - nTabCount * 4 - strColName.Length == 0) 
			{
				nTabCount -= 1;
			} 
			for (int i = 0; i < nTabCount; i++) 
			{
				strTabs += "    ";
			}
			return strColName + strTabs;
		}
		
		public string FormatVarDefination(string strVarType, string strVarName, string strVarSize)
		{
			int nTypeSize = 10;
			int nNameSize = 30;
			int nVarSize = 6;
			string strVarDefination = "";
			string strBlank = "";
			for (int i = 0; i < 100; i++) 
			{
				strBlank += " ";
			}
			if (nTypeSize <= strVarType.Length) 
			{
				strVarDefination += strVarType + " ";
			} 
			else 
			{
				strVarDefination += strVarType + strBlank.Substring(0, nTypeSize - strVarType.Length);
			}
			
			if (nNameSize <= strVarName.Length) 
			{
				strVarDefination += strVarName + " ";
			} 
			else 
			{
				strVarDefination += strVarName + strBlank.Substring(0, nNameSize - strVarName.Length);
			}
			
			if (nVarSize <= strVarSize.Length) 
			{
				strVarDefination += strVarSize + " ";
			} 
			else 
			{
				strVarDefination += strVarSize + strBlank.Substring(0, nVarSize - strVarSize.Length);
			}
			
			return strVarDefination;
		}
		
		public string GetEsqlDefination(Column col, VaribleType type)
		{
			string strVarDefination = "";
			string strVarName = GetEsqlVarNamePrefix(col, type) + GetEsqlVarTypeFlag(col) + col._ColName.ToLower();
			string strVarType = GetEsqlVarType(col);
			string strVarSize = "";
			if (strVarType == "char") 
			{
				strVarSize += "[" + GetEsqlVarSize(col) + "]";
			}
			strVarDefination += FormatVarDefination(strVarType, strVarName, strVarSize) + ";";
			return strVarDefination;
		}
	}
}
