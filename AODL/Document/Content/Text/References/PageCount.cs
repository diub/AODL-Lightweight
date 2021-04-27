
// diub - Dipl.-Ing. Uwe Barth 2021-04-26

using AODL.Document.Styles;
using System.Xml;

namespace AODL.Document.Content.Text.TextControl {
	/// <summary>
	/// LineBreak represent a line break.
	/// </summary>
	public class PageCount : IText, IContent {
		/// <summary>
		/// Initializes a new instance of the <see cref="LineBreak"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		public PageCount (IDocument document) {
			this.Document = document;
			this.NewXmlNode ();
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		private void NewXmlNode () {
			this.Node = this.Document.CreateNode ("page-count", "text");
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
		/// </summary>
		/// <value></value>
		public string Text {
			get; set;
		}

		private IDocument _document;
		/// <summary>
		/// The document to which this text content belongs to.
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
		/// </summary>
		/// <value></value>
		public string StyleName {
			get {
				return null;
			}
			set {
			}
		}

		#endregion
	}
}

/*
 * $Log: LineBreak.cs,v $
 * Revision 1.2  2006/02/05 20:02:25  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */