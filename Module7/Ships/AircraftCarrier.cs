/**************************************************************
 *                                                            *
 *  Authors:       Brandon Rath - SetAttackResults            *
 *  of Green:       Matthew Rohde- FiringPattern/ChaseHit     *
 *  Player AI:       Tyler N Hill - StartNewGame              *
 *  All other files property of original author               *
 *  Course:        CS3110-C2 C# Programming                   *
 *  Assignment:    Module 8 Group Project                     *
 *  File:          AircraftCarrier.cs                         *
 *  Description:   Airship Carrier ship.                      *
 *  Input:         none                                       *
 *  Output:        none                                       *
 *  Created:       12/10/2020                                 *
 *                                                            *
 *************************************************************/
using System;

namespace Module8
{ 
    class AircraftCarrier : Ship
    {
        public AircraftCarrier() : base(5, ConsoleColor.Blue, ShipTypes.AircraftCarrier)
        {

        }

    }
}
