using MyLocalGovt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
namespace MyLocalGovt.Controllers
{
    public class AdminController : Controller
    {
        MyLocalGovtEntities Db = new MyLocalGovtEntities();
        //
        // GET: /Admin/
        [HttpGet]

        public ActionResult Admin()
        {
            PetitionModel model = new PetitionModel();
            var list = AdminReview();
            model.AdminList = list;
            return View(model);
          

        }

        public List<PetitionModel> AdminReview()
        {
            List<PetitionModel> list = new List<PetitionModel>();

            List<PetitionInfo> petlist = Db.PetitionInfoes.OrderByDescending(x => x.Approval == false).ToList();
            foreach (var x in petlist)
            {
                PetitionModel model = new PetitionModel();
                model.PetitionId = x.PetitionId;
                model.Title = x.Title;
                model.Phone = x.Phone;
                model.StateId = x.StateId;
                model.IsApproved = x.Approval.Value;
                model.PetDate = x.PetDate.ToString();
                model.ToWhom = x.ToWhom;
                model.WhySign = x.WhySign;
                model.Selected = x.CategoryId;
                //model.CompSelected = x.ComplaintId.Value;
                model.NameOfFile = x.NameOfFile;

                list.Add(model);

            }
            return list;
        }




        [HttpPost]
        public ActionResult Admin(PetitionModel model)
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {

            var a = Db.PetitionInfoes.Where(x => x.PetitionId == Id).SingleOrDefault();
            PetitionModel model = new PetitionModel();

            model.PetitionId = Id;
            model.Title = a.Title;
            model.WhySign = a.WhySign;
            model.ToWhom = a.ToWhom;
            model.PetDate = a.PetDate.Value.ToString();
            model.IsApproved = a.Approval.Value;
          
            model.StateId = a.StateId;
            model.Phone = a.Phone;
            model.Selected = a.CategoryId;     
            model.CompSelected = a.ComplaintId.Value;
          
            model.NameOfFile = a.NameOfFile;
            model.UserId = User.Identity.GetUserId().ToString();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(PetitionModel model)
        {

            PetitionInfo petInfo = new PetitionInfo();
          
            petInfo.PetitionId = model.PetitionId;
           petInfo.NameOfFile = model.NameOfFile;
            petInfo.Title = model.Title;
            petInfo.Approval = model.IsApproved;
            petInfo.WhySign = model.WhySign;
            petInfo.ToWhom = model.ToWhom;
            petInfo.StateId = model.StateId;
            petInfo.Phone = model.Phone;
            petInfo.PetDate = DateTime.Parse(model.PetDate);
            petInfo.CategoryId = model.Selected;
            petInfo.ComplaintId = model.CompSelected;
            petInfo.UserId = model.UserId;
            
          
           
            Db.Entry(petInfo).State = EntityState.Modified;
            try
            {
                Db.SaveChanges();
                TempData["Success"] = "Your Petition Has Been Sucessfully Edited!";

                return RedirectToAction("Edit");

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }

            return RedirectToAction("Edit");

        }
    }
}