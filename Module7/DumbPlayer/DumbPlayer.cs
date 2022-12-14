/**************************************************************
 *                                                            *
 *  Authors:       Brandon Rath - SetAttackResults            *
 *  of Green:       Matthew Rohde- FiringPattern/ChaseHit     *
 *  Player AI:       Tyler N Hill - StartNewGame              *
 *  All other files property of original author               *
 *  Course:        CS3110-C2 C# Programming                   *
 *  Assignment:    Module 8 Group Project                     *
 *  File:          DumbPlayer.cs                              *
 *  Description:   Dumb AI that shoots only in a set pattern  *
 *  Input:         Details from MultiPlayerBattleShip.cs      *
 *  Output:        Attack/Ship Location to main program       *
 *  Created:       12/10/2020                                 *
 *                                                            *
 *************************************************************/
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Module8
{
    internal class DumbPlayer : IPlayer
    {
        private static int _nextGuess = 0; //Note static - we all share the same guess 

        private int _index;
        private int _gridSize;

        public DumbPlayer(string name)
        {
            Name = name;
        }

        public void StartNewGame(int playerIndex, int gridSize, Ships ships)
        {
            _gridSize = gridSize;
            _index = playerIndex;

            //DumbPlayer just puts the ships in the grid one on each row
            int y = 0;
            foreach (var ship in ships._ships)
            {
                ship.Place(new Position(0, y++), Direction.Horizontal);
            }
        }

        public Position GetAttackPosition()
        {
            //A *very* naive guessing algorithm that simply starts at 0, 0 and guess each square in order
            //All 'DumbPlayers' share the counter so they won't guess the same one
            //But we don't check to make sure the square has not been guessed before
            var pos = new Position(_nextGuess % _gridSize, (_nextGuess /_gridSize));
            _nextGuess++;
            return pos;
        }

        public void SetAttackResults(List<AttackResult> results)
        {
            //DumbPlayer does nothing with these results - its going to keep making dumb guesses
        }

        public string Name { get; }
        public int Index => _index;
    }
}
