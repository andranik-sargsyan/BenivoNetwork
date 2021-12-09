using BenivoNetwork.DAL;
using BenivoNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BenivoNetwork.Controllers
{
    public class PostController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<PostModel> model;

            using (var context = new BenivoNetworkEntities())
            {
                var posts = context.Posts.Where(p => p.UserID == 1).ToList();

                model = posts
                    .Select(p => new PostModel
                    {
                        ID = p.ID,
                        Text = p.Text,
                        DateCreated = p.DateCreated,
                        UserEmail = p.User.Email
                    })
                    .ToList();
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new PostModel());
        }

        [HttpPost]
        public ActionResult Create(PostModel model)
        {
            using (var context = new BenivoNetworkEntities())
            {
                context.Posts.Add(new Post
                {
                    UserID = 1,
                    Text = model.Text,
                    DateCreated = DateTime.Now
                });

                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
