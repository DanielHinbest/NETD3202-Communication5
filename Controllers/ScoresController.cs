/* Daniel Hinbest
 * NETD 3202 - Lab 5
 * December 6, 2020
 * This ASP.NET Core application is designed to allow the user to enter the information for a curling game, including the teams and the scores, and also provides functionality for editing, and deleting results.
 * The ScoresController generates the views for the scores tables
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NETD3202_Communication5.Data;
using NETD3202_Communication5.Models;
using Microsoft.AspNetCore.Authorization;

namespace NETD3202_Communication5.Controllers
{
    public class ScoresController : Controller
    {
        // Creates a readonly instance of the ScoresContext
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Creates an instance of the ScoresController with the ScoresContext parameter
        /// </summary>
        /// <param name="context">Sets the context to the parameter</param>
        public ScoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Generates the Scores index view with the table in a list
        /// </summary>
        /// <returns>The Scores index view with the scores table converted to a list</returns>
        public async Task<IActionResult> Index()
        {
            // Returns the scores view in a list
            return View(await _context.Scores.ToListAsync());
        }

        /// <summary>
        /// Generates the details view with the content of the selected record
        /// </summary>
        /// <param name="id">The id of the record selected</param>
        /// <returns>Returns NotFound if invalid, otherwise the details view with the selected score</returns>
        public async Task<IActionResult> Details(int? id)
        {
            // If the ID is not set...
            if (id == null)
            {
                // Return NotFound
                return NotFound();
            }

            // Take the score content with the selected ID and store it in the score variable
            var score = await _context.Scores.FirstOrDefaultAsync(m => m.scoreID == id);

            // If the score is not set...
            if (score == null)
            {
                // Return NotFound
                return NotFound();
            }

            // Return the view with the score record
            return View(score);
        }

        /// <summary>
        /// Generates the Create view, which allows the user to create a new score record
        /// </summary>
        /// <returns>Returns the Create view</returns>
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Records the information stored in the create view into the scores table
        /// </summary>
        /// <param name="scores">This parameter is the scores entered</param>
        /// <returns>Returns the view with the scores entered in post mode</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("scoreID,teamOneTotal,teamTwoTotal,teamOneEnds,TeamTwoEnds")] Score scores)
        {
            // If the model is valid...
            if (ModelState.IsValid)
            {
                // Add the entered scores into the context and redirect to the Index
                _context.Add(scores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Return the view with the scores
            return View(scores);
        }

        /// <summary>
        /// After the user selects a record from the Scores Index, this allows the user to edit the score
        /// </summary>
        /// <param name="id">The ID of the score being altered</param>
        /// <returns>Returns the Edit view with the scores</returns>
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            // If the ID is not set...
            if (id == null)
            {
                // Return NotFound
                return NotFound();
            }

            // Find the ID of the selected score and store it in a variable
            var scores = await _context.Scores.FindAsync(id);

            // If scores is not set...
            if (scores == null)
            {
                // Return NotFound
                return NotFound();
            }

            // Return the Edit view with the scores
            return View(scores);
        }

        /// <summary>
        /// Updates the edited score from the Scores table
        /// </summary>
        /// <param name="id">The ID number of the selected score</param>
        /// <param name="scores">The content of the score</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("scoreID,teamOneTotal,teamTwoTotal,teamOneEnds,TeamTwoEnds")] Score scores)
        {
            // If the ID does not match the Score ID...
            if (id != scores.scoreID)
            {
                // Return NotFound
                return NotFound();
            }

            // If the model is valid
            if (ModelState.IsValid)
            {
                // Try/Catch exception handling
                try
                {
                    // Update the context with the new scores content
                    _context.Update(scores);
                    // Save the changes to the context
                    await _context.SaveChangesAsync();
                }
                // If a DbUpdateConcurrencyException occurs...
                catch (DbUpdateConcurrencyException)
                {
                    // If the score does not exist...
                    if (!ScoreExists(scores.scoreID))
                    {
                        // Return NotFound
                        return NotFound();
                    }
                    // If the score exists...
                    else
                    {
                        // Throw an exception
                        throw;
                    }
                }

                // Redirect back to the index
                return RedirectToAction(nameof(Index));
            }

            //Return a view with the scores
            return View(scores);
        }

        /// <summary>
        /// Deletes a record from the database
        /// </summary>
        /// <param name="id">The ID of the deleted score</param>
        /// <returns>Returns a view of the scores</returns>
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            // If the ID is null...
            if (id == null)
            {
                // Return NotFound
                return NotFound();
            }

            // Get the score from the table and store it in a variable
            var scores = await _context.Scores.FirstOrDefaultAsync(m => m.scoreID == id);

            // If the score is null...
            if (scores == null)
            {
                // Return NotFound
                return NotFound();
            }

            // Return a view of the scores
            return View(scores);
        }

        /// <summary>
        /// Deletes the selected record from the database
        /// </summary>
        /// <param name="id">The ID of the selected record</param>
        /// <returns>Returns a redirection back to the Index</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Finds the record from the database with a matching ID and stores it in a variable
            var scores = await _context.Scores.FindAsync(id);

            // Removes the score from the context
            _context.Scores.Remove(scores);
            // Saves changes to the context to update the records
            await _context.SaveChangesAsync();
            // Return the user back to the Index
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// ScoreExists checks for the existance of the selected score
        /// </summary>
        /// <param name="id">The ID number of the score</param>
        /// <returns>Returns the result of the search</returns>
        public bool ScoreExists(int id)
        {
            // Returns the score with a matching ID
            return _context.Scores.Any(e => e.scoreID == id);
        }
    }
}
