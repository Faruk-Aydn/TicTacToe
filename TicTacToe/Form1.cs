using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        Button[,] buttons;
        bool turn = true; // true = X turn, false = O turn
        int turnCount = 0;
        int gridSize = 3; // Default grid size

        public Form1()
        {
            InitializeComponent();

            // Oyun boyutu seçimi için ComboBox ekleyin
            ComboBox comboBox = new ComboBox();
            comboBox.Items.AddRange(new string[] { "3x3", "6x6", "9x9" });
            comboBox.SelectedIndex = 0; // Default olarak 3x3 seçili
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            comboBox.Top = 10;
            comboBox.Left = 10;
            this.Controls.Add(comboBox);

            Button startButton = new Button();
            startButton.Text = "Başlat";
            startButton.Top = 40;
            startButton.Left = 10;
            startButton.Click += StartButton_Click;
            this.Controls.Add(startButton);
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedSize = comboBox.SelectedItem.ToString();

            if (selectedSize == "3x3")
                gridSize = 3;
            else if (selectedSize == "6x6")
                gridSize = 6;
            else if (selectedSize == "9x9")
                gridSize = 9;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            InitializeGameBoard();
        }

        private void InitializeGameBoard()
        {
            // Eski butonları temizle
            if (buttons != null)
            {
                foreach (Button btn in buttons)
                {
                    this.Controls.Remove(btn);
                }
            }

            buttons = new Button[gridSize, gridSize];
            turn = true;
            turnCount = 0;
            int top = 70; // Başlangıç yüksekliği
            int left = 10; // Başlangıç genişliği
            int buttonSize = 50; // Buton boyutu

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Width = buttonSize;
                    buttons[i, j].Height = buttonSize;
                    buttons[i, j].Top = top + (i * buttonSize);
                    buttons[i, j].Left = left + (j * buttonSize);
                    buttons[i, j].Font = new Font(buttons[i, j].Font.FontFamily, 16);
                    buttons[i, j].Click += new EventHandler(Button_Click);
                    this.Controls.Add(buttons[i, j]);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "")
            {
                button.Text = turn ? "X" : "O";
                turn = !turn;
                turnCount++;

                CheckWinner();
            }
        }

        private void CheckWinner()
        {
            bool winner = false;

            // Satırları kontrol et
            for (int i = 0; i < gridSize; i++)
            {
                if (CheckLine(buttons[i, 0].Text, i, 0, 0, 1))
                {
                    winner = true;
                    break;
                }
            }

            // Sütunları kontrol et
            for (int i = 0; i < gridSize; i++)
            {
                if (CheckLine(buttons[0, i].Text, 0, i, 1, 0))
                {
                    winner = true;
                    break;
                }
            }

            // Çaprazları kontrol et
            if (CheckLine(buttons[0, 0].Text, 0, 0, 1, 1) || CheckLine(buttons[0, gridSize - 1].Text, 0, gridSize - 1, 1, -1))
            {
                winner = true;
            }

            if (winner)
            {
                string win = turn ? "O" : "X";
                MessageBox.Show(win + " kazandı!");
                ResetGame();
            }
            else if (turnCount == gridSize * gridSize)
            {
                MessageBox.Show("Berabere!");
                ResetGame();
            }
        }

        private bool CheckLine(string symbol, int startX, int startY, int deltaX, int deltaY)
        {
            if (symbol == "") return false;

            for (int i = 0; i < gridSize; i++)
            {
                int x = startX + i * deltaX;
                int y = startY + i * deltaY;
                if (buttons[x, y].Text != symbol)
                    return false;
            }
            return true;
        }

        private void ResetGame()
        {
            foreach (Button btn in buttons)
            {
                btn.Text = "";
                btn.Enabled = true;
            }
            turn = true;
            turnCount = 0;
        }
    }
}