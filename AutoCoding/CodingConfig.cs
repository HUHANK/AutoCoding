/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-7-18
 * Time: 8:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Collections.Generic;

namespace AutoCoding
{
	/// <summary>
	/// 编码类型
	/// </summary>
	public enum CodingType
	{
		Struct, 
		/*SelectByKey, 
		SelectOneRecord, 
		SelectMultiRecord, 
		DynSelectMultiRecord,
		SelectCount,
		Insert, 
		//Insert_Retry,
		Update, 
		Update_Inc,
		Delete,
		SSMTLoad,
		SSMTUpdate,
		TEXBUpload,*/
		AllByKey
	};
	
	/// <summary>
	/// 查询选项
	/// </summary>
	public enum SelectOption
	{
		None,
		WithUR
	}
	
	/// <summary>
	/// 定义游标选项
	/// </summary>
	public enum DeclareCursorOption
	{
		None,
		WithHold
	}
	
	/// <summary>
	/// 游标选项
	/// </summary>
	public enum CursorOption
	{
		None,
		ForFetchOnly,
		ForReadOnly
	}
	
	/// <summary>
	/// Description of FuncConfig.
	/// </summary>
	[TypeConverter(typeof(CodingConfigConverter))]
	public class CodingConfig
	{
		// 编码类型
		private string _CodingType;
		
		// 数据库
		private string _TableName;
		private string _TableNameAbbr;
		private string _SelectCols;
		private string _InsertCols;
		private string _UpdateCols;
		private string _WhereCols;
		private string _UsedCols;
		private string _CursorName;
		
		// 结构体
		private string _StructName;
		private string _StructComment;
		private string _StructIn;
		private string _StructOut;
		
		// 函数
		private string _BSqlFuncName;
		private string _BSsmtFuncName;
		private string _BProFuncName;
		private string _FuncComment;
		private string _FuncNo;
		private string _NotFoundCode;
		private DeclareCursorOption _declareCursorOption;
		private CursorOption _cursorOption;
		private SelectOption _selectOption;
		private bool _bAllSoc;
		
		// 保存到文件位置
		private string _BSqlFile;
		private string _BSqlIncFile;
		private string _BProFile;
		private string _BProIncFile;
		
		public CodingConfig()
		{
			_TableName = "";
			_TableNameAbbr = "";
			_SelectCols = "";
			_InsertCols = "";
			_UpdateCols = "";
			_WhereCols = "";
			_UsedCols = "";
			_CursorName = "";
			_StructName = "";
			_StructComment = "";
			_StructIn = "";
			_StructOut = "";
			_BSqlFuncName = "";
			_BProFuncName = "";
			_FuncComment = "";
			_FuncNo = "";
			_NotFoundCode = "";
			_BSqlFile = "";
			_BSqlIncFile = "";
			_BProFile = "";
			_BProIncFile = "";
			_declareCursorOption = DeclareCursorOption.None;
			_cursorOption = CursorOption.None;
			_selectOption = SelectOption.None;
			_bAllSoc = true;
		}
		
		[Category("1.编码类型"), Description("选择编码类型")]
		//[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(CodingTypeConverter))]
		public string CodingType
		{
			get
			{
				return _CodingType;
			}
			set
			{
				_CodingType = value;
			}
		}
		
		[Category("2.数据库表"), Description("设置数据库表名")]
		[TypeConverter(typeof(TableNamesConverter))]
		public string TableName
		{
			get
			{
				return _TableName;
			}
			set
			{
				_TableName = value;
			}
		}		
		
		[Category("2.数据库表"), Description("设置数据库表名缩写")]		
		public string TableNameAbbr
		{
			get
			{
				return _TableNameAbbr;
			}
			set
			{
				_TableNameAbbr = value;
			}
		}
		
		[Category("2.数据库表"), Description("设置SELECT用到字段")]		
		[Editor(typeof(CheckListBoxEditor), typeof(UITypeEditor))]
		public string SelectCols
		{
			get
			{
				return _SelectCols;
			}
			set
			{
				_SelectCols = value;
			}
		}
		
		[Category("2.数据库表"), Description("设置INSERT INTO用到字段")]		
		[Editor(typeof(CheckListBoxEditor), typeof(UITypeEditor))]
		public string InsertCols
		{
			get
			{
				return _InsertCols;
			}
			set
			{
				_InsertCols = value;
			}
		}
		
		[Category("2.数据库表"), Description("设置UPDATE用到字段")]		
		[Editor(typeof(CheckListBoxEditor), typeof(UITypeEditor))]
		public string UpdateCols
		{
			get
			{
				return _UpdateCols;
			}
			set
			{
				_UpdateCols = value;
			}
		}
				
		[Category("2.数据库表"), Description("设置WHERE用到字段")]		
		[Editor(typeof(CheckListBoxEditor), typeof(UITypeEditor))]
		public string WhereCols
		{
			get
			{
				return _WhereCols;
			}
			set
			{
				_WhereCols = value;
			}
		}
		
