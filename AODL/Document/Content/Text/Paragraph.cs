/*
 * $Id: Paragraph.cs,v 1.3 2006/02/02 21:55:59 larsbm Exp $
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

using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Styles;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Content.Text {
	/// <summary>
	/// Represent a paragraph within a opendocument document.
	/// </summary>
	public class Paragraph : IContent, IContentContainer, ITextContainer, ICloneable {
		private ArrayList _mixedContent;
		/// <summary>
		/// Gets the content of the mixed.
		/// </summary>
		/// <value>The content of the mixed.</value>
		public ArrayList MixedContent {
			get {
				return this._mixedContent;
			}
		}

		/// <summary>
		/// Mixed content - needed for alternative
		/// exporter implementations. In OpenDocument
		/// the order will be right automatically.
		/// </summary>
		private ParentStyles _parentStyle;
		/// <summary>
		/// Gets the parent style.
		/// </summary>
		/// <value>The parent style.</value>
		public ParentStyles ParentStyle {
			get {
				return this._parentStyle;
			}
		}

		/// <summary>
		/// Gets or sets the paragraph style.
		/// </summary>
		/// <value>The paragraph style.</value>
		public ParagraphStyle ParagraphStyle {
			get {
				return (ParagraphStyle) this.Style;
			}
			set {
				this.Style = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Paragraph"/> class.
		/// This is a blank paragraph.
		/// </summary>
		/// <param name="document">The document.</param>
		public Paragraph (IDocument document) {
			this.Document = document;
			this.NewXmlNode ();
			this.InitStandards ();
		}

		/// <summary>
		/// Create a new Paragraph object.
		/// </summary>
		/// <param name="document">The Texdocumentocument.</param>
		/// <param name="styleName">The styleName which should be referenced with this paragraph.</param>
		public Paragraph (IDocument document, string styleName) {
			this.Document = document;
			this.NewXmlNode ();
			this.Init (styleName);
		}

		/// <summary>
		/// Overloaded constructor.
		/// Use this to create a standard paragraph with the given text from
		/// string simpletext. Notice, the text will be styled as standard.
		/// You won't be able to style it bold, underline, etc. this will only
		/// occur if standard style attributes of the texdocumentocument are set to
		/// this.
		/// </summary>
		/// <param name="document">The IDocument.</param>
		/// <param name="style">The only accepted ParentStyle is Standard! All other styles will be ignored!</param>
		/// <param name="simpletext">The text which should be append within this paragraph.</param>
		public Paragraph (IDocument document, ParentStyles style, string simpletext) {
			this.Document = document;
			this.NewXmlNode ();
			if (style == ParentStyles.Standard)
				this.Init (ParentStyles.Standard.ToString ());
			else if (style == ParentStyles.Table)
				this.Init (ParentStyles.Table.ToString ());
			else if (style == ParentStyles.Text_20_body)
				this.Init (ParentStyles.Text_20_body.ToString ());

			//Attach simple text withhin the paragraph
			if (simpletext != null)
				this.TextContent.Add (new SimpleText (this.Document, simpletext));
			this._parentStyle = style;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Paragraph"/> class.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="document">The document.</param>
		internal Paragraph (XmlNode node, IDocument document) {
			this.Document = document;
			this.Node = node;
			this.InitStandards ();
		}

		/// <summary>
		/// Create the Paragraph.
		/// </summary>
		/// <param name="styleName">The style name.</param>
		private void Init (string styleName) {
			if (styleName != "Standard"
				&& styleName != "Table_20_Contents"
				&& styleName != "Text_20_body") {
				this.Style = (IStyle) new ParagraphStyle (this.Document, styleName);
				this.Document.Styles.Add (this.Style);
			}
			this.InitStandards ();
			this.StyleName = styleName;
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards () {
			this.TextContent = new ITextCollection ();
			this.Content = new IContentCollection ();
			this._mixedContent = new ArrayList ();

			if (this.Document is AODL.Document.TextDocuments.TextDocument)
				this.Document.DocumentMetadata.ParagraphCount += 1;

			this.TextContent.Inserted += new AODL.Document.Collections.CollectionWithEvents.CollectionChange (TextContent_Inserted);
			this.Content.Inserted += new AODL.Document.Collections.CollectionWithEvents.CollectionChange (Content_Inserted);
			this.TextContent.Removed += new AODL.Document.Collections.CollectionWithEvents.CollectionChange (TextContent_Removed);
			this.Content.Removed += new AODL.Document.Collections.CollectionWithEvents.CollectionChange (Content_Removed);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		private void NewXmlNode () {
			this.Node = this.Document.CreateNode ("p", "text");
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

		/// <summary>
		/// Append the xml from added IText object.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		private void TextContent_Inserted (int index, object value) {
			this.Node.AppendChild (((IText) value).Node);
			this._mixedContent.Add (value);

			if (((IText) value).Text != null) {
				try {
					if (this.Document is AODL.Document.TextDocuments.TextDocument) {
						string text     = ((IText)value).Text;
						this.Document.DocumentMetadata.CharacterCount += text.Length;
						string[] words  = text.Split(' ');
						this.Document.DocumentMetadata.WordCount += words.Length;
					}
				} catch (Exception) {
					//unhandled, only word and character count wouldn' be correct
				}
			}
		}

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
				if (this._content != null)
					foreach (IContent content in this._content)
						this.Node.RemoveChild (content.Node);

				this._content = value;

				if (this._content != null)
					foreach (IContent content in this._content)
						this.Node.AppendChild (content.Node);
			}
		}

		#endregion

		/// <summary>
		/// Content_s the inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Inserted (int index, object value) {
			this.Node.AppendChild (((IContent) value).Node);
			this._mixedContent.Add (value);
		}

		/// <summary>
		/// Texts the content_ removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void TextContent_Removed (int index, object value) {
			this.Node.RemoveChild (((IText) value).Node);
			this.RemoveMixedContent (value);
		}

		/// <summary>
		/// Content_s the removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Removed (int index, object value) {
			this.Node.RemoveChild (((IContent) value).Node);
			this.RemoveMixedContent (value);
		}

		/// <summary>
		/// Removes the mixed content
		/// </summary>
		/// <param name="value">The value.</param>
		private void RemoveMixedContent (object value) {
			if (this._mixedContent.Contains (value))
				this._mixedContent.Remove (value);
		}


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
		/// Create a deep clone of this paragraph object.
		/// </summary>
		/// <remarks>A possible Attached Style wouldn't be cloned!</remarks>
		/// <returns>
		/// A clone of this object.
		/// </returns>
		public object Clone () {
			Paragraph pargaphClone      = null;

			if (this.Document != null && this.Node != null) {
				MainContentProcessor mcp    = new MainContentProcessor(this.Document);
				pargaphClone = mcp.CreateParagraph (this.Node.CloneNode (true));
			}

			return pargaphClone;
		}

		#endregion
	}
}

/*
 * $Log: Paragraph.cs,v $
 * Revision 1.3  2006/02/02 21:55:59  larsbm
 * - Added Clone object support for many AODL object types
 * - New Importer implementation PlainTextImporter and CsvImporter
 * - New tests
 *
 * Revision 1.2  2006/01/29 18:52:14  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.11  2006/01/05 10:31:10  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 * Revision 1.10  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 * Revision 1.9  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.8  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.7  2005/10/23 16:47:48  larsbm
 * - Bugfix ListItem throws IStyleInterface not implemented exeption
 * - now. build the document after call saveto instead prepare the do at runtime
 * - add remove support for IText objects in the paragraph class
 *
 * Revision 1.6  2005/10/22 10:47:41  larsbm
 * - add graphic support
 *
 * Revision 1.5  2005/10/15 11:40:31  larsbm
 * - finished first step for table support
 *
 * Revision 1.4  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 * Revision 1.3  2005/10/08 12:31:33  larsbm
 * - better usabilty of paragraph handling
 * - create paragraphs with text and blank paragraphs with one line of code
 *
 * Revision 1.2  2005/10/08 08:19:25  larsbm
 * - added cvs tags
 *
 */