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
        //Timer Values - *DO LATER*
        int counter = 0;
        int hundredsCounter = 0;
        int secondsCounter = 0;
        int minutesCounter = 0;

        //Player Controls
        Boolean leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, bDown, nDown, mDown, spaceDown, timer;

        //Player's Block
        int blockX, blockY, blockWidth, blockLength, blockSpeed;
        SolidBrush blockBrush = new SolidBrush(Color.Brown);

        //Blocks
        SolidBrush blocksBrush = new SolidBrush(Color.SandyBrown);

        List<int> levelList = new List<int>();
        List<int> blockWidthList = new List<int>();
        List<int> blockLengthList = new List<int>();
        List<int> blockXList = new List<int>();
        List<int> blockYList = new List<int>();

        SolidBrush backGroundColor = new SolidBrush(Color.White);

        //Border
        SolidBrush borderBrush = new SolidBrush(Color.Firebrick);
        public GameScreen()
        {
            InitializeComponent();
            InitializeGameValues();
        }

        public void InitializeGameValues()
        {
            blockX = 12;
            blockY = 126;
            blockWidth = 90;
            blockLength = 45;
            blockSpeed = 15;


            blockXList.Add(12);
            blockYList.Add(34);
            blockWidthList.Add(45);
            blockLengthList.Add(91);

            if (timer == true)
            {
                gameTimer.Interval = 20;
                gameTimer.Enabled = true;
            }
            //DO LATER
            else if (timer == false)
            {

            }
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

            //player 1 button presses
            if (blockX == 45 && blockY < 45)
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        downArrowDown = true;
                        upArrowDown = false;
                        break;
                    case Keys.Up:
                        upArrowDown = true;
                        downArrowDown = false;
                        break;                        
                }
            }
            
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Right:
                        rightArrowDown = true;
                        leftArrowDown = false;
                        break;
                    case Keys.Left:
                        leftArrowDown = true;
                        rightArrowDown = false;
                        break;
                }
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

                if (blockX < 12)
                {
                    blockX = 12;
                }

            }
            if (downArrowDown == true)
            {
                blockY = blockY + blockSpeed;

                if (blockY > this.Height - 58)
                {
                    blockY = this.Height - 58;
                }
            }
            if (rightArrowDown == true)
            {
                blockX = blockX + blockSpeed;

                if (blockX > this.Width - 103)
                {
                    blockX = this.Width - 103;
                }
            }
            if (upArrowDown == true)
            {
                blockY = blockY - blockSpeed;

                if (blockY < 34)
                {
                    blockY = 34;
                }
            }

            //Border Restrictions

            //TODO move npc characters



            //TODO collisions checks 
            for (int i = 0; i < blockXList.Count; i++)
            {
                Rectangle blockRec = new Rectangle(blockXList[i], blockYList[i], blockWidthList[i], blockLengthList[i]);
            }

            //calls the GameScreen_Paint method to draw the screen.
            Refresh();
        }


        //Drawn on Screen
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //Blocks
            e.Graphics.FillRectangle(backGroundColor, 11, 33, 278, 278);

            //draw rectangle to screen
            e.Graphics.FillRectangle(blockBrush, blockX, blockY, blockWidth, blockLength);

            //Border
            e.Graphics.FillRectangle(borderBrush, 0, 0, 10, this.Height);
            e.Graphics.FillRectangle(borderBrush, 0, 0, this.Width, 32);
            e.Graphics.FillRectangle(borderBrush, this.Width-11, 0, 11, this.Height);
            e.Graphics.FillRectangle(borderBrush, 0, this.Height-11, this.Width, 11);

            for (int i = 0; i < blockXList.Count; i++)
            {
                e.Graphics.FillRectangle(blocksBrush, blockXList[i], blockYList[i], blockWidthList[i], blockLengthList[i]);
            }
        }
    }
}
