/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-7-18
 * Time: 9:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;

namespace AutoCoding
{
	/// <summary>
	/// Description of TableNamesConverter.
	/// </summary>
	public class TableNamesConverter : TypeConverter
	{
		public TableNamesConverter()
			: base()
		{
		}
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(MainForm.staticInfo.Tables.Keys);
		}
	}
	
	public class CodingTypeConverter : TypeConverter
	{
		public CodingTypeConverter()
			: base()
		{
		}
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(MainForm.autoCodingType.ToArray());
		}
	}
}
