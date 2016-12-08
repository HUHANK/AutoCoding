/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-6-19
 * Time: 20:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text.RegularExpressions;

namespace AutoCoding
{
	/// <summary>
	/// Description of Column.
	/// </summary>
	public class Column : IEquatable<Column>
	{
		#region Member Varibles
		/// <summary>
		/// 列名
		/// </summary>
		public string _ColName;
		/// <summary>
		/// 列类型
		/// </summary>
		public string _ColType;
		/// <summary>
		/// 对CHAR类型来说，该变量是CHAR的长度
		/// 对DECIMAL类型来说，该变量是有效位数
		/// 对其他类型来说，该变量无意义
		/// </summary>
		public int _ColSize1;
		/// <summary>
		/// 对DECIMAL类型来说，该变量是小数位数
		/// 对其他类型来说，该变量无意义
		/// </summary>
		public int _ColSize2;
		/// <summary>
		/// 是否非空
		/// </summary>
		public bool _NotNull;
		/// <summary>
		/// 默认值
		/// </summary>
		public string _DefValue;
		/// <summary>
		/// 注释
		/// </summary>
		public string _Comment;
		#endregion
		
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
		
		#region Constructors
		public Column(string strColName, string strColType, int nColSize1, int nColSize2, bool bNotNull, string strDefValue, string strComment)
		{
			_ColName = strColName;
			_ColType = strColType;
			_ColSize1 = nColSize1;
			_ColSize2 = nColSize2;
			_NotNull = bNotNull;
			_DefValue = strDefValue;
			_Comment = strComment;
		}
		public Column(string strColName, string strColType, bool bNotNull, string strDefValue, string strComment)
			: this(strColName, strColType, 0, 0, bNotNull, strDefValue, strComment)
		{			
		}
		public Column(string strSql)
		{
			Regex regName = new Regex(@"\s*" + "\"?" + @"(?<ColName>\w+)" + "\"?" + @"\s+(?<OtherInfo>.*)", RegexOptions.Multiline | RegexOptions.Singleline);
			Match mName = regName.Match(strSql);
			if (mName.Success) 
			{
				_ColName = mName.Groups["ColName"].Value;
				string strOtherInfo = mName.Groups["OtherInfo"].Value;
				Regex regType = new Regex(@"(?<ColType>\w+)(\((?<ColSize1>\d+)(\s*,\s*(?<ColSize2>\d+)\s*)?\))?\s*(?<OtherInfo>.*)", RegexOptions.Multiline|RegexOptions.Singleline);
				Match mType = regType.Match(strOtherInfo);
				if (mType.Success) 
				{
					_ColType = mType.Groups["ColType"].Value;
					string strTmp;
					strTmp = mType.Groups["ColSize1"].Value;
					if (strTmp != "")
					{
						_ColSize1 = Convert.ToInt32(strTmp);
					} 
					else 
					{
						if (_ColType.Trim().ToUpper() == "TIMESTAMP") {
							_ColSize1 = 26;
						} else {
							_ColSize1 = 0;
						}
						
					}
					strTmp = mType.Groups["ColSize2"].Value;
					if (strTmp != "")
					{
						_ColSize2 = Convert.ToInt32(strTmp);
					} 
					else 
					{
						_ColSize2 = 0;
					}
					strOtherInfo = mType.Groups["OtherInfo"].Value;
					Regex regNotNull = new Regex(@"not\s+null\s+", RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
					if (regNotNull.Match(strOtherInfo).Success)
					{
						_NotNull = true;
					}
					else
					{
						_NotNull = false;
					}
					Regex regDefaultValue = new Regex(@"with\s+default\s+\'?(?<DefaultValue>.*?)\'?", RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
					Match mDefValue = regDefaultValue.Match(strOtherInfo);
					if (mDefValue.Success) 
					{
						_DefValue = mDefValue.Groups["DefaultValue"].Value;
					}
				}
			}
		}
		#endregion
		
		#region Equals and GetHashCode implementation
		// The code in this region is useful if you want to use this structure in collections.
		// If you don't need it, you can just remove the region and the ": IEquatable<Column>" declaration.
		
		public override bool Equals(object obj)
		{
			if (obj is Column)
				return Equals((Column)obj); // use Equals method below
			else
				return false;
		}
		
		public bool Equals(Column other)
		{
			// add comparisions for all members here
			return this._ColName == other._ColName;
		}
		
		public override int GetHashCode()
		{
			// combine the hash codes of all members here (e.g. with XOR operator ^)
			return _ColName.GetHashCode();
		}
		
		public static bool operator ==(Column left, Column right)
		{
			return left.Equals(right);
		}
		
		public static bool operator !=(Column left, Column right)
		{
			return !left.Equals(right);
		}
		#endregion
	}
}
