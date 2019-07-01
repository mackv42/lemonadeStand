using System;
using System.Collections.Generic;
using System.Text;

namespace LemonadeStand
{
    using recipe = Dictionary<String, int>;
    class Pitcher
    {
        private recipe ingredients;
        public int cups;
        public Pitcher(recipe ingredients)
        {
            this.ingredients = ingredients;
            cups = 0;
        }

        public void changeRecipe(recipe r)
        {
            ingredients = r;
        }

        public recipe getRecipe()
        {
            return ingredients;
        }
    }
}
