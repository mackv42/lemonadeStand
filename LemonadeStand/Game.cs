using System;
using System.Collections.Generic;
using System.Text;

namespace LemonadeStand
{

    class Game
    {

        private byte difficulty;
        private List<LemonadeStand> lemonadeStands;
        public recipe currentRecipe;
        public static bool yesNo(String question)
        {
            string answer = ".";
            while (true)
            {
                Console.Write("{0} Y/N ", question);
                answer = Console.ReadLine();

                if (answer == "Y" || answer == "y")
                {
                    return true;
                }
                if (answer == "N" || answer == "n")
                {
                    return false;
                }
                Console.WriteLine("Invalid it's a yes or no question");
            }
        }

        public static void loopUntilTrue(Func<bool> f)
        {
            while (!f()) { }
        }

        public static int promptForInteger(String prompt)
        {
            int _return = 0;
            loopUntilTrue(() =>
                {
                    try
                    {
                        Console.Write(prompt);
                        _return = Int32.Parse(Console.ReadLine());
                    } catch (FormatException E)
                    {
                        Console.WriteLine("You need To enter a number!");
                        return false;
                    }

                    return true;
                }
            );

            return _return;
        }

        public static double promptForDouble(String prompt)
        {
            double _return = 0;
            loopUntilTrue(() =>
            {
                try
                {
                    Console.Write(prompt);
                    _return = Double.Parse(Console.ReadLine());
                }
                catch (FormatException E)
                {
                    Console.WriteLine("You need To enter a number!");
                    return false;
                }

                return true;
            }
            );

            return _return;
        }

        public static double[] promptForMoney(String item, double price)
        {
            Console.WriteLine($"It's ${price} per {item}");
            Console.WriteLine($"How Many {item}s would you like to Buy?");

            try
            {
                Console.Write("Buy: ");
                int quantity = Int32.Parse(Console.ReadLine());
                if (quantity < 0) {
                    return new double[2] { (double)quantity * -1, quantity * -1 * price };
                }
                return new double[2] { (double)quantity, quantity * price };
            } catch (FormatException E)
            {
                Console.WriteLine("Invalid you need to enter a number/n/n");
                return promptForMoney(item, price);
            }
        }

        public Game()
        {
            currentRecipe = new recipe(5, 5, 5);

            playGame();
        }

        public void playGame() {
            Console.WriteLine("Welcome To Lemonade Stand!");
            
            Console.WriteLine("How Many Days Would You like to play for?");
            int days = 0;
            loopUntilTrue(() =>
               {
                   try
                   {
                       days = Int32.Parse(Console.ReadLine());

                   } catch (FormatException f)
                   {
                       Console.WriteLine("You need to Enter a number!");
                       return false;
                   }

                   return true;
               }
            );

            
            LemonadeStand stand = new LemonadeStand(currentRecipe);
            

            for (int currentDay = 0; currentDay < days; currentDay++)
            {
                List<String> weatherStates = new List<String> { "Rainy", "Cloudy", "Sunny" };

                Random rnd = new Random();
                String weatherState = weatherStates[rnd.Next(0, 2)];
                
                int temperature = 0;
                temperature = rnd.Next(50, 100);

                Console.WriteLine($"We have {weatherState} weather and its {temperature} degrees outside");
                Console.WriteLine($"You have {stand.getPopularity()} popularity.");
                if (!Game.yesNo("Would you like to Play through this day? [Save on potentially wasting resources]"))
                {
                    continue;
                }

                stand.showItemsInStock();
                stand.promptForRecipe();
                stand.displayWallet();
                stand.buyItems();
                Console.Clear();

                Day day = new Day(ref stand);
                day.start(ref stand, weatherState, temperature);
            }

        }
    }
}
