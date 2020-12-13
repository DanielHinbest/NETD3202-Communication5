/* Daniel Hinbest
 * NETD 3202 - Lab 5
 * December 6, 2020
 * This ASP.NET Core application is designed to allow the user to enter the information for a curling game, including the teams and the scores, and also provides functionality for editing, and deleting results.
 * This controller handles the functionality of the Teams views
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NETD3202_Communication5.Models;
using NETD3202_Communication5.Data;
using Microsoft.AspNetCore.Authorization;

namespace NETD3202_Communication5.Controllers
{
    public class TeamController : Controller
    {
        // Sets a readonly variable of a ScoresContext
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Creates a new instance of the TeamController with the ScoresContext passed as a parameter
        /// </summary>
        /// <param name="context"></param>
        public TeamController(ApplicationDbContext context)
        {
            // Sets the parameter to the value of the variable
            _context = context;
        }

        /// <summary>
        /// Generates the Team Index view
        /// </summary>
        /// <returns>Returns the Teams table</returns>
        public async Task<IActionResult> Index()
        {
            // Returns the scores view into a list
            return View(await _context.Teams.ToListAsync());
        }

        /// <summary>
        /// Generates the details view with the selected record
        /// </summary>
        /// <param name="id">The ID number of the selected record</param>
        /// <returns>Returns the details view with the team if valid</returns>
        public async Task<IActionResult> Details(int? id)
        {
            // If the ID is not set...
            if (id == null)
            {
                // Return NotFound
                return NotFound();
            }

            // Store the content of the team record into a variable
            var team = await _context.Teams.FirstOrDefaultAsync(m => m.teamID == id);

            // If the team is null...
            if (team == null)
            {
                // Return NotFound
                return NotFound();
            }

            // Return the details view with the team record
            return View(team);
        }

        /// <summary>
        /// Creates a new team record
        /// </summary>
        /// <returns>Returns the Create view</returns>
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new record from the team Create view
        /// </summary>
        /// <param name="team">The team added to the database</param>
        /// <returns>Returns the view with the team</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("scoreID,leadName,secondName,viceName,skipName")] Team team)
        {
            // If the model is valid
            if (ModelState.IsValid)
            {
                // Add the team to the context
                _context.Add(team);
                // Save the changes and update the database
                await _context.SaveChangesAsync();
                // return the Index view
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        /// <summary>
        /// Generates the view to edit a team
        /// </summary>
        /// <param name="id">The ID number of the team</param>
        /// <returns>Returns the view with the team content</returns>
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            // If the ID is null...
            if (id == null)
            {
                // Return NotFound
                return NotFound();
            }

            // Search the content for the team
            var team = await _context.Teams.FindAsync(id);

            // If the team is null...
            if (team == null)
            {
                // Return NotFound
                return NotFound();
            }
            // Return the Edit view with the team content
            return View(team);
        }

        /// <summary>
        /// Generates the edit view in post mode
        /// </summary>
        /// <param name="id">The ID view for the record</param>
        /// <param name="team">The team record content</param>
        /// <returns>Return the edit team record</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("scoreID,leadName,secondName,viceName,skipName")] Team team)
        {
            // If the team id does not match the parameter...
            if (id != team.teamID)
            {
                // Return NotFound
                return NotFound();
            }

            // If the model is valid...
            if (ModelState.IsValid)
            {
                // Try/Catch exception handling
                try
                {
                    // Update the context with the team content
                    _context.Update(team);
                    // Save the changes to the context
                    await _context.SaveChangesAsync();
                }
                // If a DbUpdateConcurrencyException occurs...
                catch (DbUpdateConcurrencyException)
                {
                    // If the team with the provided ID does not exist...
                    if (!TeamExists(team.teamID))
                    {
                        // Return NotFound
                        return NotFound();
                    }
                    else
                    {
                        // Otherwise throw an exception
                        throw;
                    }
                }

                // Redirect to the Index view
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        /// <summary>
        /// Generates the delete view to delete a record
        /// </summary>
        /// <param name="id">The ID of the record</param>
        /// <returns>Returns the view with the team content</returns>
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            // If ID is null...
            if (id == null)
            {
                //Return NotFound
                return NotFound();
            }

            // Store the team content in a variable
            var team = await _context.Teams.FirstOrDefaultAsync(m => m.teamID == id);

            // If the team is null...
            if (team == null)
            {
                // Return NotFound
                return NotFound();
            }

            // Return the delete view with the team content
            return View(team);
        }

        /// <summary>
        /// Generates the Delete view in post mode with an action to delete the record
        /// </summary>
        /// <param name="id">The ID of the selected record</param>
        /// <returns>Returns to the Index with a deleted record</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Store the team from the context in a variable
            var team = await _context.Teams.FindAsync(id);
            // Remove the team from the context
            _context.Teams.Remove(team);
            // Save the changes to the context
            await _context.SaveChangesAsync();
            // Return to Index
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if the team exists
        /// </summary>
        /// <param name="id">The ID used to check for a team</param>
        /// <returns></returns>
        private bool TeamExists(int id)
        {
            // Return true if the team exists. If not, return false
            return _context.Teams.Any(e => e.teamID == id);
        }
    }
}
