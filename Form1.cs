using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace game
{
    public partial class Form1 : Form
    {
        int score;
        int count;
        bool gameIsActive;
        Random rand = new Random();

        List<PictureBox> items = new List<PictureBox>();
        //List<Rectangle> items = new List<Rectangle>();
        public Form1()
        {
            InitializeComponent();
            score = 0;
            count = 0;

            //RestartGame();
        }

        private void MakePictureBox()
        {
            PictureBox newPic = new PictureBox();
            newPic.Height = 50;
            newPic.Width = 50;
            int color = rand.Next(1, 5);
            switch (color) {
                case 1:
                    newPic.BackColor = Color.Red;
                    break;
                case 2:
                    newPic.BackColor = Color.Yellow;
                    break;
                case 3:
                    newPic.BackColor = Color.Blue;
                    break;
                case 4:
                    newPic.BackColor = Color.Green;
                    break;
            }

            int x = rand.Next(10, this.ClientSize.Width - newPic.Width);
            int y = rand.Next(10, this.ClientSize.Height - newPic.Height);
            newPic.Location = new Point(x, y);

            newPic.Click += NewPicClick;
            items.Add(newPic);
            this.Controls.Add(newPic);
            ++count;
        }

        private void NewPicClick(object sender, EventArgs e)
        {
            PictureBox tempPic = sender as PictureBox;

            if (tempPic.BackColor == Color.Red) {
                score += 10;
            }
            else if (tempPic.BackColor == Color.Yellow)
            {
                score += 5;
            }
            else if (tempPic.BackColor == Color.Blue)
            {
                score += 0;
            }
            else
            {
                score -= 20;
            }
            items.Remove(tempPic);
            this.Controls.Remove(tempPic);
            --count;

            Score_Label.Text = "Score: " + score;

        }

        private void StartGame()
        {
            score = 0;
            gameIsActive = true;
        }
        private void RestartGame()
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MakePictureBox();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (items.Count != 0)
            {
                items.RemoveAt(0);
                this.Controls.Remove(items[0]);

                if(items[0].BackColor != Color.Green)
                {
                    score -= 50;
                }

                --count;
            }
            Score_Label.Text = "Score: " + score;
        }
    }
}
