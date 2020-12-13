/* Daniel Hinbest
 * NETD 3202 - Lab 5
 * December 6, 2020
 * This ASP.NET Core application is designed to allow the user to enter the information for a curling game, including the teams and the scores, and also provides functionality for editing, and deleting results.
 * This model sets the content for the score record, with the scores for each team as well as how many ends each team won in a game
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETD3202_Communication5.Models
{
    public class Score
    {
        /// <summary>
        /// The ID number of the score, also the primary key of the table
        /// </summary>
        public int scoreID { get; set; }

        /// <summary>
        /// The final score for the first team
        /// </summary>
        public int teamOneTotal { get; set; }

        /// <summary>
        /// The final score for the second team
        /// </summary>
        public int teamTwoTotal { get; set; }

        /// <summary>
        /// The number of ends the first team took in the game
        /// </summary>
        public int teamOneEnds { get; set; }

        /// <summary>
        /// The number of ends the second team took in the game
        /// </summary>
        public int teamTwoEnds { get; set; }
    }
}
