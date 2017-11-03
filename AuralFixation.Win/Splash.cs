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

namespace AuralFixation.Win
{
    public partial class Splash : Form
    {
		private Service _service = new Service();

        public Splash()
        {
            InitializeComponent();
        }

		private void button1_Click(object sender, EventArgs e)
		{
			var cart = _service.ListCarts().First();
			_service.PlayCart(new PlayCartRequest()
			{
				FromCart = cart
			});
		}
	}
}
