using BookxpertAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookxpertAssignment.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Employees.ToListAsync());
        }
        [HttpGet]

        public async Task<IActionResult> getSumSalary()
        {
            return Json(_context.Employees.Sum(x=>x.Salary));
        }

        [HttpPost]
        public async Task<IActionResult> Save(Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.Id == 0)
                    _context.Add(employee);
                else
                    _context.Update(employee);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", await _context.Employees.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            return Json(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

