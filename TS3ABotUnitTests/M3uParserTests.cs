using NUnit.Framework;
using System.IO;
using System.Text;
using TS3AudioBot.ResourceFactories.AudioTags;

namespace TS3ABotUnitTests
{
	[TestFixture]
	public class M3uParserTests
	{
		[Test]
		public void SimpleListTest()
		{
			var result = M3uReader.TryGetData(new MemoryStream(Encoding.UTF8.GetBytes(
@"#EXTINF:197,Delain - Delain - We Are The Others
/opt/music/bad/Delain.mp3
#EXTINF:314,MONO - MONO - The Hand That Holds the Truth
/opt/music/bad/MONO.mp3
#EXTINF:223,Deathstars - Deathstars - Opium
/opt/music/bad/Opium.mp3"
				))).Result;

			Assert.Equals(3, result.Count);

			Assert.Equals("Delain - Delain - We Are The Others", result[0].Title);
			Assert.Equals("MONO - MONO - The Hand That Holds the Truth", result[1].Title);
			Assert.Equals("Deathstars - Deathstars - Opium", result[2].Title);

			Assert.Equals("/opt/music/bad/Delain.mp3", result[0].TrackUrl);
			Assert.Equals("/opt/music/bad/MONO.mp3", result[1].TrackUrl);
			Assert.Equals("/opt/music/bad/Opium.mp3", result[2].TrackUrl);
		}

		[Test]
		public void ListWithM3uHeaderTest()
		{
			var result = M3uReader.TryGetData(new MemoryStream(Encoding.UTF8.GetBytes(
@"#EXTM3U
#EXTINF:1337,Never gonna give you up
C:\Windows\System32\firewall32.cpl
#EXTINF:1337,Never gonna let you down
C:\Windows\System32\firewall64.cpl"
				))).Result;

			Assert.Equals(2, result.Count);

			Assert.Equals("Never gonna give you up", result[0].Title);
			Assert.Equals("Never gonna let you down", result[1].Title);

			Assert.Equals(@"C:\Windows\System32\firewall32.cpl", result[0].TrackUrl);
			Assert.Equals(@"C:\Windows\System32\firewall64.cpl", result[1].TrackUrl);
		}

		[Test]
		public void ListWithoutMetaTagsTest()
		{
			var result = M3uReader.TryGetData(new MemoryStream(Encoding.UTF8.GetBytes(
@"
C:\PepeHands.jpg
./do/I/look/like/I/know/what/a/Jaypeg/is
"
				))).Result;

			Assert.Equals(2, result.Count);

			Assert.Equals(null, result[0].Title);
			Assert.Equals(null, result[1].Title);

			Assert.Equals(@"C:\PepeHands.jpg", result[0].TrackUrl);
			Assert.Equals("./do/I/look/like/I/know/what/a/Jaypeg/is", result[1].TrackUrl);
		}
	}
}
