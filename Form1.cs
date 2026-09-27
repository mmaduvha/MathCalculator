using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedMathCalculator
{
    public partial class Home : Form
    {
        string operation;
        double result;
        bool isOperationPerformed = false;

        public Home()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (inputBox.Text == "0" || isOperationPerformed)
            {
                inputBox.Clear();
            }
            Button button = (Button)sender;
            inputBox.Text += button.Text;
            isOperationPerformed = false;
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            Button_Click(sender, e);
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            Button_Click(sender, e);
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            Button_Click(sender, e);
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            Button_Click(sender, e);
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            Button_Click(sender, e);
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            Button_Click(sender, e);
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            Button_Click(sender, e);
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            Button_Click(sender, e);
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            Button_Click(sender, e);
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            Button_Click(sender, e);
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            Button_Click(sender, e);

            if (inputBox.Text.Contains("."))
            {
                return;
            }
            
        }

        private void Operator_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            operation = button.Text;
            result = Double.Parse(inputBox.Text);
            isOperationPerformed = true;
            lblText.Text = result + " " + operation;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            switch (operation)
            {
                case "+":
                    inputBox.Text = (result + Double.Parse(inputBox.Text)).ToString();
                    break;
                case "-":
                    inputBox.Text = (result - Double.Parse(inputBox.Text)).ToString();
                    break;
                case "*":
                    inputBox.Text = (result * Double.Parse(inputBox.Text)).ToString();
                    break;
                case "/":
                    inputBox.Text = (result / Double.Parse(inputBox.Text)).ToString();
                    break;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Operator_Click(sender, e);
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            Operator_Click(sender, e);
        }

        private void btnDevide_Click(object sender, EventArgs e)
        {
            Operator_Click(sender, e);
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            Operator_Click(sender, e);
        }

       
    }
}