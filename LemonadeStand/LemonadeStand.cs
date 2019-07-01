using System;
using System.Collections.Generic;
using System.Text;

namespace LemonadeStand
{
    using recipe = Dictionary<String, int>;
    class LemonadeStand
    {
        private double money;
        private double pricePerCup;
        recipe ingredientsInStock;
        recipe currentRecipe;
        private Pitcher pitcher;
        public LemonadeStand(recipe r)
        {
            money = 15;
            ingredientsInStock = Lemonade.InstantiateLemonade();
            currentRecipe = r;
            
            pricePerCup = .25;
            this.pitcher = new Pitcher(r);
            pitcher.cups = 0;
        }

        public bool refillPitcher()
        {
           
                if (ingredientsInStock["lemon"] - currentRecipe["lemon"] <= 0|| ingredientsInStock["sugar"] - currentRecipe["sugar"] <= 0 || ingredientsInStock["ice"] - currentRecipe["ice"] <= 0)
                {
                    Console.WriteLine("Sold Out!");
                    return false;
                }


           
            ingredientsInStock["lemon"] -= currentRecipe["lemon"] * 10;
            ingredientsInStock["sugar"] -= currentRecipe["sugar"] * 10;
            ingredientsInStock["ice"] -= currentRecipe["ice"] * 10;
            pitcher.cups = 10;
            return true;
        }
        public double sellTo( Person customer)
        {
            pitcher.cups--;
            this.money += this.pricePerCup;
            return pricePerCup;
        }
        public void changePricePerCup()
        {

        }
        public void changeRecipe(recipe r)
        {
            currentRecipe["lemon"] = r["lemon"];
            currentRecipe["sugar"] = r["sugar"];
            currentRecipe["ice"] = r["ice"];
            pitcher.changeRecipe(r);
        }

        public void stockItems(recipe items)
        {
            ingredientsInStock["lemon"] += items["lemon"];
            ingredientsInStock["sugar"] += items["sugar"];
            ingredientsInStock["ice"] += items["ice"];
        }

        public void showItemsInStock()
        {
            Console.WriteLine("         Current Items in stock");
            Console.WriteLine($"Lemons: {ingredientsInStock["lemon"]} Sugar: {ingredientsInStock["sugar"]} Ice: {ingredientsInStock["ice"]}");
        }

        public void displayWallet()
        {
            Console.WriteLine($"You have ${money}");
        }
        public double getPrice()
        {
            return this.pricePerCup;
        }
        public bool checkPitcher()
        {
            if(pitcher.cups <= 0)
            {
                if (!refillPitcher())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
