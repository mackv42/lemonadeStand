using System;
using System.Windows;
using System.ComponentModel;

using System.Drawing;
namespace LemonadeStand
{
    class Program
    {
        static void Main(string[] args)
        {
            Game g = new Game();
           
            int numPlayers = 1;

            UI.loopUntilTrue(() =>
            {
                try
                {
                    UI.writeAt("How Many Players[1/2]: ", 40);
                    numPlayers = Int32.Parse(Console.ReadLine());
                    if(numPlayers < 1 || numPlayers > 2)
                    {
                        throw new FormatException();
                    }
                }
                catch( FormatException F )
                {
                    UI.clearAndDisplay("Invalid you need to enter 1 or 2", 35);
                    
                    return false;
                }
               
                return true;
            }
            );

            if(numPlayers == 1)
            {
                g.playGame();
            }
            else
            {
                g.twoPlayers();
            }
        }
    }
}
