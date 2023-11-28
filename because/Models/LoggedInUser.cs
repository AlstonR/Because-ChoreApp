using System.Text.Json;

namespace because.Models
{
    public readonly record struct LoggedInHouse(int HouseHoldId)
    {
        public string ToJson() =>
            JsonSerializer.Serialize(this);

        public static LoggedInHouse LoadFromJson(string? json) =>
            string.IsNullOrWhiteSpace(json)
            ? default
            : JsonSerializer.Deserialize<LoggedInHouse>(json);
    }
	public readonly record struct LoggedInChild(int HouseHoldId, int UserId)
	{
		public string ToJson() =>
			JsonSerializer.Serialize(this);

		public static LoggedInChild LoadFromJson(string? json) =>
			string.IsNullOrWhiteSpace(json)
			? default
			: JsonSerializer.Deserialize<LoggedInChild>(json);
	}
}

