// TS3AudioBot - An advanced Musicbot for Teamspeak 3
// Copyright (C) 2017  TS3AudioBot contributors
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the Open Software License v. 3.0
//
// You should have received a copy of the Open Software License along with this
// program. If not, see <https://opensource.org/licenses/OSL-3.0>.

using Newtonsoft.Json;
using PlaylistsNET.Content;
using PlaylistsNET.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TS3AudioBot.Playlists.Parser
{
	public class JspfContent : IPlaylistParser<XspfPlaylist>, IPlaylistWriter<XspfPlaylist>
	{
		public XspfPlaylist GetFromStream(Stream stream)
		{
			var serializer = new JsonSerializer();
			using var sr = new StreamReader(stream);
			using var jsonTextReader = new JsonTextReader(sr);
			return serializer.Deserialize<XspfPlaylist>(jsonTextReader) ??
			       throw new NullReferenceException("Data empty");
		}

		public XspfPlaylist GetFromStream(Stream stream, Encoding encoding)
		{
			var serializer = new JsonSerializer();
			using var sr = new StreamReader(stream, encoding);
			using var jsonTextReader = new JsonTextReader(sr);
			return serializer.Deserialize<XspfPlaylist>(jsonTextReader) ??
			       throw new NullReferenceException("Data empty");
		}

		public XspfPlaylist GetFromString(string playlistString)
		{
			throw new NotImplementedException();
		}

		public string ToText(XspfPlaylist playlist)
		{
			return JsonConvert.SerializeObject(playlist);
		}
	}

	public class XspfPlaylist : IBasePlaylist
	{
		[JsonProperty(PropertyName = "title")] public string? Title { get; set; }

		[JsonProperty(PropertyName = "creator")]
		public string? Creator { get; set; }

		[JsonProperty(PropertyName = "track")] public List<XspfPlaylistEntry>? PlaylistEntries { get; set; }

		public string? Path { get; set; }
		public string? FileName { get; set; }

		public XspfPlaylist()
		{
		}

		public List<string> GetTracksPaths() =>
			(PlaylistEntries?.Select(x => x.Location?.FirstOrDefault()).Where(x => x != null).Select(x => x!) ??
			 Enumerable.Empty<string>()).ToList();
	}

	public class XspfPlaylistEntry
	{
		public XspfPlaylistEntry() { }

		[JsonProperty(PropertyName = "title")] public string? Title { get; set; }

		[JsonProperty(PropertyName = "duration")]
		public long? Duration { get; set; } // MS : TODO timespan converter

		[JsonProperty(PropertyName = "meta")]
		[JsonConverter(typeof(JspfMetaConverter))]
		public List<XspfMeta>? Meta { get; set; }

		[JsonProperty(PropertyName = "location")]
		public List<string>? Location { get; set; }
	}

	public class XspfMeta
	{
		public string Key { get; set; }
		public string Value { get; set; }

		public XspfMeta(string key, string value)
		{
			Key = key;
			Value = value;
		}
	}

	internal class JspfMetaConverter : JsonConverter<XspfMeta>
	{
		public override XspfMeta ReadJson(JsonReader reader, Type objectType, XspfMeta? existingValue,
			bool hasExistingValue, JsonSerializer serializer)
		{
			var key = reader.ReadAsString();
			var value = reader.ReadAsString();
			if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
				throw new FormatException();
			return new XspfMeta(key, value);
		}

		public override void WriteJson(JsonWriter writer, XspfMeta? value, JsonSerializer serializer)
		{
			if (value is null) throw new ArgumentNullException(nameof(value));
			writer.WriteStartObject();
			writer.WritePropertyName(value.Key);
			writer.WriteValue(value.Value);
			writer.WriteEndObject();
		}
	}
}
