using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module8//.CS3110Module8GroupGreen
{
    internal class GreenPlayer : IPlayer
    {
        //Class Varriables

        //This list is used to determine attacks while following the attack pattern
        private readonly List<Position> attackList = new List<Position>(); 
        private readonly List<Ship> shipList = new List<Ship>(); //This list holds the list of ships 
        private static readonly Random rand = new Random(); //Random object used for placing ships

        //Constructor
        public GreenPlayer(string name)
        {
            Name = name;
        }

        //Class Functions
        //Generates all information needed before the first attack
        public void StartNewGame(int playerIndex, int gridSize, Ships ships) 
        {
            Index = playerIndex; //assign the player index to the index
            foreach (Ship s in ships._ships)//Minimize to make navigation easier 
            {
                bool valid = false; //control flag
                while (!valid) //while false
                {
                    valid = true; //set to true

                    int x = rand.Next(0, gridSize - 1); //get random x and y
                    int y = rand.Next(0, gridSize - 1); //coordinates

                    //get a random number between 0 and 2. if it is 1, set dir to horizontal, else set to vertical
                    Direction dir = rand.Next(0, 2) == 1 ? Direction.Horizontal : Direction.Vertical;
                    s.Place(new Position(x, y), dir); //create the positions of the ship
                    
                    //make sure that whichever direction the ship is facing, it does not go off the grid
                    //if it does, it is an invalid placement and needs to try again
                    if (dir == Direction.Horizontal)
                    {
                        if (x + s.Length - 1 >= gridSize)
                        {
                            valid = false;
                        }
                    }
                    else
                    {
                        if (y + s.Length - 1 >= gridSize)
                        {
                            valid = false;
                        }
                    }

                    if (valid) //if the valid flag is still true
                    {
                        foreach (Ship ship in shipList) //for each ship our ship list
                        {
                            foreach (Position shipPos in ship.Positions) //for each of those ships positions
                            {
                                //and each of the positions of the ship we're trying to place
                                foreach (Position sPos in s.Positions) 
                                {
                                    if (shipPos.X == sPos.X && shipPos.Y == sPos.Y) //if the positions are the same
                                    {
                                        valid = false; //set valid to false
                                    }
                                }
                            }
                        }
                    }

                    if (valid) //if the ship is still valid
                    {
                        shipList.Add(s); //it's a legal ship, add it
                    }                   
                }
            }    //Expand for Ship placement code  

            //Code for creating the attack list. 
            //add to the list of positions we want to attack, every 4th space on the grid to create an attack pattern
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if((i + j) % 4 == 3)
                    {
                        attackList.Add(new Position(i, j));
                    }
                }
            }

            //then
            foreach(Ship ship in shipList)//search the list of ships
            {
                if (ship.IsBattleShip)//till you find the battleship
                {
                    foreach (Position sPos in ship.Positions)//get the list of positions from the battle ship
                    {
                        foreach (Position aPos in attackList)//and the list of places to attack
                        {
                            if (aPos.X == sPos.X && aPos.Y == sPos.Y)//find where it intersects with the BS
                            {
                                attackList.Remove(aPos); //and remove it from the list
                                break;//break out of the loop as we're done with the list.
                            }
                        }
                    }
                }
            }
        }

        public String Name { get; set; } //set; added to set the Name

        public int Index { get; set; } //set; added to set the playerIndex

        public Position GetAttackPosition() 
        {
            return new Position(0, 0); //added so I could test the start code, This should not be in the finished product. 
        }

        public void SetAttackResults(List<AttackResult> results)
        {
        }




    }
}
