using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPFMedinova.Models;

namespace WPFMedinova.Controllers
{
    public class SpecializationController : Controller
    {
        private AccountDbContext _dbContext;
        public SpecializationController( AccountDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add_Specialization()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add_Specialization(Specialization spObj)
        {
            if(ModelState.IsValid)
            {
                _dbContext.specializations.Add(spObj);
                int n=_dbContext.SaveChanges();
                if(n!=0)
                {
                    TempData["insert"] = "<script>alert('Specialization Added SuccessFully!!')</script>";
                    return View();
                }
                else
                {
                    TempData["insert"] = "<script>alert('Specialization Failed!!')</script>";
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error in Model");
            }
            return View();
        }

        public JsonResult GetSpecialization()
        {
            var Specialization = _dbContext.specializations.ToList();
            return new JsonResult(Specialization);
        }
    }
}
