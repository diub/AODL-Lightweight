/*
 * $Id: WhiteSpace.cs,v 1.2 2006/02/05 20:02:25 larsbm Exp $
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

using AODL.Document.Styles;
using System.Xml;

namespace AODL.Document.Content.Text.TextControl {

	/// <summary>
	/// WhiteSpace represent a white space element.
	/// </summary>
	public class WhiteSpace : IText {
		/// <summary>
		/// Gets or sets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count {
			// diub - Dipl.-Ing. Uwe Barth 2021-04-26
			// Much better: pretty easy!
			get; set;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WhiteSpace"/> class.
		/// </summary>
		/// <param name="Document">The document.</param>
		/// <param name="Node">The node.</param>
		public WhiteSpace (IDocument Document, XmlNode Node) {
			this.Document = Document;
			this.Node = Node;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WhiteSpace"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="whiteSpacesCount">The document.</param>
		/// <param name="StyleName">Same stylename as used for non-spaces.</param>
		public WhiteSpace (IDocument document, int whiteSpacesCount, string StyleName) {
			this.Document = document;
			this.NewXmlNode ();
			// diub - Dipl.-Ing. Uwe Barth 2021-04-26
			this.Count = whiteSpacesCount;
			this.StyleName = StyleName;

			XmlNode node;
			XmlAttribute xa, xf, xs;

			xf = this.Document.CreateAttribute ("style-name", "text");
			xf.Value = StyleName;
			this.Node.Attributes.Append (xf);

			this.Node.AppendChild (this.Document.CreateNode ("s", "text"));
			xa = this.Document.CreateAttribute ("c", "text");
			xa.Value = Count.ToString ();

			xs = this.Document.CreateAttribute ("contextual-spacing", "style");
			xs.Value = "false";

			//xs = this.Document.CreateAttribute ("text-underline-style ", "style");
			//xs.Value = "solid";
			node = this.Node.SelectSingleNode ("text:s", this.Document.NamespaceManager);
			//node.Attributes.Append (xf);
			//node.Attributes.Append (xs);
			node.Attributes.Append (xa);
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
		/// Create a new XmlNode.
		/// </summary>
		private void NewXmlNode () {
			// diub - Dipl.-Ing. Uwe Barth
			// WRONG!  
			//this.Node = this.Document.CreateNode ("s", "text");
			this.Node = this.Document.CreateNode ("span", "text");
		}

		#region IText Member

		private XmlNode _node;
		/// <summary>
		/// The node that represent the text content.
		/// </summary>
		/// <value></value>
		public XmlNode Node {
			get {
				return this._node;
			}
			set {
				this._node = value;
			}
		}

		/// <summary>
		/// A tab stop doesn't have a text.
		/// </summary>
		/// <value></value>
		public string Text {
			get {
				return null;
			}
			set {
			}
		}

		/// <summary>
		/// The document to which this text content belongs to.
		/// </summary>
		/// <value></value>
		public IDocument Document {
			get; set;
		}

		/// <summary>
		/// Is null no style is available.
		/// </summary>
		/// <value></value>
		public IStyle Style {
			get {
				return null;
			}
			set {
			}
		}

		/// <summary>
		/// No style name available
		/// diub - Dipl.-Ing. Uwe Barth 2021-04-26
		/// MUST BE: to set right font for space width!
		/// </summary>
		/// <value></value>
		public string StyleName {
			get; set;
		}

		#endregion
	}

}


/*
 * $Log: WhiteSpace.cs,v $
 * Revision 1.2  2006/02/05 20:02:25  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.2  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 * Revision 1.1  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 */