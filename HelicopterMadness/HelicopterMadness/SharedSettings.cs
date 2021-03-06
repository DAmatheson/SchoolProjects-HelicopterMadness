﻿/* SharedSettings.cs
 * Purpose: Common Settings and values used throughout the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.04: Created
 */

using System;
using Microsoft.Xna.Framework;

namespace HelicopterMadness
{
    /// <summary>
    ///     Contains shared settings used throughout the game
    /// </summary>
    public static class SharedSettings
    {
        /// <summary>
        ///     The layer depth value for obstacles in the game
        /// </summary>
        public const float OBSTACLE_LAYER = 0.76f;

        /// <summary>
        ///     The layer depth value for the borders in the game
        /// </summary>
        public const float BORDER_LAYER = 0.75f;

        /// <summary>
        ///     The default stage X axis movement speed
        /// </summary>
        public const float DEFAULT_STAGE_SPEED_X = 540f;

        /// <summary>
        ///     The filename for the highscore save file
        /// </summary>
        public const string HIGHSCORE_FILE_NAME = "Highscore.txt";

        /// <summary>
        ///     The folder name for the game
        /// </summary>
        public const string SAVE_FOLDER_NAME = "Helicopter Madness";

        /// <summary>
        ///     The maximum number of characters in a highscore name
        /// </summary>
        public const int MAX_NAME_CHARS = 3;

        /// <summary>
        ///     The blink delay for flashing text displays
        /// </summary>
        public const int BLINK_RATE = 100;

        /// <summary>
        ///     The Y Position for title messages
        /// </summary>
        public const float TITLE_POSITION_Y = 52f;

        /// <summary>
        ///     The color for title text
        /// </summary>
        public static readonly Color TitleTextColor = Color.Black;

        /// <summary>
        ///     The Color for highlight text
        /// </summary>
        public static readonly Color HighlightTextColor = Color.DarkRed;

        /// <summary>
        ///     The Color for normal text
        /// </summary>
        public static readonly Color NormalTextColor = Color.Yellow;

        /// <summary>
        ///     The Color for blinking help text
        /// </summary>
        public static readonly Color HelpTextColor = Color.Azure;

        /// <summary>
        ///     The Color for the highest score text in the action scene
        /// </summary>
        public static readonly Color HighestScoreColor = Color.GreenYellow;

        /// <summary>
        ///     Gets the speed change from the starting speed to the current speed
        /// </summary>
        public static float StageSpeedChange
        {
            get
            {
                return StageSpeed.X / DEFAULT_STAGE_SPEED_X;
            }
        }

        /// <summary>
        ///     A Vector2 containing the greatest X and Y for the stage
        /// </summary>
        public static Vector2 Stage;

        /// <summary>
        ///     A Vector2 containing the X and Y for the center of the stage
        /// </summary>
        public static Vector2 StageCenter;

        /// <summary>
        ///     A Vector2 containing the X and Y speed for objects moving on the stage
        /// </summary>
        public static Vector2 StageSpeed = new Vector2(DEFAULT_STAGE_SPEED_X, 0);

        /// <summary>
        ///     A static random number generator
        /// </summary>
        public static readonly Random Random = new Random();
    }
}