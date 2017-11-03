using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuralFixation.Api.Media
{
	public class MediaFile
    {
		public MediaFile(string uri)
		{
			var f = new FileInfo(uri);
			Uri = f.FullName;
			FileName = f.Name;
		}

		public string Uri { get; set; }

		public string FileName { get; set; }

		public MediaFolder Folder { get; set; }
	}
}
