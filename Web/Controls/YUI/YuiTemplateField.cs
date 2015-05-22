/// Author:					Joe Audette
/// Created:				2008-12-17
/// Last Modified:			2008-12-18
///		
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.

using System;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.UI
{
    /// <summary>
    /// These YUI controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery
    /// Extends the TemplateField with a few custom properties.
    /// </summary>
    [Obsolete("These YUI controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery")]
    public class YuiTemplateField : TemplateField
    {
        private string key = string.Empty;
        private string field = string.Empty;
        //private string label = string.Empty;
        private string abbr = string.Empty;
        private string formatter = string.Empty;
        private string parser = string.Empty;
        private int maxAutoWidth = 0;
        private int minWidth = 0;
        private bool selected = false;
        private bool hidden = false;
        private bool sortable = false;
        private bool resizable = false;
        private int columnWidth = 0;

        /// <summary>
        /// (Required) The unique name assigned to each Column. When a Column key maps to a DataSource field, cells of the 
        /// Column will automatically populate with the the corresponding data. When a key does not map to a DataSource field, 
        /// the cell can be populated manually, through the use of a formatter. Keys are also assigned by DataTable to DOM attributes 
        /// (with a prefix), but they are first sanitized to ensure a correct syntax for className and DOM ID usage. If a key is not 
        /// defined in the Column definition, one will be auto-generated.
        /// </summary>
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        /// <summary>
        /// The DataSource field mapped to the Column. By default, the field value is assigned to be the Column's key. 
        /// Implementers may specify a different field explicitly in the Column definition. This feature is useful when mapping multiple 
        /// Columns to a shared field, since keys must remain unique.
        /// </summary>
        public string Field
        {
            get { return field; }
            set { field = value; }
        }

        /// <summary>
        /// By default, the <th> element is populated with the Column's key. Supply a label to display a different header.
        /// </summary>
        //public string Label
        //{
        //    get { return label; }
        //    set { label = value; }
        //}

        /// <summary>
        /// Value for the <th> element's abbr attribute.
        /// </summary>
        public string Abbr
        {
            get { return abbr; }
            set { abbr = value; }
        }

        /// <summary>
        /// A function or a pointer to a function to handle HTML formatting of cell data.
        /// Or use built in YAHOO functions like: formatter: "email", formatter:"number", formatter:"date"
        /// </summary>
        public string Formatter
        {
            get { return formatter; }
            set { formatter = value; }
        }

        /// <summary>
        /// parser:"string", parser:"number",parser:"date", parser:myParser
        /// </summary>
        public string Parser
        {
            get { return parser; }
            set { parser = value; }
        }

        /// <summary>
        /// Upper limit pixel width that a Column should auto-size to when its width is not set. Please note that maxAutoWidth 
        /// validation is executed after cells are rendered, which may cause a visual flicker of content, especially on non-scrolling DataTables.
        /// </summary>
        public int MaxAutoWidth
        {
            get { return maxAutoWidth; }
            set { maxAutoWidth = value; }
        }

        /// <summary>
        /// Minimum pixel width. Please note that minWidth validation is executed after cells are rendered, which may cause a visual flicker of 
        /// content, especially on non-scrolling DataTables.
        /// </summary>
        public int MinWidth
        {
            get { return minWidth; }
            set { minWidth = value; }
        }


        /// <summary>
        /// True if Column is hidden.
        /// </summary>
        public bool Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }

        /// <summary>
        /// True if Column is sortable.
        /// </summary>
        public bool Sortable
        {
            get { return sortable; }
            set { sortable = value; }
        }

        /// <summary>
        /// True if Column is resizeable. The Drag & Drop Utility is required to enable this feature. 
        /// Only bottom-level and non-nested Columns are resizeble.
        /// </summary>
        public bool Resizable
        {
            get { return resizable; }
            set { resizable = value; }
        }

        /// <summary>
        /// True if Column is hidden.
        /// </summary>
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        /// <summary>
        /// Pixel width.
        /// </summary>
        public int ColumnWidth
        {
            get { return columnWidth; }
            set { columnWidth = value; }
        }
    }
}
