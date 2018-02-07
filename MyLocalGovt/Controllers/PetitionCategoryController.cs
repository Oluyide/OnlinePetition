using MyLocalGovt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyLocalGovt.Controllers
{
    public class PetitionCategoryController : Controller
    {
        MyLocalGovtEntities Db = new MyLocalGovtEntities();
        //
        // GET: /PetitionCategory/
        [HttpGet]
        public ActionResult ShowCategory(int? id)
        {
             if (id == null)
            {
               return RedirectToAction("Index", "Home");
          }
            PetitionModel model = new PetitionModel();
            var list = PetitionTypes();
            model.PetitionsList = list;
           var property = Db.PetitionCategories.Where(x => x.CategoryNameId == id).SingleOrDefault();
            ViewBag.Category = property.Category;
            ViewBag.CategoryPic = property.CategoryPic;
            var listSign = PetitionSign();
            model.PetitionsSignList = listSign;
            return View(model);
        }
        public List<PetitionModel> PetitionTypes()
        {
            List<PetitionModel> list = new List<PetitionModel>();

            List<PetitionCategory> CategoryList = Db.PetitionCategories.OrderBy(x => x.Category).ToList();

            foreach (var a in CategoryList)
            {
                PetitionModel model = new PetitionModel();
                model.CategoryNameId = a.CategoryNameId;
                model.Category = a.Category;
                model.CategoryPic = a.CategoryPic;
                list.Add(model);

            }
            return list;
        }
        public List<PetitionModel> PetitionSign()
        {
            List<PetitionModel> listSign = new List<PetitionModel>();

            List<PetitionInfo> SignList = Db.PetitionInfoes.OrderBy(x => x.PetitionId).ToList();

            foreach (var a in SignList)
            {
                PetitionModel model = new PetitionModel();
                model.PetitionId = a.PetitionId;
                model.NameOfFile = a.NameOfFile;
                model.Title = a.Title;
                model.WhySign = a.WhySign;

                listSign.Add(model);

            }
            return listSign;
        }

         
        [HttpPost]
        public ActionResult ShowCategory()
        {
            return View();

        }
	}
}