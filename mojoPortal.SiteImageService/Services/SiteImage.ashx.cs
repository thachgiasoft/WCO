// Created by Joe Audette 2009-07-14
// Last Modified 2009-07-14
// I put this in a separate project because of the reference to System.Windows.Forms
// and because I suspect this won't work in medium trust.
// TODO: test in medium trust

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Reflection;
using System.Web;

namespace mojoPortal.SiteImageService
{
    /// <summary>
    /// Generates or retrieves and image of a web page.
    /// </summary>
    public class SiteImageHandler : IHttpHandler
    {
        private int browserWidth = 1024;
        private int browserHeight = 768;
        private int thumbnailWidth = 205;
        private int thumbnailHeight = 154;
        private string siteUrl = "http://www.mojoportal.com";
        private string imageCachePath = "~/Data/systemfiles/";
        private string imageFileName = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            LoadSettings(context);

            //TODO: need a way to prevent other sites from using this service as it could be a substantial load
            //it would be easy for others to piggyback and use our service to generate/serve images
            //hopefully we can validate using UrlReferrer, need to verify that is present when the image is
            //from src= in a page.

            GetImage(context);

        }

        private void GetImage(HttpContext context)
        {
            
            string fullPath = context.Server.MapPath(imageCachePath);

            if (!fullPath.EndsWith(@"\")) { fullPath += @"\"; }

            //Image img = (Image)SiteImage.GetSiteThumbnail(siteUrl, browserWidth, browserHeight, thumbnailWidth, thumbnailHeight, fullPath);
            SiteImage siteImage = new SiteImage(siteUrl, browserWidth, browserHeight, thumbnailWidth, thumbnailHeight, fullPath);

            //Image img = (Image)siteImage.GetScreenShot();

            Bitmap b = siteImage.GetScreenShot();
            if (b == null)
                b = (Bitmap)System.Drawing.Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("mojoPortal.SiteImageService.Notavailable.jpg"));

            Image img = (Image)b;

            context.Response.ContentType = "image/jpg";

            img.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        }

        private void LoadSettings(HttpContext context)
        {
            if (context.Request.QueryString["site"] != null) { siteUrl = context.Request.QueryString["site"]; }

            if (ConfigurationManager.AppSettings["SiteImageCachePath"] != null)
            {
                imageCachePath = ConfigurationManager.AppSettings["SiteImageCachePath"];
            }

            if (ConfigurationManager.AppSettings["SiteImageBrowserWidth"] != null)
            {
                int.TryParse(ConfigurationManager.AppSettings["SiteImageBrowserWidth"], out browserWidth); 
            }

            if (ConfigurationManager.AppSettings["SiteImageBrowserHeight"] != null)
            {
                int.TryParse(ConfigurationManager.AppSettings["SiteImageBrowserHeight"], out browserHeight);
            }

            if (ConfigurationManager.AppSettings["SiteImageThumbnailWidth"] != null)
            {
                int.TryParse(ConfigurationManager.AppSettings["SiteImageThumbnailWidth"], out thumbnailWidth);
            }

            if (ConfigurationManager.AppSettings["SiteImageThumbnailHeight"] != null)
            {
                int.TryParse(ConfigurationManager.AppSettings["SiteImageThumbnailHeight"], out thumbnailHeight);
            }





            //imageFileName = GetImageFileName(siteUrl);


        }

        //private string GetImageFileName(string siteUrl)
        //{
        //    if(string.IsNullOrEmpty(siteUrl)) { return siteUrl;}

        //    return siteUrl.Replace("https:", string.Empty).Replace("http:", string.Empty).Replace("/", string.Empty) + ".jpg";

        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
