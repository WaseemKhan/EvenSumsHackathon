using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvenSumsApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtArray.Text = "4,5,3,7,2";
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (txtArray.Text != string.Empty)
            {
                int[] sumsArray = Array.ConvertAll(txtArray.Text.Split(','), int.Parse);

                Solution s = new Solution();

                txtSolution.Text = s.solution(sumsArray);
            }
        }
    }
}
