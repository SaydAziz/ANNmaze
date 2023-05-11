using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnMaze
{
    internal class Entity
    {
        public string name = "Player";
        public float powerLVL;

        Item[] inventory = new Item[6];

        public void EquipItem(Item item)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = item;
                    return;
                }
            }
            Console.WriteLine("Your Inventory is Full. Cannot add any more Items.");
        }

        public void ResetInv()
        {
            inventory = new Item[6];
        }

        public void PrintInv()
        {
            Console.WriteLine("\nINVENTORY:");

            for (int i = 0; i < inventory.Length;i++)
            {
                if (inventory[i] == null)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("- " + inventory[i].name);
                }
            }
            Console.WriteLine(" ");
        }

        public double[,] getInv()
        {
            double[,] inv = new double[1, 6];

            for (int i = 0; i < 6; i++)
            {
                inv[0, i] = inventory[i].power;
            }

            return inv;
        }


    }
}
