/* SceneManager.cs
 * Purpose: Manage the state of GameScenes
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.05: Created
 */

using System.Collections.Generic;
using HelicopterMadness.Scenes;
using HelicopterMadness.Scenes.ActionComponents;
using HelicopterMadness.Scenes.CommonComponents;
using HelicopterMadness.Scenes.HighScoreComponents;
using HelicopterMadness.Scenes.BaseScene;
using HelicopterMadness.Scenes.MenuComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HelicopterMadness
{
    /// <summary>
    ///     Manages which GameScene is active
    /// </summary>
    public class SceneManager : DrawableGameComponent
    {
        private readonly Dictionary<MenuItems, GameScene> scenes;

        private readonly MenuScene menuScene;
        private readonly ActionScene actionScene;
        private readonly HighScoreScene highScoreScene;
        private readonly ScreenLoopSprite gameBackground;

        private readonly SoundEffect menuSelectSound;
        private readonly SoundEffect backToMenuSound;

        private GameScene enabledScene;

        /// <summary>
        ///     Initializes a new instance of SceneManager
        /// </summary>
        /// <param name="game">The Game the SceneManager belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the SceneManager uses to draw its components</param>
        public SceneManager(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            // These must match up with the order and values of the enum MenuItems
            List<string> menuEntries = new List<string>
            {
                "Start Game", "How To Play", "Help", "High Score", "Credits", "Quit"
            };

            menuSelectSound = Game.Content.Load<SoundEffect>("Sounds/MenuSelection");
            backToMenuSound = Game.Content.Load<SoundEffect>("Sounds/BackToMenu");
            Song menuMusic = Game.Content.Load<Song>("Sounds/MenuMusic");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(menuMusic);

            menuScene = new MenuScene(game, spriteBatch, this, menuEntries);

            highScoreScene = new HighScoreScene(game, spriteBatch);
            actionScene = new ActionScene(game, spriteBatch);
            HowToPlayScene howToPlayScene = new HowToPlayScene(game, spriteBatch);
            HelpScene helpScene = new HelpScene(game, spriteBatch);
            CreditScene creditScene = new CreditScene(game, spriteBatch);

            scenes = new Dictionary<MenuItems, GameScene>
            {
                { MenuItems.StartGame, actionScene },
                { MenuItems.HowToPlay, howToPlayScene },
                { MenuItems.Help, helpScene },
                { MenuItems.HighScore, highScoreScene },
                { MenuItems.Credit, creditScene }
            };

            gameBackground = new ScreenLoopSprite(game, spriteBatch,
                Game.Content.Load<Texture2D>("Images/Background"), 0.65f);

            HideAllScenes();

            menuScene.Show();

            enabledScene = menuScene;
        }

        /// <summary>
        ///     Updates the enabled scene and potentially returns to the main menu
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Escape) && enabledScene != menuScene)
            {
                HideAllScenes();

                menuScene.Show();

                enabledScene = menuScene;

                backToMenuSound.Play();
            }

            if (enabledScene == actionScene && actionScene.State == ActionSceneStates.GameOver &&
                highScoreScene.State == HighScoreSceneStates.View &&
                actionScene.GetScore() > highScoreScene.LowestScore)
            {
                enabledScene.Hide();

                enabledScene = highScoreScene;
                  
                highScoreScene.Show();

                highScoreScene.AddScoreEntry(actionScene.GetScore());
            }
            else if (enabledScene == highScoreScene && highScoreScene.State == HighScoreSceneStates.Action)
            {
                enabledScene.Hide();

                enabledScene = actionScene;

                actionScene.Show();
            }

            if (enabledScene == actionScene && actionScene.State == ActionSceneStates.InPlay)
            {
                MediaPlayer.Volume = 0.1f;

                gameBackground.Update(gameTime);
            }
            else
            {
                MediaPlayer.Volume = 0.3f;
            }

            enabledScene.Update(gameTime);
        }

        /// <summary>
        ///     Draws the enabled scene
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            gameBackground.Draw(gameTime);
            enabledScene.Draw(gameTime);
        }

        /// <summary>
        ///     Hides and disables all managed scenes
        /// </summary>
        private void HideAllScenes()
        {
            foreach (GameScene gameScene in scenes.Values)
            {
                gameScene.Hide();
            }
        }

        /// <summary>
        ///     Switches the scene when a menu item is selected
        /// </summary>
        /// <param name="selectedItem">The scene to switch to</param>
        public void OnMenuSelection(MenuItems selectedItem)
        {
            menuScene.Hide();

            if (selectedItem == MenuItems.Quit)
            {
                Game.Exit();
            }
            else
            {
                menuSelectSound.Play();

                enabledScene = scenes[selectedItem];

                enabledScene.Show();
            }
        }
    }
}