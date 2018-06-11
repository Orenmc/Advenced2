using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PhotoController : Controller
    {
        private static PhotoAlbum photos = new PhotoAlbum(); 
        // GET: Photo
        public ActionResult PhotoView()
        {
            return View(photos.PhotosAlbum);
        }
        public ActionResult DeletePhoto(string photoRelPath, string photoRelThumb)
        {
            ViewBag.path = photoRelPath;
            return View();
        }
        public ActionResult ViewPhoto(string photoRelPath)
        {
            return View();
        }
    }
}