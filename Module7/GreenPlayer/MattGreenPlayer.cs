/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module8
{
    internal class GreenPlayer : IPlayer
    {
        private int _index;
        private int _gridSize;
        public string Name { get; }
        public int Index { get; set; }
        public int GridSize
        {
            get { return _gridSize; }
            set { _gridSize = value; }
        }
  
        //This list is used to determine attacks while following the attack pattern
        private List<Position> attackList = new List<Position>();
        private readonly List<Ship> shipList = new List<Ship>(); //This list holds the list of ships 
        private List<char[,]> playerGrids = new List<char[,]>(); //this list holds a list of grids, 1 grid for each player
        private static readonly Random rand = new Random();
        private Position battleshipPos;
        public GreenPlayer(string name)
        {
            Name = name;

        }
        public Position GetAttackPosition()
        {
            foreach (char[,] grid in playerGrids)
            {
                if (grid == playerGrids[_index])
                {
                    continue;
                }
                else
                {
                    return ChaseHit();
                }
            }
            return FiringPattern();
        }

        public void SetAttackResults(List<AttackResult> results)
        {
            //if there is no grids for players create them
            if (playerGrids.Count == 0)
            {
                //create a grid for each player
                for (int i = 0; i < results.Count; i++)
                {
                    playerGrids.Add(new char[_gridSize, _gridSize]);
                    for (int x = 0; x < _gridSize; x++)
                    {
                        for (int y = 0; y < _gridSize; y++)
                        {
                            (playerGrids[i])[x, y] = '.';
                        }
                    }
                }

                //add our ships to our grid
                foreach (Ship ship in shipList)
                {
                    foreach (Position position in ship.Positions)
                    {
                        (playerGrids[_index])[position.X, position.Y] = ship.Character;
                    }
                }
            }

            //Add the attack results to the grids
            for (int curIndex = 0; curIndex < playerGrids.Count; curIndex++)
            {
                if (results[curIndex].ResultType == AttackResultType.Hit)
                {
                    //if it was a hit mark that spot as an upper case X
                    (playerGrids[curIndex])[results[curIndex].Position.X, results[curIndex].Position.Y] = 'X';
                }
                else if (results[curIndex].ResultType == AttackResultType.Sank)
                {
                    //if a ship was sunk if it was a battleship remove their board otherwise just make it as a hot
                    if (results[curIndex].SunkShip == ShipTypes.Battleship)
                    {
                        playerGrids.RemoveAt(curIndex);
                        //if the player was farther up the list move our index
                        if (curIndex < _index)
                        {
                            _index--;
                        }
                    }
                    else
                    {
                        (playerGrids[curIndex])[results[curIndex].Position.X, results[curIndex].Position.Y] = 'X';
                    }
                }
                else
                {
                    //if the result was a miss mark with a lower case x
                    (playerGrids[curIndex])[results[curIndex].Position.X, results[curIndex].Position.Y] = 'x';
                }
            }//end for

            for (int i = 0; i < playerGrids.Count; i++)
            {
                //if the attack list contains any shots fired remove them
                if (attackList.Contains(new Position(results[i].Position.X, results[i].Position.Y)))
                {
                    attackList.Remove(new Position(results[i].Position.X, results[i].Position.Y));
                }
            }
        }

        public void StartNewGame(int playerIndex, int gridSize, Ships ships)
        {
            
            Index = playerIndex; //assign the player index to the index
            GridSize = gridSize;
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
                    if ((i + j) % 4 == 3)
                    {
                        attackList.Add(new Position(i, j));
                    }
                }
            }

            //then
            foreach (Ship ship in shipList)//search the list of ships
            {
                if (ship.IsBattleShip)//till you find the battleship
                {
                    foreach (Position sPos in ship.Positions)//get the list of positions from the battle ship
                    {
                        foreach (Position aPos in attackList)//and the list of places to attack
                        {
                            if (aPos.X == sPos.X && aPos.Y == sPos.Y)//find where it intersects with the BS
                            {
                                battleshipPos = new Position(aPos.X, aPos.Y);
                                attackList.Remove(aPos); //and remove it from the list
                                break;//break out of the loop as we're done with the list.
                            }
                        }
                    }
                }
            }
        }

        public Position ChaseHit()
        {
            if (playerGrids.Count > 2 && attackList.Count != 0)
            {
                return FiringPattern();
            }
            for (int curIndex = 0; curIndex < playerGrids.Count; curIndex++) //Loop through playerGrids
            {
                if (curIndex != _index)  //As long as curIndex is not ours, run through code to choose shot. If currently our index, break and call FiringPattern();
                {
                    //Loop through grid of shot results to determine where to place next shot, 
                    for (int i = 0; i < _gridSize; i++)
                    {
                        for (int j = 0; j < _gridSize; j++)
                        {
                            if ((j + 2) < _gridSize && (playerGrids[curIndex])[i, (j + 2)] == '.' && (playerGrids[curIndex])[i, j] == 'X' ) //If in grid bounds, and empty space, fire 2 above
                            {
                                Position guess = new Position(i, (j + 2));

                                return guess;
                            }
                            else if ((j - 2) > _gridSize && (playerGrids[curIndex])[i, (j - 2)] == '.' && (playerGrids[curIndex])[i, j] == 'X') //If in grid bounds, and empty space, fire 2 below
                            {
                                Position guess = new Position(i, (j - 2));

                                return guess;
                            }
                            else if ((i + 2) < _gridSize && (playerGrids[curIndex])[(i + 2), j] == '.' && (playerGrids[curIndex])[i, j] == 'X') //If in grid bounds, and empty space, fire 2 to the right
                            {
                                Position guess = new Position((i + 2), j);

                                return guess;
                            }
                            else if ((i - 2) > _gridSize && (playerGrids[curIndex])[(i - 2), j] == '.' && (playerGrids[curIndex])[i, j] == 'X') //If in grid bounds, and empty space, fire 2 to the left
                            {
                                Position guess = new Position((i - 2), j);

                                return guess;
                            }
                            if ((j + 2) < _gridSize && (playerGrids[curIndex])[i , (j + 2)] == 'X' && (playerGrids[curIndex])[i, (j + 1)] == '.' && (playerGrids[curIndex])[i, j] == 'X') //Fire 1 above if conditions met
                            {
                                Position guess = new Position(i, (j + 1));

                                return guess;
                            }
                            else if ((j - 2) > _gridSize && (playerGrids[curIndex])[i, (j - 2)] == 'X' && (playerGrids[curIndex])[i, (j - 1)] == '.' && (playerGrids[curIndex])[i, j] == 'X') //Fire 1 below if conditions met
                            {
                                Position guess = new Position(i, (j - 1));

                                return guess;

                            }
                            else if ((i + 2) < _gridSize && (playerGrids[curIndex])[(i + 2), j] == 'X' && (playerGrids[curIndex])[(i + 1), j] == '.' && (playerGrids[curIndex])[i, j] == 'X') //Fire 1 to the right if conditions met
                            {
                                Position guess = new Position((i + 1), j);

                                return guess;

                            }
                            else if ((i - 2) > _gridSize && (playerGrids[curIndex])[(i - 2), j] == 'X' && (playerGrids[curIndex])[(i - 1), j] == '.' && (playerGrids[curIndex])[i, j] == 'X') //Fire 1 to the left if conditions met
                            {
                                Position guess = new Position((i - 1), j);

                                return guess;

                            }
                            
                        }
                    }
                }
               
            }

            return FiringPattern();
        }
    
        public Position FiringPattern()
        {
        //Use random to select a random index from firingpattern list
              
           int index = rand.Next(attackList.Count);

         //Create/Set guess variable to random firingpattern list item, then remove that item from the list

            Position guess = new Position(attackList[index].X, attackList[index].Y);
            //attackList.Remove(new Position(attackList[index].X, attackList[index].Y));
            attackList.RemoveAt(index); 

            return guess;
        }
    
    }
}
*/