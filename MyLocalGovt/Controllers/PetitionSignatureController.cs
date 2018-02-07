using MyLocalGovt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyLocalGovt.Controllers
{
    public class PetitionSignatureController : Controller
    {
        MyLocalGovtEntities Db = new MyLocalGovtEntities();

       // PetitionSignatureModel model = new PetitionSignatureModel();
        //
        // GET: /PetitionSignature/
        [HttpGet]
        public ActionResult SignatureIndex(int? id, PetitionModel sign)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            PetitionSignatureModel model = new PetitionSignatureModel();


            var property = Db.PetitionInfoes.Where(x => x.PetitionId == id).SingleOrDefault();

            ViewBag.PetTitle = property.Title;
            ViewBag.Petition = property.WhySign;
            ViewBag.Image = property.NameOfFile;

            return View(model);
        }
      
        [HttpPost]


        public ActionResult SignatureIndex(PetitionSignatureModel model, int id)

        {
            
            PetitionSignature petitionSignature= new PetitionSignature();
            petitionSignature.SignAdd=model.SignAdd;
            petitionSignature.SignCity=model.SignCity;
            petitionSignature.SignCountry=model.SignCountry;
            petitionSignature.SignEmail=model.SignEmail;
            petitionSignature.SignFirstName=model.SignFirstName;
            petitionSignature.SignId=model.SignId;
            petitionSignature.SignLastName=model.SignLastName;
            petitionSignature.SignState=model.SignState;
            petitionSignature.SignWord=model.SignWord;
            petitionSignature.PetitionId = id;
            Db.PetitionSignatures.Add(petitionSignature);             
             Db.SaveChanges();

            return RedirectToAction("SignatureIndex");

        }

       
    }
}