using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using rent.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace rent.Controllers
{
    public class AracController : Controller
    {
        RentCarContext _db;
        public AracController(RentCarContext db)
        {
            _db = db;
        }
        public IActionResult AracList()
        {
            List<Arac> araclar = _db.Aracs.OrderBy(a => a.Marka).ThenBy(a => a.Model).ToList();
            return View(araclar);
        }
         
        public IActionResult AracEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AracEkle(Arac arac)
        {
      
                _db.Aracs.Add(arac);
                _db.SaveChanges();
                return RedirectToAction("AracList");       
        }
        public IActionResult AracSil(int id)
        {
           Arac arac= _db.Aracs.Find(id);
            _db.Aracs.Remove(arac);
            _db.SaveChanges();
            return RedirectToAction("AracList");
        }
        public IActionResult AracDuzenle(int id)
        {
            Arac arac=_db.Aracs.Find(id);

            return View(arac);
        }
        [HttpPost]
        public IActionResult AracDuzenle(Arac arac)
        { 
            _db.Aracs.Update(arac);
            _db.SaveChanges();
            return RedirectToAction("AracList");
        }

    }
}


