/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-7-18
 * Time: 10:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;

namespace AutoCoding
{
	/// <summary>
	/// Description of StructNamesConverter.
	/// </summary>
	public class StructNamesConverter : TypeConverter
	{
		public StructNamesConverter()
			: base()
		{
		}
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(MainForm.staticInfo.Structs.Keys);
		}
	}
}
