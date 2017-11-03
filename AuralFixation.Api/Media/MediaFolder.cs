using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuralFixation.Api.Media
{
    public class MediaFolder
    {
		public string Uri { get; set; }

		public List<MediaFile> Files { get; set; }
    }
}
