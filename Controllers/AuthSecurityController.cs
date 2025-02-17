using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Composition.Convention;
using WPFMedinova.Models;

namespace WPFMedinova.Controllers
{
    public class AuthSecurityController : Controller
    {
        private AccountDbContext _dbcontext;
        public AuthSecurityController(AccountDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public IActionResult Index()                                //when admin login
        {
            return View();
        }
        public IActionResult List_Doctor()
        {
            var listofdoctors = _dbcontext.Doctor_Table.ToList();
            return View(listofdoctors);
        }
        public IActionResult List_Patients()
        {
            ViewBag.role = HttpContext.Session.GetString("role");
            var listofpatients = _dbcontext.Register_Table.ToList();  //when doctor login
            return View(listofpatients);
        }
        public IActionResult Patient_Details()                      //when patient login
        {
            string username = HttpContext.Session.GetString("username"); ;
            var PatientDetails = _dbcontext.Register_Table.Where(m => m.Username == username).FirstOrDefault();
            return View(PatientDetails);
        }
        public IActionResult Appointment_List()
        {
            string username = HttpContext.Session.GetString("username");
            string name = HttpContext.Session.GetString("name");
            ViewBag.role = HttpContext.Session.GetString("role");

            if (ViewBag.role == "Admin")
            {
                var list = _dbcontext.Appointment_Table.ToList();
                return View(list);
            }
            else if (ViewBag.role == "Doctor")
            {
                var list = _dbcontext.Appointment_Table.Where(x => x.Doctor == name && x.Status == "Pending").OrderBy(x => x.Id).ToList();
                return View(list);
            }
            else
            {
                var list = _dbcontext.Appointment_Table.Where(x => x.Patient_Name == name).OrderBy(x => x.Id).ToList();
                return View(list);
            }
        }

        public async Task<IActionResult> Approve(int id)
        {
            var data =await _dbcontext.Appointment_Table.Where(m => m.Id == id).FirstOrDefaultAsync();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Approve(AppointmentModel appObj)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Entry(appObj).State = EntityState.Modified;
                int n = await _dbcontext.SaveChangesAsync();
                if (n != 0)
                {
                    TempData["approve"] = "<script>alert('Status Approved SuccessFully!!')</script>";
                    return RedirectToAction("Appointment_List", "AuthSecurity");
                }
                else
                {
                    TempData["approve"] = "<script>alert('Status Failed!!')</script>";
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error in Model");
            }
            return View();
        }

        public IActionResult Reject(int id)
        {
            if (ModelState.IsValid)
            {
                AppointmentModel appObj = new AppointmentModel();
                //var data = _dbcontext.Appointment_Table.Where(m => m.Id == id).FirstOrDefault();
                appObj = _dbcontext.Appointment_Table.Where(m => m.Id == id).FirstOrDefault();
                appObj.Status = "Rejected";
                //_dbcontext.Entry(appObj).State= EntityState.Modified;
                int n = _dbcontext.SaveChanges();
                if (n != 0)
                {
                    TempData["reject"] = "<script>alert('Status Rejected SuccessFully!!')</script>";
                    return RedirectToAction("Appointment_List", "AuthSecurity");
                }
                else
                {
                    TempData["reject"] = "<script>alert('Status Failed!!')</script>";
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error in Model");
            }
            return View();
        }

        public IActionResult Approved_List()
        {
            var approvedlist = _dbcontext.Appointment_Table.Where(m=>m.Status=="Approved").ToList();
            return View(approvedlist);
        }
    }
}