		[Category("2.数据库表"), Description("设置结构体用到字段")]		
		[Editor(typeof(CheckListBoxEditor), typeof(UITypeEditor))]
		public string UsedCols
		{
			get
			{
				return _UsedCols;
			}
			set
			{
				_UsedCols = value;
			}
		}
			
		[Category("2.数据库表"), Description("设置游标名")]
		public string CursorName
		{
			get
			{
				return _CursorName;
			}
			set
			{
				_CursorName = value;
			}
		}
		
				
		[Category("2.数据库表"), Description("定义游标选项")]
		public DeclareCursorOption DeclareCursorOption {
			get { return _declareCursorOption; }
			set { _declareCursorOption = value; }
		}
		
		[Category("2.数据库表"), Description("定义游标选项")]
		public CursorOption CursorOption {
			get { return _cursorOption; }
			set { _cursorOption = value; }
		}
		
		[Category("2.数据库表"), Description("查询选项")]
		public SelectOption SelectOption {
			get { return _selectOption; }
			set { _selectOption = value; }
		}
				
		[Category("3.结构体"), Description("设置结构体名称")]
		public string StructName
		{
			get
			{
				return _StructName;
			}
			set
			{
				_StructName = value;
			}
		}
		
		[Category("3.结构体"), Description("设置结构体说明")]
		public string StructComment
		{
			get
			{
				return _StructComment;
			}
			set
			{
				_StructComment = value;
			}
		}
		
		[Category("3.结构体"), Description("设置输入结构体")]
		[TypeConverter(typeof(StructNamesConverter))]
		public string StructIn
		{
			get
			{
				return _StructIn;
			}
			set
			{
				_StructIn = value;
			}
		}
		
		[Category("3.结构体"), Description("设置输出结构体")]
		[TypeConverter(typeof(StructNamesConverter))]
		public string StructOut
		{
			get
			{
				return _StructOut;
			}
			set
			{
				_StructOut = value;
			}
		}
		
		[Category("4.函数其他信息"), Description("设置BSql层函数名")]
		public string BSqlFuncName
		{
			get
			{
				return _BSqlFuncName;
			}
			set
			{
				_BSqlFuncName = value;
			}
		}
		
		[Category("4.函数其他信息"), Description("设置BSSMT函数名")]
		public string BSsmtFuncName 
		{
			get { return _BSsmtFuncName; }
			set { _BSsmtFuncName = value; }
		}
		
		[Category("4.函数其他信息"), Description("设置BPro层函数名")]
		public string BProFuncName
		{
			get
			{
				return _BProFuncName;
			}
			set
			{
				_BProFuncName = value;
			}
		}
			
		[Category("4.函数其他信息"), Description("设置函数注释")]
		public string FuncComment
		{
			get
			{
				return _FuncComment;
			}
			set
			{
				_FuncComment = value;
			}
		}
		
		[Category("4.函数其他信息"), Description("设置函数编号")]
		public string FuncNo
		{
			get
			{
				return _FuncNo;
			}
			set
			{
				_FuncNo = value;
			}
		}
		
		[Category("4.函数其他信息"), Description("SQLCODE为NOTFOUND时错误代码")]
		public string NotFoundCode
		{
			get
			{
				return _NotFoundCode;
			}
			set
			{
				_NotFoundCode = value;
			}
		}
		
		[Category("4.函数其他信息"), Description("是否上场到所有订单系统")]
		public bool BAllSoc {
			get { return _bAllSoc; }
			set { _bAllSoc = value; }
		}
		
		
		[EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("5.保存到文件"), Description("BSQL文件位置")]
		public string BSqlFile
		{
			get
			{
				return _BSqlFile;
			}
			set
			{
				_BSqlFile = value;
			}
		}
		
		[EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("5.保存到文件"), Description("BSQL头文件位置")]
		public string BSqlIncFile
		{
			get
			{
				return _BSqlIncFile;
			}
			set
			{
				_BSqlIncFile = value;
			}
		}
		
		[EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("5.保存到文件"), Description("BPRO文件位置")]
		public string BProFile
		{
			get
			{
				return _BProFile;
			}
			set
			{
				_BProFile = value;
			}
		}
		
		[EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("5.保存到文件"), Description("BPRO头文件位置")]
		public string BProIncFile
		{
			get
			{
				return _BProIncFile;
			}
			set
			{
				_BProIncFile = value;
			}
		}		
		
