using Microsoft.AspNetCore.Mvc;
using rent.Models;
using System.Collections.Generic;
using System.Linq;

namespace rent.Controllers
{
    public class musteriController : Controller
    {
        RentCarContext _db;
        public musteriController(RentCarContext db)
        {
            _db = db;
        }
        public IActionResult musteriList()
        {
            List<Musteri> musteriler = _db.Musteris.ToList();
            return View(musteriler);
        }
        public IActionResult musteriEkle() 
        {
            return View();   
        }
        [HttpPost]
        public IActionResult musteriEkle(Musteri musteri)
        {
            _db.Musteris.Add(musteri);
            _db.SaveChanges();  
            return RedirectToAction("musteriList");
        }
        public IActionResult musteriSil(int id)
        {
            Musteri musteri=_db.Musteris.Find(id);
            _db.Musteris.Remove(musteri);
            _db.SaveChanges();
            return RedirectToAction("musteriList");
        }
        public IActionResult musteriDuzenle(int id)
        {
            Musteri musteri = _db.Musteris.Find(id);
            return View(musteri);
        }
        [HttpPost]
        public IActionResult musteriDuzenle(Musteri musteri)
        {
            _db.Musteris.Update(musteri);
            _db.SaveChanges();
            return RedirectToAction("musteriList");
        }
    }
}
