using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SCP_Foundation.Models;
using Microsoft.AspNetCore.Authorization;

namespace SCP_Foundation.Controllers
{
    public class SCPcontroller : Controller
    {
        private readonly SCPcontext _context;

        public SCPcontroller(SCPcontext context)
        {
            _context = context;
        }

        // GET: SCPcontroller

        [Authorize(Roles = "Administrator,Manager,User")]
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["IdNumberSort"] = String.IsNullOrEmpty(sortOrder) ? "IdNumber_desc" : "";
            ViewData["NameSort"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["RclassSort"] = sortOrder == "Rclass" ? "Rclass_desc" : "Rclass";
            ViewData["CclassSort"] = sortOrder == "Cclass" ? "Cclass_desc" : "Cclass";
            ViewData["DclassSort"] = sortOrder == "Dclass" ? "Dclass_desc" : "Dclass";

            var sCPcontext = from s in _context.SCPs.Include(s => s.Classified)
                             .Include(s => s.Contain)
                             .Include(s => s.Disruption)
                             .Include(s => s.Risk)
                             select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                sCPcontext = sCPcontext.Where(s => s.IdNumber.Contains(searchString)
                || s.Name.Contains(searchString)
                || s.Risk.Rclass.Contains(searchString)
                || s.Contain.Cclass.Contains(searchString)
                || s.Disruption.Dclass.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "IdNumber_desc":
                    sCPcontext = sCPcontext.OrderByDescending(s => s.IdNumber);
                    break;

                case "Name":
                    sCPcontext = sCPcontext.OrderBy(s => s.Name);
                    break;
                case "Name_desc":
                    sCPcontext = sCPcontext.OrderByDescending(s => s.Name);
                    break;

                case "Rclass":
                    sCPcontext = sCPcontext.OrderBy(s => s.Risk.Rclass);
                    break;
                case "Rclass_desc":
                    sCPcontext = sCPcontext.OrderByDescending(s => s.Risk.Rclass);
                    break;

                case "Cclass":
                    sCPcontext = sCPcontext.OrderBy(s => s.Contain.Cclass);
                    break;
                case "Cclass_desc":
                    sCPcontext = sCPcontext.OrderByDescending(s => s.Contain.Cclass);
                    break;

                case "Dclass":
                    sCPcontext = sCPcontext.OrderBy(s => s.Disruption.Dclass);
                    break;
                case "Dclass_desc":
                    sCPcontext = sCPcontext.OrderByDescending(s => s.Disruption.Dclass);
                    break;

                default:
                    sCPcontext = sCPcontext.OrderBy(s => s.IdNumber);
                    break;
            }
            return View(await sCPcontext.ToListAsync());
        }

        // GET: SCPcontroller/Details/5
        [Authorize(Roles = "Administrator,Manager")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sCP = await _context.SCPs
                .Include(s => s.Classified)
                .Include(s => s.Contain)
                .Include(s => s.Disruption)
                .Include(s => s.Risk)
                .FirstOrDefaultAsync(m => m.SCPID == id);
            if (sCP == null)
            {
                return NotFound();
            }

            return View(sCP);
        }

        // GET: SCPcontroller/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["ClassifiedId"] = new SelectList(_context.Classifieds, "ClassifiedId", "ClassificationLevel");
            ViewData["ContainId"] = new SelectList(_context.Contains, "ContainId", "Cclass");
            ViewData["DisruptionId"] = new SelectList(_context.Disruptions, "DisruptionId", "Dclass");
            ViewData["RiskId"] = new SelectList(_context.Risks, "RiskId", "Rclass");
            return View();
        }

        // POST: SCPcontroller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SCPID,IdNumber,Name,ContainmentProcedure,Description,RiskId,ContainId,DisruptionId,ClassifiedId")] SCP sCP)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sCP);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassifiedId"] = new SelectList(_context.Classifieds, "ClassifiedId", "ClassificationLevel");
            ViewData["ContainId"] = new SelectList(_context.Contains, "ContainId", "Cclass", sCP.ContainId);
            ViewData["DisruptionId"] = new SelectList(_context.Disruptions, "DisruptionId", "Dclass", sCP.DisruptionId);
            ViewData["RiskId"] = new SelectList(_context.Risks, "RiskId", "Rclass", sCP.RiskId);
            return View(sCP);
        }

        // GET: SCPcontroller/Edit/5
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sCP = await _context.SCPs.FindAsync(id);
            if (sCP == null)
            {
                return NotFound();
            }
            ViewData["ClassifiedId"] = new SelectList(_context.Classifieds, "ClassifiedId", "ClassificationLevel");
            ViewData["ContainId"] = new SelectList(_context.Contains, "ContainId", "Cclass", sCP.ContainId);
            ViewData["DisruptionId"] = new SelectList(_context.Disruptions, "DisruptionId", "Dclass", sCP.DisruptionId);
            ViewData["RiskId"] = new SelectList(_context.Risks, "RiskId", "Rclass", sCP.RiskId);
            return View(sCP);
        }

        // POST: SCPcontroller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SCPID,IdNumber,Name,ContainmentProcedure,Description,RiskId,ContainId,DisruptionId,ClassifiedId")] SCP sCP)
        {
            if (id != sCP.SCPID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sCP);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SCPExists(sCP.SCPID))
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
            ViewData["ClassifiedId"] = new SelectList(_context.Classifieds, "ClassifiedId", "ClassificationLevel");
            ViewData["ContainId"] = new SelectList(_context.Contains, "ContainId", "Cclass", sCP.ContainId);
            ViewData["DisruptionId"] = new SelectList(_context.Disruptions, "DisruptionId", "Dclass", sCP.DisruptionId);
            ViewData["RiskId"] = new SelectList(_context.Risks, "RiskId", "Rclass", sCP.RiskId);
            return View(sCP);
        }

        // GET: SCPcontroller/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sCP = await _context.SCPs
                .Include(s => s.Classified)
                .Include(s => s.Contain)
                .Include(s => s.Disruption)
                .Include(s => s.Risk)
                .FirstOrDefaultAsync(m => m.SCPID == id);
            if (sCP == null)
            {
                return NotFound();
            }

            return View(sCP);
        }

        // POST: SCPcontroller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sCP = await _context.SCPs.FindAsync(id);
            _context.SCPs.Remove(sCP);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SCPExists(int id)
        {
            return _context.SCPs.Any(e => e.SCPID == id);
        }
    }
}
