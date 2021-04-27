//	diub - Dipl.-Ing. Uwe Barth
//	2021-04-22

using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Content.Text {
	/// <summary>
	/// Represent a hyperlink, which could be 
	/// a web-, ftp- or telnet link
	/// </summary>
	public class PageRef : IText, ITextContainer, ICloneable {

		const string scope= "text";
		const string name = "ref-name";
		const string fullname = "@" + scope + ":" + name;

		/// <summary>
		/// </summary>
		/// <value></value>
		public string RefName {
			get {
				XmlNode xn = this._node.SelectSingleNode(fullname, this.Document.NamespaceManager) ;
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set {
				XmlNode xn = this._node.SelectSingleNode(fullname, this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute (name, value, scope);
				this._node.SelectSingleNode (fullname, this.Document.NamespaceManager).InnerText = value;
			}
		}


		/// <summary>
		/// Needs no comment!
		/// </summary>
		/// <param name="document"></param>
		/// <param name="RefName"></param>
		public PageRef (IDocument document, string RefName) {
			this.Document = document;
			this.NewXmlNode ();
			this.InitStandards ();
			this.RefName = RefName;
			this.TextContent.Add (new SimpleText (this.Document, RefName));
		}

		/// <summary>
		/// Needs no comment!
		/// </summary>
		/// <param name="document"></param>
		public PageRef (IDocument document) {
			this.Document = document;
			this.InitStandards ();
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards () {
			this.TextContent = new ITextCollection ();

			this.TextContent.Inserted += new AODL.Document.Collections.CollectionWithEvents.CollectionChange (TextContent_Inserted);
			this.TextContent.Removed += new AODL.Document.Collections.CollectionWithEvents.CollectionChange (TextContent_Removed);
		}

		/// <summary>
		/// 
		/// </summary>
		private void NewXmlNode () {
			Node = this.Document.CreateNode ("bookmark-ref", "text");
			CreateAttribute ("reference-format","page", "text");
			CreateAttribute ("ref-name", RefName, "text");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="AttributName"></param>
		/// <param name="Value">Innerer Wert.</param>
		/// <param name="Prefix">The prefix.</param>
		private void CreateAttribute (string AttributName, string Value, string Prefix) {
			XmlAttribute xa = this.Document.CreateAttribute(AttributName, Prefix);
			xa.Value = Value;
			this.Node.Attributes.Append (xa);
		}

		/// <summary>
		/// Append the xml from added IText object.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		private void TextContent_Inserted (int index, object value) {
			this.Node.AppendChild (((IText) value).Node);
		}

		/// <summary>
		/// Texts the content_ removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void TextContent_Removed (int index, object value) {
			this.Node.RemoveChild (((IText) value).Node);
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
		/// 
		/// </summary>
		/// <value></value>
		public string Text {
			get {
				return this.Node.InnerText;
			}
			set {
				this.Node.InnerText = value;
			}
		}

		private IDocument _document;
		/// <summary>
		/// 
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

		private string _styleName;
		/// <summary>
		/// </summary>
		/// <value></value>
		public string StyleName {
			get {
				return this._styleName;
			}
			set {
				this._styleName = value;
			}
		}

		#endregion

		#region ITextContainer Member

		private ITextCollection _textContent;
		/// <summary>
		/// All Content objects have a Text container. Which represents
		/// his Text this could be SimpleText, FormatedText or mixed.
		/// </summary>
		/// <value></value>
		public ITextCollection TextContent {
			get {
				return this._textContent;
			}
			set {
				if (this._textContent != null)
					foreach (IText text in this._textContent)
						this.Node.RemoveChild (text.Node);

				this._textContent = value;

				if (this._textContent != null)
					foreach (IText text in this._textContent)
						this.Node.AppendChild (text.Node);
			}
		}

		#endregion

		#region ICloneable Member
		/// <summary>
		/// Create a deep clone of this XLink object.
		/// </summary>
		/// <remarks>A possible Attached Style wouldn't be cloned!</remarks>
		/// <returns>
		/// A clone of this object.
		/// </returns>
		public object Clone () {
			PageRef PageRef            = null;

			if (this.Document != null && this.Node != null) {
				TextContentProcessor tcp    = new TextContentProcessor();
				PageRef = tcp.CreatePageRef (this.Document, this.Node.CloneNode (true));
			}

			return PageRef;
		}

		#endregion
	}
}
