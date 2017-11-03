using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AuralFixation.Api.Media;

namespace AuralFixation.Api.Model
{
	public class Track
    {
		public MediaFile File { get; set; }

		public Artist Artist { get; set; }
		public Album Album { get; set; }

		public string Title { get; }
	}
}
