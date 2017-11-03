using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AuralFixation.Api;
using AuralFixation.Api.Media;
using AuralFixation.Api.Player;

namespace AuralFixation.Win
{
    public partial class Splash : Form
    {
		private WinAmp _winamp = new WinAmp();

        public Splash()
        {
            InitializeComponent();
        }
	}
}
