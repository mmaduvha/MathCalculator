using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AdvancedMathCalculator
{
    public partial class Home : Form
    {
        private string operation = "";
        private double result = 0;
        private bool isOperationPerformed = false;
        private bool advancedMode = false;

        public Home()
        {
            InitializeComponent();
        }

        private void btnOne_Click(object sender, EventArgs e) => HandleInput(sender);
        private void btnTwo_Click(object sender, EventArgs e) => HandleInput(sender);
        private void btnThree_Click(object sender, EventArgs e) => HandleInput(sender);
        private void btnFour_Click(object sender, EventArgs e) => HandleInput(sender);
        private void btnFive_Click(object sender, EventArgs e) => HandleInput(sender);
        private void btnSix_Click(object sender, EventArgs e) => HandleInput(sender);
        private void btnSeven_Click(object sender, EventArgs e) => HandleInput(sender);
        private void btnEight_Click(object sender, EventArgs e) => HandleInput(sender);
        private void btnNine_Click(object sender, EventArgs e) => HandleInput(sender);
        private void btnZero_Click(object sender, EventArgs e) => HandleInput(sender);
        private void btnOpenBracket_Click(object sender, EventArgs e) => HandleInput(sender);
        private void btnCloseBracket_Click_1(object sender, EventArgs e) => HandleInput(sender);
        private void btnXvariable_Click(object sender, EventArgs e) => HandleInput(sender);

        private void HandleInput(object sender)
        {
            Button button = (Button)sender;

            if (!advancedMode)
            {
                if (inputBox.Text == "0" || isOperationPerformed)
                {
                    inputBox.Clear();
                }
                inputBox.Text += button.Text;
                lblText.Text += button.Text;
                isOperationPerformed = false;
            }
            else
            {
                if (inputBox.Text == "0")
                {
                    inputBox.Clear();
                }
                inputBox.Text += button.Text;
                lblText.Text = inputBox.Text;
            }
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (!inputBox.Text.Contains("."))
            {
                HandleInput(sender);
            }
        }

        private void Operator_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (!advancedMode)
            {
                if (double.TryParse(inputBox.Text, out result))
                {
                    operation = button.Text;
                    isOperationPerformed = true;
                    lblText.Text = result + " " + operation + " ";
                }
            }
            else
            {
                HandleInput(sender);
            }
        }

        private void addButton_Click(object sender, EventArgs e) => Operator_Click(sender, e);
        private void btnMinus_Click(object sender, EventArgs e) => Operator_Click(sender, e);
        private void btnDevide_Click(object sender, EventArgs e) => Operator_Click(sender, e);
        private void btnMultiply_Click(object sender, EventArgs e) => Operator_Click(sender, e);

        private void btnPower_Click(object sender, EventArgs e)
        {
            if (!advancedMode)
            {
                if (double.TryParse(inputBox.Text, out result))
                {
                    operation = "^";
                    isOperationPerformed = true;
                    lblText.Text = result + " ^ ";
                }
            }
            else
            {
                HandleInput(sender);
            }
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (!advancedMode)
            {
                if (!double.TryParse(inputBox.Text, out double secondOperand))
                {
                    return;
                }

                switch (operation)
                {
                    case "+":
                        result += secondOperand;
                        break;
                    case "-":
                        result -= secondOperand;
                        break;
                    case "*":
                        result *= secondOperand;
                        break;
                    case "/":
                        if (secondOperand == 0)
                        {
                            inputBox.Text = "Cannot divide by zero";
                            return;
                        }
                        result /= secondOperand;
                        break;
                    case "^":
                        result = Math.Pow(result, secondOperand);
                        break;
                }

                inputBox.Text = result.ToString();
                lblText.Text = "";
                isOperationPerformed = true;
            }
            else
            {
                inputBox.Text = ExpandFoilExpression(inputBox.Text);
                lblText.Text = inputBox.Text;
            }
        }

        private string ExpandFoilExpression(string expression)
        {
            expression = expression.Replace(" ", "");
            Match match = Regex.Match(expression, @"^\((?:([+-]?\d*)x|([+-]?\d+))\+?([+-]?\d+)?\)\((?:([+-]?\d*)x|([+-]?\d+))\+?([+-]?\d+)?\)$");

            Match simpleMatch = Regex.Match(expression, @"^\(([+-]?\d*x?[+-]?\d*)\)\(([+-]?\d*x?[+-]?\d*)\)$");

            if (!simpleMatch.Success)
            {
                return "Invalid FOIL format: (ax+b)(cx+d)";
            }

            string factor1 = simpleMatch.Groups[1].Value;
            string factor2 = simpleMatch.Groups[2].Value;

            ParseTerm(factor1, out int a, out int b);
            ParseTerm(factor2, out int c, out int d);

            int x2Coeff = a * c;
            int xCoeff = (a * d) + (b * c);
            int constCoeff = b * d;

            string resultStr = "";

            if (x2Coeff != 0)
            {
                resultStr += (x2Coeff == 1 ? "x^2" : (x2Coeff == -1 ? "-x^2" : x2Coeff + "x^2"));
            }

            if (xCoeff != 0)
            {
                if (xCoeff > 0 && resultStr.Length > 0)
                {
                    resultStr += "+";
                }
                resultStr += (xCoeff == 1 ? "x" : (xCoeff == -1 ? "-x" : xCoeff + "x"));
            }

            if (constCoeff != 0)
            {
                if (constCoeff > 0 && resultStr.Length > 0)
                {
                    resultStr += "+";
                }
                resultStr += constCoeff;
            }

            return string.IsNullOrEmpty(resultStr) ? "0" : resultStr;
        }

        private void ParseTerm(string factor, out int xCoeff, out int constCoeff)
        {
            xCoeff = 0;
            constCoeff = 0;

            Match match = Regex.Match(factor, @"^([+-]?\d*)x([+-]\d+)?$");
            if (match.Success)
            {
                string xStr = match.Groups[1].Value;
                if (xStr == "" || xStr == "+")
                {
                    xCoeff = 1;
                }
                else if (xStr == "-")
                {
                    xCoeff = -1;
                }
                else
                {
                    int.TryParse(xStr, out xCoeff);
                }

                if (match.Groups[2].Value != "")
                {
                    int.TryParse(match.Groups[2].Value, out constCoeff);
                }
                return;
            }

            Match matchXOnly = Regex.Match(factor, @"^([+-]?\d+)x$");
            if (matchXOnly.Success)
            {
                int.TryParse(matchXOnly.Groups[1].Value, out xCoeff);
                return;
            }

            Match matchConstOnly = Regex.Match(factor, @"^([+-]?\d+)$");
            if (matchConstOnly.Success)
            {
                int.TryParse(matchConstOnly.Groups[1].Value, out constCoeff);
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblText.Text = "";
            inputBox.Text = "0";
            result = 0;
            operation = "";
            isOperationPerformed = false;
        }

        private void btnFoil_Click(object sender, EventArgs e)
        {
            advancedMode = !advancedMode;
            inputBox.Text = "0";
            lblText.Text = advancedMode ? "Advanced Mode (FOIL Enabled)" : "";
        }
    }
}