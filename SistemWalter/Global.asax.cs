using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SistemWalter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SistemWalter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ApplicationDbContext db = new ApplicationDbContext();
            //CreateRoles(db);
            CreateSuperuser(db);
            AddPermisionsToSuperuser(db);
            db.Dispose();
        }
        private void AddPermisionsToSuperuser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //var user = userManager.FindByName("roger_rivera@gmail.com");

            //if (!userManager.IsInRole(user.Id, "Administrador"))
            //{
            //    userManager.AddToRole(user.Id, "Administrador");
            //}

            //if (!userManager.IsInRole(user.Id, "Cliente"))
            //{
            //    userManager.AddToRole(user.Id, "Cliente");
            //}


        }

        private void CreateSuperuser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));


            //    var user = userManager.FindByName("roger_rivera@gmail.com");
            //    if (user == null)
            //    {
            //        user = new ApplicationUser
            //        {
            //            UserName = "roger_rivera@gmail.com",
            //            Email = "roger_rivera@gmail.com"

            //        };
            //        userManager.Create(user, "rR_123456@");
            //    }
            //}

            //private void CreateRoles(ApplicationDbContext db)
            //{
            //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            //    if (!roleManager.RoleExists("Administrador"))
            //    {
            //        roleManager.Create(new IdentityRole("Administrador"));
            //    }

            //    if (!roleManager.RoleExists("Cliente"))
            //    {
            //        roleManager.Create(new IdentityRole("Cliente"));
            //    }


            //}
        }
    }
}
