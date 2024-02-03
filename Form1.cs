using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacTocGame.Properties;

namespace TicTacTocGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        enum players :byte
        {
            player1 = 1,
            player2 = 2
        }
        struct data
        {
            public players p;
            public int count;
            public data(players pl)
            {
                p = pl;
                count = 0;
            }
        }
        static data pla1 = new data(players.player1);
        static data pla2=new data(players.player2);
        static players currentPlayer = pla1.p;
        Button[] arr = new Button[3];
        void click(Button b)
        {
            if(b.Tag.ToString()!="?")
                return;
            if(currentPlayer==pla1.p)
            {
                b.Image = Resources.X;
                b.Tag = "x";
                checkWin(pla1);
                turnPlayer.Text = "Player 2";
                currentPlayer = pla2.p;
                return;
            }
            b.Image = Resources.O;
            b.Tag = "o";
            checkWin(pla2);
            turnPlayer.Text = "Player 1";
            currentPlayer = pla1.p;
        }
        bool testRecord(Button b1, Button b2, Button b3,data p)
        {
            if (b1.Tag.ToString() == "?" || b2.Tag.ToString() == "?" || b3.Tag.ToString() == "?")
                return false;
            if (b1.Tag.ToString() != b2.Tag.ToString())
                return false;
            if (b2.Tag.ToString() != b3.Tag.ToString())
                return false;
            b1.BackColor = Color.Green;
            b2.BackColor = Color.Green;
            b3.BackColor = Color.Green;
            arr[0] = b1;
            arr[1] = b2;
            arr[2] = b3;
            p.count++;
            return true ;
        }
        void checkWin(data p)
        {
            if(
            testRecord(button1,button2 , button3 ,p) ||
            testRecord(button4,button5 , button6 ,p) ||
            testRecord(button7,button8 , button9 ,p) ||
            testRecord(button1,button4 , button7 ,p) ||
            testRecord(button2,button5 , button8 ,p) ||
            testRecord(button3,button6 , button9 ,p) ||
            testRecord(button1,button5 , button9 ,p) ||
            testRecord(button3,button5 , button7 ,p)
            )
            {
                MessageBox.Show(currentPlayer.ToString() + " Win!!");
                restertGame(p,false);
                return;
            }
            if(
                button1.Tag.ToString()!="?" &&
                button2.Tag.ToString() != "?" &&
                button3.Tag.ToString() != "?" &&
                button4.Tag.ToString() != "?" &&
                button5.Tag.ToString() != "?" &&
                button6.Tag.ToString() != "?" &&
                button7.Tag.ToString() != "?" &&
                button8.Tag.ToString() != "?" &&
                button9.Tag.ToString() != "?"
                )
            {
                MessageBox.Show("Draw..!");
                restertGame(p,true);
            }    

        }
        void restertButton(Button b)
        {
            b.Tag = "?";
            b.Image = Resources.question_mark_96;

        }
        void restertGame(data win ,bool IsDraw)
        {
            restertButton(button1);
            restertButton(button2);
            restertButton(button3);
            restertButton(button4);
            restertButton(button5);
            restertButton(button6);
            restertButton(button7);
            restertButton(button8);
            restertButton(button9);
            for (int i = 0; i < 3; i++)
            {
                arr[i].BackColor = Color.Black;
            }
            if (!IsDraw)
            {
                if (win.p == players.player1)
                    resultPlayer1.Text = (Convert.ToInt32(resultPlayer1.Text) + 1).ToString();
                else if (win.p == players.player2)
                    resultPlayer2.Text = Convert.ToString((Convert.ToInt32(resultPlayer2.Text) + 1));
            }
            colorResultNames();
        }
        void colorResultNames()
        {
            int player1Result = Convert.ToInt32(resultPlayer1.Text.ToString());
            int player2Result = Convert.ToInt32(resultPlayer2.Text.ToString());

            if (player1Result > player2Result)
            {
                resultPlayer1.ForeColor = Color.Green;
                resultPlayer2.ForeColor = Color.Red;
                player1TextResult.ForeColor = Color.Green;
                player2TextResult.ForeColor = Color.Red;
            }
            else if (player1Result < player2Result)
            {
                resultPlayer1.ForeColor = Color.Red;
                resultPlayer2.ForeColor = Color.Green;
                player1TextResult.ForeColor = Color.Red;
                player2TextResult.ForeColor = Color.Green;
            }
            else
            {
                resultPlayer1.ForeColor = Color.White;
                resultPlayer2.ForeColor = Color.White;
                player1TextResult.ForeColor = Color.White;
                player2TextResult.ForeColor = Color.White;
            }
        }
        private void button_Click(object sender, EventArgs e)
        {
            click((Button)sender);
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure for New game ?", "Restert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                return;
            resultPlayer1.Text = "0";
            resultPlayer2.Text = "0";
            player1TextResult.ForeColor = Color.White;
            player2TextResult.ForeColor = Color.White;
            resultPlayer1.ForeColor = Color.White;
            resultPlayer2.ForeColor = Color.White;
            restertButton(button1);
            restertButton(button2);
            restertButton(button3);
            restertButton(button4);
            restertButton(button5);
            restertButton(button6);
            restertButton(button7);
            restertButton(button8);
            restertButton(button9);
            currentPlayer = pla1.p;
            turnPlayer.Text = "Player 1";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color white=Color.FromArgb(255,255,255,255);
            Pen p = new Pen(white);
            p.Width = 15;
            p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            e.Graphics.DrawLine(p, 323, 150, 750, 150);
            e.Graphics.DrawLine(p, 323, 280, 750, 280);

            e.Graphics.DrawLine(p, 455, 32, 455, 407);
            e.Graphics.DrawLine(p, 600, 32, 600, 407);
        }
    }
}
