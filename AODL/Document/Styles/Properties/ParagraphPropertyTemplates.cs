
/*
 * http://docs.oasis-open.org/office/v1.2/os/OpenDocument-v1.2-os-part1.html#__RefHeading__1419828_253892949
*/

/*
 * License: 
 * GNU Lesser General Public License. You should recieve a
 * copy of this within the library. If not you will find
 * a whole copy at http://www.gnu.org/licenses/lgpl.html .
 * 
 * Author:
 * Copyright 2023, diub - Dipl.-Ing. Uwe Barth, diub@diub.de
 * 
 */

using System.ComponentModel;
using System.Xml;

namespace AODL.Document.Styles.Properties {

public partial class ParagraphProperties : IProperty {

	/// <summary>
	/// 2023-01-21: diub
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="Name"></param>
	/// <param name="Value"></param>
	private void SetProperty<T> (string Name, T Value) {
		string fo;

		fo = "@fo:" + Name;
		XmlNode xn = this._node.SelectSingleNode (fo, this.Style.Document.NamespaceManager);
		if (xn == null)
			this.CreateAttribute (Name, Value.ToString (), "fo");
		this._node.SelectSingleNode (fo, this.Style.Document.NamespaceManager).InnerText = Value.ToString ();
	}

	/// <summary>
	/// 2023-01-21: diub
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="Name"></param>
	/// <param name="DefaultValue"></param>
	/// <returns></returns>
	private T GetProperty<T> (string Name, T DefaultValue) {
		string fo;
		TypeConverter tc;

		fo = "@fo:" + Name;
		XmlNode xn = this._node.SelectSingleNode (fo, this.Style.Document.NamespaceManager);
		if (xn != null) {
			tc = TypeDescriptor.GetConverter (typeof (T));
			return (T) tc.ConvertTo (xn.InnerText, typeof (T));
		}
		return DefaultValue;

	}

	/// <summary>
	/// 
	/// </summary>
	public string MarginTop {
		set {
			SetProperty<string> ("margin-top", value); 
		}
		get {
			return GetProperty<string> ("margin-top", null); 
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public string MarginRight {
		set {
			SetProperty<string> ("margin-right", value); 
		}
		get {
			return GetProperty<string> ("margin-right", null); 
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public string MarginBottom {
		set {
			SetProperty<string> ("margin-bottom", value); 
		}
		get {
			return GetProperty<string> ("margin-bottom", null); 
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public string MarginLeft {
		set {
			SetProperty<string> ("margin-left", value); 
		}
		get {
			return GetProperty<string> ("margin-left", null); 
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public string BreakBefore {
		set {
			SetProperty<string> ("break-before", value); 
		}
		get {
			return GetProperty<string> ("break-before", null); 
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public string BreakAfter {
		set {
			SetProperty<string> ("break-after", value); 
		}
		get {
			return GetProperty<string> ("break-after", null); 
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public string Border {
		set {
			SetProperty<string> ("border", value); 
		}
		get {
			return GetProperty<string> ("border", null); 
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public string BackgroundColor {
		set {
			SetProperty<string> ("background-color", value); 
		}
		get {
			return GetProperty<string> ("background-color", null); 
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public string Padding {
		set {
			SetProperty<string> ("padding", value); 
		}
		get {
			return GetProperty<string> ("padding", null); 
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public string LineHeight {
		set {
			SetProperty<string> ("line-height", value); 
		}
		get {
			return GetProperty<string> ("line-height", null); 
		}
	}

	/// <summary>
	/// start (default), end, center, justify
	/// </summary>
	public string TextAlign {
		set {
			SetProperty<string> ("text-align", value); 
		}
		get {
			return GetProperty<string> ("text-align", null); 
		}
	}

	/// <summary>
	/// Hurenkinder
	/// </summary>
	public int Orphans {
		set {
			SetProperty<int> ("orphans", value); 
		}
		get {
			return GetProperty<int> ("orphans", 0); 
		}
	}

	/// <summary>
	/// Schusterjungen
	/// </summary>
	public int Widows {
		set {
			SetProperty<int> ("widows", value); 
		}
		get {
			return GetProperty<int> ("widows", 0); 
		}
	}

	/// <summary>
	/// Auto (default), Always
	/// </summary>
	public string KeepWithNext {
		set {
			SetProperty<string> ("keep-with-next", value); 
		}
		get {
			return GetProperty<string> ("keep-with-next", null); 
		}
	}


}	// class

} //	namespace	2023-01-21 - 16.28.51

