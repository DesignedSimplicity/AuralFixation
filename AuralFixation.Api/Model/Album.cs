using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuralFixation.Api.Model
{
    public class Album
    {
		public Artist Artist { get; set; }
		public Genre Genre { get; set; }

		public List<Track> Tracks { get; set; }
	}
}
