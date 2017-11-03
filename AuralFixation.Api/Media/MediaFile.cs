using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AuralFixation.Api.Actions;

namespace AuralFixation.Api.Media
{
	public class MediaFile
	{
		private static LoadMedia _loader = new LoadMedia();

		//================================================================================
		private FileInfo _file;

		internal MediaFile(string uri)
		{
			_file = new FileInfo(uri);
			//Folder = LoadMedia.FromFolder(Path.GetDirectoryName(uri));
			//if (!Folder.Files.Contains(this)) Folder.Files.Add(this);
		}

		//--------------------------------------------------------------------------------
		public MediaFolder Folder { get; private set; }

		public string Key { get { return Uri.ToLowerInvariant(); } }

		public string Uri { get { return _file.FullName; } }

		public string FileName { get { return _file.Name; } }
	}
}
