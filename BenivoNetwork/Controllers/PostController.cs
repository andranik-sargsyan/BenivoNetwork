using BenivoNetwork.DAL;
using BenivoNetwork.Filters;
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
                var posts = context.Posts.ToList();

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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PostModel model;

            using (var context = new BenivoNetworkEntities())
            {
                var post = context.Posts.FirstOrDefault(p => p.ID == id);

                model = new PostModel
                {
                    ID = post.ID,
                    Text = post.Text,
                    DateCreated = post.DateCreated,
                    UserEmail = post.User.Email
                };
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PostModel model)
        {
            using (var context = new BenivoNetworkEntities())
            {
                var post = context.Posts.FirstOrDefault(p => p.ID == model.ID);

                post.Text = model.Text;

                context.Entry(post).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(new PostModel { ID = id });
        }

        [HttpPost]
        public ActionResult Delete(PostModel model)
        {
            using (var context = new BenivoNetworkEntities())
            {
                var post = context.Posts.FirstOrDefault(p => p.ID == model.ID);

                context.Posts.Remove(post);

                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
