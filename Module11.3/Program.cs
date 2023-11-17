using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module11._3
{
    // enumeration of food
    enum FoodType
    {
        Fish,
        Mouse,
        Milk
    }

    // Cat 
    class Cat
    {
        private int hungerLevel; 

        public int HungerLevel
        {
            get { return hungerLevel; }
        }

        // the way to eat
        public void EatFood(FoodType food)
        {
            switch (food)
            {
                case FoodType.Fish:
                    hungerLevel -= 3;
                    break;
                case FoodType.Mouse:
                    hungerLevel -= 2;
                    break;
                case FoodType.Milk:
                    hungerLevel -= 1;
                    break;
                default:
                    Console.WriteLine("Unknown food type");
                    break;
            }

            
            if (hungerLevel < 0)
            {
                hungerLevel = 0;
            }
        }

        public Cat()
        {
            hungerLevel = 10;
        }
    }

    class Program
    {
        static void Main()
        {
            Cat myCat = new Cat();

            Console.WriteLine($"Initial Hunger Level: {myCat.HungerLevel}");

            myCat.EatFood(FoodType.Fish);
            Console.WriteLine($"After eating Fish. Hunger Level: {myCat.HungerLevel}");

            myCat.EatFood(FoodType.Mouse);
            Console.WriteLine($"After eating Mouse. Hunger Level: {myCat.HungerLevel}");

            myCat.EatFood(FoodType.Milk);
            Console.WriteLine($"After drinking Milk. Hunger Level: {myCat.HungerLevel}");

            myCat.EatFood((FoodType)100);

            Console.ReadLine();
        }
    }

}
