using System;
using System.Collections.Generic;
using System.Text;

namespace LemonadeStand
{
    using recipe = Dictionary<String, int>;
    class Lemonade
    {
        public static recipe InstantiateLemonade()
        {
            recipe r = new recipe();
            r["lemon"] = 0;
            r["sugar"] = 0;
            r["ice"] = 0;
            return r;
        }
    }
}
