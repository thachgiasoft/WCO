/// Author:		        Joe Audette
/// Created:            2007-05-29
/// Last Modified:      2009-08-31
/// 
/// Licensed under the terms of the GNU Lesser General Public License:
///	http://www.opensource.org/licenses/lgpl-license.php
///
/// You must not remove this notice, or any other, from this software.

using System;
using System.Globalization;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.Editor
{
    
    public class FCKeditorAdapter : IWebEditor
    {
        #region Constructors

        public FCKeditorAdapter() 
        {
            
            InitializeEditor();
            ConfigureEditor();
        }

        #endregion

        #region Private Properties

        private FCKeditor fckEditor = null;
        private string scriptBaseUrl = "~/ClientScript/FCKeditor/";
        //private string appRelativeFileBrowserPath = "~/ClientScript/FCKeditor/editor/filemanager/browser/default/browser.html";
        //private string appRelativeImageBrowserPath = "~/ClientScript/FCKeditor/editor/filemanager/browser/default/browser.html";
        //private string appRelativeFileBrowserServicePath = "~/ClientScript/FCKeditor/editor/filemanager/connectors/aspx/connector.aspx";
        //private string appRelativeImageBrowserServicePath = "~/ClientScript/FCKeditor/editor/filemanager/connectors/aspx/connector.aspx";
        //private string appRelativeLinkBrowserServicePath = "~/ClientScript/FCKeditor/editor/filemanager/connectors/aspx/connector.aspx";
        private string stylesXmlPath = "~/ClientScript/FCKeditor/fckstyles.xml";

        //mojoportal specific
        //private string appRelativeUserImageServicePath = "/Dialog/UserImageConnector.aspx";


        private string siteRoot = string.Empty;
        private string skinName = "default"; // default, office2003, silver
        private Unit editorWidth = Unit.Percentage(98);
        private Unit editorHeight = Unit.Pixel(350);
        private string editorCSSUrl = string.Empty;
        private Direction textDirection = Direction.LeftToRight;
        private ToolBar toolBar = ToolBar.AnonymousUser;
        private bool setFocusOnStart = false;
        private bool fullPageMode = false;
        //private bool useConnector = true;
        private bool useFullyQualifiedUrlsForResources = false;

        #endregion

        #region Public Properties

        public string ControlID
        {
            get
            {
                InitializeEditor();
                return fckEditor.ID;
            }
            set
            {
                InitializeEditor();
                fckEditor.ID = value;
            }
        }

        public string ClientID
        {
            get
            {
                InitializeEditor();
                return fckEditor.ClientID;
            }

        }

        public string Text
        {
            get 
            {
                InitializeEditor();
                return fckEditor.Value; 
            }
            set 
            {
                InitializeEditor();
                fckEditor.Value = value; 
            }
        }

        public string ScriptBaseUrl
        {
            get
            {
                return scriptBaseUrl;
            }
            set
            {
                //scriptBaseUrl = value;
                
                
            }
        }

        public string SkinName
        {
            get
            {
                return skinName;
            }
            set
            {
                skinName = value;
                //if (skinName == "normal") skinName = "default";
                //InitializeEditor();
                //fckEditor.SkinPath = scriptBaseUrl
                //    + "editor/skins/" + skinName + "/";
                
            }
        }

        public string SiteRoot
        {
            get
            {
                return siteRoot;
            }
            set
            {
                siteRoot = value;
                
                InitializeEditor();

                fckEditor.BaseHref = fckEditor.ResolveUrl(siteRoot);
                ConfigurePaths();
            }
        }

        public string EditorCSSUrl
        {
            get
            {
                return editorCSSUrl;
            }
            set
            {
                editorCSSUrl = value;
                if (editorCSSUrl.Length > 0)
                {
                    InitializeEditor();
                    fckEditor.Config["EditorAreaCSS"] = editorCSSUrl;
                }
            }
        }

        public Unit Width
        {
            get
            {
                return editorWidth;
            }
            set
            {
                InitializeEditor();
                fckEditor.Width = value;
                editorWidth = value;
            }
        }

        public Unit Height
        {
            get
            {
                return editorHeight;
            }
            set
            {
                InitializeEditor();
                fckEditor.Height = value;
                editorHeight = value;
            }
        }

        public Direction TextDirection
        {
            get
            {
                return textDirection;
            }
            set
            {
                if (value == Direction.RightToLeft)
                {
                    InitializeEditor();
                    fckEditor.Config["ContentLangDirection"] = "rtl";
                }
                textDirection = value;
            }
        }

        public ToolBar ToolBar
        {
            get
            {
                return toolBar;
            }
            set
            {
                toolBar = value;
                SetToolBar();
            }
        }

        public bool SetFocusOnStart
        {
            get { return setFocusOnStart; }
            set
            {
                setFocusOnStart = value;
                InitializeEditor();
                fckEditor.StartupFocus = setFocusOnStart;
            }
        }

        public bool FullPageMode
        {
            get { return fullPageMode; }
            set
            {
                fullPageMode = value;
                InitializeEditor();
                if(fullPageMode)
                fckEditor.FullPage = true;
            }
        }

        public bool UseFullyQualifiedUrlsForResources
        {
            get { return useFullyQualifiedUrlsForResources; }
            set
            {
                useFullyQualifiedUrlsForResources = value;
                ConfigurePaths();
                
            }
        }

        #endregion

        #region Private Methods

        private void InitializeEditor()
        {
            if (fckEditor == null) fckEditor = new FCKeditor();

            if (ConfigurationManager.AppSettings["FCKeditor:CustomConfigPath"] != null)
                fckEditor.CustomConfigurationsPath = fckEditor.ResolveUrl(ConfigurationManager.AppSettings["FCKeditor:CustomConfigPath"]);

            fckEditor.HtmlEncodeOutput = false;


            if(ConfigurationManager.AppSettings["FCKeditor:BasePath"] != null)
                scriptBaseUrl = ConfigurationManager.AppSettings["FCKeditor:BasePath"];

            if (ConfigurationManager.AppSettings["FCKeditor:StylesXmlPath"] != null)
            {
                stylesXmlPath = fckEditor.ResolveUrl(ConfigurationManager.AppSettings["FCKeditor:StylesXmlPath"]);
                if (stylesXmlPath.EndsWith(".ashx")) { stylesXmlPath += "?cb=" + Guid.NewGuid().ToString(); } //prevent cache
            }
            else
            {
                stylesXmlPath = scriptBaseUrl + "fckstyles.xml";
            }

            

            if(scriptBaseUrl.StartsWith("~/"))
            {
                scriptBaseUrl = fckEditor.ResolveUrl(scriptBaseUrl); 
            }

        }

        private void ConfigureEditor()
        {
            fckEditor.CustomConfigurationsPath = "";

            fckEditor.BasePath = scriptBaseUrl;
            fckEditor.SkinPath = scriptBaseUrl + "editor/skins/" + WebConfigSettings.FCKeditorSkin + "/";
            //fckEditor.BaseHref = fckEditor.ResolveUrl("~/");

            //fckEditor.PluginsPath = "";
            ConfigurePaths();
            


            if (editorCSSUrl.Length > 0)
            {
                fckEditor.Config["EditorAreaCSS"] = editorCSSUrl;
            }

            CultureInfo defaultCulture = SiteUtils.GetDefaultUICulture();

            fckEditor.AutoDetectLanguage = false;

            fckEditor.DefaultLanguage = defaultCulture.TwoLetterISOLanguageName;

            

            if ((textDirection == Direction.RightToLeft)||(defaultCulture.TextInfo.IsRightToLeft))
            {
                fckEditor.Config["ContentLangDirection"] = "rtl";
            }

            fckEditor.Width = editorWidth;
            fckEditor.Height = editorHeight;
            if (setFocusOnStart)
            {
                fckEditor.StartupFocus = true;
            }

            if ((ConfigurationManager.AppSettings["FCKeditor:Debug"] != null)
                && (ConfigurationManager.AppSettings["FCKeditor:Debug"] == "true"))
            {
                fckEditor.Debug = true;
            }
           
            SetToolBar();

           

            

            
        }

        private void ConfigurePaths()
        {
            if (fckEditor == null) return;

            if (stylesXmlPath.Contains("EditorStyles.ashx"))
            {
                fckEditor.StylesXmlPath = siteRoot + stylesXmlPath;

            }
            else
            {
                fckEditor.StylesXmlPath = stylesXmlPath;
            }

            
            fckEditor.ImageUpload = false;
            fckEditor.FlashUpload = false;
            fckEditor.LinkUpload = false;


            fckEditor.SmileyPath = fckEditor.ResolveUrl(WebConfigSettings.FCKeditorEmoticonsPath);


           


        }

        private void SetToolBar()
        {
            if (fckEditor == null) InitializeEditor();

            switch (toolBar)
            {
                case ToolBar.Full:
                    fckEditor.ToolbarSet = "Full";
                    fckEditor.ForcePasteAsPlainText = false;
                    fckEditor.Config["LinkBrowser"] = "true";
                    fckEditor.LinkBrowserURL = siteRoot + WebConfigSettings.FileDialogRelativeUrl + "?ed=fck&type=file";
                    fckEditor.ImageBrowserURL = siteRoot + WebConfigSettings.FileDialogRelativeUrl + "?ed=fck&type=image";
                    fckEditor.FlashBrowserURL = siteRoot + WebConfigSettings.FileDialogRelativeUrl + "?ed=fck&type=media";
                    break;

                case ToolBar.FullWithTemplates:
                    fckEditor.ToolbarSet = "FullWithTemplates";
                    fckEditor.ForcePasteAsPlainText = false;
                    fckEditor.Config["LinkBrowser"] = "true";

                    fckEditor.LinkBrowserURL = siteRoot + WebConfigSettings.FileDialogRelativeUrl + "?ed=fck&type=file";
                    fckEditor.ImageBrowserURL = siteRoot + WebConfigSettings.FileDialogRelativeUrl + "?ed=fck&type=image";
                    fckEditor.FlashBrowserURL = siteRoot + WebConfigSettings.FileDialogRelativeUrl + "?ed=fck&type=media";

                    fckEditor.Config["TemplatesXmlPath"] = siteRoot + "/Services/HtmlTemplates.ashx?cb=" + Guid.NewGuid().ToString(); //prevent caching with a guid param
                    fckEditor.Config["TemplateReplaceAll"] = "false";

                    break;

                case ToolBar.Newsletter:
                    fckEditor.ToolbarSet = "Newsletter";
                    fckEditor.ForcePasteAsPlainText = false;
                    fckEditor.Config["LinkBrowser"] = "true";
                    fckEditor.LinkBrowserURL = siteRoot + WebConfigSettings.FileDialogRelativeUrl + "?ed=fck&type=file";
                    fckEditor.ImageBrowserURL = siteRoot + WebConfigSettings.FileDialogRelativeUrl + "?ed=fck&type=image";
                    fckEditor.FlashBrowserURL = siteRoot + WebConfigSettings.FileDialogRelativeUrl + "?ed=fck&type=media";
                    fckEditor.Config["EditorAreaCSS"] = string.Empty;
                    fckEditor.Config["ProtectedTags"] = "style";
                    fckEditor.EnableXHTML = false;
                    fckEditor.DocType = string.Empty;
                    //fckEditor.Config
                        
                    break;

                case ToolBar.Forum:
                    fckEditor.ToolbarSet = "Forum";
                    fckEditor.Config["LinkDlgHideTarget"] = "true";
                    fckEditor.Config["LinkUpload"] = "false";
                    fckEditor.Config["LinkDlgHideAdvanced"] = "true";
                    fckEditor.Config["LinkBrowser"] = "false";
                    fckEditor.ForcePasteAsPlainText = true;
                    break;

                case ToolBar.ForumWithImages:
                    fckEditor.ToolbarSet = "ForumWithImages";
                    fckEditor.Config["LinkDlgHideTarget"] = "true";
                    fckEditor.Config["LinkUpload"] = "false";
                    fckEditor.Config["LinkDlgHideAdvanced"] = "true";
                    fckEditor.Config["LinkBrowser"] = "false";
                    fckEditor.ForcePasteAsPlainText = true;
                    fckEditor.ImageBrowserURL = siteRoot + WebConfigSettings.FileDialogRelativeUrl + "?ed=fck&type=image";
                   
                    break;

                case ToolBar.AnonymousUser:
                    fckEditor.ToolbarSet = "BlogComment";
                    fckEditor.Config["LinkDlgHideTarget"] = "true";
                    fckEditor.Config["LinkUpload"] = "false";
                    fckEditor.Config["LinkDlgHideAdvanced"] = "true";
                    fckEditor.Config["LinkBrowser"] = "false";
                    fckEditor.ForcePasteAsPlainText = true;
                    break;

                case ToolBar.SimpleWithSource:
                    fckEditor.ToolbarSet = "SimpleWithSource";
                    fckEditor.Config["LinkDlgHideTarget"] = "true";
                    fckEditor.Config["LinkUpload"] = "false";
                    fckEditor.Config["LinkDlgHideAdvanced"] = "true";
                    fckEditor.Config["LinkBrowser"] = "false";
                    fckEditor.ForcePasteAsPlainText = true;

                    break;
            }

        }

        #endregion

        #region Public Methods

        public Control GetEditorControl()
        {
            InitializeEditor();
            return fckEditor;
        }

        

        #endregion


        private static bool GetBoolPropertyFromConfig(string key, bool defaultValue)
        {
            if (ConfigurationManager.AppSettings[key] == null) return defaultValue;


            if (string.Equals(ConfigurationManager.AppSettings[key], 
                "true", StringComparison.InvariantCultureIgnoreCase)) return true;

            if (string.Equals(ConfigurationManager.AppSettings[key], 
                "false", StringComparison.InvariantCultureIgnoreCase)) return false;

            return defaultValue;


        }

    }
}
