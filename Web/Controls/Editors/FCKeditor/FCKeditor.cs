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
 * File Name: FCKeditor.cs
 * 	This is the FCKeditor Asp.Net control.
 * 
 * File Authors:
 * 		Frederico Caldeira Knabben (fredck@fckeditor.net)
 * 
 * Joe Audette changes
 * 2007-01-19 added handling for when javascript is disabled
 * to use a textarea that is hidden when javascript is enabled
 * 2007-05-26 moved into mojoPortal.Web.Editor and changed namespace
 * 2007-05-30 added config property for FlashBrowserURL
 * 2007-11-22 made it possible to use FCKeditor in Safari and Opera by config settings, or to force textarea
 * 2008-09-03 changed config to support Safari and Chroms, it Works!
 * 2008-11-02 
 */

using System ;
using System.Web.UI ;
using System.Web.UI.WebControls ;
using System.Collections.Specialized;
using System.ComponentModel ;
using System.Configuration;
using System.Text.RegularExpressions ;
using System.Globalization ;
using System.Security.Permissions ;
using log4net;

namespace mojoPortal.Web.Editor
{
	public enum LanguageDirection
	{
		LeftToRight,
		RightToLeft
	}

	[ DefaultProperty("Value") ]
	[ ValidationProperty("Value") ]
	[ ToolboxData("<{0}:FCKeditor runat=server></{0}:FCKeditor>") ]
	[ Designer("mojoPortal.Web.Editor.FCKeditorDesigner") ]
	[ ParseChildren(false) ]
	public class FCKeditor : System.Web.UI.Control, IPostBackDataHandler
	{
        private static readonly ILog log = LogManager.GetLogger(typeof(FCKeditor));

		public FCKeditor()
		{
           
        }

		#region Base Configurations Properties

        private FCKeditorConfigurations configSettings;

        [Browsable(false)]
        public FCKeditorConfigurations Config
        {
            get
            {
                if (configSettings == null)
                    configSettings = new FCKeditorConfigurations();
                return configSettings;
            }
        }

        
        private string innerValue = string.Empty;

        [DefaultValue("")]
        public string Value
        {
            get { return innerValue; }
            set { innerValue = value; }
        }

       
        private string basePath = ConfigurationManager.AppSettings["FCKeditor:BasePath"];
		/// <summary>
		/// <p>
		///		Sets or gets the virtual path to the editor's directory. It is
		///		relative to the current page.
		/// </p>
		/// <p>
		///		The default value is "/FCKeditor/".
		/// </p>
		/// <p>
		///		The base path can be also set in the Web.config file using the 
		///		appSettings section. Just set the "FCKeditor:BasePath" for that. 
		///		For example:
		///		<code>
		///		&lt;configuration&gt;
		///			&lt;appSettings&gt;
		///				&lt;add key="FCKeditor:BasePath" value="/scripts/FCKeditor/" /&gt;
		///			&lt;/appSettings&gt;
		///		&lt;/configuration&gt;
		///		</code>
		/// </p>
		/// </summary>
		[ DefaultValue( "/FCKeditor/" ) ]
		public string BasePath
		{
			get 
			{ 
				return basePath;
			}
            set { basePath = value; }
		}

       
        private string toolbarSet = "Default";

        [DefaultValue("Default")]
        public string ToolbarSet
        {
            get { return toolbarSet; }
            set { toolbarSet = value; }
        }

      
		#endregion

		#region Appearence Properties

        private Unit editorWidth = Unit.Percentage(100);

        [Category("Appearence")]
        [DefaultValue("100%")]
        public Unit Width
        {
            get { return editorWidth; }
            set { editorWidth = value; }
        }

        private Unit editorHeight = Unit.Pixel(200);

        [Category("Appearence")]
        [DefaultValue("200px")]
        public Unit Height
        {
            get { return editorHeight; }
            set { editorHeight = value; }
        }

       
		#endregion

		#region Configurations Properties

