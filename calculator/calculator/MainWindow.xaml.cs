using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        SelectedOperator selectedoperator;
        double lastnumber=0;
        double newnumber=0;
        double middlenumber = 0;
        int count = 0;
   
        public MainWindow()
        {
            InitializeComponent();

            buttonAC.Click += buttonAC_Click;
            buttonneggative.Click += buttonneggative_Click;
            buttonpercent.Click += buttonpercent_Click;
        }

        public enum SelectedOperator
        {
            Addition,
            Substraction,
            Multiplication,
            Division
        }

        class SimpleMath
        {
            public static double Add(double a, double b) { 
                return a + b;
            }
            public static double Sub(double a, double b)
            {
                return a - b;
            }
            public static double Mul(double a, double b)
            {
                return a * b;
            }
            public static double Div(double a, double b)
            {
                return a / b;
            }
        }

        private void operationButton_Click(object sender, RoutedEventArgs e)
        {
           
            
            
            if (count>=1)
            {
                newnumber = double.Parse(txtboxCalculator.Text.ToString());
                if (middlenumber == 0)
                {
                    switch (selectedoperator)
                    {
                        case SelectedOperator.Addition:
                            middlenumber = SimpleMath.Add(lastnumber, newnumber);
                            break;
                        case SelectedOperator.Substraction:
                            middlenumber = SimpleMath.Sub(lastnumber, newnumber);
                            break;
                        case SelectedOperator.Multiplication:
                            middlenumber = SimpleMath.Mul(lastnumber, newnumber);
                            break;
                        case SelectedOperator.Division:
                            middlenumber = SimpleMath.Div(lastnumber, newnumber);
                            break;

                    }
                }
                else
                {
                    switch (selectedoperator)
                    {
                        case SelectedOperator.Addition:
                            middlenumber = SimpleMath.Add(middlenumber, newnumber);
                            break;
                        case SelectedOperator.Substraction:
                            middlenumber = SimpleMath.Sub(middlenumber, newnumber);
                            break;
                        case SelectedOperator.Multiplication:
                            middlenumber = SimpleMath.Mul(middlenumber, newnumber);
                            break;
                        case SelectedOperator.Division:
                            middlenumber = SimpleMath.Div(middlenumber, newnumber);
                            break;

                    }
                }
            }
            lastnumber = double.Parse(txtboxCalculator.Text.ToString());
            if (double.TryParse(txtboxCalculator.Text, out lastnumber))
            {
                txtboxCalculator.Text = "0";
            }
            
                if (sender == buttonplus)
                {
                    selectedoperator = SelectedOperator.Addition;
                    calcutxt.Text += lastnumber + " + ";
                }
                else if (sender == buttonminus)
                {
                    selectedoperator = SelectedOperator.Substraction;
                    calcutxt.Text += lastnumber + " - ";
                }
                else if (sender == buttonmultiple)
                {
                    selectedoperator = SelectedOperator.Multiplication;
                    calcutxt.Text += lastnumber + " * ";
                }
                else if (sender == buttondivision)
                {
                    selectedoperator = SelectedOperator.Division;
                    calcutxt.Text += lastnumber + " / ";
                }
            count++;
        }


        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            int SelectedValue = 0;
           
            Button selectedButton = (Button)sender;

            SelectedValue = int.Parse(selectedButton.Content.ToString());

                if (txtboxCalculator.Text.Equals("0"))
                {
                    txtboxCalculator.Text = SelectedValue.ToString();
                }
                else
                {
                    txtboxCalculator.Text += SelectedValue.ToString();
                }
            
        }
        private void buttonAC_Click(object sender, RoutedEventArgs e)
        {
            txtboxCalculator.Text = "0";
            lastnumber = 0;
            calcutxt.Text = "";
            middlenumber = 0;
        }

        private void buttonneggative_Click(object sender, RoutedEventArgs e)
        {
            //1 Cal = Convert.ToDouble(txtboxCalculator.Text);

            //2 Cal = double.Parse(txtboxCalculator.Text);

            if (double.TryParse(txtboxCalculator.Text, out lastnumber))
            {
                lastnumber *= -1;
                txtboxCalculator.Text = Convert.ToString(lastnumber);
            }
        }

        private void buttonpercent_Click(object sender, RoutedEventArgs e)
        {
            lastnumber = Convert.ToDouble(txtboxCalculator.Text);
            lastnumber /= 100;
            txtboxCalculator.Text = Convert.ToString(lastnumber);
        }

        private void buttonresult_Click(object sender, RoutedEventArgs e)
        {
         
            newnumber = double.Parse(txtboxCalculator.Text.ToString());
            double result = 0 ;
            if (count == 1)
            {
                switch (selectedoperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastnumber, newnumber);
                        break;
                    case SelectedOperator.Substraction:
                        result = SimpleMath.Sub(lastnumber, newnumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Mul(lastnumber, newnumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Div(lastnumber, newnumber);
                        break;

                }
            }
            else
            {
                switch (selectedoperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(middlenumber, newnumber);
                        break;
                    case SelectedOperator.Substraction:
                        result = SimpleMath.Sub(middlenumber, newnumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Mul(middlenumber, newnumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Div(middlenumber, newnumber);
                        break;

                }
            }
            middlenumber = 0;
            count = 0;
            txtboxCalculator.Text = result.ToString();
            listview.Items.Add(calcutxt.Text + newnumber +  " = " + result.ToString());
            calcutxt.Text = "";

             
           
        }

        private void buttondot_Click(object sender, RoutedEventArgs e)
        {
            if (!txtboxCalculator.Text.Contains("."))
            {
                txtboxCalculator.Text += ".";
            }
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            txtboxCalculator.Text = txtboxCalculator.Text.Substring(0, txtboxCalculator.Text.Length - 1);
            if (txtboxCalculator.Text.Equals(""))
            {
                txtboxCalculator.Text = "0";
            }
        }

        private void ButtonSqrt_Click(object sender, RoutedEventArgs e)
        {
            
            lastnumber = Convert.ToDouble(txtboxCalculator.Text);
            if (lastnumber > 0)
            {
                lastnumber = Math.Sqrt(lastnumber);
                txtboxCalculator.Text = lastnumber.ToString();
            }
        }

        private void Buttonsquare_Click(object sender, RoutedEventArgs e)
        {
            lastnumber = Convert.ToDouble(txtboxCalculator.Text);
            lastnumber = Math.Pow(lastnumber, 2);
            txtboxCalculator.Text = lastnumber.ToString();
        }

        private void ButtonInverse_Click(object sender, RoutedEventArgs e)
        {
            lastnumber = Convert.ToDouble(txtboxCalculator.Text);
            lastnumber = 1 / lastnumber;
            txtboxCalculator.Text = lastnumber.ToString();
        }
    }
}
