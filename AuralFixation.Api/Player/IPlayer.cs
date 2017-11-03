using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AuralFixation.Api;
using AuralFixation.Api.Media;

namespace AuralFixation.Api.Player
{
	public enum PlayerStatus { Playing = 1, Stopped = -1, Paused = 0 };

	public interface IPlayer
	{
		bool Initialized { get; }

		PlayerStatus Status { get; }

		bool Start();
		void Play();
		void Pause();
		void Stop();
		void Clear();

		void Play(MediaFile file);
		void Play(IEnumerable<MediaFile> files);

		void Add(MediaFile file);
		void Add(IEnumerable<MediaFile> files);
	}
}