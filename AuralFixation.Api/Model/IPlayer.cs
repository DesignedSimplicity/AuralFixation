using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AuralFixation.Api.Model
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

		void Play(Media file);
		void Play(IEnumerable<Media> files);

		void Add(Media file);
		void Add(IEnumerable<Media> files);
	}
}