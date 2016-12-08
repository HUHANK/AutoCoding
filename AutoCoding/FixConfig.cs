/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-7-14
 * Time: 22:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace AutoCoding
{
	
	/// <summary>
	/// Description of FixConfig.
	/// </summary>
	[XmlRootAttribute(ElementName="FixConfig", IsNullable=false)]
	[DefaultPropertyAttribute("Author")]
	public class FixConfig
	{
		private string _ProjectName;
		private string _ProjectPath;
		private string _BuStructFilePath;
		private string _TbStructFilePath;
		private string _CreateTableFilePath;
		private string _BSqlFilePrefix;
		private string _BProcessFilePrefix;
		private string _BSqlIncFiles;
		private string _BProcessIncFiles;		
		private string _BSqlHIncFiles;
		private string _BProcessHIncFiles;
		private string _DefStructSuffix;
		private string _Version;
		private string _Package;
		private string _Author;
		private string _ModuleNameConst;
		private string _ModuleNameValue;
		private string _DefTbSchema;
		
		public FixConfig()
		{
			_ProjectName = "";
			_ProjectPath = "";
			_BuStructFilePath = "";
			_TbStructFilePath = "";
			_CreateTableFilePath = "";
			_BSqlFilePrefix = "";
			_BProcessFilePrefix = "";
			_BSqlIncFiles = "";
			_BSqlHIncFiles = "";
			_BProcessIncFiles = "";
			_BProcessHIncFiles = "";
			_DefStructSuffix = "";
			_Author = "";
			_Version = "";
			_Package = "";
			_ModuleNameConst = "";
			_ModuleNameValue = "";
			_DefTbSchema = "";
		}

		[CategoryAttribute("项目信息"), DescriptionAttribute("设置项目信息")]
		public string ProjectName
		{
			get
			{
				return _ProjectName;
			}
			set
			{
				_ProjectName = value;
			}
		}
		
		[EditorAttribute(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[CategoryAttribute("项目信息"), DescriptionAttribute("设置项目信息")]
		public string ProjectPath
		{
			get
			{
				return _ProjectPath;
			}
			set
			{
				_ProjectPath = value;
			}
		}
		
		
		[EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[CategoryAttribute("项目信息"), DescriptionAttribute("设置数据结构体定义文件")]
		public string TbStructFilePath
		{
			get
			{
				return _TbStructFilePath;
			}
			set
			{
				_TbStructFilePath = value;
			}
		}
		
		[EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[CategoryAttribute("项目信息"), DescriptionAttribute("设置业务结构体定义文件")]
		public string BuStructFilePath
		{
			get
			{
				return _BuStructFilePath;
			}
			set
			{
				_BuStructFilePath = value;
			}
		}
		
		[EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[CategoryAttribute("项目信息"), DescriptionAttribute("设置创建表SQL语句文件")]
		public string CreateTableFilePath
		{
			get
			{
				return _CreateTableFilePath;
			}
			set
			{
				_CreateTableFilePath = value;
			}
		}		
		
		[CategoryAttribute("项目信息"), DescriptionAttribute("BSql层文件前缀")]
		public string BSqlFilePrefix
		{
			get
			{
				return _BSqlFilePrefix;
			}
			set
			{
				_BSqlFilePrefix = value;
			}
		}
		
		[CategoryAttribute("项目信息"), DescriptionAttribute("BProcess层文件前缀")]
		public string BProcessFilePrefix
		{
			get
			{
				return _BProcessFilePrefix;
			}
			set
			{
				_BProcessFilePrefix = value;
			}
		}
		
		[CategoryAttribute("项目信息"), DescriptionAttribute("BSql实现文件包含文件：多个文件用|分割")]
		public string BSqlIncFiles
		{
			get
			{
				return _BSqlIncFiles;
			}
			set
			{
				_BSqlIncFiles = value;
			}
		}
		
		[CategoryAttribute("项目信息"), DescriptionAttribute("BProcess实现包含文件：多个文件用|分割")]
		public string BProcessIncFiles
		{
			get
			{
				return _BProcessIncFiles;
			}
			set
			{
				_BProcessIncFiles = value;
			}
		}
		
		[CategoryAttribute("项目信息"), DescriptionAttribute("BSql头文件包含文件：多个文件用|分割")]
		public string BSqlHIncFiles
		{
			get
			{
				return _BSqlHIncFiles;
			}
			set
			{
				_BSqlHIncFiles = value;
			}
		}
		
		[CategoryAttribute("项目信息"), DescriptionAttribute("BProcess头文件包含文件：多个文件用|分割")]
		public string BProcessHIncFiles
		{
			get
			{
				return _BProcessHIncFiles;
			}
			set
			{
				_BProcessHIncFiles = value;
			}
		}
		
		[CategoryAttribute("项目信息"), DescriptionAttribute("结构体默认后缀")]
		public string DefStructSuffix {
			get { return _DefStructSuffix; }
			set { _DefStructSuffix = value; }
		}
		
		[CategoryAttribute("项目信息"), DescriptionAttribute("表默认Schema")]
		public string DefTbSchema {
			get { return _DefTbSchema; }
			set { _DefTbSchema = value; }
		}

		[CategoryAttribute("版本专题"), DescriptionAttribute("设置当前版本信息")]
		public string Version
		{
			get
			{
				return _Version;
			}
			set
			{
				_Version = value;
			}
		}
		
		[CategoryAttribute("版本专题"), DescriptionAttribute("设置专题信息")]
		public string Package
		{
			get
			{
				return _Package;
			}
			set 
			{
				_Package = value;
			}
		}

		[CategoryAttribute("程序作者"), DescriptionAttribute("设置程序作者")]
		public string Author
		{
			get
			{
				return _Author;
			}
			set
			{
				_Author = value;
			}
		}
		
		[CategoryAttribute("模块信息"), DescriptionAttribute("模块常量定义")]
		public string ModuleNameConst {
			get { return _ModuleNameConst; }
			set { _ModuleNameConst = value; }
		}
		
		[CategoryAttribute("模块信息"), DescriptionAttribute("模块名称")]
		public string ModuleNameValue {
			get { return _ModuleNameValue; }
			set { _ModuleNameValue = value; }
		}
	}
}
