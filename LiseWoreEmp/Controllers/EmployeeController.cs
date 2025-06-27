using LiseWoreEmp.Models;
using LiseWoreEmp.Models.ViewModel;
using LiseWoreEmp.Security;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiseWoreEmp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly LiseWoreEmpContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IDataProtector _protector;

        public EmployeeController(LiseWoreEmpContext context, IWebHostEnvironment env, DataSecurityProvider key, IDataProtectionProvider provider)
        {
            _context = context;
            _protector = provider.CreateProtector(key.Key);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _context.Employees
                .OrderBy(e => e.HireDate)
                .Select(e => new EmployeeViewModel
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    MiddleName = e.MiddleName,
                    LastName = e.LastName,
                    Department = e.Department,
                    Position = e.Position,
                    Email = e.Email,
                    EmpEncId = _protector.Protect(e.EmployeeId.ToString()),
                })
                .ToList();
            ViewBag.TotalEmployees = employees.Count;

            return View(employees);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeViewModel em)
        {
            //return Json(em);
            try
            {
                var empEmail = _context.Employees.Where(x => x.Email == em.Email).FirstOrDefault();
                if (empEmail != null)
                {
                    TempData["empEmail"] = "Employee already exist with this email!";
                    return View(em);
                }

                var empPhone = _context.Employees.Where(x => x.Phone == em.Phone).FirstOrDefault();
                if (empPhone != null)
                {
                    TempData["empPhone"] = "Employee already exist with this Phone!";
                    return View(em);
                }

                // Generate Employee ID
                short maxid;
                if (_context.Employees.Any())
                    maxid = Convert.ToInt16(_context.Employees.Max(e => e.EmployeeId) + 1);
                else
                    maxid = 1;
                em.EmployeeId = maxid;

                Employee employee = new()
                {
                    EmployeeId = em.EmployeeId,
                    FirstName = em.FirstName,
                    MiddleName = em.MiddleName,
                    LastName = em.LastName,
                    Email = em.Email,
                    Phone = em.Phone,
                    Department = em.Department,
                    Position = em.Position,
                    HireDate = em.HireDate,
                    AddedBy = Convert.ToInt16(User.Identity!.Name),
                };

                //return Json(employee);
                _context.Add(employee);
                await _context.SaveChangesAsync();

                TempData["AddSuccess"] = "Employee added successfully!";
                return RedirectToAction("Index","Employee");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding the employee.");
                return View(em);
            }
        }

        [HttpGet]
        public IActionResult EditEmployee(string empEncId)
        {

            //return Json(empEncId);
            var empId = Convert.ToInt16(_protector.Unprotect(empEncId));
            var employee = _context.Employees
                .Where(e => e.EmployeeId == empId)
                .Select(e => new EmployeeViewModel
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    MiddleName = e.MiddleName,
                    LastName = e.LastName,
                    Department = e.Department,
                    Position = e.Position,
                    Email = e.Email,
                    Phone = e.Phone,
                    HireDate = e.HireDate,
                    AddedBy = _context.UserLists
                        .Where(u => u.UserId == e.AddedBy)
                        .Select(u => u.EmailAddress)
                        .FirstOrDefault(),
                    EmpEncId = _protector.Protect(e.EmployeeId.ToString()),
                })
                .FirstOrDefault();
            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(EmployeeViewModel em)
        {
            //return Json(em);
            try
            {
                var empEmail = _context.Employees.Where(x => x.Email == em.Email && x.EmployeeId != em.EmployeeId).FirstOrDefault();
                if (empEmail != null)
                {
                    TempData["empEmail"] = "Employee already exist with this email!";
                    return View(em);
                }
                var empPhone = _context.Employees.Where(x => x.Phone == em.Phone && x.EmployeeId != em.EmployeeId).FirstOrDefault();
                if (empPhone != null)
                {
                    TempData["empPhone"] = "Employee already exist with this Phone!";
                    return View(em);
                }
                var employee = await _context.Employees.FindAsync(em.EmployeeId);
                if (employee == null)
                {
                    return NotFound("Employee not found");
                }
                employee.FirstName = em.FirstName;
                employee.MiddleName = em.MiddleName;
                employee.LastName = em.LastName;
                employee.Email = em.Email;
                employee.Phone = em.Phone;
                employee.Department = em.Department;
                employee.Position = em.Position;
                employee.HireDate = em.HireDate;

                //return Json(employee);
                _context.Update(employee);
                await _context.SaveChangesAsync();
                TempData["EditSuccess"] = "Employee updated successfully!";
                return RedirectToAction("Index", "Employee");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the employee.");
                return View(em);
            }
        }


 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployee(string empEncId)
        {
            short employeeId = Convert.ToInt16(_protector.Unprotect(empEncId));

            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                TempData["DeleteMessage"] = "Employee record deleted successfully!";
                return RedirectToAction("Index", "Employee");
            }

            return RedirectToAction("TenderPage", "PublisherTender");
        }

        [HttpGet]
        public IActionResult EmployeeDetails(string empEncId)
        {
            //return Json(empEncId);
            var empId = Convert.ToInt16(_protector.Unprotect(empEncId));
            var employee = _context.Employees
                .Where(e => e.EmployeeId == empId)
                .Select(e => new EmployeeViewModel
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    MiddleName = e.MiddleName,
                    LastName = e.LastName,
                    Department = e.Department,
                    Position = e.Position,
                    Email = e.Email,
                    Phone = e.Phone,
                    HireDate = e.HireDate,
                    AddedBy = _context.UserLists
                        .Where(u => u.UserId == e.AddedBy)
                        .Select(u => u.EmailAddress)
                        .FirstOrDefault(),
                    EmpEncId = _protector.Protect(e.EmployeeId.ToString()),
                })
                .FirstOrDefault();
            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            return View(employee);

        }
    }
}
