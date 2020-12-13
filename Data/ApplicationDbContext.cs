/* Daniel Hinbest
 * NETD 3202 - Communication Activity 5
 * December 13, 2020
 * This new version now includes sign-in functionality with the Identity Framework
 * 
 * This ASP.NET Core application is designed to allow the user to enter the information for a curling game, including the teams and the scores, and also provides functionality for editing, and deleting results.
 * The context for the database records. This class takes the content in the models and stores them in a DbSet to get saved in a table
 */
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NETD3202_Communication5.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETD3202_Communication5.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>
        /// Generates an instance of the ScoresContext
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// The DbSet for the Scores table
        /// </summary>
        public DbSet<Score> Scores { get; set; }

        /// <summary>
        /// The DbSet for the Teams table
        /// </summary>
        public DbSet<Team> Teams { get; set; }
    }
}
