using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int row = 0, col = 0, students = 0;
        public bool[,] state;
        List<int> li;

        private void Form1_Load(object sender, EventArgs e)
        {
            width = this.Width;
            height = this.Height;
            label1.Location = new Point(this.width / 2 - label1.Width / 2, 5);
            label1.Visible = false;
            StudentBox.Location = new Point(this.width / 2 - ColumnBox.Width / 2, 250);
            ColumnBox.Location = new Point(this.width / 2 - RowBox.Width / 2 - 65, 380);
            RowBox.Location = new Point(this.width / 2 - StudentBox.Width / 2 + 65, 380);
            ConfirmButton.Location = new Point(this.width / 2 - RestartButton.Width / 2, 500);
            label2.Location = new Point(this.width / 2 - label2.Width / 2 - 90, 250);
            label3.Location = new Point(this.width / 2 - label3.Width / 2 + 55, 420);
            label4.Location = new Point(this.width / 2 - label4.Width / 2 - 55, 420);
            label5.Location = new Point(this.width / 2 - label5.Width / 2, 390);
            label6.Location = new Point(this.width / 2 + 80, 260);
            NumberListBox.Location = new Point(this.width / 2 + 150, 250);
            label6.Visible = false;
            NumberListBox.Visible = false;
            RowBox.Enabled = false;
            ColumnBox.Enabled = false;
            ResetButton.Visible = false;
            StartButton.Visible = false;
        }

        int btnval = 0;
        public int width, height, size, totalTable;

        private void StudentBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void ColumnBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void RowBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                {
                    buttons[r, c].Enabled = true;
                    buttons[r, c].Text = "O";
                    state[r, c] = true;
                    totalTable = row * col;
                }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Random rd = new Random();
            int idx = 0;
            for (int i = 0; i < students * 100; i++)
            {
                int tmp = rd.Next() % students;
                int tmp2 = rd.Next() % students;
                int tmp3 = li[tmp];
                li[tmp] = li[tmp2];
                li[tmp2] = tmp3;
            }

            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                {
                    buttons[r, c].Enabled = false;
                    if (state[r, c] == true && idx < students)
                    {
                        buttons[r, c].Text = li[idx].ToString();
                        idx++;
                    }
                }
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnval == 0)
                {
                    students = int.Parse(StudentBox.Text);
                    if (students == 0)
                    {
                        MessageBox.Show("班上一個人都沒有?");
                        return;
                    }
                    if (students > 100)
                    {
                        MessageBox.Show("班上人太多了歐!");
                        return;
                    }
                    StudentBox.Enabled = false;
                    ColumnBox.Enabled = true;
                    RowBox.Enabled = true;
                    label6.Visible = true;
                    NumberListBox.Visible = true;
                    btnval++;
                    for (int i = 0; i < students; i++)
                    {
                        NumberListBox.Items.Add("" + (i + 1));
                        NumberListBox.SetItemChecked(i, true);
                    }
                }
                else
                {
                    col = int.Parse(ColumnBox.Text);
                    row = int.Parse(RowBox.Text);
                    totalTable = row * col;
                    if (totalTable == 0)
                    {
                        MessageBox.Show("請輸入正確數值");
                        return;
                    }
                    if (totalTable > 100)
                    {
                        MessageBox.Show("全校桌椅都在你們班?");
                        return;
                    }
                    if ((row > 3 * col || col > 3 * row) && totalTable > 60)
                    {
                        MessageBox.Show("請輸入合理的規格!");
                        return;
                    }
                    state = new bool[row, col];
                    for (int r = 0; r < row; r++)
                        for (int c = 0; c < col; c++)
                            state[r, c] = true;
                    li = new List<int>();
                    for (int i = 0; i < students; i++)
                        if (NumberListBox.GetItemChecked(i))
                            li.Add(i + 1);

                    students = li.Count;
                    if (row * col < students)
                    {
                        MessageBox.Show("位置太少啦!!");
                        return;
                    }
                    NumberListBox.Visible = false;
                    label6.Visible = false;
                    ConfirmButton.Location = new Point(ConfirmButton.Left + 180, ConfirmButton.Top);
                    changeScr();
                    generate_Button();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("請輸入正確數值");
            }
        }


        private void changeScr()
        {
            label1.Visible = true;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            ColumnBox.Visible = false;
            RowBox.Visible = false;
            StudentBox.Visible = false;
            ConfirmButton.Visible = false;
            ResetButton.Visible = true;
            StartButton.Visible = true;
        }
        Button[,] buttons;
        private void generate_Button()
        {
            width /= (col + 1);
            height = (height - 40) / (row + 1);
            size = width > height ? height : width;
            buttons = new Button[row, col];
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    buttons[r, c] = new Button();
                    buttons[r, c].Location = new Point(width * (c + 1) - size / 2, height * (r + 1) - 40);
                    buttons[r, c].Size = new Size(size, size);
                    buttons[r, c].Text = "O";
                    Font f = buttons[r, c].Font;
                    buttons[r, c].Font = new Font(f.FontFamily, size / 3);
                    buttons[r, c].Name = "" + r + ',' + c;
                    buttons[r, c].Click += new EventHandler(this.buttons_Click);
                    this.Controls.Add(buttons[r, c]);
                }
            }
        }

        private void buttons_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string[] tmp = btn.Name.Split(',');
            int r = int.Parse(tmp[0]);
            int c = int.Parse(tmp[1]);
            if (state[r, c] == true)
            {
                if (totalTable <= students)
                {
                    MessageBox.Show("位置不夠啦!!");
                    return;
                }
                btn.Text = "X";
                totalTable--;
                state[r, c] = !state[r, c];
            }
            else
            {
                btn.Text = "O";
                totalTable++;
                state[r, c] = !state[r, c];
            }
        }
    }
}
