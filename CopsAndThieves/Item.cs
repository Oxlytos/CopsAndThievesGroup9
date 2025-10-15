using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThieves
{
    internal class Item
    {
        //Name of the item i.e. Iphone 7, Wallet etc
        public string Name { get; set; }

        //Red, 256gb Storage, Notes totaling 50€ in value, Picture of a family member etc
        public string Description { get; set; }

        //Some total value of an item (steal something worth alot => More prison time?)
        public double Value { get; set; }

        //Mark something as stolen
        public bool Stolen { get; set; }

        //Original owner either just name or the Person class as a direct reference
        public string OriginalOwner { get; set; }


    }

    class Phone : Item
    {
        public bool PasswordProtected { get; set; }
    }

    class ServiceEquipment : Item
    {
        //Radio, Baton, Gun
        bool policeOwned = true;
    }
}
