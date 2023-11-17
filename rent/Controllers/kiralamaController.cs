using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rent.Dto;
using rent.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace rent.Controllers
{
    public class kiralamaController : Controller
    {
        RentCarContext _db;
        public kiralamaController(RentCarContext db)
        {
            _db = db;
        }

        public IActionResult KiralamaList()
        {
           List<KiralamaBilgi> kiralamalistesi //select veya dto mantığını kullan olmazsa
                = _db.KiralamaBilgis.OrderBy(a => a.Arac.Marka).ThenBy(a => a.Arac.Model).Include(a=>a.Arac).Include(m=>m.Musteri).ToList();
            return View(kiralamalistesi);

        }

        public IActionResult KiralamaEkle()
        {
            List<aracDto> araclar = _db.Aracs.Select(a =>
            new aracDto
            {
             id=a.AracId,
             marka=a.Marka,
             model=a.Model,
             Yil=a.Yil,
             gunkira=a.GunlukKiraUcret,
             Plaka=a.Plaka
            }).ToList();

            List<musteriDto> musteriler =_db.Musteris.Select(m=>
            new musteriDto
            {
                id=m.MusteriId,
                tckimlik=m.TcKimlik,
                Ad=m.Ad,
                Soyad=m.Soyad
               
            }).ToList();
            return View((new KiralamaBilgi(),araclar,musteriler));
            
        }
        [HttpPost]
        public IActionResult KiralamaEkle([Bind(Prefix ="Item1")] KiralamaBilgi kiralamabilgi, int aracSayisi)
        {
            Arac kiralananArac = _db.Aracs.Find(kiralamabilgi.AracId);

            // Kiralama gün sayısını al
            int? kiralamaGun = kiralamabilgi.KiralamaGun;


            int? aracYil = kiralananArac?.Yil;
            int currentYear = DateTime.Now.Year;

            int? kiralamaSayisi = _db.KiralamaBilgis.Count(k => k.MusteriId == kiralamabilgi.MusteriId);



            var mevcutarac = _db.KiralamaBilgis.FirstOrDefault(k =>k.AracId == kiralamabilgi.AracId);

            
            // Eğer araç ve kiralama günü bilgileri mevcutsa işlem yap
            if (kiralananArac != null && kiralamaGun.HasValue)
            {
               
                // Günlük kira ücretini al (varsayılan olarak 0)
                decimal gunlukKiraUcret = kiralananArac.GunlukKiraUcret ?? 0;

                // Toplam ücreti hesapla
                decimal toplamUcret = gunlukKiraUcret * kiralamaGun.Value;
                
                // İndirimleri uygula
                if (kiralamaGun >= 365)
                {
                    toplamUcret *= 0.88m; // 1 yıl ve üstü kiralamalarda %12 indirim
                }
                if(aracYil.HasValue &&(currentYear-aracYil)>5)
                {
                    toplamUcret *= 0.95m; //5 yıllık araç
                }
                if (!kiralamaSayisi.HasValue || kiralamaSayisi == 0)
                {
                    toplamUcret *= 0.9m; // İlk defa kiralama yapan şahıslara %10 indirim
                }
                if (kiralamaSayisi >= 4)
                {
                    toplamUcret *= 0.9m; // 5 veya daha fazla araç kiralayanlara toplam ücrete %10 indirim
                    var indirimliAraclar = _db.KiralamaBilgis
                        .Where(k => k.MusteriId == kiralamabilgi.MusteriId && k.KiralamaBilgiId != kiralamabilgi.KiralamaBilgiId)
                        .ToList();

                    foreach (var arac in indirimliAraclar)
                    {
                        arac.ToplamUcret *= 0.9m;
                    }

                    _db.UpdateRange(indirimliAraclar);
                }
                if (kiralamabilgi.KiralamaGun <= 0 || kiralamaGun.HasValue ==null)
                {
                    TempData["AlertMessage"] = "Hatali gun girisi yaptiniz.Gecerli Sayi Girin";
                    return RedirectToAction("kiralamaEkle");
                }

                if (mevcutarac != null)
                {
                    TempData["AlertMessage"] = "Bu arac zaten kiralanmis durumda.";
                    return RedirectToAction("KiralamaEkle");
                }
                kiralamabilgi.ToplamUcret = toplamUcret;


                kiralamabilgi.KiraGunu = DateTime.Now;

                
                kiralamabilgi.TeslimGunu =DateTime.Now.AddDays(kiralamaGun.Value);
               
            }

            _db.KiralamaBilgis.Add(kiralamabilgi);
            _db.SaveChanges();

            return RedirectToAction("KiralamaList");
        }
        public IActionResult kiralamaSil(int id)
        {
            KiralamaBilgi kiralama = _db.KiralamaBilgis.Find(id);
            _db.KiralamaBilgis.Remove(kiralama);
            _db.SaveChanges();
            return RedirectToAction("kiralamaList");
        }

        public IActionResult ArizaEkle(int id)
        {
            KiralamaBilgi kiralamaBilgi = _db.KiralamaBilgis.Find(id);

            if (kiralamaBilgi != null && kiralamaBilgi.HasarDurum != true)
            {
                kiralamaBilgi.ToplamUcret *= 1.15m;
                kiralamaBilgi.HasarDurum = true; // Hasar cezası uygulandığını işaretle

                _db.SaveChanges();
                TempData["AlertMessage"] = "Hasar cezasi (%10) uygulandi";
            }
            else if (kiralamaBilgi != null && kiralamaBilgi.HasarDurum == true)
            {
                TempData["AlertMessage"] = "Hasar cezasi zaten eklendi";
            }

            return RedirectToAction("KiralamaList");
        }

    }
}
