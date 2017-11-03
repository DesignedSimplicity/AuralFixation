using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuralFixation.Api.Media
{
	public class MediaList
	{
		public MediaList(List<MediaFile> files) { Files = files; }

		public List<MediaFile> Files { get; set; }
	}
}
