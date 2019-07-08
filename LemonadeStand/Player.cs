using System;
using System.Collections.Generic;
using System.Text;

namespace LemonadeStand
{
    public class Player
    {
        private LemonadeStand stand;

        public Player()
        {
            this.stand = new LemonadeStand(new recipe(5, 5, 5));
        }

        public Player(LemonadeStand stand)
        {
            this.stand = stand;
        }

        public ref LemonadeStand getStand()
        {
            return ref stand;
        }

        public bool playDay()
        {
            if (!UI.yesNo("Would you like to Play through this day? [Save on potentially wasting resources]"))
            {
                return true;
            }

            return false;
        }

        public void shop()
        {
            stand.showItemsInStock();
            stand.displayWallet();
            stand.promptForRecipe();
            stand.buyItems();
            Console.Clear();
        }

    }
}
