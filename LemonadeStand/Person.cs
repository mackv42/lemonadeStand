using System;
using System.Collections.Generic;
using System.Text;

namespace LemonadeStand
{
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
            buyChance = 0;
            //assigns a random name
            this.name = peopleNames[rnd.Next(0, peopleNames.Count - 1)];

            this.preference = new recipe(rnd.Next(3, 7), rnd.Next(3, 7), rnd.Next(3, 7));
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
            this.preference.Lemon += recipe_influence.Lemon;
            this.preference.Sugar += recipe_influence.Sugar;
            this.preference.Ice += recipe_influence.Ice;
        }

        private bool inbetween(int p1, int p2, int n)
        {
            if (n >= p1 && n <= p2)
            {
                return true;
            }

            return false;
        }

        public bool likes(recipe r)
        {
            bool lemonThreshold = inbetween(preference.Lemon - 1, preference.Lemon + 1, r.Lemon);
            bool sugarThreshold = inbetween(preference.Sugar - 1, preference.Sugar + 1, r.Sugar);
            bool iceThreshold = inbetween(preference.Ice - 1, preference.Ice + 1, r.Ice);
            return (lemonThreshold | sugarThreshold) & iceThreshold;
        }

        public bool makeDecision( ref LemonadeStand L )
        {
          
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
                    
                    if (this.likes(L.getRecipe()))
                    {
                        Console.WriteLine($"{this.name}: Like the lemonade");
                        L.incrementPopularity();
                    }
                    else
                    {
                        Console.WriteLine($"{this.name}: This lemonade is meh");
                    }
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
