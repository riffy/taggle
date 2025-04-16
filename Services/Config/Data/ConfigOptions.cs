﻿namespace Taggle.Services.Config.Data;

public static class ConfigOptions
{
	/// <summary>
	/// Options to be set when reading / writing the config json.
	/// </summary>
	public static readonly JsonSerializerOptions JsonSerializerOptions = new()
	{
		PropertyNameCaseInsensitive = true,
		IncludeFields = true,
		NumberHandling = JsonNumberHandling.AllowReadingFromString,
		WriteIndented = true,
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
	};
}
