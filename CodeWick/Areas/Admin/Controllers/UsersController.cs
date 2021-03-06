using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CodeWick.Helpers;
using CodeWick.Models;
using CodeWick.Repository;

namespace CodeWick.Areas.Admin.Controllers{
	[HandleError]
    [SessionAttribute]
    [AdminAuthorize]
    public class UsersController : Controller {
		private readonly IUserRepository userRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public UsersController() : this(new UserRepository()){}

        public UsersController(IUserRepository userRepository){
			this.userRepository = userRepository;
        }

        // GET: /Users/
        public ViewResult Index(){
            return View(userRepository.AllIncluding(user => user.Roles));
        }

        // GET: /Users/Details/5
        public ViewResult Details(System.Guid id)
        {
            return View(userRepository.Find(id));
        }

        // GET: /Users/Create
        public ActionResult Create()
        {
            return View();
        } 

        // POST: /Users/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid) {
                userRepository.InsertOrUpdate(user);
                userRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(System.Guid id)
        {
             return View(userRepository.Find(id));
        }

        // POST: /Users/Edit/5
        [HttpPost]
        public ActionResult Edit(User user){
            if (ModelState.IsValid) {
                userRepository.InsertOrUpdate(user);
                userRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(System.Guid id){
            return View(userRepository.Find(id));
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id){
            userRepository.Delete(id);
            userRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

