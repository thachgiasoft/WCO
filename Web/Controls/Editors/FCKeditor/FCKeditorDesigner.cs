/*
 * FCKeditor - The text editor for internet
 * Copyright (C) 2003-2005 Frederico Caldeira Knabben
 * 
 * Licensed under the terms of the GNU Lesser General Public License:
 * 		http://www.opensource.org/licenses/lgpl-license.php
 * 
 * For further information visit:
 * 		http://www.fckeditor.net/
 * 
 * "Support Open Source software. What about a donation today?"
 * 
 * File Name: FCKeditorDesigner.cs
 * 	The EditorDesigner class defines the editor visualization at design 
 * 	time. 
 * 
 * File Authors:
 * 		Frederico Caldeira Knabben (fredck@fckeditor.net)
 */

using System ;
using System.Globalization ;

namespace mojoPortal.Web.Editor
{
	public class FCKeditorDesigner : System.Web.UI.Design.ControlDesigner
	{
		public FCKeditorDesigner()
		{
		}

		public override string GetDesignTimeHtml() 
		{
			FCKeditor control = (FCKeditor)Component ;
			return String.Format( CultureInfo.InvariantCulture,
				"<div><table width=\"{0}\" height=\"{1}\" bgcolor=\"#f5f5f5\" bordercolor=\"#c7c7c7\" cellpadding=\"0\" cellspacing=\"0\" border=\"1\"><tr><td valign=\"middle\" align=\"center\">FCKeditor V2 - <b>{2}</b></td></tr></table></div>",
					control.Width,
					control.Height,
					control.ID ) ;
		}
	}
}