        /// <summary>
        /// This option sets the DOCTYPE to be used in the editable area. The actual rendering depends on the value set here.
        /// FCKConfig.DocType = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">' ;
        /// </summary>
        [Category("Configurations")]
        public string DocType
        {
            set { this.Config["DocType"] = value; }
        }

        /// <summary>
        /// use this setting to point the editor to a secondary configuration file that overrides the configurations defined in the default fckconfig.js file
        /// </summary>
		[ Category("Configurations") ]
		public string CustomConfigurationsPath
		{
			set { this.Config["CustomConfigurationsPath"] = value ; }
		}


        /// <summary>
        /// simulate the output of your site inside FCKeditor, including background colors, font styles and sizes and custom CSS definitions. To do that, just point this setting to the CSS file that you want the editor to use
        /// </summary>
		[ Category("Configurations") ]
		public string EditorAreaCSS
		{
			set { this.Config["EditorAreaCSS"] = value ; }
		}

        /// <summary>
        /// defines the base URL to be used for all resources loaded in the editor area, including images and links
        /// </summary>
		[ Category("Configurations") ]
		public string BaseHref
		{
			set { this.Config["BaseHref"] = value ; }
		}

		[ Category("Configurations") ]
		public string SkinPath
		{
			set { this.Config["SkinPath"] = value ; }
		}

		[ Category("Configurations") ]
		public string PluginsPath
		{
			set { this.Config["PluginsPath"] = value ; }
		}

        /// <summary>
        /// This setting controls the behavior in the Paste from Word dialog. It's default value is false, and that way it keeps the 
        /// same behavior in the clean up 
        /// function as it was present in FCKeditor 2.3.2 and previous versions. If it's switched to true then the routine will 
        /// prefer to keep the HTML structure of the data instead of modifying it to keep the look as it was in word.
        /// Enabling this setting allows to keep a properly structured document as it was created in word as well as keep anchors and you 
        /// can use CSS to make it look like it was in Word.
        /// </summary>
        [Category("Configurations")]
        public bool CleanWordKeepsStructure
        {
            set { this.Config["CleanWordKeepsStructure"] = (value ? "true" : "false"); }
        }

