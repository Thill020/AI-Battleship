/**************************************************************
 *                                                            *
 *  Authors:       Brandon Rath - SetAttackResults            *
 *  of Green:       Matthew Rohde- FiringPattern/ChaseHit     *
 *  Player AI:       Tyler N Hill - StartNewGame              *
 *  All other files property of original author               *
 *  Course:        CS3110-C2 C# Programming                   *
 *  Assignment:    Module 8 Group Project                     *
 *  File:          Program.cs                                 *
 *  Description:   Pre-Game processer of information          *
 *  Input:         List of ships and players                  *
 *  Output:        main game engine                           *
 *  Created:       12/10/2020                                 *
 *                                                            *
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS3110Module8Group;

namespace Module8
{
    class Program
    {
        static void Main(string[] arg)
        {
            List<IPlayer> players = new List<IPlayer>();
            players.Add(new DumbPlayer("Dumb 1"));
          //players.Add(new DumbPlayer("Dumb 2"));
          //players.Add(new DumbPlayer("Dumb 3"));
            players.Add(new RandomPlayer("Random 1"));
            players.Add(new RandomPlayer("Random 2"));
            players.Add(new RandomPlayer("Random 3"));
            players.Add(new RandomPlayer("Random 4"));
          //players.Add(new RandomPlayer("Random 5"));

            //Your code here
            players.Add(new GreenPlayer("Green 1"));
            players.Add(new GreenPlayer("Green 2"));
            players.Add(new GreenPlayer("Green 3"));

            MultiPlayerBattleShip game = new MultiPlayerBattleShip(players);
            game.Play(PlayMode.Pause);
        }
    }
}
