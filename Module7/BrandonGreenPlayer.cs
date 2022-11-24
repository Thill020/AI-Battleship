using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module8
{
    class GreenPlayer : IPlayer
    {
        //This list is used to determine attacks while following the attack pattern
        private readonly List<Position> attackList = new List<Position>();
        private readonly List<Ship> shipList = new List<Ship>(); //This list holds the list of ships 
        private List<char[,]> playerGrids= new List<char[,]>(); //this list holds a list of grids, 1 grid for each player
        private int _index;
        private int _gridSize;

        public GreenPlayer(string name)
        {
            Name = name;
        }

        public void StartNewGame(int playerIndex, int gridSize, Ships ships)
        {
            _gridSize = gridSize;
            _index = playerIndex;

            int y = 0;
            foreach (var ship in ships._ships)
            {
                ship.Place(new Position(0, y++), Direction.Horizontal);
            }
        }

        public Position GetAttackPosition()
        {
            return new Position(0, 0);
        }

        public void SetAttackResults(List<AttackResult> results)
        {
            //if there is no grids for players create them
            if(playerGrids.Count == 0)
            {
                //create a grid for each player
                for (int i = 0; i < results.Count; i++)
                {
                    playerGrids.Add(new char[_gridSize, _gridSize]);
                    for(int x = 0; x < _gridSize; x++)
                    {
                        for(int y = 0; y < _gridSize; y++)
                        {
                            (playerGrids[i])[x, y] = '.';
                        }
                    }
                }

                //add our ships to our grid
                foreach (Ship ship in shipList)
                {
                    foreach (Position position in ship.Positions) {
                        (playerGrids[_index])[position.X, position.Y] = ship.Character;
                    }
                }
            }

            //Add the attack results to the grids
            for(int curIndex = 0; curIndex < playerGrids.Count; curIndex++)
            {
                if (results[curIndex].ResultType == AttackResultType.Hit)
                {
                    //if it was a hit mark that spot as an upper case X
                    (playerGrids[curIndex])[results[curIndex].Position.X, results[curIndex].Position.Y] = 'X';
                }else if (results[curIndex].ResultType == AttackResultType.Sank)
                {
                    //if a ship was sunk if it was a battleship remove their board otherwise just make it as a hot
                    if(results[curIndex].SunkShip == ShipTypes.Battleship)
                    {
                        playerGrids.RemoveAt(curIndex);
                        //if the player was farther up the list move our index
                        if(curIndex < _index)
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

            //if the attack list contains any shots fired remove them
            if (attackList.Contains(new Position(results[0].Position.X, results[0].Position.Y)))
            {
                attackList.Remove(new Position(results[0].Position.X, results[0].Position.Y));
            }
        }

        public string Name { get; }
        public int Index => _index;
    }
}
