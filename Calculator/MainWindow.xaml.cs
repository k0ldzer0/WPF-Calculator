using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_calc
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool op_after;  // operator가 눌린 바로 다음에 true
        private bool m_inCalc;  // 계산중
        private double l_value;
        private double r_value;
        private char m_Op;
        private double memNum;
        private bool ms_after;  // memory save 등 바로 다음에 true

        public MainWindow()
        {
            InitializeComponent();

            txtResult.Text = "0";
            txtResult.TextAlignment = TextAlignment.Right;
            txtResult.VerticalContentAlignment = VerticalAlignment.Bottom;

            txtResult.FontSize = 19;

        }

        // 숫자 버튼 처리
        private void btnNum_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            String num = btn.Name.Remove(0, 3); // 맨 앞에서 3글자 삭제

            if (op_after == true || ms_after == true)
            {
                txtResult.Text = num;
                op_after = false;
                ms_after = false;
            }
            else if (txtResult.Text.IndexOf('.') != -1)  // 소수점이 있다면, 0. 을 처리하기 위함
            {
                txtResult.Text += num;
            }
            else if (double.Parse(txtResult.Text) == 0)
            {
                txtResult.Text = num;
            }
            else
                txtResult.Text += num;
        }

        private void btnBS_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = txtResult.Text.Remove(txtResult.Text.Length - 1);
            if (txtResult.Text.Length == 0)
                txtResult.Text = "0";
        }

        private void btnCE_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = "0";
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = "0";
            expDisp.Text = "";
            l_value = 0;
            r_value = 0;
            op_after = false;
            m_inCalc = false;
        }

        private void btnPlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (expDisp.Text != "")
                expDisp.Text = "negate(" + expDisp.Text + ")";

            txtResult.Text = (-double.Parse(txtResult.Text)).ToString();
        }

        private void btnSqrt_Click(object sender, RoutedEventArgs e)
        {
            if (expDisp.Text != "")
                expDisp.Text = "sqrt(" + expDisp.Text + ")";
            else
                expDisp.Text = "sqrt(" + txtResult.Text + ")";

            txtResult.Text = Math.Sqrt(double.Parse(txtResult.Text)).ToString();
            op_after = true;

        }

        private void btnDivide_Click(object sender, RoutedEventArgs e)
        {
            l_value = Convert.ToDouble(txtResult.Text);
            m_inCalc = true;
            op_after = true;
            m_Op = '/';
            expDisp.Text = l_value.ToString() + " /";
        }

        private void btnTimes_Click(object sender, RoutedEventArgs e)
        {
            l_value = Convert.ToDouble(txtResult.Text);
            m_inCalc = true;
            op_after = true;
            m_Op = '*';
            expDisp.Text = l_value.ToString() + " *";
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            l_value = Convert.ToDouble(txtResult.Text);
            m_inCalc = true;
            op_after = true;
            m_Op = '-';
            expDisp.Text = l_value.ToString() + " -";
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            l_value = Convert.ToDouble(txtResult.Text);
            m_inCalc = true;
            op_after = true;
            m_Op = '+';
            expDisp.Text = l_value.ToString() + " +";
        }

        private void btnEqual_Click(object sender, RoutedEventArgs e)
        {
            if (m_inCalc == true)
            {
                r_value = double.Parse(txtResult.Text);
                switch (m_Op)
                {
                    case '+': txtResult.Text = (l_value + r_value).ToString(); break;
                    case '-': txtResult.Text = (l_value - r_value).ToString(); break;
                    case '*': txtResult.Text = (l_value * r_value).ToString(); break;
                    case '/': txtResult.Text = (l_value / r_value).ToString(); break;
                }
                expDisp.Text = "";
                op_after = true;
            }

        }

        private void btnReciprocal_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = (1.0 / double.Parse(txtResult.Text)).ToString();
            op_after = true;
        }

        private void btnDot_Click(object sender, RoutedEventArgs e)
        {
            if (op_after == true || double.Parse(txtResult.Text) == 0)
            {
                txtResult.Text = "0.";
                op_after = false;
            }
            else if (double.Parse(txtResult.Text) == (int)(double.Parse(txtResult.Text))) // 정수라면
            {
                txtResult.Text += ".";
            }
        }

        private void btnMS_Click(object sender, RoutedEventArgs e)
        {
            memDisp.Text = "M";
            memNum = double.Parse(txtResult.Text);
            ms_after = true;
        }

        private void btnMC_Click(object sender, RoutedEventArgs e)
        {
            memDisp.Text = "";
            memNum = 0;
        }

        private void btnMR_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = memNum.ToString();
            ms_after = true;
        }

        private void btnMPlus_Click(object sender, RoutedEventArgs e)
        {
            memNum += double.Parse(txtResult.Text);
        }

        private void btnMMinus_Click(object sender, RoutedEventArgs e)
        {
            memNum -= double.Parse(txtResult.Text);
        }

        private void btnPercent_Click(object sender, RoutedEventArgs e)
        {
            double p_value = l_value * (double.Parse(txtResult.Text) * 0.01);
            expDisp.Text += " " + p_value.ToString();
            txtResult.Text = p_value.ToString();
        }
    }
}