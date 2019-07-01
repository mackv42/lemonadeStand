using System;
using System.Collections.Generic;
using System.Text;

namespace LemonadeStand
{
    using recipe = Dictionary<String, int>;
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

        public static int promptForInteger( String prompt )
        {
            int _return = 0;
            loopUntilTrue(() =>
                {
                    try
                    {
                        Console.Write(prompt);
                        _return = Int32.Parse(Console.ReadLine());
                    } catch( FormatException E)
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

        public static double[] promptForMoney( String item,  double price )
        {
            Console.WriteLine($"It's ${price} per {item}");
            Console.WriteLine($"How Many {item}s would you like to Buy?");
            
            try
            {
                Console.Write("Buy: ");
                int quantity = Int32.Parse(Console.ReadLine());
                if(quantity < 0) {
                    return new double[2] { (double)quantity*-1, quantity*-1 * price };
                }
                return new double[2] {(double)quantity, quantity * price};
            } catch( FormatException E)
            {
                Console.WriteLine("Invalid you need to enter a number/n/n");
                return promptForMoney(item, price);
            }
        }

        public Game()
        {
            currentRecipe = new recipe();
            currentRecipe.Add("lemon", 5);
            currentRecipe.Add("sugar", 5);
            currentRecipe.Add("ice", 5);

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
            recipe stock = Lemonade.InstantiateLemonade();

            for (int currentDay = 0; currentDay < days; currentDay++)
            {
                stand.displayWallet();
                stand.showItemsInStock();
                Console.WriteLine("What Recipe would you like to use?");
                currentRecipe["lemon"] = promptForInteger("Lemons: ");
                currentRecipe["sugar"] = promptForInteger("Sugar: ");
                currentRecipe["ice"] = promptForInteger("Ice: ");
                stand.changeRecipe(currentRecipe);

                stock["lemon"] = (int)promptForMoney("Lemon", .25)[0];
                stock["sugar"] = (int)promptForMoney("Sugar", .25)[0];
                stock["ice"] = (int)promptForMoney("Ice", .25)[0];

                double price = promptForDouble("Price / Cup: ");

                stand.stockItems(stock);
                Console.Clear();

                Day day = new Day(ref stand);
                day.start(ref stand);
            }

        }
    }
}
