/*
 * $Id: Graphic.cs,v 1.4 2006/05/02 17:37:16 larsbm Exp $
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

using System;
using System.Drawing;
using System.Xml;
using AODL.Document.Styles;
using AODL.Document.Content;
using AODL.Document;

namespace AODL.Document.Content.Draw {
	/// <summary>
	/// Graphic represent a graphic resp. image.
	/// </summary>
	public class Graphic : IContent, IContentContainer {
		/// <summary>
		/// Gets or sets the H ref.
		/// </summary>
		/// <value>The H ref.</value>
		public string HRef {
			get {
				XmlNode xn = this._node.SelectSingleNode("@xlink:href",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set {
				XmlNode xn = this._node.SelectSingleNode("@xlink:href",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute ("href", value, "xlink");
				this._node.SelectSingleNode ("@xlink:href",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the actuate.
		/// e.g. onLoad
		/// </summary>
		/// <value>The actuate.</value>
		public string Actuate {
			get {
				XmlNode xn = this._node.SelectSingleNode("@xlink:actuate",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set {
				XmlNode xn = this._node.SelectSingleNode("@xlink:actuate",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute ("actuate", value, "xlink");
				this._node.SelectSingleNode ("@xlink:actuate",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the type of the Xlink.
		/// e.g. simple, standard, ..
		/// </summary>
		/// <value>The type of the X link.</value>
		public string XLinkType {
			get {
				XmlNode xn = this._node.SelectSingleNode("@xlink:type",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set {
				XmlNode xn = this._node.SelectSingleNode("@xlink:type",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute ("type", value, "xlink");
				this._node.SelectSingleNode ("@xlink:type",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the show.
		/// e.g. embed
		/// </summary>
		/// <value>The show.</value>
		public string Show {
			get {
				XmlNode xn = this._node.SelectSingleNode("@xlink:show",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set {
				XmlNode xn = this._node.SelectSingleNode("@xlink:show",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute ("show", value, "xlink");
				this._node.SelectSingleNode ("@xlink:show",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		private string _graphicRealPath;
		/// <summary>
		/// Gets or sets the graphic real path.
		/// </summary>
		/// <value>The graphic real path.</value>
		public string GraphicRealPath {
			get {
				return this._graphicRealPath;
			}
			set {
				this._graphicRealPath = value;
			}
		}

		private string _graphicFileName;
		/// <summary>
		/// Gets or sets the name of the graphic file.
		/// </summary>
		/// <value>The name of the graphic file.</value>
		public string GraphicFileName {
			get {
				return this._graphicFileName;
			}
			set {
				this._graphicFileName = value;
			}
		}

		private Frame _frame;
		/// <summary>
		/// Gets or sets the frame.
		/// </summary>
		/// <value>The frame.</value>
		public Frame Frame {
			get {
				return this._frame;
			}
			set {
				this._frame = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Graphic"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="frame">The frame.</param>
		/// <param name="graphiclink">The graphiclink.</param>
		public Graphic (IDocument document, Frame frame, string graphiclink) {
			this.Frame = frame;
			this.Document = document;
			this.GraphicFileName = graphiclink;
			this.NewXmlNode ("Pictures/" + graphiclink);
			this.InitStandards ();
			this.Document.Graphics.Add (this);
			this.Document.DocumentMetadata.ImageCount += 1;
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards () {
			this.Content = new IContentCollection ();
			this.Content.Inserted += new AODL.Document.Collections.CollectionWithEvents.CollectionChange (Content_Inserted);
			this.Content.Removed += new AODL.Document.Collections.CollectionWithEvents.CollectionChange (Content_Removed);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		/// <param name="graphiclink">The stylename which should be referenced with this frame.</param>
		private void NewXmlNode (string graphiclink) {
			this.Node = this.Document.CreateNode ("image", "draw");

			XmlAttribute xa = this.Document.CreateAttribute("href", "xlink");
			xa.Value = graphiclink;

			this.Node.Attributes.Append (xa);

			xa = this.Document.CreateAttribute ("type", "xlink");
			xa.Value = "standard";

			this.Node.Attributes.Append (xa);

			xa = this.Document.CreateAttribute ("show", "xlink");
			xa.Value = "embed";

			this.Node.Attributes.Append (xa);

			xa = this.Document.CreateAttribute ("actuate", "xlink");
			xa.Value = "onLoad";

			this.Node.Attributes.Append (xa);
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute (string name, string text, string prefix) {
			XmlAttribute xa = this.Document.CreateAttribute(name, prefix);
			xa.Value = text;
			this.Node.Attributes.Append (xa);
		}

		/// <summary>
		/// Content_s the inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Inserted (int index, object value) {
			this.Node.AppendChild (((IContent) value).Node);
		}

		/// <summary>
		/// Content_s the removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Removed (int index, object value) {
			this.Node.RemoveChild (((IContent) value).Node);
		}

		#region IContent Member
		/// <summary>
		/// Gets or sets the name of the style.
		/// </summary>
		/// <value>The name of the style.</value>
		public string StyleName {
			get {
				XmlNode xn = this._node.SelectSingleNode("@text:style-name",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set {
				XmlNode xn = this._node.SelectSingleNode("@text:style-name",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute ("style-name", value, "text");
				this._node.SelectSingleNode ("@text:style-name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		private IDocument _document;
		/// <summary>
		/// Every object (typeof(IContent)) have to know his document.
		/// </summary>
		/// <value></value>
		public IDocument Document {
			get {
				return this._document;
			}
			set {
				this._document = value;
			}
		}

		private IStyle _style;
		/// <summary>
		/// A Style class wich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		/// <value></value>
		public IStyle Style {
			get {
				return this._style;
			}
			set {
				this.StyleName = value.StyleName;
				this._style = value;
			}
		}

		private XmlNode _node;
		/// <summary>
		/// Gets or sets the node.
		/// </summary>
		/// <value>The node.</value>
		public XmlNode Node {
			get {
				return this._node;
			}
			set {
				this._node = value;
			}
		}

		#endregion


		#region IContentContainer Member

		private IContentCollection _content;
		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		/// <value>The content.</value>
		public IContentCollection Content {
			get {
				return this._content;
			}
			set {
				this._content = value;
			}
		}

		#endregion
	}
}
