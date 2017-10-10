using System;
using System.Windows.Forms;

namespace X0
{
    public partial class Form1 : Form
    {
        private bool nichya = false;
        private Button[,] buttons;
        private int[,] step;
        private int[,] prediction = new int[1, 2];

        public Form1()
        {
            InitializeComponent();
            buttons = new Button[,]
            { { button1, button2, button3 },{ button4,button5,button6},{ button7,button8,button9} };
            step = Hod();
            FlashButtons();
            label2.Text = "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TurnButtons(false);
        }

        private void TurnButtons(bool turn)
        {
            foreach (var btn in buttons)
            {
                if (turn)
                {
                    btn.Enabled = true;
                }
                else
                    btn.Enabled = false;
            }
        }

        private void RefreshField()
        {
            foreach (var btn in buttons)
            {
                btn.Text = "";
            }
        }

        private void FieldClick(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "") { ((Button)sender).Text = "X"; } else return;

            if (CheckHorizontal("X"))
            {
                label1.Text = "Вы выиграли!";
                stop_button_Click(stop_button, e);
            }
            if (CheckVertical("X"))
            {
                label1.Text = "Вы выиграли!";
                stop_button_Click(stop_button, e);
            }
            if (CheckDiagonal("X"))
            {
                label1.Text = "Вы выиграли!";
                stop_button_Click(stop_button, e);
            }
            if (CheckForFull()) { Nichya(); return; }
            CompoStep();
        }

