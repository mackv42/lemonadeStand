using System;
using System.Collections.Generic;
using System.Text;

namespace LemonadeStand
{ 
    public struct recipe
    {
        public int Lemon;
        public int Sugar;
        public int Ice;

        public recipe(int Lemon, int Sugar, int Ice)
        {
            this.Lemon = Lemon;
            this.Sugar = Sugar;
            this.Ice = Ice;
        }
    };
    public class LemonadeStand
    {
        private double money;
        private double pricePerCup;
        private double popularity;
        public int pos;
        recipe ingredientsInStock;
        recipe currentRecipe;
        private Pitcher pitcher;
        public LemonadeStand(recipe r)
        {
            this.pos = 0;
            money = 15;
            ingredientsInStock = Lemonade.InstantiateLemonade();
            currentRecipe = r;
            
            pricePerCup = .25;
            this.pitcher = new Pitcher(r);
            pitcher.cups = 0;
        }

        public LemonadeStand(recipe r, int pos)
        {
            //position it writes to
            this.pos = pos;

            money = 15;
            ingredientsInStock = Lemonade.InstantiateLemonade();
            currentRecipe = r;
            
            pricePerCup = .25;
            this.pitcher = new Pitcher(r);
            pitcher.cups = 0;
        }

        public bool refillPitcher()
        {
           
            if (ingredientsInStock.Lemon - currentRecipe.Lemon < 0|| ingredientsInStock.Sugar - currentRecipe.Sugar < 0 || ingredientsInStock.Ice - currentRecipe.Ice < 0)
            {
                UI.writeLineAt("Sold Out!", 45);
                return false;
            }
           
            ingredientsInStock.Lemon -= currentRecipe.Lemon;
            ingredientsInStock.Sugar -= currentRecipe.Sugar;
            ingredientsInStock.Ice -= currentRecipe.Ice;
            pitcher.cups = 10;
            return true;
        }
        public double sellTo( Person customer )
        {
            pitcher.cups--;
            this.money += this.pricePerCup;
            return pricePerCup;
        }
        public void changePricePerCup()
        {

        }

        public recipe getRecipe()
        {
            return currentRecipe;
        }
        public void changeRecipe(recipe r)
        {
            currentRecipe.Lemon = r.Lemon;
            currentRecipe.Sugar = r.Sugar;
            currentRecipe.Sugar = r.Sugar;
            pitcher.changeRecipe(r);
        }

        public void promptForRecipe()
        {
            Console.WriteLine("What Recipe would you like to use?");
            currentRecipe.Lemon = UI.promptForInteger("Lemons: ");
            currentRecipe.Sugar = UI.promptForInteger("Sugar: ");
            currentRecipe.Ice = UI.promptForInteger("Ice: ");
        }

        public void buyItems()
        {
            recipe stock = Lemonade.InstantiateLemonade();
            

            double [] lemons = UI.promptForMoney("Lemon", .10);
            double [] sugar = UI.promptForMoney("Sugar", .12);
            double [] ice = UI.promptForMoney("Ice Cube", .01);
            this.pricePerCup = UI.promptForDouble("Price / Cup: ");
            stock.Lemon += (int)lemons[0];
            stock.Sugar += (int)sugar[0];
            stock.Ice += (int)ice[0];
            this.money -= lemons[1] + sugar[1] + ice[1];
            this.stockItems(stock);
        }

        public void stockItems(recipe items)
        {
            ingredientsInStock.Lemon += items.Lemon;
            ingredientsInStock.Sugar += items.Sugar;
            ingredientsInStock.Ice += items.Ice;
        }

        public void showItemsInStock()
        {
            Console.WriteLine("         Current Items in stock");
            Console.WriteLine($"Lemons: {ingredientsInStock.Lemon} Sugar: {ingredientsInStock.Sugar} Ice: {ingredientsInStock.Ice}");
        }

        public void displayWallet()
        {
            Console.WriteLine($"You have ${money}");
        }
        public double getPrice()
        {
            return this.pricePerCup;
        }

        public void incrementPopularity()
        {
            popularity += 0.005;
        }

        public double getPopularity()
        {
            return popularity;
        }

        public bool checkPitcher()
        {
            if (pitcher.cups <= 0)
            {
                if (!refillPitcher())
                {
                    return false;
                }
            }
            return true;
        }

        public double getMoney()
        {
            return money;
        }
    }
}
