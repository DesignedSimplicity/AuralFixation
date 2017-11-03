using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AuralFixation.Api.Media;
using AuralFixation.Api.Model;
using AuralFixation.Api.Player;
using AuralFixation.Api.Reader;

namespace AuralFixation.Api
{
	public class Engine
	{
		private List<IReader> _readers;
		private IPlayer _player;

		public void Init()
		{
			_player = new WinAmp();

			_readers = new List<IReader>();
			_readers.Add(new AlbumGenreReader());

			ListCarts();
		}

		public List<Cart> ListCarts()
		{
			return _readers.Select(x => x.Cart).ToList();
		}

		public IReader LoadCart(Cart cart)
		{
			return _readers.FirstOrDefault(x => x.Cart == cart);
		}

		public IPlayer GetPlayer()
		{
			// start up player if needed
			if (!_player.Initialized) _player.Start();

			// get instance of player
			return _player;
		}
	}
}
