/**************************************************************
 *                                                            *
 *  Authors:       Brandon Rath - SetAttackResults            *
 *  of Green:       Matthew Rohde- FiringPattern/ChaseHit     *
 *  Player AI:       Tyler N Hill - StartNewGame              *
 *  All other files property of original author               *
 *  Course:        CS3110-C2 C# Programming                   *
 *  Assignment:    Module 8 Group Project                     *
 *  File:          PatrolBoat.cs                              *
 *  Description:   Patrol Boat Ship                           *
 *  Input:         none                                       *
 *  Output:        none                                       *
 *  Created:       12/10/2020                                 *
 *                                                            *
 *************************************************************/
using System;

namespace Module8
{
    class PatrolBoat : Ship
    {
        public PatrolBoat() : base(2, ConsoleColor.White, ShipTypes.PatrolBoat)
        {
        }

    }
}
