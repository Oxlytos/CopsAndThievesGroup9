using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThieves
{
    public class Item
    {
        internal static Random random = new Random();
        //Name of the item i.e. Iphone 7, Wallet etc
        public string Name { get; set; }

        //Original owner either just name or the Person class as a direct reference
        public Person OriginalOwner { get; set; }

        public Item(string _name, Person _orgOwner)
        {
            Name = _name;
            OriginalOwner = _orgOwner;
        }

    }
    //Standard stuff really, emojiis for the fun factor
    class CivilianItem : Item
    {
        static string[] itemNames = { "📱 Phone", "🔑 Keys", "👛 Wallet", "⌚ Watch" };
        public CivilianItem(string _name, Person _orgOwner) : base(_name, _orgOwner)
        {
            _name = itemNames[random.Next(0, itemNames.Length)];
            Name = _name;
            OriginalOwner = _orgOwner;
        }
    }

    //Some other group had knives for theives, so a subsection of TheifItem : Item could be fun

    //If we ever wanted to explore the possibilites of more intrinsic law enforcment in this simulation
    //Police have their own items
    class PoliceItem : Item
    {
        static string[] itemNames = { "🔫 Gun", "🦯 Baton", "🎖️ Badge", "📱 Phone" };
        public PoliceItem(string _name, Person _orgOwner) : base(_name, _orgOwner)
        {
            _name = itemNames[random.Next(0, itemNames.Length)];
            Name = _name;
            OriginalOwner = _orgOwner;
        }
    }

}
