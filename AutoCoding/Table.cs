/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-6-19
 * Time: 20:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;

namespace AutoCoding
{
	public struct Range
	{
		public Range(int nStart, int nEnd)
		{
			Start = nStart;
			End = nEnd;
		}
		public int Start;
		public int End;
	}
	/// <summary>
	/// Description of Table.
	/// </summary>
	public class Table : IEquatable<Table>
	{
		#region Member Varibles
		/// <summary>
		/// 表名
		/// </summary>
		protected string _TableName;
		/// <summary>
		/// 所有列
		/// </summary>
		protected Dictionary<string, Column> _Columns;
		/// <summary>
		/// 主键
		/// </summary>
		protected List<string> _PrimaryKeys;
		/// <summary>
		/// 表注释
		/// </summary>
		protected string _Comment;
		/// <summary>
		/// 创建表语句
		/// </summary>
		protected string _TableBody;
		
		protected string _SSMTComment;
		#endregion
		
		#region Attribute
		public string TableName
		{
			get
			{
				return _TableName;
			}
		}
		public Dictionary<string, Column> Columns
		{
			get
			{
				return _Columns;
			}
		}
		public List<string> PrimaryKeys
		{
			get
			{
				return _PrimaryKeys;
			}
		}
		public string Comment
		{
			get
			{
				return _Comment;
			}
			set
			{
				_Comment = value;
			}
		}
		
		public string SSMTComment
		{
			get
			{
				return _SSMTComment;
			}
			set
			{
				_SSMTComment = value.Trim();
			}
		}
		#endregion
		
		#region Constructors
		public Table()
		{
			_TableName = "";
			_TableBody = "";
			_Columns = new Dictionary<string, Column>();
			_PrimaryKeys = new List<string>();
			_SSMTComment = "";
		}
		public Table(string strTableName, string strTableBody, string SSMTComment)
		{
			_TableName = strTableName;
			_TableBody = strTableBody;
			_Columns = new Dictionary<string, Column>();
			_PrimaryKeys = new List<string>();
			AnalyseTableBody();
			_SSMTComment = SSMTComment;
		}
		#endregion
		
		#region Methods
		protected void AnalyseTableBody()
		{
			// step1. 分析所有列
			List<string> colStrings = GetColumnStrings();
			foreach(string colStr in colStrings)
			{
				Column col = new Column(colStr);
				if (!_Columns.ContainsKey(col._ColName))
				{
					_Columns.Add(col._ColName.ToUpper(), col);
				}
			}
			
			// step2. 分析所有主键
			AnalysePrimaryKeys();
		}
		
		protected bool IsPrimaryKey(string str)
		{
			Regex regPK = new Regex(@"primary\s+key\s*", RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
			Match mPK = regPK.Match(str);
			return mPK.Success;
		}
		
		protected List<string> GetColumnStrings()
		{
			List<string> colStrings = new List<string>();
			
			// 查找有(*,*)的区域,这些区域中的区域不作为分隔列条件
			List<Range> ranges = new List<Range>();
			Regex regRange = new Regex(@"(?<Range>\([^,\)]*?,.*?\))", RegexOptions.Multiline | RegexOptions.Singleline);
			MatchCollection msRange = regRange.Matches(_TableBody);
			foreach(Match mRange in msRange)
			{
				ranges.Add(new Range(mRange.Groups["Range"].Index, mRange.Groups["Range"].Index + mRange.Groups["Range"].Length));
			}
			
			// 分隔各列
			int nStart = 0;
			int nPos = _TableBody.IndexOf(',', nStart);
			string strFound;
			while(nPos != -1)
			{
				strFound = _TableBody.Substring(nStart, nPos - nStart);
				if (!IsPosInRanges(nPos, ranges))
				{
					if (!IsPrimaryKey(strFound))
					{
						colStrings.Add(strFound.Trim().Replace("\"", ""));
					}
					nStart = nPos + 1;
					nPos = _TableBody.IndexOf(',', nStart);
				}
				else
				{
					nPos = _TableBody.IndexOf(',', nPos + 1);
				}
			}
			// 检查最后一个逗号到最后是否是一列的定义
			if(nStart < _TableBody.Length)
			{
				strFound = _TableBody.Substring(nStart).Trim();
				if(strFound != "" && !IsPrimaryKey(strFound))	// 主要适用于没有主键的情况
				{
					colStrings.Add(strFound.Replace("\"", ""));
				}
			}
			
			return colStrings;
		}
		
		protected bool IsPosInRanges(int nPos, List<Range> ranges)
		{
			foreach(Range r in ranges)
			{
				if (nPos > r.Start && nPos < r.End) 
				{
					return true;
				}
			}
			return false;
		}
		
		protected void AnalysePrimaryKeys()
		{
			Regex regPKs = new Regex(@"primary\s+key\s*\((?<PrimaryKey>.*?)\)", RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
			Match m = regPKs.Match(_TableBody);
			if (m.Success) 
			{
				string strPrimaryKey = m.Groups["PrimaryKey"].Value;
				string[] splitRes = strPrimaryKey.Split(',');
				foreach (string str in splitRes) 
				{
					_PrimaryKeys.Add(str.Trim().ToUpper().Replace("\"", ""));
				}
			}
		}
		#endregion
		
		#region Equals and GetHashCode implementation
		// The code in this region is useful if you want to use this structure in collections.
		// If you don't need it, you can just remove the region and the ": IEquatable<Table>" declaration.
		
		public override bool Equals(object obj)
		{
			if (obj is Table)
				return Equals((Table)obj); // use Equals method below
			else
				return false;
		}
		
		public bool Equals(Table other)
		{
			// add comparisions for all members here
			return this._TableName == other._TableName;
		}
		
		public override int GetHashCode()
		{
			// combine the hash codes of all members here (e.g. with XOR operator ^)
			return _TableName.GetHashCode();
		}
		
		public static bool operator ==(Table left, Table right)
		{
			return left.Equals(right);
		}
		
		public static bool operator !=(Table left, Table right)
		{
			return !left.Equals(right);
		}
		#endregion
	}
}