		private class CodingConfigConverter : ExpandableObjectConverter
		{
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				List<string> properties = new List<string>();
				List<string> StructProperties = 
					new List<string>(new string[]{"CodingType", "TableName"/*, "UsedCols", "StructName", "StructComment"*/});
				List<string> SelectByKeyProperties = 
					new List<string>(new string[]{"CodingType", 
                          "TableName", "SelectOption",
                          "BSqlFuncName", "BSsmtFuncName","BProFuncName", "FuncNo", "NotFoundCode",
					        "BSqlFile", "BSqlIncFile", "BProFile", "BProIncFile"});
				List<string> SelectOneRecordProperties = 
					new List<string>(new string[]{"CodingType", 
                          "TableName", "SelectCols","WhereCols","SelectOption",
                          "StructIn","StructOut",
                          "BSqlFuncName","BProFuncName", "FuncComment", "FuncNo", "NotFoundCode",
					        "BSqlFile", "BSqlIncFile", "BProFile", "BProIncFile"});
				List<string> SelectMultiRecordProperties = 
					new List<string>(new string[]{"CodingType", 
					        "TableName","SelectCols","WhereCols","CursorName","SelectOption","DeclareCursorOption","CursorOption",
					        "StructIn","StructOut",
                          "BSqlFuncName","BProFuncName", "FuncComment", "FuncNo",
					        "BSqlFile", "BSqlIncFile", "BProFile", "BProIncFile"});
				List<string> DynSelectMultiRecordProperties = 
					new List<string>(new string[]{"CodingType", 
					        "TableName","SelectCols","CursorName","SelectOption","DeclareCursorOption","CursorOption",
					        "StructOut",
                          "BSqlFuncName","BProFuncName", "FuncComment", "FuncNo",
					        "BSqlFile", "BSqlIncFile", "BProFile", "BProIncFile"});
				List<string> SelectCountProperties = 
					new List<string>(new string[]{"CodingType", 
                          "TableName", "SelectCols","WhereCols","SelectOption",
                          "StructIn",
                          "BSqlFuncName","BProFuncName", "FuncComment", "FuncNo",
					        "BSqlFile", "BSqlIncFile", "BProFile", "BProIncFile"});
				List<string> InsertProperties = 
					new List<string>(new string[]{"CodingType", "TableName", 
			              "InsertCols","StructIn",
			              "BSqlFuncName","BProFuncName", "FuncComment", "FuncNo",
					        "BSqlFile", "BSqlIncFile", "BProFile", "BProIncFile"});
				List<string> UpdateProperties = 
					new List<string>(new string[]{"CodingType", "TableName", 
                      "UpdateCols","WhereCols","StructIn",
                      "BSqlFuncName", "BProFuncName","FuncComment", "FuncNo", "NotFoundCode",
					    "BSqlFile", "BSqlIncFile", "BProFile", "BProIncFile"});
				List<string> DeleteProperties = 
					new List<string>(new string[]{"CodingType", "TableName", 
                      "WhereCols","StructIn",
                      "BSqlFuncName", "BProFuncName","FuncComment", "FuncNo",
					        "BSqlFile", "BSqlIncFile", "BProFile", "BProIncFile"});
				List<string> SSMTLoadProperties = 
					new List<string>(new string[]{"CodingType", "TableName", 
                      "FuncNo"});
				List<string> SSMTUpdateProperties = 
					new List<string>(new string[]{"CodingType", "TableName", 
                      "FuncNo"});
				List<string> SSMTCreateProperties = 
					new List<string>(new string[]{"CodingType", "TableName", 
                      "BSqlFuncName","FuncComment", "FuncNo"});
				List<string> TEXBUploadProperties = 
					new List<string>(new string[]{"CodingType", 
					  "TableName", "SelectCols",
					  "StructIn",
                      "BProFuncName","BAllSoc", "FuncComment"});
				List<string> AllByKeyProperties = 
					new List<string>(new string[]{"CodingType", 
                          "TableName"});
				PropertyDescriptorCollection pdc = base.GetProperties(context, value, attributes);
				List<PropertyDescriptor> list = new List<PropertyDescriptor>();
				properties = StructProperties;
				/*switch ((string)value)
				{
					case null:
						properties = StructProperties;
						break;
					case "Struct":
					//case CodingType.Struct:
						properties = StructProperties;
						break;
					/*case CodingType.SelectByKey:
						properties = SelectByKeyProperties;
						break;
					case CodingType.SelectOneRecord:
						properties = SelectOneRecordProperties;
						break;
					case CodingType.SelectMultiRecord:
						properties = SelectMultiRecordProperties;
						break;
					case CodingType.DynSelectMultiRecord:
						properties = DynSelectMultiRecordProperties;
						break;
					case CodingType.SelectCount:
						properties = SelectCountProperties;
						break;
					case CodingType.Insert:
					//case CodingType.Insert_Retry:
						properties = InsertProperties;
						break;
					case CodingType.Update:
					case CodingType.Update_Inc:
						properties = UpdateProperties;
						break;
					case CodingType.Delete:
						properties = DeleteProperties;
						break;
					case CodingType.SSMTLoad:
						properties = SSMTLoadProperties;
						break;
					case CodingType.SSMTUpdate:
						properties = SSMTUpdateProperties;
						break;
					case CodingType.SSMTCreate:
						properties = SSMTCreateProperties;
						break;
					case CodingType.TEXBUpload:
						properties = TEXBUploadProperties;
						break;
					//case CodingType.AllByKey:
					//	properties = AllByKeyProperties;
					//	break;
					default:
						//properties = StructProperties;
						throw new Exception("Invalid value for CodingType");
				}*/
				foreach (string str in properties) 
				{
					list.Add(pdc[str]);
				}
				return new PropertyDescriptorCollection(list.ToArray());
			}
		}
	}
}
