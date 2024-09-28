using System.Text.RegularExpressions;

namespace Collectioneer.API.Shared.Infrastructure.Configuration.Extensions
{
	public static class StringExtensions
	{
		public static string ToSnakeCase(this string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return input;
			}

			var startUnderscores = Regex.Match(input, @"^_+");
			var snakeCase = Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
			snakeCase = Regex.Replace(snakeCase, @"\s+", "_");
			return startUnderscores + snakeCase;
		}
	}
}