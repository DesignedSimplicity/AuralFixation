using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AuralFixation.Api.Media;
using AuralFixation.Api.Model;

namespace AuralFixation.Api.Reader
{
	public interface IReader
	{
		Cart Cart { get; }

		List<MediaFile> Pick(string group);
	}

	public class Picker
	{
		private static Random _random = new Random(Convert.ToInt32(DateTime.UtcNow.Ticks % Int32.MaxValue));

		public static int Pick(int max)
		{
			// simple random
			if (max > 1) return _random.Next(max);

			// save random
			return Convert.ToInt32(Math.Round(_random.NextDouble(), 0));
		}
	}

}
