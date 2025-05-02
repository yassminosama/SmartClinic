using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Data;
using SmartClinic.Models;

namespace SmartClinic.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var doctors = new List<Doctor>
            {
                new Doctor { FullName = "Dr. John Smith", Specialization = "Cardiology", Image = "doctor1.jpg", IsAvailable = true },
               
            };

            return View(doctors);
        }

        //public IActionResult Edit([FromRoute] int id)
        //{

        //    var doctor = _context.Doctors.Find(id);
        //    if (doctor is not null)
        //    {

        //        return View(doctor);
        //    }

        //    return NotFound();

        //}
        //[HttpPost]
        //public IActionResult Edit(Doctor doctor, IFormFile Img)
        //{
        //    var doctorInDb = _context.Doctors.AsNoTracking().FirstOrDefault(d => d.Id == doctor.Id);


        //    if (doctorInDb == null)
        //    {
        //        return NotFound();
        //    }

        //    if (Img is not null && Img.Length > 0)
        //    {
  
        //        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);
        //        var path = Path.Combine(Directory.GetCurrentDirectory(), "", "wwwroot\\images", fileName);

        //        using (var stream = System.IO.File.Create(path))
        //        {
        //            Img.CopyTo(stream);
        //        }


        //        var oldpath = Path.Combine(Directory.GetCurrentDirectory(), "", "wwwroot\\images", doctorInDb.Image);

        //        if (System.IO.File.Exists(oldpath))
        //        {
        //            System.IO.File.Delete(oldpath);
        //        }

        //        doctor.Image = fileName;
        //    }
        //    else
        //    {
        //        doctor.Image = doctorInDb.Image;
        //    }

        //    _context.Doctors.Update(doctor);
        //    _context.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
