/* Daniel Hinbest
 * NETD 3202 - Lab 5
 * December 6, 2020
 * This new version now includes sign-in functionality with the Identity Framework
 * 
 * This ASP.NET Core application is designed to allow the user to enter the information for a curling game, including the teams and the scores, and also provides functionality for editing, and deleting results.
 * This model sets the Team ID, as well as each of the players at each position
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETD3202_Communication5.Models
{
    public class Team
    {
        /// <summary>
        /// The ID number for the table, also the primary key for the table
        /// </summary>
        public int teamID { set; get; }

        /// <summary>
        /// The lead's name. The lead is the player who throws first
        /// </summary>
        public string leadName { set; get; }

        /// <summary>
        /// The second's name. The second is the player who throws second
        /// </summary>
        public string secondName { set; get; }

        /// <summary>
        /// The vice's name. The vice throws third and takes over at the far end when the skip throws
        /// </summary>
        public string viceName { set; get; }

        /// <summary>
        /// The skip's name. The skip throws last and calls the game at the far end, essentially being the team captain.
        /// </summary>
        public string skipName { set; get; }
    }
}
