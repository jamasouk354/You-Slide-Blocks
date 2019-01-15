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
        Boolean leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, bDown, nDown, mDown, spaceDown, timer, forward, backward;

        //Player's Block
        int blockX, bYLis, blockWidth, blockLength, blockSpeed, block;
        SolidBrush blockBrush = new SolidBrush(Color.Brown);

        //Blocks
        SolidBrush blocksBrush = new SolidBrush(Color.SandyBrown);

        List<int> levelList = new List<int>();
        List<int> bWidthList = new List<int>();
        List<int> bLengthList = new List<int>();
        List<int> bXList = new List<int>();
        List<int> bYList = new List<int>();
        List<int> bCount = new List<int>();

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
            blockSpeed = 15;
            block = 0;

            //Player Block
            bCount.Add(0);
            bXList.Add(12);
            bYList.Add(126);
            bWidthList.Add(91);
            bLengthList.Add(45);

            //Obstacles
            bCount.Add(1);
            bXList.Add(12);
            bYList.Add(34);
            bWidthList.Add(45);
            bLengthList.Add(91);

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
                rightArrowDown = leftArrowDown = upArrowDown = downArrowDown = forward = backward = false;

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

            if (bXList.Count == 45 && bYList.Count < 45)
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

            switch (e.KeyCode)
            {
                case Keys.M:
                    forward = true;
                    break;
                case Keys.N:
                    backward = true;
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
                case Keys.M:
                    forward = false;
                    break;
                case Keys.N:
                    backward = false;
                    break;
            }
        }

        /// <summary>
        /// Um Dont know yet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            int count = 0;
            //Main Controls

            //Switches to another Block
            if (forward == true)
            {
                count++;
            }
            if (backward == true)
            {
                count--;
            }

            //Moving the Blocks
            if (count == bCount)
            {
                if (leftArrowDown == true)
                {
                    bXList[count] = bXList[count] - blockSpeed;

                    if (bXList[count] < 12)
                    {
                        bXList[count] = 12;
                    }

                }
                if (downArrowDown == true)
                {
                    bYList[count] = bYList[count] + blockSpeed;

                    if (bYList[count] > this.Height - 58)
                    {
                        bYList[count] = this.Height - 58;
                    }
                }
                if (rightArrowDown == true)
                {
                    blockX = blockX + blockSpeed;

                    if (bXList[count] > this.Width - 103)
                    {
                        bXList[count] = this.Width - 103;
                    }
                }
                if (upArrowDown == true)
                {
                    bYList[count] = bYList[count] - blockSpeed;

                    if (bYList[count] < 34)
                    {
                        bYList[count] = 34;
                    }
                }
            }

            //Collisions
            for (int i = 0; i < bXList.Count; i++)
            {
                Rectangle blockRec = new Rectangle(bXList[i], bYList[i], bWidthList[i], bLengthList[i]);
            }

            //calls the GameScreen_Paint method to draw the screen.
            Refresh();
        }


        //Drawn on Screen
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //Background
            e.Graphics.FillRectangle(backGroundColor, 11, 33, 278, 278);

            //Blocks
            for (int i = 0; i < bCount.Count; i++)
            {
                e.Graphics.FillRectangle(blocksBrush, bXList[i], bYList[i], bWidthList[i], bLengthList[i]);
            }

            //Border
            e.Graphics.FillRectangle(borderBrush, 0, 0, 10, this.Height);
            e.Graphics.FillRectangle(borderBrush, 0, 0, this.Width, 32);
            e.Graphics.FillRectangle(borderBrush, this.Width-11, 0, 11, this.Height);
            e.Graphics.FillRectangle(borderBrush, 0, this.Height-11, this.Width, 11);

            
        }
    }
}
