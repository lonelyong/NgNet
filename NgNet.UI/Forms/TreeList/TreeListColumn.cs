using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace NgNet.UI.Forms
{
	public class HitInfo
	{
		public enum eHitType
		{
			kColumnHeader			= 0x0001,
			kColumnHeaderResize		= 0x0002,
		}

		public eHitType			HitType = 0;
		public TreeListColumn	Column = null;
	}
	
	/// <summary>
	/// DesignTimeVisible(false) prevents the columns from showing in the component tray (bottom of screen)
	/// If the class implement IComponent it must also implement default (void) constructor and when overriding
	/// the collection editors CreateInstance the return object must be used (if implementing IComponent), the reason
	/// is that ISite is needed, and ISite is set when base.CreateInstance is called.
	/// If no default constructor then the object will not be added to the collection in the initialize.
	/// In addition if implementing IComponent then name and generatemember is shown in the property grid
	/// Columns should just be added to the collection, no need for member, so no need to implement IComponent 
	/// </summary>
	[DesignTimeVisible(false)]
	[TypeConverter(typeof(ColumnConverter))]
	public class TreeListColumn
	{
		TextFormatting	m_headerFormat = new TextFormatting();
		TextFormatting	m_cellFormat = new TextFormatting();
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TextFormatting HeaderFormat
		{
			get { return m_headerFormat; }
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TextFormatting CellFormat
		{
			get { return m_cellFormat; }
		}

		TreeListColumnCollection m_owner = null;
		Rectangle	m_calculatedRect;
		int			m_visibleIndex = -1;
		int			m_colIndex = -1;
		int			m_width = 50;
		string		m_fieldName = string.Empty;
		string		m_caption = string.Empty;

		internal TreeListColumnCollection Owner
		{
			get { return m_owner; }
			set { m_owner = value; }
		}
		internal Rectangle internalCalculatedRect
		{
			get { return m_calculatedRect; }
			set { m_calculatedRect = value; }
		}
		internal int internalVisibleIndex
		{
			get { return m_visibleIndex; }
			set { m_visibleIndex = value; }
		}
		internal int internalIndex
		{
			get { return m_colIndex; }
			set { m_colIndex = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Rectangle CalculatedRect
		{
			get { return internalCalculatedRect; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeList TreeList
		{
			get 
			{ 
				if (Owner == null)
					return null;
				return Owner.Owner;
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Font Font
		{
			get { return m_owner.Font; }
		}
		public int Width
		{
			get { return m_width; }
			set 
			{ 
				if (m_width == value)
					return;
				m_width = value; 
				if (m_owner != null && m_owner.DesignMode)
					m_owner.RecalcVisibleColumsRect();
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Caption
		{
			get { return m_caption; }
			set 
			{ 
				m_caption = value; 
				if (m_owner != null)
					m_owner.Invalidate();
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Fieldname
		{
			get { return m_fieldName; }
			set
			{
				if (m_owner == null || m_owner.DesignMode == false)
					throw new Exception("Fieldname can only be set at design time, Use Constructor to set programatically");
				if (value.Length == 0)
					throw new Exception("empty Fieldname not value");
				if (m_owner[value] != null)
					throw new Exception("fieldname already exist in collection");
				m_fieldName = value;
			}
		}

		public TreeListColumn(string fieldName)
		{
			m_fieldName = fieldName;
		}
		public TreeListColumn(string fieldName, string caption)
		{
			m_fieldName = fieldName;
			m_caption = caption;
		}
		public TreeListColumn(string fieldName, string caption, int width)
		{
			m_fieldName = fieldName;
			m_caption = caption;
			m_width = width;
		}
		
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int VisibleIndex
		{
			get { return internalVisibleIndex; }
			set	
			{
				if (m_owner != null)
					m_owner.SetVisibleIndex(this, value);
			}
		}
		
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Index
		{
			get { return internalIndex; }
		}
		public bool ishot = false;
		public virtual void Draw(Graphics dc, ColumnHeaderPainter painter, Rectangle r)
		{
			painter.DrawHeader(dc, r, this, HeaderFormat, ishot);
		}

		bool	m_autoSize = false;
		float	m_autoSizeRatio = 100;
		int		m_autoSizeMinSize;
		
		[DefaultValue(false)]
		public bool AutoSize
		{
			get { return m_autoSize; } 
			set { m_autoSize = value; }
		}
		[DefaultValue(100f)]
		public float AutoSizeRatio
		{
			get { return m_autoSizeRatio; } 
			set { m_autoSizeRatio = value; }
		}
		public int AutoSizeMinSize
		{
			get { return m_autoSizeMinSize; } 
			set { m_autoSizeMinSize = value; }
		}
		int m_calculatedAutoSize;
		internal int CalculatedAutoSize
		{
			get { return m_calculatedAutoSize; }
			set { m_calculatedAutoSize = value; }
		}
	}


}
