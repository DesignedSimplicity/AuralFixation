using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuralFixation.Api.Model
{
	public enum CoverFileTypes { jpg, png, gif }

	public class Cover
	{
		//================================================================================
		private Cover(FileInfo file) { File = file; }

		public FileInfo File { get; private set; }

		public string Key { get { return Uri.ToLowerInvariant(); } }

		public string Uri { get { return File.FullName; } }

		public string FileName { get { return File.Name; } }

		public string Name { get { return Path.GetFileNameWithoutExtension(FileName); } }


		//================================================================================
		private static List<string> _extensions = new List<string>();

		static Cover()
		{
			lock (_extensions)
			{
				foreach (var t in Enum.GetValues(typeof(CoverFileTypes)))
				{
					_extensions.Add("." + t.ToString().ToLowerInvariant());
				}
			}
		}

		public static Cover FromUri(string uri)
		{
			var file = new FileInfo(uri);
			if (!file.Exists) return null;
			if (!_extensions.Contains(file.Extension.ToLowerInvariant())) return null;
			return new Cover(file);
		}

		public static Cover FromFile(FileInfo file)
		{
			if (!file.Exists) return null;
			if (!_extensions.Contains(file.Extension.ToLowerInvariant())) return null;
			return new Cover(file);
		}

		public static List<Cover> FromPath(string uri)
		{
			var files = new List<Cover>();
			var dir = new DirectoryInfo(uri);
			if (dir.Exists)
			{
				foreach (var file in dir.GetFiles())
				{
					var f = FromFile(file);
					if (f != null) files.Add(f);
				}
			}
			return files;
		}
	}
}
