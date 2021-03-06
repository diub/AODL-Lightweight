/*
 * $Id: CellProperties.cs,v 1.1 2006/01/29 11:28:23 larsbm Exp $
 */

/*
 * License: 
 * GNU Lesser General Public License. You should recieve a
 * copy of this within the library. If not you will find
 * a whole copy at http://www.gnu.org/licenses/lgpl.html .
 * 
 * Author:
 * Copyright 2006, Lars Behrmann, lb@OpenDocument4all.com
 * 
 * Last changes:
 * 
 */

using System.Xml;

namespace AODL.Document.Styles.Properties {
	/// <summary>
	/// Represent the Cell Properties within a Cell which is used
	/// within a Tabe resp. a Row.
	/// </summary>
	public class CellProperties : IProperty {
		/// <summary>
		/// Gets or sets the cell style.
		/// </summary>
		/// <value>The cell style.</value>
		public CellStyle CellStyle {
			get {
				return (CellStyle) this.Style;
			}
			set {
				this.Style = value;
			}
		}

		/// <summary>
		/// Gets or sets the padding. 
		/// Default 0.097cm
		/// </summary>
		/// <value>The padding.</value>
		public string Padding {
			get {
				XmlNode xn = this._node.SelectSingleNode("@fo:padding",
					this.CellStyle.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set {
				XmlNode xn = this._node.SelectSingleNode("@fo:padding",
					this.CellStyle.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute ("padding", value, "fo");
				this._node.SelectSingleNode ("@fo:padding",
					this.CellStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the border.
		/// This could be e.g. 0.002cm solid #000000 (width, linestyle, color)
		/// or none
		/// </summary>
		/// <value>The border.</value>
		public string Border {
			get {
				XmlNode xn = this._node.SelectSingleNode("@fo:border",
					this.CellStyle.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set {
				XmlNode xn = this._node.SelectSingleNode("@fo:border",
					this.CellStyle.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute ("border", value, "fo");
				this._node.SelectSingleNode ("@fo:border",
					this.CellStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the border left.
		/// This could be e.g. 0.002cm solid #000000 (width, linestyle, color)
		/// or none
		/// </summary>
		/// <value>The border left.</value>
		public string BorderLeft {
			get {
				XmlNode xn = this._node.SelectSingleNode("@fo:border-left",
					this.CellStyle.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set {
				XmlNode xn = this._node.SelectSingleNode("@fo:border-left",
					this.CellStyle.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute ("border-left", value, "fo");
				this._node.SelectSingleNode ("@fo:border-left",
					this.CellStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the border right.
		/// This could be e.g. 0.002cm solid #000000 (width, linestyle, color)
		/// or none
		/// </summary>
		/// <value>The border right.</value>
		public string BorderRight {
			get {
				XmlNode xn = this._node.SelectSingleNode("@fo:border-right",
					this.CellStyle.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set {
				XmlNode xn = this._node.SelectSingleNode("@fo:border-right",
					this.CellStyle.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute ("border-right", value, "fo");
				this._node.SelectSingleNode ("@fo:border-right",
					this.CellStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the border top.
		/// This could be e.g. 0.002cm solid #000000 (width, linestyle, color)
		/// or none
		/// </summary>
		/// <value>The border top.</value>
		public string BorderTop {
			get {
				XmlNode xn = this._node.SelectSingleNode("@fo:border-top",
					this.CellStyle.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set {
				XmlNode xn = this._node.SelectSingleNode("@fo:border-top",
					this.CellStyle.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute ("border-top", value, "fo");
				this._node.SelectSingleNode ("@fo:border-top",
					this.CellStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the border bottom.
		/// This could be e.g. 0.002cm solid #000000 (width, linestyle, color)
		/// or none
		/// </summary>
		/// <value>The border bottom.</value>
		public string BorderBottom {
			get {
				XmlNode xn = this._node.SelectSingleNode("@fo:border-bottom",
					this.CellStyle.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set {
				XmlNode xn = this._node.SelectSingleNode("@fo:border-bottom",
					this.CellStyle.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute ("border-bottom", value, "fo");
				this._node.SelectSingleNode ("@fo:border-bottom",
					this.CellStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the color of the background. e.g #000000 for black
		/// </summary>
		/// <value>The color of the background.</value>
		public string BackgroundColor {
			get {
				XmlNode xn = this._node.SelectSingleNode("@fo:background-color",
					this.CellStyle.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set {
				XmlNode xn = this._node.SelectSingleNode("@fo:background-color",
					this.CellStyle.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute ("background-color", value, "fo");
				this._node.SelectSingleNode ("@fo:background-color",
					this.CellStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CellProperties"/> class.
		/// </summary>
		/// <param name="cellstyle">The cellstyle.</param>
		public CellProperties (CellStyle cellstyle) {
			this.CellStyle = cellstyle;
			this.NewXmlNode ();
			//TODO: Check localisations cm?? inch??
			//defaults 
			this.Padding = "0.097cm";
		}

		/// <summary>
		/// Create the XmlNode which represent the propertie element.
		/// </summary>
		private void NewXmlNode () {
			this.Node = this.Style.Document.CreateNode ("table-cell-properties", "style");
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute (string name, string text, string prefix) {
			XmlAttribute xa = this.CellStyle.Document.CreateAttribute(name, prefix);
			xa.Value = text;
			this.Node.Attributes.Append (xa);
		}

		#region IProperty Member
		private XmlNode _node;
		/// <summary>
		/// The XmlNode which represent the property element.
		/// </summary>
		/// <value>The node</value>
		public System.Xml.XmlNode Node {
			get {
				return this._node;
			}
			set {
				this._node = value;
			}
		}

		private IStyle _style;
		/// <summary>
		/// The style object to which this property object belongs
		/// </summary>
		/// <value></value>
		public IStyle Style {
			get {
				return this._style;
			}
			set {
				this._style = value;
			}
		}
		#endregion

		#region IHtmlStyle Member

		/// <summary>
		/// Get the css style fragement
		/// </summary>
		/// <returns>The css style attribute</returns>
		public string GetHtmlStyle () {
			string style        = "style=\"";

			if (this.BackgroundColor != null)
				if (this.BackgroundColor.ToLower () != "transparent")
					style += "background-color: " + this.BackgroundColor + "; ";
				else
					style += "background-color: #FFFFFF; ";
			else
				style += "background-color: #FFFFFF; ";

			if (!style.EndsWith ("; "))
				style = "";
			else
				style += "\"";

			return style;
		}

		#endregion

	}
}
