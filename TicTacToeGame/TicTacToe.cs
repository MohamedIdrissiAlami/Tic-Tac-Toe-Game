using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeGame.Properties;

namespace TicTacToeGame
{
    public partial class TicTacToe : Form
    {
        public TicTacToe()
        {
            InitializeComponent();
        }
        private enTurn _PlayerTurn = enTurn.Player1;
        private stGameStatus _GameStatus;

        enum enWinner { Player1, Player2, Draw,InProgress }
        enum enTurn { Player1, Player2 }

        struct stGameStatus
        {
            public byte PlayCount;
            public bool GameOver;
            public enWinner Winner;
        }

        private void _SetBtnBackColorTo(Button btn,Color Color)
        {
            btn.BackColor = Color;
        }
        private bool _CheckValues(Button Button1,Button Button2,Button Button3)
        {
            if(Button1.Tag.ToString()== Button2.Tag.ToString() && Button1.Tag.ToString()== Button3.Tag.ToString()&& Button1.Tag.ToString()!="?")
            {
                _SetBtnBackColorTo(Button1, Color.YellowGreen);
                _SetBtnBackColorTo(Button2, Color.YellowGreen);
                _SetBtnBackColorTo(Button3, Color.YellowGreen);
                _GameStatus.GameOver = true;
                _GameStatus.Winner = (Button1.Tag.ToString()=="X"? enWinner.Player1: enWinner.Player2);
                return true;
            }
            _GameStatus.GameOver = false;
            return false;
        }
        private bool CheckWinner()
        {

            //check row1
            if (_CheckValues(btn1, btn2, btn3))
                return true;
            //check row2
            if (_CheckValues(btn4, btn5, btn6))
                return true;
            //check row3
            if (_CheckValues(btn7, btn8, btn9))
                return true;
            //check column 1
            if (_CheckValues(btn1, btn4, btn7))
                return true;
            //check column 2
            if (_CheckValues(btn2, btn5, btn8))
                return true;
            //check column 3
            if (_CheckValues(btn3, btn6, btn9))
                return true;
            //check diagonals
            if (_CheckValues(btn1, btn5, btn9))
                return true;
            if (_CheckValues(btn3, btn5, btn7))
                return true;
            
            if(_GameStatus.PlayCount==9)
            {
                _GameStatus.Winner = enWinner.Draw;
                return true;
            }

            return false;
        }

        private void _EndGame()
        {
            lblTurn.Text = "Game Over";
            switch(_GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player1";
                    break;
                case enWinner.Player2:
                    lblWinner.Text = "Player2";
                    break;
                default:
                    lblWinner.Text = "Draw";
                    break;
            }
            MessageBox.Show("Game Over","Game Over",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void _ChangeImage(Button btn)
        {
            if(btn.Tag.ToString()=="?")
            {
                switch(_PlayerTurn)
                {
                    case enTurn.Player1:
                        btn.Image = Resources.X;
                        btn.Tag = "X";
                        ++_GameStatus.PlayCount;
                        lblTurn.Text = "Player2";
                        _PlayerTurn = enTurn.Player2;
                        break;
                    case enTurn.Player2:
                        btn.Image = Resources.O;
                        btn.Tag = "O";
                        ++_GameStatus.PlayCount;
                        lblTurn.Text = "Player1";
                        _PlayerTurn = enTurn.Player1;
                        break;
                }
                if(CheckWinner()||_GameStatus.PlayCount == 9)
                {
                    _GameStatus.GameOver = true;
                    _EndGame();
                }
            }
            else
            {
                MessageBox.Show("Wrong Choice","Wrong",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            _ChangeImage((Button)sender);
        }

        private void ResetButton(Button btn)
        {
            btn.BackColor = Color.Transparent;
            btn.Tag = "?";
            btn.Image = Resources.question_mark_96;
        }
        private void _ResetGame()
        {
            ResetButton(btn1);
            ResetButton(btn2);
            ResetButton(btn3);
            ResetButton(btn4);
            ResetButton(btn5);
            ResetButton(btn6);
            ResetButton(btn7);
            ResetButton(btn8);
            ResetButton(btn9);

            lblTurn.Text = "Player1";
            _PlayerTurn = enTurn.Player1;
            lblWinner.Text = "In Progress";
            _GameStatus.GameOver = false;
            _GameStatus.PlayCount = 0;
            _GameStatus.Winner = enWinner.InProgress;
        }
        private void btnResetGame_Click(object sender, EventArgs e)
        {
            _ResetGame();
        }
    }
}