        private bool CheckHorizontal(string n, bool comp = false, string str = "")
        {
            bool[] flags = new bool[3];
            if (comp)
            {
                for (int i = 0; i <= 2; i++)
                {
                    for (int j = 0; j <= 2; j++)
                    {
                        if (buttons[i, j].Text == str || buttons[i, j].Text == n)
                        {
                            flags[j] = true;
                        }
                        else flags[j] = false;
                    }
                    bool control = true;
                    foreach (bool ch in flags)
                    {
                        if (ch == false) control = false;
                    }
                    if (control)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                for (int i = 0; i <= 2; i++)
                {
                    for (int j = 0; j <= 2; j++)
                    {
                        if (buttons[i, j].Text == n)
                        {
                            flags[j] = true;
                        }
                        else flags[j] = false;
                    }
                    bool control = true;
                    foreach (bool ch in flags)
                    {
                        if (ch == false) control = false;
                    }
                    if (control)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private bool CheckVertical(string n, bool comp = false, string str = "")
        {
            bool[] flags = new bool[3];
            if (comp)
            {
                for (int i = 0; i <= 2; i++)
                {
                    for (int j = 0; j <= 2; j++)
                    {
                        if (buttons[j, i].Text == str || buttons[j, i].Text == n)
                        {
                            flags[j] = true;
                        }
                    }
                    bool control = true;
                    foreach (bool ch in flags)
                    {
                        if (ch == false) control = false;
                    }
                    if (control)
                    {
                        return true;
                    }
                    else
                    {
                        for (int k = 0; k < flags.Length; k++) flags[k] = false;
                    }
                }
                return false;
            }
            else
            {
                for (int i = 0; i <= 2; i++)
                {
                    for (int j = 0; j <= 2; j++)
                    {
                        if (buttons[j, i].Text == n)
                        {
                            flags[j] = true;
                        }
                    }
                    bool control = true;
                    foreach (bool ch in flags)
                    {
                        if (ch == false) control = false;
                    }
                    if (control)
                    {
                        return true;
                    }
                    else
                    {
                        for (int k = 0; k < flags.Length; k++) flags[k] = false;
                    }
                }
                return false;
            }
        }

        private bool CheckDiagonal(string n, bool comp = false, string str = "")
        {
            if (comp)
            {
                if ((buttons[0, 0].Text == str || buttons[0, 0].Text == n) && (buttons[1, 1].Text == str || buttons[1, 1].Text == n) && (buttons[2, 2].Text == str || buttons[2, 2].Text == n)) return true;
                if ((buttons[0, 2].Text == str || buttons[0, 2].Text == n) && (buttons[1, 1].Text == str || buttons[1, 1].Text == n) && (buttons[2, 0].Text == str || buttons[2, 0].Text == n)) return true;
                return false;
            }
            else
            {
                if (buttons[0, 0].Text == n && buttons[1, 1].Text == n && buttons[2, 2].Text == n) return true;
                if (buttons[0, 2].Text == n && buttons[1, 1].Text == n && buttons[2, 0].Text == n) return true;
                return false;
            }
        }
        private bool CheckForFull()
        {
            foreach(Button btn in buttons)
            {
                if (btn.Text == "") return false;
            }
            return true;
        }
        private bool Prediction(string str="X")
        {
            //horizontal:
            {
                bool[] flags = new bool[3];
                int x = 0, y = 0;
                for (int i = 0; i <= 2; i++)
                {
                    for (int j = 0; j <= 2; j++)
                    {
                        if (buttons[i, j].Text == str)
                        {
                            flags[j] = true;
                        }
                        else { x = i; y = j; }
                    }
                    int count = 0;
                    foreach (bool ch in flags)
                    {
                        if (ch == true) count++;
                    }
                    if (count == 2)
                    {
                        bool check1 = false;
                        for (int j = 0; j <= 2; j++)
                        {
                            if (buttons[i, j].Text == "")
                            {
                                check1 = true;
                            }
                        }
                        if (check1)
                        {
                            prediction[0, 0] = x;
                            prediction[0, 1] = y;
                            return true;
                        }
                    }
                    flags = new bool[3];
                }
            }
            //vertical:
            {
                bool[] flags = new bool[3];
                int x = 0, y = 0;
                for (int i = 0; i <= 2; i++)
                {
                    for (int j = 0; j <= 2; j++)
                    {
                        if (buttons[j, i].Text == str)
                        {
                            flags[j] = true;
                        }
                        else { x = j; y = i; }
                    }
                    int count = 0;
                    foreach (bool ch in flags)
                    {
                        if (ch == true) count++;
                    }
                    if (count == 2)
                    {
                        bool check1 = false;
                        for (int j = 0; j <= 2; j++)
                        {
                            if (buttons[j, i].Text == "")
                            {
                                check1 = true;
                            }
                        }
                        if (check1)
                        {
                            prediction[0, 0] = x;
                            prediction[0, 1] = y;
                            return true;
                        }
                    }
                    flags = new bool[3];
                }
            }
            //diagonal:
            {
                bool[] flags = new bool[3];
                int x = 0, y = 0;
                int[,] coor1 = { { 0, 0 }, { 1, 1 }, { 2, 2 } };
                int[,] coor2 = { { 0, 2 }, { 1, 1 }, { 2, 0 } };
                //coor1:
                {
                    for (int j = 0; j <= 2; j++)
                    {
                        if (buttons[coor1[j, 0], coor1[j, 1]].Text == str)
                        {
                            flags[j] = true;
                        }
                        else { x = coor1[j, 0]; y = coor1[j, 1]; }
                    }
                    int count = 0;
                    foreach (bool ch in flags)
                    {
                        if (ch == true) count++;
                    }
                    if (count == 2)
                    {
                        bool check1 = false;
                        for (int j = 0; j <= 2; j++)
                        {
                            if (buttons[coor1[j, 0], coor1[j, 1]].Text == "")
                            {
                                check1 = true;
                            }
                        }
                        if (check1)
                        {
                            prediction[0, 0] = x;
                            prediction[0, 1] = y;
                            return true;
                        }

                        flags = new bool[3];
                    }
                }
                ////////coor2:
                {
                    for (int j = 0; j <= 2; j++)
                    {
                        if (buttons[coor2[j, 0], coor2[j, 1]].Text == str)
                        {
                            flags[j] = true;
                        }
                        else { x = coor2[j, 0]; y = coor2[j, 1]; }
                    }
                    int count = 0;
                    foreach (bool ch in flags)
                    {
                        if (ch == true) count++;
                    }
                    if (count == 2)
                    {
                        bool check1 = false;
                        for (int j = 0; j <= 2; j++)
                        {
                            if (buttons[coor2[j, 0], coor2[j, 1]].Text == "")
                            {
                                check1 = true;
                            }
                        }
                        if (check1)
                        {
                            prediction[0, 0] = x;
                            prediction[0, 1] = y;
                            return true;
                        }

                        flags = new bool[3];
                    }
                }
            }
            return false;
        }

        private void CompoStep()
        {
            if (Prediction("0"))
            {
                step = prediction;
                FlashButtons1();
                buttons[step[0, 0], step[0, 1]].Text = "0";
                step = Hod();
                CheckForLoose();
            }
            else {
                if (Prediction())
                {
                    step = prediction;
                    FlashButtons1();
                    buttons[step[0, 0], step[0, 1]].Text = "0";
                    step = Hod();
                    CheckForLoose();                   
                }
                else {
                    f:
                    if (CheckForEmptySpace(step))
                    {
                        for (int i = 0; i <= 2; i++)
                        {
                            if (CheckForEmptyField(step[i, 0], step[i, 1]))
                            {
                                buttons[step[i, 0], step[i, 1]].Text = "0";
                                if (CheckForFull()) Nichya();
                                FlashButtons();
                                CheckForLoose();
                                break;
                            }
                        }
                    }
                    else
                    {
                        label2.Text = (Int32.Parse(label2.Text) + 1).ToString();
                        step = Hod();
                        FlashButtons();
                        if (nichya) Nichya();
                        goto f;
                    }
                    CheckForLoose();
                }
            }
        }
        private void CheckForLoose()
        {
            if (CheckHorizontal("0"))
            {
                label1.Text = "Вы проиграли!";
                Stop();
            }
            if (CheckVertical("0"))
            {
                label1.Text = "Вы проиграли!";
                Stop();
            }
            if (CheckDiagonal("0"))
            {
                label1.Text = "Вы проиграли!";
                Stop();
            }
        }
        private int[,] Hod()
        {
            int[,] steps = new int[3, 2];
            if (CheckHorizontal("0", true))
            {
               
                return FindHorizontal();
            }
            else
            {
                if (CheckVertical("0", true))
                {
                    
                    return FindVertical();
                }
                else
                {
                    if (CheckDiagonal("0", true))
                    {
                       
                        return FindDiagonal();
                    }
                }
            }
            nichya = true;
            return steps;
        }

        private void Nichya()
        {
            label1.Text = "Ничья!";
            Stop();
        }

        private bool CheckForEmptySpace(int[,] steps)
        {
            if ((buttons[steps[0, 0], steps[0, 1]].Text == "" || buttons[steps[0, 0], steps[0, 1]].Text == "0") &&
                (buttons[steps[1, 0], steps[1, 1]].Text == "" || buttons[steps[1, 0], steps[1, 1]].Text == "0")
                && (buttons[steps[2, 0], steps[2, 1]].Text == "" || buttons[steps[2, 0], steps[2, 1]].Text == "0")) return true;
            return false;
        }

        private int[,] FindHorizontal(string str = "", string n = "0")
        {
            bool[] flags = new bool[3];
            int[,] steps = new int[3, 2];
            for (int i = 0; i <= 2; i++)
            {
                int l = 0;
                for (int j = 0; j <= 2; j++)
                {
                    if (buttons[i, j].Text == str || buttons[i, j].Text == n)
                    {
                        flags[j] = true;
                        steps[l, 0] = i;
                        steps[l, 1] = j;
                        l++;
                    }
                    else { flags[j] = false; }
                }
                bool control = true;
                foreach (bool ch in flags)
                {
                    if (ch == false) control = false;
                }
                if (control)
                {
                    return steps;
                }
            }
            return steps;
        }

        private int[,] FindVertical(string str = "", string n = "0")
        {
            bool[] flags = new bool[3];
            int[,] steps = new int[3, 2];
            for (int i = 0; i <= 2; i++)
            {
                int l = 0;
                for (int j = 0; j <= 2; j++)
                {
                    if (buttons[j, i].Text == str || buttons[j, i].Text == n)
                    {
                        flags[j] = true;
                        steps[l, 0] = j;
                        steps[l, 1] = i;
                        l++;
                    }
                }
                bool control = true;
                foreach (bool ch in flags)
                {
                    if (ch == false) control = false;
                }
                if (control)
                {
                    return steps;
                }
                else
                {
                    for (int k = 0; k < flags.Length; k++) flags[k] = false;
                }
            }
            return steps;
        }

        private int[,] FindDiagonal(string str = "", string n = "0")
        {
            int[,] steps = new int[3, 2];
            if ((buttons[0, 0].Text == str || buttons[0, 0].Text == n) && (buttons[1, 1].Text == str || buttons[1, 1].Text == n) && (buttons[2, 2].Text == str || buttons[2, 2].Text == n))
            {
                steps[0, 0] = 0;
                steps[0, 1] = 0;
                steps[1, 0] = 1;
                steps[1, 1] = 1;
                steps[2, 0] = 2;
                steps[2, 1] = 2;
                return steps;
            }
            if ((buttons[0, 2].Text == str || buttons[0, 2].Text == n) && (buttons[1, 1].Text == str || buttons[1, 1].Text == n) && (buttons[2, 0].Text == str || buttons[2, 0].Text == n))
            {
                steps[0, 0] = 0;
                steps[0, 1] = 2;
                steps[1, 0] = 1;
                steps[1, 1] = 1;
                steps[2, 0] = 2;
                steps[2, 1] = 0;
                return steps;
            }
            return steps;
        }

        private bool CheckForEmptyField(int x, int y, string n = "0")
        {
            if (buttons[x, y].Text == "") return true;
            return false;
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            RefreshField();
            TurnButtons(true);
            nichya = false;
            step = Hod();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            RefreshField();
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            RefreshField();
            TurnButtons(false);
            nichya = false;
        }

        private void Stop()
        {
            // RefreshField();
            TurnButtons(false);
        }
        private void FlashButtons()
        {
            for(int i = 0; i <=2; i++)
            {
                for(int j = 0; j <= 2; j++)
                {
                    buttons[i,j].BackColor = System.Drawing.Color.White;
                }                
            }
            buttons[step[0, 0], step[0, 1]].BackColor = System.Drawing.Color.Red;
            buttons[step[1, 0], step[1, 1]].BackColor = System.Drawing.Color.Red;
            buttons[step[2, 0], step[2, 1]].BackColor = System.Drawing.Color.Red;
        }
        private void FlashButtons1()
        {
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    buttons[i, j].BackColor = System.Drawing.Color.White;
                }
            }
            buttons[step[0, 0], step[0, 1]].BackColor = System.Drawing.Color.Red;           
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
        }
    }
}