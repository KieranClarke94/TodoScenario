using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Scenario.Tests.Integration
{
	/// <summary>
	/// Provides common HTTP extension methods for testing.
	/// </summary>
	public static class HttpExtensions
	{
		/// <summary>
		/// Returns the content from the <see cref="HttpResponseMessage"/> as an object <typeparamref name="T"/>.
		/// </summary>
		public static async Task<T> GetContentAsync<T>(this HttpResponseMessage message)
		{
			var contents = await message.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(contents);
		}

		/// <summary>
		/// Creates a new <see cref="HttpContent"/> object by serializing the object as JSON.
		/// </summary>
		public static HttpContent ToHttpContent(this object model, JsonSerializerSettings settings = null)
		{
			string serialized;

			if (settings != null)
			{
				serialized = JsonConvert.SerializeObject(model, settings);
			}
			else
			{
				serialized = JsonConvert.SerializeObject(model);
			}

			return new StringContent(serialized, Encoding.UTF8, "application/json");
		}
	}
}
