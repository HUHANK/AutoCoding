/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-7-1
 * Time: 22:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutoCoding
{
	public class ST_VARIBLE
	{
		public ST_VARIBLE(string strVarType, string strVarName, string strVarComment)
		{
			VarType = strVarType;
			VarName = strVarName;
			VarComment = strVarComment;
		}
		public string VarType;
		public string VarName;
		public string VarComment;
	}
	public class ST_STRUCT
	{
		public ST_STRUCT(string strName)
		{
			StruName = strName;
			VarList = new List<ST_VARIBLE>();
		}
		public string StruName;
		public List<ST_VARIBLE> VarList;
		
		public string GetVarType(string strVarName)
		{
			foreach (ST_VARIBLE var in VarList) {
				if (strVarName == var.VarName) {
					return var.VarType;
				}
			}
			return "";
		}
	}
	/// <summary>
	/// Description of StructAnalyse.
	/// </summary>
	public class StructAnalyse
	{
		protected string _StrStruct;
		protected Dictionary<string, ST_STRUCT> _Structs;
		
		public Dictionary<string, ST_STRUCT> Structs
		{
			get
			{
				return _Structs;
			}
		}
		public StructAnalyse(string strStruct)
		{
			_StrStruct = strStruct;
			_Structs = new Dictionary<string, ST_STRUCT>();
		}
		
		public void Analyse()
		{
			try 
			{
				Regex reg = new Regex(@"typedef\s+struct\s+\w+\s*\{\s*(?<StruBody>.*?)\s*\}\s*(?<StruName>\w+)\s*;", RegexOptions.Multiline | RegexOptions.Singleline);
				MatchCollection mc = reg.Matches(_StrStruct);
				foreach (Match m in mc) 
				{
					if (m.Groups["StruName"].Value != "") 
					{
						ST_STRUCT stru = new ST_STRUCT(m.Groups["StruName"].Value);
						if (m.Groups["StruBody"].Value != "")
						{
							string strText = m.Groups["StruBody"].Value;
							strText = strText.Replace("long long", "long");
							strText = strText.Replace("unsigned", "");
							Regex regBody = new Regex(@"^\s*(?<Type>\w+)\s+(?<Col>\w+)\s*.*?;(?<Comment>.*?)$", RegexOptions.Multiline | RegexOptions.Singleline);
							MatchCollection mc2 = regBody.Matches(strText);
							foreach (Match m2 in mc2) 
							{
								string strComment = m2.Groups["Comment"].Value.Trim();
								strComment = strComment.Trim(new char[]{'/'}).Trim();
								strComment = strComment.Trim(new char[]{'*'}).Trim();
								strComment = strComment.Trim(new char[]{'<'}).Trim();
								if (m2.Groups["Col"].Value != "")
								{
									stru.VarList.Add(new ST_VARIBLE(m2.Groups["Type"].Value, m2.Groups["Col"].Value, strComment));
								}
							}
						}
						_Structs.Add(stru.StruName, stru);
					}
								
				}
			} catch (Exception) 
			{
				
				throw;
			}
		}
		
		public string GetVarType(string strStruct, string strVarName)
		{
			if (!_Structs.ContainsKey(strStruct)) {
				return "";
			}
			return _Structs[strStruct].GetVarType(strVarName);
		}
	}
}
