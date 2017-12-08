using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuralFixation.Api.Player
{
	public class PlaylistBuilder
	{
		private string _playlistUri = "";

		public PlaylistBuilder()
		{
			var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			_playlistUri = Path.Combine(directory, "playlist.m3u");
		}

		public string WritePlaylist(string directoryUri)
		{
			// delete existing playlist
			if (File.Exists(_playlistUri)) File.Delete(_playlistUri);

			// write file with UTF8
			using (FileStream file = File.OpenWrite(_playlistUri))
			{
				using (StreamWriter writer = new StreamWriter(file, Encoding.UTF8))
				{
					writer.Write(directoryUri);
				}
			}

			// return name
			return _playlistUri;
		}
	}
}
