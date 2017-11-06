using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuralFixation.Api.Model
{
	public enum MediaFileTypes { mp3, wmv, flac }

	public class Media
	{
		//================================================================================
		private Media(FileInfo file) { File = file; }

		public FileInfo File { get; private set; }

		public string Key { get { return Uri.ToLowerInvariant(); } }

		public string Uri { get { return File.FullName; } }

		public string FileName { get { return File.Name; } }


		//================================================================================
		private static List<string> _extensions = new List<string>();

		static Media()
		{
			lock (_extensions)
			{
				foreach (var t in Enum.GetValues(typeof(MediaFileTypes)))
				{
					_extensions.Add("." + t.ToString().ToLowerInvariant());
				}
			}
		}

		public static Media FromUri(string uri)
		{
			var file = new FileInfo(uri);
			if (!file.Exists) return null;
			if (!_extensions.Contains(file.Extension.ToLowerInvariant())) return null;
			return new Media(file);
		}

		public static Media FromFile(FileInfo file)
		{
			if (!file.Exists) return null;
			if (!_extensions.Contains(file.Extension.ToLowerInvariant())) return null;
			return new Media(file);
		}

		public static List<Media> FromPath(string uri)
		{
			var files = new List<Media>();
			var dir = new DirectoryInfo(uri);
			if (dir.Exists)
			{
				foreach(var file in dir.GetFiles().OrderBy(x => x.Name.ToLowerInvariant()))
				{
					var f = FromFile(file);
					if (f != null) files.Add(f);
				}
			}
			return files;
		}
	}
}
