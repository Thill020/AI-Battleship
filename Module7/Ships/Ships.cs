/**************************************************************
 *                                                            *
 *  Authors:       Brandon Rath - SetAttackResults            *
 *  of Green:       Matthew Rohde- FiringPattern/ChaseHit     *
 *  Player AI:       Tyler N Hill - StartNewGame              *
 *  All other files property of original author               *
 *  Course:        CS3110-C2 C# Programming                   *
 *  Assignment:    Module 8 Group Project                     *
 *  File:          Ships.cs                                   *
 *  Description:   Class file to store all indiciduals ships  *
 *  Input:         list of ships                              *
 *  Output:        Details involving ship list                *
 *  Created:       12/10/2020                                 *
 *                                                            *
 *************************************************************/
using System;
using System.Collections.Generic;

namespace Module8
{
    public class Ships
    {
        public readonly List<Ship> _ships = new List<Ship>();

        public void Clear()
        {
            _ships.Clear();
        }

        public bool SunkMyBattleShip
        {
            get
            {
                //Find the battleship and see if its sunk
                foreach (var ship in _ships)
                {
                    var battleShip = ship as Battleship;
                    if (battleShip != null)
                    {
                        return battleShip.Sunk;
                    }
                }

                throw new Exception("Cannot find a battleship");
            }
        }

        public void Add(Ship ship)
        {
            _ships.Add(ship);
        }

        public AttackResult Attack(Position pos)
        {
            //Search the positions for a hit 
            foreach (var ship in _ships)
            {
                AttackResult attackResult = ship.Attack(pos);
                if (attackResult.ResultType != AttackResultType.Miss)
                {
                    return attackResult; //Once we hit then no point looking any more
                }
            }

            //No hits means a miss!
            return new AttackResult(0, pos);
        }
    }
}
