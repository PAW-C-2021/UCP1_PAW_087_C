using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UCP1_PAW_087_C.Models;

namespace UCP1_PAW_087_C.Controllers
{
    public class UsersController : Controller
    {
        private readonly EmployeeAttendanceContext _context;

        public UsersController(EmployeeAttendanceContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var employeeAttendanceContext = _context.Users.Include(u => u.IdPositionNavigation).Include(u => u.IdRoleNavigation);
            return View(await employeeAttendanceContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.IdPositionNavigation)
                .Include(u => u.IdRoleNavigation)
                .FirstOrDefaultAsync(m => m.IdUser == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["IdPosition"] = new SelectList(_context.Position, "IdPosition", "IdPosition");
            ViewData["IdRole"] = new SelectList(_context.Role, "IdRole", "IdRole");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUser,Name,Email,Password,IdRole,IdPosition")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPosition"] = new SelectList(_context.Position, "IdPosition", "IdPosition", users.IdPosition);
            ViewData["IdRole"] = new SelectList(_context.Role, "IdRole", "IdRole", users.IdRole);
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            ViewData["IdPosition"] = new SelectList(_context.Position, "IdPosition", "IdPosition", users.IdPosition);
            ViewData["IdRole"] = new SelectList(_context.Role, "IdRole", "IdRole", users.IdRole);
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUser,Name,Email,Password,IdRole,IdPosition")] Users users)
        {
            if (id != users.IdUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.IdUser))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPosition"] = new SelectList(_context.Position, "IdPosition", "IdPosition", users.IdPosition);
            ViewData["IdRole"] = new SelectList(_context.Role, "IdRole", "IdRole", users.IdRole);
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.IdPositionNavigation)
                .Include(u => u.IdRoleNavigation)
                .FirstOrDefaultAsync(m => m.IdUser == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.IdUser == id);
        }
    }
}
