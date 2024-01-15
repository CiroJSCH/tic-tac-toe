using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tic_tac_toe
{
    internal class Utils
    {
        /// <summary>
        /// Validate if the user input is valid
        /// </summary>
        /// <param name="userInput">Value entered through the console by the user</param>
        /// <param name="validOptions">List of options that the user can choose</param>
        /// <returns>Valid user input</returns>
        public static int ValidateInput(string userInput, int[] validOptions)
        {
            while (true)
            {
                try
                {
                    if (validOptions.Contains(Convert.ToInt32(userInput))) return int.Parse(userInput);
                    else throw new FormatException();
                }
                catch (FormatException)
                {
                    Console.Write("\nPlease enter a valid option: ");
                    userInput = Console.ReadLine();
                }
            }
        }
    }
}
