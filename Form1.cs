using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace game
{
    public partial class Form1 : Form
    {
        int score;
        int count;
        bool gameIsActive;
        Random rand = new Random();
        string soundFilePath;
        SoundPlayer clickSoundPlayer;

        List<PictureBox> items = new List<PictureBox>();
        public Form1()
        {
            InitializeComponent();
            score = 0;
            count = 0;
            gameIsActive = true;

            string baseDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName;
            soundFilePath = Path.Combine(baseDirectory, "files", "PopSound.wav");
            clickSoundPlayer = new SoundPlayer(soundFilePath);
        }

        private void MakePictureBox()
        {
            PictureBox newPic = new PictureBox();
            newPic.Height = 50;
            newPic.Width = 50;

            int color = rand.Next(1, 5);
            switch (color)
            {
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

            newPic.Paint += new PaintEventHandler((sender, e) =>
            {
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(0, 0, newPic.Width - 1, newPic.Height - 1);
                Region region = new Region(path);
                newPic.Region = region;
            });

            newPic.Click += NewPicClick;
            items.Add(newPic);
            this.Controls.Add(newPic);
            ++count;
        }

        private void NewPicClick(object sender, EventArgs e)
        {
            PictureBox tempPic = sender as PictureBox;

            if (tempPic.BackColor == Color.Red)
            {
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

            // Play the click sound
            clickSoundPlayer.Play();
        }

        private void RestartGame()
        {
            foreach (PictureBox p in items)
                this.Controls.Remove(p);
            items.Clear();
            score = 0;
            count = 0;
            gameIsActive = true;
            timer1.Start();
            timer2.Start();

            Score_Label.Text = "Score: " + score;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MakePictureBox();
            if (score <= -100)
            {
                gameIsActive = false;
                timer1.Stop();
                DialogResult result = MessageBox.Show("You clicked on Green a lot. Play again?", "GAME OVER", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    RestartGame();
                }
                else
                {
                    Close();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (items.Count != 0)
            {
                this.Controls.Remove(items[0]);
                items.RemoveAt(0);

                if (items.Count != 0 && items[0].BackColor != Color.Green)
                {
                    score -= 50;
                }

                --count;
            }
            else
            {
                return;
            }

            Score_Label.Text = "Score: " + score;

            if (score <= -100)
            {
                gameIsActive = false;
                timer1.Stop();
                timer2.Stop();
                DialogResult result = MessageBox.Show("You missed a lot. Play again?", "GAME OVER", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    RestartGame();
                }
                else
                {
                    Close();
                }
            }
        }
    }
}
