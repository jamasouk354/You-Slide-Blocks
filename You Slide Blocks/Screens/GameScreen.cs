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
        int currentPiece = 0;

        int cValue = 0;

        //Player Controls
        Boolean leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, bDown, nDown, mDown, spaceDown, timer, forward, backward;

        //Player's Block
        int blockX, bYLis, blockWidth, blockLength, blockSpeed, block;
        SolidBrush blockBrush = new SolidBrush(Color.Brown);

        //Blocks
        SolidBrush blocksBrush = new SolidBrush(Color.SandyBrown);

        List<int> levelList = new List<int>();
        List<int> bWidthList = new List<int>();
        List<int> bHeightList = new List<int>();
        List<int> bXList = new List<int>();
        List<int> bYList = new List<int>();
        List<int> bCount = new List<int>();
        List<Boolean> move = new List<bool>();

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
            //Change Block into Rectangles
            bWidthList.Add(91);
            bHeightList.Add(45);

            //Obstacles
            bCount.Add(1);
            bXList.Add(12);
            bYList.Add(34);
            bWidthList.Add(45);
            bHeightList.Add(91);

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
            int blockX = bHeightList[cValue];
            int blockY = bWidthList[cValue];

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


        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //Main Controls

            //Switches to another Block
            if (forward == true)
            {
                currentPiece++;
                forward = false;
            }
            if (backward == true)
            {
                currentPiece--;
                backward = false;
            }

            //Moving the Blocks
                if (leftArrowDown == true)
                {
                    bXList[currentPiece] = bXList[currentPiece] - blockSpeed;

                    if (bXList[currentPiece] < 12)
                    {
                        bXList[currentPiece] = 12;
                    }

                }
                if (downArrowDown == true)
                {
                    bYList[currentPiece] = bYList[currentPiece] + blockSpeed;

                    if (bYList[currentPiece] > this.Height - 58)
                    {
                        bYList[currentPiece] = this.Height - 58;
                    }
                }
                if (rightArrowDown == true)
                {
                    bXList[currentPiece] = bXList[currentPiece] + blockSpeed;

                    if (bXList[currentPiece] > this.Width - 103)
                    {
                        bXList[currentPiece] = this.Width - 103;
                    }
                }
                if (upArrowDown == true)
                {
                    bYList[currentPiece] = bYList[currentPiece] - blockSpeed;

                    if (bYList[currentPiece] < 34)
                    {
                        bYList[currentPiece] = 34;
                    }
                }

            //Collisions
            for (int i = 0; i < bXList.Count; i++)
            {
                Rectangle blockRec = new Rectangle(bXList[i], bYList[i], bWidthList[i], bHeightList[i]);
            }

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
                e.Graphics.FillRectangle(blocksBrush, bXList[i], bYList[i], bWidthList[i], bHeightList[i]);
            }

            //Border
            e.Graphics.FillRectangle(borderBrush, 0, 0, 10, this.Height);
            e.Graphics.FillRectangle(borderBrush, 0, 0, this.Width, 32);
            e.Graphics.FillRectangle(borderBrush, this.Width-11, 0, 11, this.Height);
            e.Graphics.FillRectangle(borderBrush, 0, this.Height-11, this.Width, 11);

            
        }
    }
}
