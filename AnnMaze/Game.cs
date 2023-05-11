using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AnnMaze
{
    internal class Game
    {
        List<Item> arsenalInv = new List<Item>();
        Entity player;
        NeuralNetwork neuralNetwork;

        int kills;

        double[,] enemyInv;

        public Game(NeuralNetwork neuralNet)
        {
            player = new Entity();
            neuralNetwork = neuralNet;
        }


        public void Start()
        {
            
            bool playing = true;
            Console.WriteLine("Welcome to the Death Maze!!!");
            Console.WriteLine("You have access to the Arsenal, where you can pick out your gear which you will use to defeat the monsters.");
            PopulateArsenal();

            while (playing)
            {
                Reset();

                Console.Write("Press any key to enter the Arsenal...  ");
                Console.ReadKey(true);          

                for (int i = 0; i < 6; i++)
                {
                    BuyCycle();
                }

                Console.Clear();
                player.PrintInv();
                CalculatePower(player);
                Console.WriteLine("-------------- LEVEL " + player.powerLVL + " --------------");

                Console.WriteLine("Press any key to enter the maze...");            
                Console.ReadKey(true);

                PlayMaze();
                Console.WriteLine("Would you like to try again? (y/n)");
                string choice = Console.ReadKey(true).KeyChar.ToString();

                if (choice == "n")
                {
                    playing = false;
                }

            }
        }


        void Reset()
        {
            kills = 0;
            player.ResetInv();
            enemyInv = null;
        }

        void PlayMaze()
        {
            SpawnEnemy();
            Console.WriteLine("You have entered the maze, but as you go through it you encounter something weird...");
            //Console.ReadKey(true);

            bool winning = true;

            while(winning)
            {
                winning = Fight();
                SpawnEnemy();
            }

            Console.Clear();
            Console.WriteLine("On your quest through the Maze, you managed to kill " + kills + " monsters. You have been commended for your efforts.\n\n");
        }

        void SpawnEnemy()
        {
            if (enemyInv == null)
            {
                enemyInv = new double[1, 6] { { 0, 0, 0, 0, 0, 0 } };
            }
            else
            {
                for (int i = 5; i > -1; i--)
                {
                    if (enemyInv[0,i] == 0)
                    {
                        enemyInv[0, i] = 1;
                        break;
                    }
                }
            }
        }

        bool Fight()
        {


            Console.WriteLine("\nIT'S AN ENEMY! (Press any key to fight...)\n");
            Console.ReadKey(true);

            var output = neuralNetwork.Think(enemyInv);
            int enemyLVL = int.Parse((output[0, 0] * 1000000).ToString().Substring(0, 2));
            Console.WriteLine("PLAYER (" + player.powerLVL + ") VERSUS MONSTER (" + enemyLVL + ")\n........................");


            if (player.powerLVL > enemyLVL)
            {
                Console.WriteLine("You have defeated the monster!\nYou keep going thorugh the maze...");
                Console.ReadKey(true);
                kills++;
                return true;
            }
            else
            {
                Console.WriteLine("You have been defeated by the monster.");
                Console.ReadKey(true);
                return false;
            }


        }
        void CalculatePower(Entity ent)
        {
            var output = neuralNetwork.Think(ent.getInv());
            //Console.WriteLine(output[0, 0]);
            ent.powerLVL = int.Parse((output[0, 0] * 1000000).ToString().Substring(0, 2));
        }

        private void BuyCycle()
        {
            Console.Clear();
            int choice;
            Console.WriteLine("---------THE ARESENAL---------");
            PrintArsenal();

            player.PrintInv();

            Console.WriteLine("Press the Number of the Item to equip it...");
            choice = int.Parse(Console.ReadKey(true).KeyChar.ToString());

            player.EquipItem(arsenalInv[choice]);
        }


        private void PrintArsenal()
        {
            int i = 0;
            foreach(Item item in arsenalInv)
            {
                Console.WriteLine(i + ". " + item.name);
                i++;
            }
            Console.WriteLine("------------------------------");

        }

        private void PopulateArsenal()
        {
            arsenalInv.Add(new Item(0, "Leather Clothing"));
            arsenalInv.Add(new Item(0, "Hunting Bow"));
            arsenalInv.Add(new Item(0, "Wooden Shield"));
            arsenalInv.Add(new Item(0, "Cooked Meat"));
            arsenalInv.Add(new Item(1, "Scythe"));
            arsenalInv.Add(new Item(1, "Iron Armor"));
            arsenalInv.Add(new Item(1, "Steel Sword"));
            arsenalInv.Add(new Item(1, "Crossbow"));
            arsenalInv.Add(new Item(1, "Steel Shield"));
            arsenalInv.Add(new Item(1, "Strength Potion"));

        }
    }
}
