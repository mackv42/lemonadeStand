using System;
using System.Collections.Generic;
using System.Text;

namespace LemonadeStand
{
    public class UI
    {
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
                    if( _return < 0)
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException E)
                {
                    Console.WriteLine("You need To enter a number greater than 0!");
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
                    if(_return < 0)
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException E)
                {
                    Console.WriteLine("You need To enter a decimal greater than 0!");
                    return false;
                }

                return true;
            }
            );

            return _return;
        }

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



        public static double[] promptForMoney(String item, double price)
        {
            Console.WriteLine($"It's ${price} per {item}");
            Console.WriteLine($"How Many {item}s would you like to Buy?");

            try
            {
                Console.Write("Buy: ");
                int quantity = Int32.Parse(Console.ReadLine());
                if (quantity < 0)
                {
                    throw new FormatException();
                }
                if (quantity < 0)
                {
                    return new double[2] { (double)quantity * -1, quantity * -1 * price };
                }
                return new double[2] { (double)quantity, quantity * price };
            }
            catch (FormatException E)
            {
                Console.WriteLine("Invalid you need to enter a number greater than 0");
                return promptForMoney(item, price);
            }
        }
        
        public static void clearAndDisplay(String s, int x)
        {
            Console.Clear();
            UI.writeAt(s, x);
            System.Threading.Thread.Sleep(1500);
            Console.Clear();
        }

        public static void writeAt(String s, int x)
        {
            Console.SetCursorPosition(x, 0);
            Console.Write(s);
        }

        public static int cy;
        public static void writeLineAt(String s, int x)
        {
            cy++;
            Console.SetCursorPosition(x, cy);
            Console.WriteLine(s);
        }
    }
}
