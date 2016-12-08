/*
 * Created by SharpDevelop.
 * User: dongdong.shen
 * Date: 2012/6/27
 * Time: 19:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using LuaInterface;
using System.Reflection;

namespace AutoCoding
{
	/// <summary>
	/// Description of LuaHelper.
	/// </summary>
	public class LuaHelper
	{
		protected Table _Table;
		public FixConfig _FixConfig;
		public CodingConfig _CodingConfig;
		protected Lua lua;
		protected Dictionary<string, Table> _Tables;
		protected string _scriptPath;
		protected LuaTable _batchCodingConfig;
		protected LuaTable _autoCodingConfig;
		
		public string ScriptPath {
			get { return _scriptPath; }
			set { _scriptPath = value; }
		}
		
		public LuaHelper()
		{
			_Tables = MainForm.staticInfo.Tables;
			_scriptPath = "";
			lua = new Lua();
			lua.RegisterFunction("CSGetConfig", this, this.GetType().GetMethod("GetConfig"));
			lua.RegisterFunction("CSGetTableName", this, this.GetType().GetMethod("GetTableName"));
			lua.RegisterFunction("CSGetTableFullName", this, this.GetType().GetMethod("GetTableFullName"));
			lua.RegisterFunction("CSGetTableComment", this, this.GetType().GetMethod("GetTableComment"));
			lua.RegisterFunction("CSGetUsedCols", this, this.GetType().GetMethod("GetUsedCols"));
			lua.RegisterFunction("CSGetAllCols", this, this.GetType().GetMethod("GetAllCols"));
			lua.RegisterFunction("CSGetKeyCols", this, this.GetType().GetMethod("GetKeyCols"));
			lua.RegisterFunction("CSGetNonKeyCols", this, this.GetType().GetMethod("GetNonKeyCols"));
			lua.RegisterFunction("CSGetColType", this, this.GetType().GetMethod("GetColType"));
			lua.RegisterFunction("CSGetColComment", this, this.GetType().GetMethod("GetColComment"));
			lua.RegisterFunction("CSGetColSize1", this, this.GetType().GetMethod("GetColSize1"));
			lua.RegisterFunction("CSGetColSize2", this, this.GetType().GetMethod("GetColSize2"));
			lua.RegisterFunction("CSFormatVarName", this, this.GetType().GetMethod("FormatVarName"));
			lua.RegisterFunction("CSGetAllTables", this, this.GetType().GetMethod("GetAllTables"));
			lua.RegisterFunction("CSSetCurTable", this, this.GetType().GetMethod("SetCurTable"));
			lua.RegisterFunction("CSGetCurTable", this, this.GetType().GetMethod("GetCurTable"));
			lua.RegisterFunction("CSDoAutoCoding", this, this.GetType().GetMethod("DoAutoCoding"));
			lua.RegisterFunction("CSDoBatchCoding", this, this.GetType().GetMethod("DoBatchCoding"));
		}
		
		public string GetTableFullName()
		{
			return _Table.TableName;
		}
		
		public string GetTableComment()
		{
			return _Table.Comment;
		}
		
		public string GetConfig(string configName)
		{
			PropertyInfo pi = _FixConfig.GetType().GetProperty(configName);
			if(pi != null)
			{
				return (string)pi.GetValue(_FixConfig, null);
			}
			return "";
		}
		
		public string GetUsedCols()
		{
			return _CodingConfig.UsedCols;
		}
		
		public string GetAllCols()
		{
			string str = "";
			foreach (string key in _Table.Columns.Keys) {
				if (str == "") {
					str += key;
				} else {
					str += "," + key;
				}
			}
			return str;
		}
		
		public string GetColType(string col)
		{
			if (_Table.Columns.ContainsKey(col)) {
				return _Table.Columns[col]._ColType;
			} else {
				return "";
			}
		}
		
		public string GetColComment(string col)
		{
			if (_Table.Columns.ContainsKey(col)) {
				return (_Table.Columns[col]._Comment==null) ? "" : _Table.Columns[col]._Comment;
			} else {
				return "";
			}
		}
		
		public int GetColSize1(string col)
		{
			if (_Table.Columns.ContainsKey(col)) {
				return _Table.Columns[col]._ColSize1;
			} else {
				return 0;
			}
		}
		
		public int GetColSize2(string col)
		{
			if (_Table.Columns.ContainsKey(col)) {
				return _Table.Columns[col]._ColSize2;
			} else {
				return 0;
			}
		}
		
		public string GetKeyCols()
		{
			string cols = "";
			foreach (string col in _Table.PrimaryKeys) {
				if (cols == "") {
					cols += col;
				} else {
					cols += "," + col;
				}
			}
			return cols;
		}
		
		public string GetNonKeyCols()
		{
			string cols = "";
			foreach (string col in _Table.Columns.Keys) {
				if (!_Table.PrimaryKeys.Contains(col)) {
					if (cols == "") {
						cols += col;
					} else {
						cols += "," + col;
					}
				}
			}
			return cols;
		}
		
		public string FormatVarName(string strVar)
		{
			string strRet = strVar;
			string ch;
			bool bChangeNext = false;
			strRet = strRet.ToLower();
			for (int i = 0; i < strRet.Length; i++) 
			{
				if (bChangeNext == true || i == 0) 
				{
					ch = strRet[i].ToString();
					ch = ch.ToUpper();
					strRet = strRet.Substring(0, i) + ch.ToString() + strRet.Substring(i + 1);
					bChangeNext = false;
				}
				if (strRet[i] == '_') 
				{
					bChangeNext = true;
				}
			}
			strRet = strRet.Replace("_", "");
			return strRet;
		}
		
		public string GetTableName()
		{
			int nIndex = _Table.TableName.IndexOf('.');
			return _Table.TableName.Substring(nIndex + 1);
		}
		
		public string GetAllTables()
		{
			string strAll = "";
			foreach (string t in _Tables.Keys) {
				if (strAll != "") {
					strAll += ",";
				}
				strAll += t;
			}
			return strAll;
		}
		
		public void SetCurTable(string tableName)
		{
			if (!_Tables.ContainsKey(tableName)) {
				throw new Exception(tableName + "表不存在");
			}
			_Table = _Tables[tableName];
		}
		
		public string GetCurTable()
		{
			if (_Table == null) {
				return "";
			}
			return _Table.TableName;
		}
		
		public object[] DoFile(string fileName)
		{
			return lua.DoFile(_scriptPath + fileName.Trim());
		}
		
		public object[] DoString(string chunk)
		{
			return lua.DoString(chunk);
		}
		
		public object GetResult(string fullPath)
		{
			return lua[fullPath];
		}
		
		public void LoadConfig()
		{
			DoFile("config.lua");
			_autoCodingConfig = (LuaTable)lua["AutoCodingConfig"];
			_batchCodingConfig = (LuaTable)lua["BatchCodingConfig"];
		}
		
		public List<string> GetAutoCodingTypes()
		{
			if (_autoCodingConfig == null) {
				return null;
			}
			List<string> ret = new List<string>();
			foreach (object t in _autoCodingConfig.Keys) {
				ret.Add((string)t);
			}
			return ret;
		}
		
		public List<string> GetBatchCodingTypes()
		{
			if (_batchCodingConfig == null) {
				return null;
			}
			List<string> ret = new List<string>();
			foreach (object t in _batchCodingConfig.Keys) {
				ret.Add((string)t);
			}
			return ret;
		}
		
		public string DoAutoCoding(string strType, bool bAutoSave = true)
		{
			if (_autoCodingConfig == null) {
				return "";
			}
			string strFiles = (string)((LuaTable)_autoCodingConfig[strType])["files"];
			if (strFiles == null) {
				return "";
			}
			string[] files = strFiles.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string file in files) {
				DoFile(file);
			}
			string code = (string)lua["code"];
			string filepath = (string)lua["filepath"];
			if (bAutoSave && filepath != null && filepath != "") {
				WriteToFile(filepath, code);
			}
			return code;
		}
		
		public string DoBatchCoding(string strType, bool bAutoSave = true)
		{
			if (_batchCodingConfig == null) {
				return "";
			}
			string strFiles = (string)((LuaTable)_batchCodingConfig[strType])["files"];
			if (strFiles == null) {
				return "";
			}
			string[] files = strFiles.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string file in files) {
				DoFile(file);
			}
			string code = (string)lua["code"];
			string filepath = (string)lua["filepath"];
			if (bAutoSave && filepath != null && filepath != "") {
				WriteToFile(filepath, code);
			}
			return code;
		}
		
		public void WriteToFile(string strFileName, string strText)
		{
			StreamWriter sw = new StreamWriter(strFileName, false, Encoding.GetEncoding("GB2312"));
			sw.Write(strText);
			sw.Close();
		}
	}
}
