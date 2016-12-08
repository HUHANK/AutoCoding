/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-6-18
 * Time: 23:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace AutoCoding
{
	/// <summary>
	/// Description of SqlAnalyse.
	/// </summary>
	public class SqlAnalyse
	{
		#region Member Varibles
		protected string m_strSql;
		protected Dictionary<string, Table> _Tables;
		protected string m_strLastError;
		#endregion
		
		#region Constructors
		public SqlAnalyse() : this("")
		{
		}
		public SqlAnalyse(string strSqlText)
		{
			m_strSql = strSqlText.ToUpper();
			m_strLastError = "";
			_Tables = new Dictionary<string, Table>();
		}
		#endregion
		
		#region Attributes
		public string SqlText
		{
			get
			{
				return m_strSql;
			}
			set
			{
				m_strSql = value;
			}
		}
		public string LastError
		{
			get
			{
				return m_strLastError;
			}
		}
		public Dictionary<string, Table> Tables
		{
			get
			{
				return _Tables;
			}
		}
		#endregion
		
		#region FileAnalyse
		public bool Analyse()
		{
			try
			{
				Regex reg = new Regex(@"(-- 内存表:(?<SSMT>.*?)$.*?)?create\s+table\s+(?<TableName>.*?)\s*\((?<TableBody>([^()]*?\([^()]*?\)[^()]*?)*?)\)", 
				                      RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
				MatchCollection mc = reg.Matches(m_strSql);
				string strTableName;
				foreach (Match m in mc) 
				{
					strTableName = m.Groups["TableName"].Value;
					if (strTableName.ToUpper().IndexOf(" LIKE ") >= 0) {
						continue;
					}
					if (!_Tables.ContainsKey(strTableName))
					{
						Table tb = new Table(strTableName, m.Groups["TableBody"].Value, m.Groups["SSMT"].Value);
						_Tables.Add(strTableName, tb);
					}
				}
				Regex regCommentTb = new Regex(@"comment\s+on\s+table\s+(?<TableName>.*?)\s+is\s+\'(?<TableComment>.*?)\'\s*;", RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
				MatchCollection mcCommentTb = regCommentTb.Matches(m_strSql);
				string strComment;
				foreach (Match mCommentTb in mcCommentTb) 
				{
					strTableName = mCommentTb.Groups["TableName"].Value;
					strComment = mCommentTb.Groups["TableComment"].Value;
					if (_Tables.ContainsKey(strTableName))
					{
						_Tables[strTableName].Comment = strComment;
					}
				}
				Regex regCommentCol = new Regex(@"comment\s+on\s+column\s+(?<TableName>(\w+\.)?\w+)\.(?<ColName>\w+)\s+is\s+\'(?<ColComment>.*?)\'\s*;", RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
				MatchCollection mcCommentCol = regCommentCol.Matches(m_strSql);
				string strColName;
				foreach (Match mCommentCol in mcCommentCol) 
				{
					strTableName = mCommentCol.Groups["TableName"].Value;
					strColName = mCommentCol.Groups["ColName"].Value;
					strComment = mCommentCol.Groups["ColComment"].Value;
					if (_Tables.ContainsKey(strTableName) && _Tables[strTableName].Columns.ContainsKey(strColName))
					{
						_Tables[strTableName].Columns[strColName].Comment = strComment;
					}
				}
			}
			catch(Exception ex)
			{
				m_strLastError = ex.Message;
				return false;
			}
			return true;
		}
		#endregion
	}
}
