using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using AuralFixation.Api.Model;

namespace AuralFixation.Api
{
	public class Service
	{
		private Engine _engine;

		public Service()
		{
			_engine = new Engine();
		}

		public List<Cover> LoadIcons(string uri)
		{
			return Cover.FromPath(uri);
		}

		/// <summary>
		/// Loads and lists all media cartridges available from loaders
		/// </summary>
		/// <returns></returns>
		public List<IReader> ListReaders()
		{
			return _engine.Readers;
		}

		public void Reset()
		{
			_engine.Player.Stop();
			_engine.Player.Clear();
		}

		/// <summary>
		/// Plays a random set of media from a given cartridge with the option to limit it by a group
		/// </summary>
		/// <returns></returns>
		public PlayResponse Play(PlayRequest request)
		{
			var response = new PlayResponse();

			var reader = _engine.GetReader(request.FromCart);
			var files = reader.Pick(request.InCategory);

			var player = _engine.Player;
			if (request.ResetPlaylist || player.Status == PlayerStatus.Stopped)
			{
				player.Stop();
				player.Clear();
				Thread.Sleep(100);
			}

			player.Play(files);
			response.Playing = player.Status == PlayerStatus.Playing;

			return response;
		}
	}

	public class PlayRequest
	{
		public string FromCart { get; set; }

		// optional category based filter for each reader
		public string InCategory { get; set; } = "";

		// if playing then add files to end of current queue
		// if stopped then clear queue and play files
		// if reset playlist then stop and clear queue and play files
		public bool ResetPlaylist { get; set; } = false;
	}

	public class PlayResponse
	{
		public bool Playing { get; set; }
		public string Error { get; set; }
	}
}
