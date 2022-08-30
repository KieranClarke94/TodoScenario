using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Scenario
{
	/// <summary>
	/// Describes the possible states in which a <see cref="Todo"/> can be in.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TodoState
	{
		/// <summary>
		/// The <see cref="Todo"/> is in a pending state
		/// </summary>
		Pending = 0,

		/// <summary>
		/// The <see cref="Todo"/> is in a complete state
		/// </summary>
		Complete = 1,
	}
}