        /// <summary>
        /// By default, FCKeditor is set to edit pieces of HTML to be used on pages. This is the most common usage of it.
        /// But, in many situations, we want to give users the ability of editing entire pages, 
        /// from <html> to </html>. To do that, just set the "FullPage" configuration option to "true".
        /// </summary>
		[ Category("Configurations") ]
		public bool FullPage
		{
			set { this.Config["FullPage"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool Debug
		{
			set { this.Config["Debug"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool AutoDetectLanguage
		{
			set { this.Config["AutoDetectLanguage"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public string DefaultLanguage
		{
			set { this.Config["DefaultLanguage"] = value ; }
		}

		[ Category("Configurations") ]
		public LanguageDirection ContentLangDirection
		{
			set { this.Config["ContentLangDirection"] = ( value == LanguageDirection.LeftToRight ? "ltr" : "rtl" )  ; }
		}

		[ Category("Configurations") ]
		public bool EnableXHTML
		{
			set { this.Config["EnableXHTML"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool EnableSourceXHTML
		{
			set { this.Config["EnableSourceXHTML"] = ( value ? "true" : "false" ) ; }
		}

        [Category("Configurations")]
        public bool FillEmptyBlocks
        {
            set { this.Config["FillEmptyBlocks"] = (value ? "true" : "false"); }
        }

		[ Category("Configurations") ]
		public bool FormatSource
		{
			set { this.Config["FormatSource"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool FormatOutput
		{
			set { this.Config["FormatOutput"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public string FormatIndentator
		{
			set { this.Config["FormatIndentator"] = value ; }
		}

		[ Category("Configurations") ]
		public bool GeckoUseSPAN
		{
			set { this.Config["GeckoUseSPAN"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool StartupFocus
		{
			set { this.Config["StartupFocus"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool ForcePasteAsPlainText
		{
			set { this.Config["ForcePasteAsPlainText"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool ForceSimpleAmpersand
		{
			set { this.Config["ForceSimpleAmpersand"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public int TabSpaces
		{
			set { this.Config["TabSpaces"] = value.ToString( CultureInfo.InvariantCulture ) ; }
		}

		[ Category("Configurations") ]
		public bool UseBROnCarriageReturn
		{
			set { this.Config["UseBROnCarriageReturn"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool ToolbarStartExpanded
		{
			set { this.Config["ToolbarStartExpanded"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool ToolbarCanCollapse
		{
			set { this.Config["ToolbarCanCollapse"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public string FontColors
		{
			set { this.Config["FontColors"] = value ; }
		}

		[ Category("Configurations") ]
		public string FontNames
		{
			set { this.Config["FontNames"] = value ; }
		}

		[ Category("Configurations") ]
		public string FontSizes
		{
			set { this.Config["FontSizes"] = value ; }
		}

		[ Category("Configurations") ]
		public string FontFormats
		{
			set { this.Config["FontFormats"] = value ; }
		}

		[ Category("Configurations") ]
		public string StylesXmlPath
		{
			set { this.Config["StylesXmlPath"] = value ; }
		}

        [Category("Configurations")]
        public string TemplatesXmlPath
        {
            set { this.Config["TemplatesXmlPath"] = value; }
        }

		[ Category("Configurations") ]
		public string LinkBrowserURL
		{
			set { this.Config["LinkBrowserURL"] = value ; }
		}



        [Category("Configurations")]
        public bool ImageUpload
        {
            set { this.Config["ImageUpload"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public bool FlashUpload
        {
            set { this.Config["FlashUpload"] = (value ? "true" : "false"); }
        }

        [Category("Configurations")]
        public bool LinkUpload
        {
            set { this.Config["LinkUpload"] = (value ? "true" : "false"); }
        }





		[ Category("Configurations") ]
		public string ImageBrowserURL
		{
			set { this.Config["ImageBrowserURL"] = value ; }
		}

        [Category("Configurations")]
        public string FlashBrowserURL
        {
            set { this.Config["FlashBrowserURL"] = value; }
        }

        [Category("Configurations")]
        public string SmileyPath
        {
            set { this.Config["SmileyPath"] = value; }
        }

        [Category("Configurations")]
        public bool HtmlEncodeOutput
        {
            set { this.Config["HtmlEncodeOutput"] = (value ? "true" : "false"); }
        }

		#endregion

		#region Rendering

		protected override void Render(HtmlTextWriter writer)
		{
            if (this.Site != null && this.Site.DesignMode)
            {
                // render to the designer
                writer.Write("[" + this.ID + "]");
            }
            else
            {

                writer.Write("<div>");

                if (this.CheckBrowserCompatibility())
                {
                    string sLink = this.BasePath;
                    if (sLink.StartsWith("~"))
                        sLink = this.ResolveUrl(sLink);

                    string sFile =
                        Page.Request.QueryString["fcksource"] == "true" ?
                            "fckeditor.original.html" :
                            "fckeditor.html";

                    sLink += "editor/" + sFile + "?InstanceName=" + this.ClientID;
                    if (this.ToolbarSet.Length > 0) sLink += "&amp;Toolbar=" + this.ToolbarSet;

                    // this is for when javascript is disabled
                    writer.Write(
                        "<noscript><textarea name=\"{0}\" rows=\"4\" cols=\"40\" style=\"width: {1}; height: {2}\" >{3}</textarea></noscript>",
                            this.UniqueID.Replace(this.ID, "editor_noscript"),
                            this.Width,
                            this.Height,
                            System.Web.HttpUtility.HtmlEncode(this.Value));

                    // Render the linked hidden field.
                    writer.Write(
                        "<input type=\"hidden\" id=\"{0}\" name=\"{1}\" value=\"{2}\" />",
                            this.ClientID,
                            this.UniqueID,
                            System.Web.HttpUtility.HtmlEncode(this.Value));

                    // Render the configurations hidden field.
                    writer.Write(
                        "<input type=\"hidden\" id=\"{0}___Config\" value=\"{1}\" />",
                            this.ClientID,
                            this.Config.GetHiddenFieldString());

                    if ((ConfigurationManager.AppSettings["CombineJavaScript"] != null)
                        && (ConfigurationManager.AppSettings["CombineJavaScript"] == "true"))
                    {
                        // for some reason when we combine the javascript files and move them to the bottom
                        // (not talking about fck javascript, but the rest of the js in the page)
                        // the editor height is initialized to 0 and then doesn't get resized by the inline js
                        // in the else clause below, so we just initialize it to the real height.
                        //downside is if javscript is disabled there is bot a big text area which is needed and a big 
                        // empy iframe which is not needed

                        writer.Write(
                            "<iframe id=\"{0}___Frame\" src=\"{1}\" width=\"{2}\" height=\"{3}\" frameborder=\"0\" scrolling=\"no\" title='editor area'></iframe>",
                                this.ClientID,
                                sLink,
                                this.Width,
                                this.Height.ToString().Replace("px", string.Empty));

                    }
                    else
                    {
                        // Render the editor iframe.
                        // set height of frame to 0, 
                        // if javascript is disabled it will be invisible
                        // and the textarea above will be visible
                        writer.Write(
                            "<iframe id=\"{0}___Frame\" src=\"{1}\" width=\"{2}\" height=\"{3}\" frameborder=\"0\" scrolling=\"no\" title='editor area'></iframe>",
                                this.ClientID,
                                sLink,
                                this.Width,
                                Unit.Parse("0", CultureInfo.InvariantCulture));



                        // if javascript is enabled this will set the height of the frame to make it visible
                        writer.Write("<script type=\"text/javascript\" >  "
                            + "var fra_" + this.ClientID
                            + " = document.getElementById('" + this.ClientID + "___Frame'); "
                            + "fra_" + this.ClientID + ".height = " + this.Height.ToString().Replace("px", "")
                            + "; </script>");

                        //string setupScript = "<script type=\"text/javascript\" >  "
                        //    + "var fra_" + this.ClientID
                        //    + " = document.getElementById('" + this.ClientID + "___Frame'); "
                        //    + "fra_" + this.ClientID + ".height = " + this.Height.ToString().Replace("px", "")
                        //    + "; </script>";

                        //Page.ClientScript.RegisterStartupScript(this.GetType(), this.UniqueID, setupScript);
                    }


                }
                else
                {
                    writer.Write(
                        "<textarea name=\"{0}\" rows=\"4\" cols=\"40\" style=\"width: {1}; height: {2}\" >{3}</textarea>",
                            this.UniqueID,
                            this.Width,
                            this.Height,
                            System.Web.HttpUtility.HtmlEncode(this.Value));
                }

                writer.Write("</div>");

            }
		}

		

		#endregion

		#region Postback Handling

		public bool LoadPostData(
            string postDataKey, 
            NameValueCollection postCollection)
		{
            if (postDataKey == null) return false;
            if (postCollection == null) return false;

            if (
                (postCollection[postDataKey] != null)
                &&( postCollection[postDataKey] != this.Value )
                )
			{
				this.Value = postCollection[postDataKey] ;
				return true ;
			}
            // this is for when javascript is disabled
            if (this.ID != null)
            {
                String noScriptKey = postDataKey.Replace(this.ID, "editor_noscript");

                noScriptKey = this.UniqueID.Replace(this.ID, "editor_noscript");

                if ((postCollection[noScriptKey] != null)
                    && (postCollection[noScriptKey] != this.Value)
                    )
                {
                    this.Value = postCollection[noScriptKey];
                    return true;
                }

                noScriptKey = postDataKey.Replace(this.ID, "xhtml");
                if ((postCollection[noScriptKey] != null)
                    && (postCollection[noScriptKey] != this.Value)
                    )
                {
                    this.Value = postCollection[noScriptKey];
                    return true;
                }
            }

            
			return false ;
		}

		public void RaisePostDataChangedEvent()
		{
			// Do nothing
		}

		#endregion

        public bool CheckBrowserCompatibility()
        {
            //System.Web.HttpBrowserCapabilities oBrowser = Page.Request.Browser;
            string loweredBrowser = Page.Request.Browser.Browser.ToLower();

            log.Debug("Current Browser is: " + loweredBrowser);

            if (loweredBrowser.Contains("ie"))
            {
                log.Debug("FCKeditor returning true for ie ");
                return true;
            }

            if (loweredBrowser.Contains("firefox"))
            {
                log.Debug("FCKeditor returning true for firefox ");
                return true;
            }

            if (loweredBrowser.Contains("mozilla"))
            {
                log.Debug("FCKeditor returning true for mozilla ");
                return true;
            }

            if (loweredBrowser.Contains("netscape"))
            {
                log.Debug("FCKeditor returning true for netscape ");
                return true;
            }

            // this is what google chrome browser reports as the browser
            // this is returning true for Safari on Windows as well
            // but FCKeditor works as of 2008-09-03!!!
            if(loweredBrowser.Contains("applemac-safari"))
            {
                log.Debug("FCKeditor returning true for chrome/applemac-safari ");
                return true;

            }

            
            if (loweredBrowser.Contains("safari"))
            {
                if (
                    (ConfigurationManager.AppSettings["ForceFCKToDegradeToTextAreaInSafari"] != null)
                    && (string.Equals(ConfigurationManager.AppSettings["ForceFCKToDegradeToTextAreaInSafari"], "true", StringComparison.InvariantCultureIgnoreCase))
                    )
                {
                    log.Debug("FCKeditor returning false for safari ");
                    return false;

                }
                else
                {
                    log.Debug("FCKeditor returning true for safari ");
                    return true;
                }
            }
            



            if (loweredBrowser.Contains("opera"))
            {
                if (
                    (ConfigurationManager.AppSettings["ForceFCKToDegradeToTextAreaInOpera"] != null)
                    && (string.Equals(ConfigurationManager.AppSettings["ForceFCKToDegradeToTextAreaInOpera"], "true", StringComparison.InvariantCultureIgnoreCase))
                    )
                {
                    log.Debug("FCKeditor returning false for opera ");
                    return false;

                }
                else
                {
                    log.Debug("FCKeditor returning true for opera ");
                    return true;

                }
            }

            if (!Page.Request.Browser.Frames)
            {
                log.Debug("FCKeditor returning false for no frames support ");
                return false;
            }

            string loweredUserAgent = this.Page.Request.UserAgent.ToLowerInvariant();

            if (loweredUserAgent.Contains("msie"))
            {
                return true;
            }

            if (loweredUserAgent.Contains("firefox"))
            {
                return true;
            }

            if (loweredUserAgent.Contains("mozilla"))
            {
                return true;
            }

            
            //return (oBrowser.EcmaScriptVersion.Major >= 1);

        
            // Internet Explorer 5.5+ for Windows
            //if (oBrowser.Browser == "IE" && (oBrowser.MajorVersion >= 6 || (oBrowser.MajorVersion == 5 && oBrowser.MinorVersion >= 0.5)) && oBrowser.Win32)
            //{
            //    log.Debug("FCKeditor returning true for IE check ");
            //    return true;
            //}
            //else
            //{
            //    log.Debug("FCKeditor entering final else in browser check ");
            //    Match oMatch = Regex.Match(this.Page.Request.UserAgent, @"(?<=Gecko/)\d{8}");
            //    return (oMatch.Success && int.Parse(oMatch.Value, CultureInfo.InvariantCulture) >= 20030210);
            //}

            if (ConfigurationManager.AppSettings["UseFCKeditorForUnknownBrowsers"] != null)
            {
                if (ConfigurationManager.AppSettings["UseFCKeditorForUnknownBrowsers"].ToLowerInvariant() == "true")
                {
                    return true;
                }

            }

            return false;

        }
	}
}
