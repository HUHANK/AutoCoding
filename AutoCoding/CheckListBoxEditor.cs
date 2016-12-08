/*
 * Created by SharpDevelop.
 * User: Dongdong.Shen
 * Date: 2010-7-18
 * Time: 14:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

namespace AutoCoding
{
	/// <summary>
	/// Description of CheckListBoxEditor.
	/// </summary>
	public class CheckListBoxEditor : UITypeEditor
	{
		private CheckListBoxUI m_ui = new CheckListBoxUI();
		public CheckListBoxEditor()
		{
			
		}
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return false;
		}
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}	
		public override bool IsDropDownResizable 
		{
			get { return true; }
		}
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
				if (editorService == null) 
				{
					return value;
				}
				if (value == null) 
				{
					value = "";
				}
				string strTest = context.PropertyDescriptor.Name;
				m_ui.SetValue(value.ToString());
				editorService.DropDownControl(m_ui);
				value = m_ui.GetValue();
			}

      			return value;
		}
	}
}
