namespace tic_tac_toe
{
    internal class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                int option = Game.Welcome();
                switch (option)
                {
                    case 1:
                        Game.StartGame(vsComputer: false);
                        break;
                    case 2:
                        Game.StartGame(vsComputer: true);
                        break;
                    case 3:
                        Game.ShowRules();
                        break;
                    case 4:
                        Game.ExitGame();
                        break;
                }
            }
        }
    }
}
