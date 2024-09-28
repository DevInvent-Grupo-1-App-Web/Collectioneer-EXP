using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Collectioneer.API.Shared.Infrastructure.Configuration.Extensions
{
	public static class ModelStateExtensions
	{
		public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
		{
			return dictionary.Where(m => m.Value != null)
							 .SelectMany(m => m.Value!.Errors)
							 .Select(m => m.ErrorMessage)
							 .ToList();
		}
	}
}