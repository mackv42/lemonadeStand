using System;
using System.Collections.Generic;
using System.Text;

namespace LemonadeStand
{
    using recipe = Dictionary<String, int>;
    class Person
    {
        private double money;
        private double satisfaction;
        private double buyChance;
        private recipe preference;

        public String name;
        public Person(List<String> peopleNames)
        {
            // influences if they want to buy
            Random rnd = new Random();
            this.money = rnd.Next(0, 10);
            
            //assigns a random name
            this.name = peopleNames[rnd.Next(0, peopleNames.Count - 1)];
        }

        

        public void influenceBuyChance(Func<double> f)
        {
            buyChance += f();
        }

        public void influenceBuyChance(double i)
        {
            buyChance += i;
        }


        public void influencePreference(Func<recipe> f)
        {
            recipe recipe_influence = f();
            this.preference["lemon"] += recipe_influence["lemon"];
            this.preference["sugar"] += recipe_influence["sugar"];
            this.preference["ice"] += recipe_influence["ice"];
        }

        public bool makeDecision( ref LemonadeStand L )
        {
            buyChance = 1;
            //decides if they want some lemonade

            Random rnd = new Random();
            Func<bool> decision = () => {
                double n = rnd.Next(0, 100) * buyChance;
                if(n > 50)
                {
                    return true;
                }
                return false;
            };

            if (decision())
            {
                //buyin some lemonade
                if (this.money > L.getPrice())
                {
                    double cost = L.sellTo(this);
                    this.money -= cost;
                    Console.WriteLine($"{this.name}: Like the lemonade");
                    return true;
                }
                else
                {
                    return false;
                    Console.WriteLine($"{name}: I don't have the money for that!");
                }
            }
            else
            {
                Console.WriteLine();
                return false;
            }
        }

    }
}
