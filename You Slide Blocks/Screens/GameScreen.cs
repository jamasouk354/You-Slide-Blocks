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
        Boolean leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, spaceDown, timer, forward, backward;

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

        List<int> borderX = new List<int>();
        List<int> borderY = new List<int>();
        List<int> borderWidth = new List<int>();
        List<int> borderHeight = new List<int>();

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
            bXList.Add(12);
            bYList.Add(126);
            bWidthList.Add(90);
            bHeightList.Add(45);

            ///Obstacles
            
            //1x2
            bXList.Add(12);
            bYList.Add(34);
            bWidthList.Add(45);
            bHeightList.Add(91);

            //2x1
            bXList.Add(103);
            bYList.Add(34);
            bWidthList.Add(91);
            bHeightList.Add(45);

            //3x1
            bXList.Add(58);
            bYList.Add(80);
            bWidthList.Add(136);
            bHeightList.Add(45);

            //1x3
            bXList.Add(195);
            bYList.Add(34);
            bWidthList.Add(45);
            bHeightList.Add(137);

            //1x3
            bXList.Add(241);
            bYList.Add(126);
            bWidthList.Add(45);
            bHeightList.Add(137);

            //1x2
            bXList.Add(103);
            bYList.Add(172);
            bWidthList.Add(91);
            bHeightList.Add(45);

            //1x2
            bXList.Add(149);
            bYList.Add(218);
            bWidthList.Add(91);
            bHeightList.Add(45);

            //2x1
            bXList.Add(57);
            bYList.Add(172);
            bWidthList.Add(45);
            bHeightList.Add(91);

            //2x1
            bXList.Add(103);
            bYList.Add(218);
            bWidthList.Add(45);
            bHeightList.Add(91);

            /*
            e.Graphics.FillRectangle(borderBrush, 0, 0, 10, this.Height);
            e.Graphics.FillRectangle(borderBrush, 0, 0, this.Width, 32);
            e.Graphics.FillRectangle(borderBrush, this.Width-11, 0, 11, this.Height);
            e.Graphics.FillRectangle(borderBrush, 0, this.Height-11, this.Width, 11);
             */
            //Border
            borderX.Add(0);
            borderX.Add(0);
            borderX.Add(this.Width-11);
            borderX.Add(0);
            borderY.Add(0);
            borderY.Add(0);
            borderY.Add(0);
            borderY.Add(this.Height-11);
            borderWidth.Add(10);
            borderWidth.Add(this.Width);
            borderWidth.Add(11);
            borderWidth.Add(this.Width);
            borderHeight.Add(this.Height);
            borderHeight.Add(32);
            borderHeight.Add(this.Height);
            borderHeight.Add(11);
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

            switch (e.KeyCode)
            {
                case Keys.Down:
                    downArrowDown = true;
                    leftArrowDown = false;
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    leftArrowDown = false;
                    rightArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    upArrowDown = false;
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    upArrowDown = false;
                    downArrowDown = false;
                    break;
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

            int tempX = bXList[currentPiece];
            int tempY = bYList[currentPiece];

            //Moving the Blocks
            if (leftArrowDown == true)
            {
                bXList[currentPiece] = bXList[currentPiece] - blockSpeed;
            }
            if (downArrowDown == true)
            {
                bYList[currentPiece] = bYList[currentPiece] + blockSpeed;
            }
            if (rightArrowDown == true)
            {
                bXList[currentPiece] = bXList[currentPiece] + blockSpeed;
            }
            if (upArrowDown == true)
            {
                bYList[currentPiece] = bYList[currentPiece] - blockSpeed;
            }

            //Collisions
            for (int i = 0; i < bXList.Count && i < borderX.Count; i++)
            {
                Rectangle blockRec = new Rectangle(bXList[i], bYList[i], bWidthList[i], bHeightList[i]);

                Rectangle borderRec = new Rectangle(borderX[i], borderY[i], borderWidth[i], borderHeight[i]);

                if (blockRec.IntersectsWith(borderRec))
                {
                    bXList[currentPiece] = tempX;
                    bYList[currentPiece] = tempY;
                }
            }
            Refresh();
        }


        //Drawn on Screen
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //Background
            e.Graphics.FillRectangle(backGroundColor, 11, 33, 276, 277);

            //Blocks

            for (int i = 0; i < bXList.Count; i++)
            {
                e.Graphics.FillRectangle(blocksBrush, bXList[i], bYList[i], bWidthList[i], bHeightList[i]);
            }

            //Border
            for (int i = 0; i < borderX.Count; i++)
            {
                e.Graphics.FillRectangle(borderBrush, borderX[i], borderY[i], borderWidth[i], borderHeight[i]);
            }
            /*
            e.Graphics.FillRectangle(borderBrush, 0, 0, 10, this.Height);
            e.Graphics.FillRectangle(borderBrush, 0, 0, this.Width, 32);
            e.Graphics.FillRectangle(borderBrush, this.Width-11, 0, 11, this.Height);
            e.Graphics.FillRectangle(borderBrush, 0, this.Height-11, this.Width, 11);
            */
        }
    }
}
