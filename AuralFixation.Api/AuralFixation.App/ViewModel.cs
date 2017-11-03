using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AuralFixation.Api.Model;
using System.Windows.Media.Imaging;

namespace AuralFixation.App
{
	public class PlayerTree
	{
		public List<ReaderNode> Readers { get; set; } = new List<ReaderNode>();
	}


	public class ReaderNode
	{
		public ReaderNode(IReader reader) { Reader = reader; }

		public IReader Reader { get; private set; } 

		public List<CategoryNode> Categories { get; set; } = new List<CategoryNode>();
	}

	public class CategoryNode
	{
		public CategoryNode(string category, BitmapImage icon)
		{
			Name = category;
			Icon = icon;
		}

		public string Name { get; private set; }

		public BitmapImage Icon { get; private set; }
	}
}
