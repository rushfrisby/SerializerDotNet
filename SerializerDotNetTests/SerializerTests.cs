using SerializerDotNet;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SerializerDotNetTests
{
	public class SerializerTests
	{
		[Fact]
		public void GetDefaultMappings_HasNoNullMaps()
		{
			var maps = Serializer.GetDefaultMappings();

			Assert.NotNull(maps);
			Assert.True(maps.Any());
			foreach (var item in maps)
			{
				Assert.NotNull(item.Value);
			}
		}

		[Fact]
		public void GetSerializerFor_JsonDefault_IsJsonSerializer()
		{
			var serializer = Serializer.GetSerializerFor(ContentType.Json);

			Assert.NotNull(serializer);
			Assert.IsType<JsonSerializer>(serializer);
		}

		[Fact]
		public void SetMappings_GetSerializerFor_NotNullAndCorrectType()
		{
			const string contentType = "application/x-notimplemente";
			var maps = new Dictionary<string, ISerializer>()
			{
				{ contentType, new NotImplementedSerializer() }
			};

			Serializer.SetMappings(maps);
			var serializer = Serializer.GetSerializerFor(contentType);

			Assert.NotNull(serializer);
			Assert.IsType<NotImplementedSerializer>(serializer);
		}
	}
}
