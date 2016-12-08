/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010/11/5
 * Time: 21:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutoCoding
{
	public struct RelationString
	{
		public string str1;
		public string str2;
		
		public RelationString(string s1, string s2)
		{
			str1 = s1;
			str2 = s2;
		}
	}
	/// <summary>
	/// Description of RelationAnalyse.
	/// </summary>
	public class RelationAnalyse
	{
		protected string _SrcStr1;
		protected string _SrcStr2;
		protected List<RelationString> _RelationList;
		
		public string SrcStr1 {
			get { return _SrcStr1; }
			set { _SrcStr1 = value; }
		}

		public string SrcStr2 {
			get { return _SrcStr2; }
			set { _SrcStr2 = value; }
		}
		
		public RelationAnalyse()
		{
			_SrcStr1 = "";
			_SrcStr2 = "";
			_RelationList = new List<RelationString>();
		}
		
		public RelationAnalyse(string str1, string str2)
		{
			_SrcStr1 = str1;
			_SrcStr2 = str2;
			_RelationList = new List<RelationString>();
		}
		
		public bool Analyse()
		{
			_SrcStr1 = _SrcStr1.Replace("\r\n", "");
			_SrcStr2 = _SrcStr2.Replace("\r\n", "");
			string[] strs1 = _SrcStr1.Split(new char[]{','});
			string[] strs2 = _SrcStr2.Split(new char[]{','});
			
			_RelationList.Clear();
			
			if (strs1.Length != strs2.Length) {
				return false;
			}
			
			for (int i = 0; i < strs1.Length; i++) {
				_RelationList.Add(new RelationString(strs1[i].Trim(), strs2[i].Trim()));
			}
			
			return true;
		}
		
		protected string FormatString(string strToFormat)
		{
			string strRet = strToFormat;
			int iLen = strRet.Length;
			int totalLen = 20;
			if (iLen < totalLen) {
				for (int i = 0; i < totalLen-iLen; i++) {
					strRet += " ";
				}
			} else {
				strRet += " ";
			}
			return strRet;
		}
		
		public string GetResult()
		{
			string strResult = "";
			foreach (RelationString relstr in _RelationList) {
				if (strResult != "") {
					strResult += ",\r\n";
				}
				strResult += FormatString(relstr.str1) + " = " + relstr.str2;
			}
			return strResult;
		}
	}
}
