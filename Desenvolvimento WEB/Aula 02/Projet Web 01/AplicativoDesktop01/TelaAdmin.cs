using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AplicativoDesktop01
{
    public partial class TelaAdmin : Form
    {
        public TelaAdmin() => InitializeComponent();

        private void TelaAdmin_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // TelaAdmin
            // 
            ClientSize = new Size(329, 447);
            Name = "TelaAdmin";
            Load += TelaAdmin_Load_1;
            ResumeLayout(false);

        }

        private void TelaAdmin_Load_1(object sender, EventArgs e)
        {

        }
    }
}
