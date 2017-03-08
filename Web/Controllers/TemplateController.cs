using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNet.Identity.Dapper;
using PPOK_System.Domain;

namespace Web.Controllers
{
    public class TemplateController : Controller
    {
        TemplatesManager manager = new TemplatesManager();

        // GET: Templates
        public ActionResult Index()
        {

            List<Template> templates = manager.listTemplates();
            return View(templates);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Find in the database the template to edit

            Template templateToEdit = manager.findTemplateGivenId(id);
            return View(templateToEdit);
        }

        [HttpPost]
        public ActionResult Edit(Template template)
        {
            //update the template
            manager.updateTemplate(template);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            Template detailedTemplate = manager.findTemplateGivenId(Id);
            return View(detailedTemplate);
        }

    }
}