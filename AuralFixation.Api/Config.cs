using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuralFixation.Api
{
	public class Config
	{
		public const string WinAmpPath = @"C:\Program Files (x86)\Winamp\winamp.exe";
		public const string GenreIconPath = @"D:\Drives\Dropbox\Media\Music\Albums\_ICONS";
		public static readonly string[] AlbumGenrePaths = { @"D:\Drives\Dropbox\Media\Music\Albums", @"D:\Drives\Dropbox\Media\Music\Albums\__MP3", @"D:\Drives\Dropbox\Media\Music\Albums\__FLAC" };
	}
}
