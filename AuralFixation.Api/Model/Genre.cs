using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuralFixation.Api.Model
{
	public class Genre
	{
		public Genre(string name) { Name = name; }
		public string Name { get; private set; }
		public string Key { get { return Name.ToLowerInvariant(); } }
	}
}
