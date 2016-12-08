/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-7-18
 * Time: 9:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace AutoCoding
{
	/// <summary>
	/// Description of StaticTablesInfo.
	/// </summary>
	public class StaticInfo
	{
		public Dictionary<string, Table> Tables;
		public Dictionary<string, ST_STRUCT> Structs;
		public string CurTableName;
		public string CurBSqlFile;
		public CodingType CurCodingType;
		
		public StaticInfo()
		{
			Tables = new Dictionary<string, Table>();
			Structs = new Dictionary<string, ST_STRUCT>();
			CurTableName = "";
			CurBSqlFile = "";
			CurCodingType = CodingType.Struct;
		}
	}
}
