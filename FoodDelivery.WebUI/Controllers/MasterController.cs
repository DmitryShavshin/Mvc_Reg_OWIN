using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using FoodDelivery.WebUI.Models;

namespace FoodDelivery.WebUI.Controllers
{
    [Authorize(Roles = "Master")]
    public class MasterController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public MasterController()
        {

        }

        public MasterController(ApplicationRoleManager roleManager, ApplicationUserManager userManager)
        {
            RoleManager = RoleManager;
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public void ReadRolesAndUsers()
        {
            var roleList = RoleManager.Roles.OrderBy(r => r.Name).ToList().
                               Select(rr => new SelectListItem
                               {
                                   Value = rr.Name.ToString(),
                                   Text = rr.Name
                               })
                               .ToList();
            ViewBag.Roles = roleList;
            var userList = UserManager.Users.OrderBy(u => u.UserName).ToList().
                               Select(uu => new SelectListItem
                               {
                                   Value = uu.UserName.ToString(),
                                   Text = uu.UserName
                               })
                               .ToList();
            ViewBag.Users = userList;
        }

        public ActionResult Index()
        {
            ReadRolesAndUsers();
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                RoleManager.Create(new IdentityRole(collection["RoleName"]));
                ViewBag.Message = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string RoleName)
        {
            var thisRole = RoleManager.Roles.Where
                           (r => r.Name.Equals(
                               RoleName,
                               StringComparison.CurrentCultureIgnoreCase))
                           .FirstOrDefault();
            RoleManager.Delete(thisRole);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string roleName)
        {
            var thisRole = RoleManager.Roles.Where
                           (r => r.Name.Equals(
                               roleName,
                               StringComparison.CurrentCultureIgnoreCase))
                           .FirstOrDefault();
            return View(thisRole);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                RoleManager.Update(role);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult RoleAddToUser()
        {
            return View();
        }

        //  Adding Roles to a user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            try
            {
                var user = UserManager.Users.Where
                                        (u => u.UserName.Equals(
                                            UserName,
                                            StringComparison.CurrentCultureIgnoreCase))
                                        .FirstOrDefault();
                UserManager.AddToRole(user.Id, RoleName);
                ViewBag.Message = "Role created successfully !";
                ReadRolesAndUsers();
                return View("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }            
        }

        public PartialViewResult Error()
        {
            return PartialView();
        }

        public ActionResult GetRoles()
        {
            return View();
        }

        //Getting a List of Roles for a User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    ApplicationUser user = UserManager.Users.Where
                                            (u => u.UserName.Equals(
                                                UserName,
                                                StringComparison.CurrentCultureIgnoreCase))
                                            .FirstOrDefault();
                    ReadRolesAndUsers();
                    ViewBag.Message = "Roles retrieved successfully !";
                    ViewBag.RolesForThisUser = UserManager.GetRoles(user.Id);
                }
                return View("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        public ActionResult DeleteRoleForUser()
        {
            return View();
        }

        //Deleting a User from A Role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            try
            {
                ApplicationUser user = UserManager.Users.Where
                                      (u => u.UserName.Equals(
                                          UserName,
                                          StringComparison.CurrentCultureIgnoreCase))
                                      .FirstOrDefault();
                if (UserManager.IsInRole(user.Id, RoleName))
                {
                    UserManager.RemoveFromRole(user.Id, RoleName);
                    ViewBag.Message = "Role removed from this user successfully !";
                }
                else
                {
                    ViewBag.Message = "This user doesn't belong to selected role.";
                }
                ReadRolesAndUsers();
                return View("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public PartialViewResult RolesListPartial()
        {
            return PartialView();
        }

        public PartialViewResult UsersListPartial()
        {
            return PartialView();
        }

        public PartialViewResult UsersList()
        {

            return PartialView();
        }
    }
}