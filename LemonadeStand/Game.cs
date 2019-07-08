using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
namespace LemonadeStand
{

    class Game
    {
        //private byte difficulty;
        public recipe currentRecipe;

        public Game()
        {
            currentRecipe = new recipe(5, 5, 5);
        }

        public void playGame() {

            Console.WriteLine("Welcome To Lemonade Stand!");
            
            Console.WriteLine("How Many Days Would You like to play for?");
            int days = 0;
            UI.loopUntilTrue(() =>
               {
                   try
                   {
                       days = Int32.Parse(Console.ReadLine());

                   } catch (FormatException E)
                   {
                       Console.WriteLine("You need to Enter a number!");
                       return false;
                   }

                   return true;
               }
            );

            Player p1 = new Player();

            for (int currentDay = 0; currentDay < days; currentDay++)
            {
                double tmp = 0;
                List<String> weatherStates = new List<String> { "Rainy", "Cloudy", "Sunny" };

                Random rnd = new Random();
                String weatherState = weatherStates[rnd.Next(0, 2)];
                
                int temperature = 0;
                temperature = rnd.Next(50, 100);

                tmp = p1.getStand().getMoney();
                Console.WriteLine($"You have {p1.getStand().getMoney() - 15}  total net profit");
                Thread.Sleep(5000);
                Console.Clear();

                Console.WriteLine($"We have {weatherState} weather and its {temperature} degrees outside");
                Console.WriteLine($"You have {p1.getStand().getPopularity()} popularity.");

                if (!UI.yesNo("Would you like to Play through this day? [Save on potentially wasting resources]"))
                {
                    continue;
                }

                p1.shop();
                Console.Clear();
                
                Day day = new Day(ref p1.getStand());
                
                day.start(ref p1.getStand(), weatherState, temperature);
                
               
                Console.WriteLine($"You made {p1.getStand().getMoney() - tmp} net profit today.");
                
            }

            Console.WriteLine($"You made {p1.getStand().getMoney() - 15} net profit");
        }

        public void twoPlayers()
        {

            Console.WriteLine("Welcome To Lemonade Stand!");

            Console.WriteLine("How Many Days Would You like to play for?");
            int days = 0;
            UI.loopUntilTrue(() =>
            {
                try
                {
                    days = Int32.Parse(Console.ReadLine());

                }
                catch (FormatException E)
                {
                    Console.WriteLine("You need to Enter a number!");
                    return false;
                }

                return true;
            }
            );

            Player p1 = new Player();
            Player p2 = new Player(new LemonadeStand(new recipe(3, 3, 3), 40));
            
            //LemonadeStand stand = new LemonadeStand(currentRecipe);
           // LemonadeStand stand2 = new LemonadeStand(currentRecipe2, 40);

            for (int currentDay = 0; currentDay < days; currentDay++)
            {
               
                List<String> weatherStates = new List<String> { "Rainy", "Cloudy", "Sunny" };

                Random rnd = new Random();
                String weatherState = weatherStates[rnd.Next(0, 2)];

                int temperature = 0;
                temperature = rnd.Next(50, 100);

                
                /////////////////////////////// Player1 input /////////////////////////////////////////
                Console.WriteLine($"Player1 has {p1.getStand().getMoney() - 15}  total net profit");
                Thread.Sleep(2000);
                Console.Clear();

                Console.WriteLine($"We have {weatherState} weather and its {temperature} degrees outside");
                Console.WriteLine($"Player1 has {p1.getStand().getPopularity()} popularity.");

                bool p1Play = true;
                if (p1.playDay())
                {
                    p1Play = false;
                }
                else
                {
                    p1.shop();
                    Console.Clear();
                }

                //////////////////////////// Start p2 input ///////////////////////////////////////////
                Console.WriteLine($"Player 2 has {p2.getStand().getMoney() - 15}  total net profit");
                Thread.Sleep(3000);
                Console.Clear();

                Console.WriteLine($"We have {weatherState} weather and its {temperature} degrees outside");
                Console.WriteLine($"Player 2 has {p2.getStand().getPopularity()} popularity.");
                p2.getStand().displayWallet();
                bool p2Play = true;

                if (p2.playDay())
                {
                    p2Play = false;
                }
                else
                {
                    p2.shop();
                    Console.Clear();
                }

                ///////// Init threads for both players //////////////////
                if (p1Play && p2Play)
                {
                    Day day = new Day(ref p1.getStand());
                    Thread T;
                    T = new Thread(() => { day.start( ref p1.getStand(), weatherState, temperature); });
                    T.Start();

                    Day day2 = new Day(ref p2.getStand());
                    Thread T2= new Thread(() => { day2.start( ref p2.getStand(), weatherState, temperature); });
                    T2.Start();

                    T.Join();
                    T2.Join();
                } else if (p2Play)
                {
                    Day day = new Day(ref p2.getStand());
                    
                   day.start(ref p1.getStand(), weatherState, temperature);
                   
                }
                else if (p1Play)
                {
                    Day day2 = new Day(ref p1.getStand());
                    day2.start(ref p2.getStand(), weatherState, temperature);
                }

                //Console.WriteLine($"You made {stand.getMoney() - tmp} net profit today.");
            }

            System.Threading.Thread.Sleep(500);
            Console.WriteLine($"Player1 made {p1.getStand().getMoney() - 15} net profit");
            Console.WriteLine($"Player2 made {p2.getStand().getMoney() - 15} net profit");
        }
    }
}
