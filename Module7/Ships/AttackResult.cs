/**************************************************************
 *                                                            *
 *  Authors:       Brandon Rath - SetAttackResults            *
 *  of Green:       Matthew Rohde- FiringPattern/ChaseHit     *
 *  Player AI:       Tyler N Hill - StartNewGame              *
 *  All other files property of original author               *
 *  Course:        CS3110-C2 C# Programming                   *
 *  Assignment:    Module 8 Group Project                     *
 *  File:          AttackResults.cs                           *
 *  Description:   Structure for attack results               *
 *  Input:         none                                       *
 *  Output:        none                                       *
 *  Created:       12/10/2020                                 *
 *                                                            *
 *************************************************************/
using System;

namespace Module8
{
    public struct AttackResult
    {
        public int PlayerIndex;
        public Position Position;
        public AttackResultType ResultType;
        public ShipTypes SunkShip; //Filled in if ResultType is Sunk

        public AttackResult(int playerIndex, Position position, AttackResultType attackResultType= AttackResultType.Miss, ShipTypes sunkShip = ShipTypes.None)
        {
            PlayerIndex = playerIndex;
            Position = position;
            ResultType = attackResultType;
            SunkShip = sunkShip;
        }
    }
}
