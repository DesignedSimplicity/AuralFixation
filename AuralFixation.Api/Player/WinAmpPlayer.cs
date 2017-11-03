using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AuralFixation.Api;
using AuralFixation.Api.Model;

namespace AuralFixation.Api.Player
{
    public class WinAmpPlayer : IPlayer
    {
		//================================================================================
		#region Internal Helpers

		private int _winAmpWindowHandle = 0;

		private enum WinAmpMessages
		{
			GetVersion = 0,
			ClearPlayList = 101,
			GetStatus = 104,
			GetTrackPosition = 105,
			GetTrackLength = 105,
			SeekToPosition = 106,
			SetVolume = 122,
			SetBallance = 123,
			GetEQData = 127,
			SetEQData = 128
		}

		private enum WinAmpCommands
		{
			PrevTrack = 40044,
			NextTrack = 40048,
			Play = 40045,
			Pause = 40046,
			Stop = 40047,
			FadeOutStop = 40147,
			StopAfterTrack = 40157,
			FastForward = 40148,
			FastRewind = 40144,
			PlayListHome = 40154,
			DialogOpenFile = 40029,
			DialogOpenURL = 40155,
			DialogFileInfo = 40188,
			TimeDisplayElapsed = 40037,
			TimeDisplayRemaining = 40038,
			TogglePreferences = 40012,
			DialogVisualOptions = 40190,
			DialogVisualPluginOptions = 40191,
			StartVisualPlugin = 40192,
			ToggleAbout = 40041,
			ToggleAutoScroll = 40189,
			ToggleAlwaysOnTop = 40019,
			ToggleWindowShade = 40042,
			TogglePlayListWindowShade = 40266,
			ToggleDoublSize = 40165,
			ToggleEQ = 40036,
			TogglePlayList = 40040,
			ToggleMainWindow = 40258,
			ToggleMiniBrowser = 40298,
			ToggleEasyMode = 40186,
			VolumeUp = 40058,
			VolumeDown = 40059,
			ToggleRepear = 40022,
			ToggleShuffle = 40023,
			DialogJumpToTime = 40193,
			DialogJumpToFile = 40194,
			DialogSkinSelector = 40219,
			DialogConfigureVisualPlugin = 40221,
			ReloadSkin = 40291,
			Close = 40001
		}

		private bool Get()
		{
			_winAmpWindowHandle = Handle.FindWindow("Winamp v1.x", null);
			return (_winAmpWindowHandle > 0);
		}

		private bool Run(string args = null)
		{
			try
			{
				Process p = new Process();
				ProcessStartInfo psi = new ProcessStartInfo();
				psi.FileName = Config.WinAmpPath;
				psi.Arguments = args;
				p.StartInfo = psi;
				p.Start();
				return true;
			}
			catch { return false; }
		}

		private bool Wait()
		{
			// try to find
			DateTime until = DateTime.UtcNow.AddSeconds(5);
			bool wait = !Get();
			while (wait)
			{
				Thread.Sleep(100);
				if (Get()) return true;
				wait = DateTime.UtcNow < until;
			}

			// not found
			return false;
		}

		private int SendMessage(WinAmpMessages message)
		{
			return Handle.SendMessage(_winAmpWindowHandle, Handle.WM_USER, 0, (int)message);
		}

		private int SendCommand(WinAmpCommands command)
		{
			return Handle.SendMessage(_winAmpWindowHandle, Handle.WM_COMMAND, (int)command, 0);
		}

		#endregion
		//================================================================================

		//--------------------------------------------------------------------------------
		/// <summary>
		/// True if WinAmp is running and found
		/// </summary>
		public bool Initialized { get { return _winAmpWindowHandle > 0; } }

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Gets the current playing status
		/// </summary>
		public PlayerStatus Status
		{
			get
			{
				var mode = SendMessage(WinAmpMessages.GetStatus);
				if (mode == 1) return PlayerStatus.Playing;
				if (mode == 3) return PlayerStatus.Paused;
				return PlayerStatus.Stopped;
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Finds or opens an instance of WinAmp
		/// </summary>
		public bool Start()
		{
			// find existing window
			if (Get()) return true;

			// launch from path
			Run();

			// wait to find
			return Wait();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Starts playing the current playlist
		/// </summary>
		public void Play()
		{
			SendCommand(WinAmpCommands.Play);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Pauses the current playlist
		/// </summary>
		public void Pause()
		{
			SendCommand(WinAmpCommands.Pause);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Stops playing current playlist
		/// </summary>
		public void Stop()
		{
			SendCommand(WinAmpCommands.Stop);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Clears the current playlist
		/// </summary>
		public void Clear()
		{
			SendMessage(WinAmpMessages.ClearPlayList);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Adds file to current playlist and starts playing if stopped
		/// </summary>
		/// <param name="file"></param>
		public void Play(Media file)
		{
			Add(file);
			if (Status != PlayerStatus.Playing)
			{
				Thread.Sleep(1000);
				Play();
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Adds files to current playlist and starts playing if stopped
		/// </summary>
		public void Play(IEnumerable<Media> files)
		{
			Add(files);
			if (Status != PlayerStatus.Playing)
			{
				Thread.Sleep(1000);
				Play();
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Adds file to current playlist
		/// </summary>
		public void Add(Media file)
		{
			Run($"/ADD \"{file.Uri}\"");
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Adds files to current playlist
		/// </summary>
		public void Add(IEnumerable<Media> files)
		{
			foreach(var file in files)
			{
				Add(file);
			}
		}
	}
}