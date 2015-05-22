// Author:					Joe Audette
// Created:				2007-11-04
// Last Modified:			2007-11-04
//		
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.

using System;
using System.Web.UI;

namespace mojoPortal.Web.Controls.ExtJs
{
    /// <summary>
    ///	 These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery
    /// </summary>
    [Obsolete("These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery")]
    public static class ExtScriptManager
    {

        public static void IncludeBase(Page currentPage, string extJsBasePath)
        {
            currentPage.ClientScript.RegisterClientScriptBlock(
                typeof(ExtScriptManager),
                "ext-base", "\n<script type=\"text/javascript\" src=\""
                + currentPage.ResolveUrl(extJsBasePath + "adapter/ext/ext-base.js") + "\" ></script>");

        }

        public static void IncludeAll(Page currentPage, string extJsBasePath)
        {
            currentPage.ClientScript.RegisterClientScriptBlock(
                typeof(ExtScriptManager),
                "ext-all", "\n<script type=\"text/javascript\" src=\""
                + currentPage.ResolveUrl(extJsBasePath + "ext-all.js") + "\" ></script>");

        }

        public static void IncludeTableGrid(Page currentPage, string extJsBasePath)
        {
            currentPage.ClientScript.RegisterClientScriptBlock(
                typeof(ExtScriptManager),
                "ext-tablegrid", "\n<script type=\"text/javascript\" src=\""
                + currentPage.ResolveUrl(extJsBasePath + "ext-mojo/TableGrid.js") + "\" ></script>");

        }

        public static void IncludeAllDebug(Page currentPage,string extJsBasePath)
        {
            currentPage.ClientScript.RegisterClientScriptBlock(
                typeof(ExtScriptManager),
                "ext-all", "\n<script type=\"text/javascript\" src=\""
                + currentPage.ResolveUrl(extJsBasePath + "ext-all-debug.js") + "\" ></script>");

        }


        //public static void IncludePropertyGrid(Page currentPage, string extJsBasePath)
        //{
        //    currentPage.ClientScript.RegisterClientScriptBlock(
        //        typeof(ExtScriptManager),
        //        "ext-propsgrid", "\n<script type=\"text/javascript\" src=\""
        //        + currentPage.ResolveUrl(extJsBasePath + "package/grid/PropsGrid.js") + "\" ></script>");

        //}


    }
}
