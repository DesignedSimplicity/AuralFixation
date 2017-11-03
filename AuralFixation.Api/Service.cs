using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AuralFixation.Api.Model;

namespace AuralFixation.Api
{
	public class Service
	{
		private Engine _engine;

		public Service()
		{
			_engine = new Engine();
			_engine.Init();
		}

		/// <summary>
		/// Loads and lists all media cartridges available from loaders
		/// </summary>
		/// <returns></returns>
		public List<Cart> ListCarts()
		{
			return _engine.ListCarts();
		}

		/// <summary>
		/// Plays a random set of media from a given cartridge with the option to limit it by a group
		/// </summary>
		/// <returns></returns>
		public PlayCartResponse PlayCart(PlayCartRequest request)
		{
			var response = new PlayCartResponse();
			try
			{
				var reader = _engine.LoadCart(request.FromCart);
				var files = reader.Pick(request.InGroup);

				var player = _engine.GetPlayer();
				if (request.ResetPlaylist || player.Status == Player.PlayerStatus.Stopped) player.Clear();

				player.Play(files);
				response.Playing = player.Status == Player.PlayerStatus.Playing;
			}
			catch (Exception ex)
			{
				response.Error = ex.Message;
			}

			return response;
		}
	}

	public class PlayCartRequest
	{
		public Cart FromCart { get; set; }

		// optional group based filter for each reader
		public string InGroup { get; set; } = "";

		// if playing then add files to end of current queue
		// if stopped then clear queue and play files
		// if reset playlist then stop and clear queue and play files
		public bool ResetPlaylist { get; set; } = false;
	}

	public class PlayCartResponse
	{
		public bool Playing { get; set; }
		public string Error { get; set; }
	}
}
