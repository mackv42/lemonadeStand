using System;
using System.Collections.Generic;
using System.Text;

namespace LemonadeStand
{
    class Day
    {
        private int temperature;
        
        public Day( ref LemonadeStand stand )
        {
            Random rnd = new Random();
            temperature = rnd.Next(0, 50) + 50;
        }

        public int[] start( ref LemonadeStand stand, String weatherState, int temperature )
        {
            
            List<String> peopleNames = new List<String>{
                "Joe", "Jimmy", "Jon", "Velda", "Colleen", "Borus", "Patrick", "Anna", "Joanne",
                "Upsilon", "Plato", "Socrates", "Earl", "Ingryd", "Ella", "Terryl"};


            //preference according to weather
            int potentialCustomers = 0;
            int soldTo = 0;
            Random rnd = new Random();
            // 10 waves of 3-10 people
                for ( int i=0; i<10; i++) {
                    //make a random amount of people appear here
                    for (int j = 0; j < rnd.Next(3, 10); j++)
                    {
                        if (stand.checkPitcher())
                        {
                            Person p = new Person(peopleNames);
                            // how weather influences person
                            if (weatherState == "Sunny") p.influenceBuyChance(0.3);
                            if (weatherState == "Rainy") p.influenceBuyChance(-0.3);
                            p.influenceBuyChance(stand.getPopularity);
                            p.influenceBuyChance(.25 - stand.getPrice());

                            p.influenceBuyChance(temperature/65);
                            
                            if (p.makeDecision(ref stand))
                            {
                                soldTo++;
                            }
                        }
                        
                        potentialCustomers++;
                    }
                    //people make a decision if they want to by here
                    System.Threading.Thread.Sleep(2000);
                }
            //returns money made and popularity gained
            Console.Clear();
            Console.WriteLine($"You Sold to {soldTo} of {potentialCustomers} potential customers");
            return new int[2] { 0, 1 };
        }
    }
}
