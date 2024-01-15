using System.Reflection;
using System.Security.Cryptography;

namespace tic_tac_toe
{
    internal class Game
    {
        /// <summary>
        /// Show welcome message and ask the user to select the game mode
        /// </summary>
        /// <returns>Game mode</returns>
        public static int Welcome()
        {
            Console.Write("+" + new string('-', 30) + "+" + "\n" + "| Welcome to Tic Tac Toe Game! |" + "\n" + "+" + new string('-', 30) + "+");

            Console.Write("\n\nPlease select the game mode:\n1. VS Player\n2. VS Computer\n\nOr:\n3. Show rules\n4. Exit\n\nYour choice: ");

            return Utils.ValidateInput(Console.ReadLine(), [1, 2, 3, 4]);
        }

        public static void StartGame(bool vsComputer = false)
        {
            Console.Clear();
            Console.WriteLine(new string('-', 30) + "\n\tTIC TAC TOE\n" + new string('-', 30));
            Console.Write($"Game mode: {(vsComputer ? "Player VS Computer" : "Player 1 vs Player 2")}\n\n");

            string[][] choices = [["-", "-", "-"], ["-", "-", "-"], ["-", "-", "-"]];
            Dictionary<int, int[]> posInBoard = new() { { 0, [0, 0] }, { 1, [0, 1] }, { 2, [0, 2] }, { 3, [1, 0] }, { 4, [1, 1] }, { 5, [1, 2] }, { 6, [2, 0] }, { 7, [2, 1] }, { 8, [2, 2] } };

            DrawBoard(choices);

            Random rnd = new();
            int turn = rnd.Next(1, 3);

            if (vsComputer) Console.Write($"{(turn == 1 ? "Player" : "Computer")} starts the game!\n");
            else Console.Write($"Player {turn} starts the game!\n");

            Thread.Sleep(1500);
            Console.Clear();

            List<int> availableChoices = [0, 1, 2, 3, 4, 5, 6, 7, 8];
            int turnsPlayed = 0;

            while (true)
            {
                DrawBoard(choices);

                if (turnsPlayed >= 5)
                {
                    string winner = CheckWinner(choices, posInBoard);

                    if (winner != null)
                    {
                        EndGame(choices, winner, vsComputer);
                        return;
                    }
                }

                if (vsComputer) Console.Write($"{(turn == 1 ? "Player" : "Computer")} turn: ");
                else Console.Write("Player " + turn + " turn: ");

                int choice;
                if (vsComputer && turn == 2)
                {
                    int randIndex = rnd.Next(availableChoices.Count);
                    choice = availableChoices[randIndex];
                    Thread.Sleep(1500);
                }
                else
                {
                    choice = Utils.ValidateInput(Console.ReadLine(), [0, 1, 2, 3, 4, 5, 6, 7, 8]);
                }
                string figure = turn == 1 ? "X" : "O";

                if (choices[posInBoard[choice][0]][posInBoard[choice][1]] == "-")
                {
                    choices[posInBoard[choice][0]][posInBoard[choice][1]] = figure;
                    turn = turn == 1 ? 2 : 1;

                    availableChoices.Remove(choice);
                }
                else
                {
                    Console.Write("\nThis position is already taken. Please choose another one.\n");
                    Thread.Sleep(1500);
                }
              
                turnsPlayed++;
                Console.Clear();
            }
        }

        public static string CheckWinner(string[][] currentGameBoard, Dictionary<int, int[]> positions)
        {
            int[][] winningCombinations = [
                [0, 1, 2], // Rows
                [3, 4, 5],
                [6, 7, 8],
                [0, 3, 6], // Columns
                [1, 4, 7],
                [2, 5, 8],
                [0, 4, 8], // Diagonals
                [2, 4, 6]
               ];

            string winner = "";

            foreach (int[] combination in winningCombinations)
            {
                int xCount = 0;
                int oCount = 0;

                foreach (int position in combination)
                {
                    string figure = currentGameBoard[positions[position][0]][positions[position][1]];
                    if (figure == "-") break;

                    _ = figure == "X" ? xCount++ : oCount++;

                    winner = xCount == 3 ? "X" : oCount == 3 ? "O" : null;
                }

                if (winner != null) break;
            }

            return winner;
        }

        /// <summary>
        /// Show the rules of the game
        /// </summary>
        public static void ShowRules()
        {
            Console.Clear();
            Console.Write(new string('=', 22) + "\n\tRULES\n" + new string('=', 22));
            Console.Write("\n\nEnter a number that corresponds to the position on the board. \nAlign 3 dots vertically, horizontally or diagonally to win.\n\n");

            DrawBoard([], true);

            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Draw the board with the choices
        /// </summary>
        /// <param name="choices">Actions performed by users or the computer</param>
        /// <param name="isRules">If true, shows the number of each position on the board. Use only to show the rules</param>
        public static void DrawBoard(string[][] choices, bool isRules = false)
        {
            if (isRules) choices = [["0", "1", "2"], ["3", "4", "5"], ["6", "7", "8"]];

            for (int i = 0; i < choices.Length; i++)
            {
                if (i != 0) Console.Write("\n-----------\n");
                for (int j = 0; j < choices[i].Length; j++)
                {
                    Console.Write(" " + choices[i][j] + " " + (j != 2 ? "|" : ""));
                }
            }

            Console.Write("\n\n");
        }

        public static void EndGame(string[][] finalBoard, string winner, bool vsComputer)
        {
            Console.Clear();
            Console.WriteLine(new string('-', 30) + "\n\tTIC TAC TOE\n" + new string('-', 30) + "\n");
            DrawBoard(finalBoard);

            string formattedWinner;

            if (vsComputer) formattedWinner = winner == "X" ? "Player" : "Computer";

            else formattedWinner =  winner == "X" ? "Player 1" : "Player 2";

            Console.Write("\n" + new string('*', 40) + "\n\n" + formattedWinner + " wins the game!. Congratulations" + "\n\n" + new string('*', 40));
            Console.ReadKey();
        }

        /// <summary>
        /// Exit the game
        /// </summary>
        public static void ExitGame()
        {
            Console.Write("\nThanks for playing! See you next time!");
            Environment.Exit(0);
        }
    }
}
