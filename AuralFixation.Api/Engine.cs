using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AuralFixation.Api.Model;
using AuralFixation.Api.Player;
using AuralFixation.Api.Reader;

namespace AuralFixation.Api
{
	public class Engine
	{
		private IPlayer _player;
		private List<IReader> _readers;

		public IPlayer Player
		{
			get
			{
				// init player
				if (_player == null) _player = new WinAmpPlayer();

				// start up player if needed
				if (!_player.Initialized) _player.Start();

				// wait 100 ms
				Thread.Sleep(100);

				// get instance of player
				return _player;
			}
		}

		public List<IReader> Readers
		{
			get
			{
				// init readers if necessary
				if (_readers == null)
				{
					_readers = new List<IReader>();
					_readers.Add(new AlbumGenreReader());
				}

				// list all readers
				return _readers;
			}
		}

		public IReader GetReader(string key)
		{
			return _readers.FirstOrDefault(x => x.Key == key);
		}
	}
}
