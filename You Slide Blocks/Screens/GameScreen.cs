using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameSystemServices;

namespace You_Slide_Blocks
{
    public partial class GameScreen : UserControl
    {
        //Player Controls
        Boolean leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, bDown, nDown, mDown, spaceDown;

        //Player's Block
        int blockX, blockY, blockWidth, blockLength, blockSpeed;
        SolidBrush blockBrush = new SolidBrush(Color.Brown);

        //Blocks
        int blocksX, blocksY, blocksSize, blocksSpeed;
        SolidBrush blocksBrush = new SolidBrush(Color.SandyBrown);

        List<int> levelList = new List<int>();
        List<int> blockSizeList = new List<int>();
        List<int> blockCountList = new List<int>();
        List<int> blockXList = new List<int>();
        List<int> blockYList = new List<int>();

        //Border
        SolidBrush borderBrush = new SolidBrush(Color.Firebrick);
        public GameScreen()
        {
            InitializeComponent();
            InitializeGameValues();
        }

        public void InitializeGameValues()
        {
            blockX = 100;
            blockY = 200;
            blockWidth = 100;
            blockLength = 50;
            blockSpeed = 5;


        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // opens a pause screen is escape is pressed. Depending on what is pressed
            // on pause screen the program will either continue or exit to main menu
            if (e.KeyCode == Keys.Escape && gameTimer.Enabled)
            {
                gameTimer.Enabled = false;
                rightArrowDown = leftArrowDown = upArrowDown = downArrowDown = false;

                DialogResult result = PauseForm.Show();

                if (result == DialogResult.Cancel)
                {
                    gameTimer.Enabled = true;
                }
                else if (result == DialogResult.Abort)
                {
                    MainForm.ChangeScreen(this, "MenuScreen");
                }
            }

            //TODO - basic player 1 key down bools set below. Add remainging key down
            // required for player 1 or player 2 here.

            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    downArrowDown = false;
                    rightArrowDown = false;
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    rightArrowDown = false;
                    upArrowDown = false;
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    leftArrowDown = false;
                    upArrowDown = false;
                    downArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    downArrowDown = false;
                    leftArrowDown = false;
                    rightArrowDown = false;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //TODO - basic player 1 key up bools set below. Add remainging key up
            // required for player 1 or player 2 here.

            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Space:
                    spaceDown = false;
                    break;
            }
        }

        /// <summary>
        /// Timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //Main Controls
            if (leftArrowDown == true)
            {
                blockX = blockX - blockSpeed;

                if (blockX < 13)
                {
                    blockX = 13;
                }

            }
            if (downArrowDown == true)
            {
                blockY = blockY + blockSpeed;

                if (blockY > this.Height - 64)
                {
                    blockY = this.Height - 64;
                }
            }
            if (rightArrowDown == true)
            {
                blockX = blockX + blockSpeed;

                if (blockX > this.Width - 114)
                {
                    blockX = this.Width - 114;
                }
            }
            if (upArrowDown == true)
            {
                blockY = blockY - blockSpeed;

                if (blockY < 13)
                {
                    blockY = 13;
                }
            }
            
            //Border Restrictions

            //TODO move npc characters



            //TODO collisions checks 
            

            //calls the GameScreen_Paint method to draw the screen.
            Refresh();
        }


        //Everything that is to be drawn on the screen should be done here
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw rectangle to screen
            e.Graphics.FillRectangle(blockBrush, blockX, blockY, blockWidth, blockLength);

            //Border
            e.Graphics.FillRectangle(borderBrush, 0, 0, 10, this.Height);
            e.Graphics.FillRectangle(borderBrush, 0, 0, this.Width, 32);
            e.Graphics.FillRectangle(borderBrush, this.Width-11, 0, 11, this.Height);
            e.Graphics.FillRectangle(borderBrush, 0, this.Height-11, this.Width, 11);

            //Blocks
            e.Graphics.FillRectangle(blocksBrush, 11, 33, 282, 282);
        }
    }

}
