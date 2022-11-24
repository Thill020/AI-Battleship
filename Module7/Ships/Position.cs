/**************************************************************
 *                                                            *
 *  Authors:       Brandon Rath - SetAttackResults            *
 *  of Green:       Matthew Rohde- FiringPattern/ChaseHit     *
 *  Player AI:       Tyler N Hill - StartNewGame              *
 *  All other files property of original author               *
 *  Course:        CS3110-C2 C# Programming                   *
 *  Assignment:    Module 8 Group Project                     *
 *  File:          Position.cs                                *
 *  Description:   Class object for the location of ships     *
 *  Input:         none                                       *
 *  Output:        none                                       *
 *  Created:       12/10/2020                                 *
 *                                                            *
 *************************************************************/
namespace Module8
{
    public class Position
    {
        public int X;
        public int Y;
        public bool Hit;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
            Hit = false;
        }
    }
}
