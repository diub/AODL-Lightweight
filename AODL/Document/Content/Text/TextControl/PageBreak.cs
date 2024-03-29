﻿///*
// * $Id: LineBreak.cs,v 1.2 2006/02/05 20:02:25 larsbm Exp $
// */

///*
// * License: 
// * GNU Lesser General Public License. You should recieve a
// * copy of this within the library. If not you will find
// * a whole copy at http://www.gnu.org/licenses/lgpl.html .
// * 
// * Author:
// * Copyright 2006, Lars Behrmann, lb@OpenDocument4all.com
// * 
// * Last changes:
// * 
// */

//using AODL.Document.Styles;
//using System.Xml;

//namespace AODL.Document.Content.Text.TextControl {
//	/// <summary>
//	/// LineBreak represent a line break.
//	/// </summary>
//	public class PageBreak : IText {
//		/// <summary>
//		/// </summary>
//		/// <param name="document">The document.</param>
//		public PageBreak (IDocument document) {
//			this.Document = document;
//			this.NewXmlNode ();
//		}

//		/// <summary>
//		/// Create a new XmlNode.
//		/// </summary>
//		private void NewXmlNode () {
//			this.Node = this.Document.CreateNode ("line-break", "text");
//		}

//		#region IText Member

//		private XmlNode _node;
//		/// <summary>
//		/// The node that represent the text content.
//		/// </summary>
//		/// <value></value>
//		public XmlNode Node {
//			get {
//				return this._node;
//			}
//			set {
//				this._node = value;
//			}
//		}

//		/// <summary>
//		/// A tab stop doesn't have a text.
//		/// </summary>
//		/// <value></value>
//		public string Text {
//			get {
//				return null;
//			}
//			set {
//			}
//		}

//		private IDocument _document;
//		/// <summary>
//		/// The document to which this text content belongs to.
//		/// </summary>
//		/// <value></value>
//		public IDocument Document {
//			get {
//				return this._document;
//			}
//			set {
//				this._document = value;
//			}
//		}

//		/// <summary>
//		/// Is null no style is available.
//		/// </summary>
//		/// <value></value>
//		public IStyle Style {
//			get {
//				return null;
//			}
//			set {
//			}
//		}

//		/// <summary>
//		/// No style name available
//		/// </summary>
//		/// <value></value>
//		public string StyleName {
//			get {
//				return null;
//			}
//			set {
//			}
//		}

//		#endregion
//	}
//}

///*
// * $Log: LineBreak.cs,v $
// * Revision 1.2  2006/02/05 20:02:25  larsbm
// * - Fixed several bugs
// * - clean up some messy code
// *
// * Revision 1.1  2006/01/29 11:28:22  larsbm
// * - Changes for the new version. 1.2. see next changelog for details
// *
// */