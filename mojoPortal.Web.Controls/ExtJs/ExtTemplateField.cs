// Author:					Joe Audette
// Created:				2007-11-04
// Last Modified:			2007-12-01
//		
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.

using System;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.Controls.ExtJs
{
    /// <summary>
    /// These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery
    /// Extends the TemplateField with a few custom properties.
    /// </summary>
    [Obsolete("These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery")]
    public class ExtTemplateField : TemplateField
    {
        private bool isClientHideable = false;
        private bool isClientSortable = false;
        private int columnWidth = -1;

        /// <summary>
        /// true if you want column to be hideable by client side javascript
        /// </summary>
        public bool IsClientHideable
        {
            get { return isClientHideable; }
            set { isClientHideable = value; }
        }

        /// <summary>
        /// true if you want the column to be sortable by javascript. 
        /// Note this only sorts the already fetched data.
        /// </summary>
        public bool IsClientSortable
        {
            get { return isClientSortable; }
            set { isClientSortable = value; }
        }

        /// <summary>
        /// If set to a positive number will be used for column width
        /// </summary>
        public int ColumnWidth
        {
            get { return columnWidth; }
            set { columnWidth = value; }
        }
    }
}
