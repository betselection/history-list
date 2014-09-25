//  Roulette.cs
//
//  Author:
//       Victor L. Senior (VLS) <betselection(&)gmail.com>
//
//  Web: 
//       http://betselection.cc/betsoftware/
//
//  Sources:
//       http://github.com/betselection/
//
//  Copyright (c) 2014 Victor L. Senior
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

/// <summary>
/// Roulette.
/// </summary>
namespace History__List
{
    // Directives
    using System;
    using System.Drawing;

    /// <summary>
    /// Roulette class.
    /// </summary>
    public class Roulette
    {
        /// <summary>
        /// Checks the number is a valid roulette one.
        /// </summary>
        /// <returns><c>true</c>, if passed number is a valid roulette number, <c>false</c> otherwise.</returns>
        /// <param name="number">Number as integer.</param>
        public bool IsRouletteNumber(int number)
        {
            // Check range
            if (number >= 0 && number <= 37)
            {
                // Valid
                return true;
            }

            // Default
            return false;
        }

        /// <summary>
        /// Determines whether the passed number is a valid roulette number.
        /// </summary>
        /// <returns><c>true</c> if this instance is roulette number the specified number; otherwise, <c>false</c>.</returns>
        /// <param name="number">Number as string.</param>
        public bool IsRouletteNumber(string number)
        {
            // Parsed integer variable
            int intP;

            // Try to get parsed integer
            if (int.TryParse(number, out intP))
            {
                // Check via overload
                    return this.IsRouletteNumber(number);
            }

            // Default 
            return false;
        }

        /// <summary>
        /// Gets the roulette number.
        /// </summary>
        /// <returns>The roulette number.</returns>
        /// <param name="number">Passed string to parse.</param>
        public int GetRouletteNumber(string number)
        {
            // Parsed integer variable
            int intP;

            // Try to get parsed integer
            if (int.TryParse(number, out intP))
            {
                // Check via overload
                    if (this.IsRouletteNumber(intP))
                {
                    // Return the valid number
                    return intP;
                }
            }

            // Default
            return -1;
        }

        /// <summary>
        /// Returns color structure for current number
        /// </summary>
        /// <param name="number">Input number</param>
        /// <returns>Color structure, corresponding to either Black, Red or Green</returns>
        public Color GetColor(int number)
        {
            switch (number)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 9:
                case 12:
                case 14:
                case 16:
                case 18:
                case 19:
                case 21:
                case 23:
                case 25:
                case 27:
                case 30:
                case 32:
                case 34:
                case 36:

                    // Red number
                    return Color.Red;

                case 2:
                case 4:
                case 6:
                case 8:
                case 10:
                case 11:
                case 13:
                case 15:
                case 17:
                case 20:
                case 22:
                case 24:
                case 26:
                case 28:
                case 29:
                case 31:
                case 33:
                case 35:

                    // Black number
                    return Color.Black;

                default:

                    // Anything else = green
                    return Color.Green;
            }
        }

        /// <summary>
        /// Gets the column.
        /// </summary>
        /// <returns>The column: 0, 1, 2, 3.</returns>
        /// <param name="num">Passed number.</param>
        public int GetColumn(int num)
        {
            switch (num)
            {
                case 1:
                case 4:
                case 7:
                case 10:
                case 13:
                case 16:
                case 19:
                case 22:
                case 25:
                case 28:
                case 31:
                case 34:
                    // First column
                    return 1;
                case 2:
                case 5:
                case 8:
                case 11:
                case 14:
                case 17:
                case 20:
                case 23:
                case 26:
                case 29:
                case 32:
                case 35:
                    // Second column
                    return 2;
                case 3:
                case 6:
                case 9:
                case 12:
                case 15:
                case 18:
                case 21:
                case 24:
                case 27:
                case 30:
                case 33:
                case 36:
                    // Third column
                    return 3;
                default:
                    // No column
                    return 0;
            }
        }

        /// <summary>
        /// Gets the dozen.
        /// </summary>
        /// <returns>The dozen. 0, 1, 2, 3.</returns>
        /// <param name="num">Passed number.</param>
        public int GetDozen(int num)
        {
            switch (num)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                    // First dozen
                    return 1;
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    // Second dozen
                    return 2;
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                    // Third dozen
                    return 3;
                default:
                    // No dozen
                    return 0;
            }
        }
    }
}