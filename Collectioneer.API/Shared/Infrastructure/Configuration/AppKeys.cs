namespace Collectioneer.API.Shared.Infrastructure.Configuration
{
	public class AppKeys
	{
		private readonly ILogger<AppKeys> _logger ;
		public JwtConfig Jwt { get; set; }
		public ContentSafety ContentSafety { get; set; }
		public BlobStorage BlobStorage { get; set; }
		public Persistence Persistence { get; set; }
		public ExternalCommunication ExternalCommunication { get; set; }

		public AppKeys(IConfiguration configuration, ILogger<AppKeys> logger)
		{
			_logger = logger;

			Jwt = new JwtConfig
			{
				Key = configuration["JWT_KEY"] ?? throw new ArgumentNullException("JWT key is missing."),
				Issuer = configuration["JWT_ISSUER"] ?? throw new ArgumentNullException("JWT issuer was not defined."),
				Audience = configuration["JWT_AUDIENCE"] ?? throw new ArgumentNullException("JWT audience was not defined.")
			};

			ContentSafety = new ContentSafety
			{
				Key = configuration["CONTENT_SAFETY_KEY"] ?? throw new ArgumentNullException("Content safety key is missing."),
				Endpoint = configuration["CONTENT_SAFETY_ENDPOINT"] ?? throw new ArgumentNullException("Content safety endpoint was not defined."),
				ClientServiceKey = configuration["CONTENT_SAFETY_CLIENT_SERVICE_KEY"] ?? throw new ArgumentNullException("Content safety client service key was not defined.")
			};

			BlobStorage = new BlobStorage
			{
				URL = configuration["STORAGE_URL"] ?? throw new ArgumentNullException("Storage URL is missing."),
				ConnectionString = configuration["STORAGE_ACCOUNT_CONNECTION_STRING"] ?? throw new ArgumentNullException("Storage account connection string was not defined.")
			};

			Persistence = new Persistence
			{
				ConnectionString = configuration["MYSQL_CONNECTION_STRING"] ?? throw new ArgumentNullException("MySQL connection string was not defined.")
			};

			ExternalCommunication = new ExternalCommunication
			{
				ConnectionString = configuration["COMMUNICATION_SERVICES_CONNECTION_STRING"] ?? throw new ArgumentNullException("Communication services connection string was not defined.")
			};
		}

		public void CheckKeys()
		{
			if (!Jwt.Validate())
			{
				_logger.LogError("JWT configuration is invalid. Please check your configuration.");
			}
			if (!ContentSafety.Validate())
			{
				_logger.LogError("Content safety configuration is invalid. Please check your configuration.");
			}
			if (!BlobStorage.Validate())
			{
				_logger.LogError("Blob storage configuration is invalid. Please check your configuration.");
			}
			if (!Persistence.Validate())
			{
				_logger.LogError("Persistence configuration is invalid. Please check your configuration.");
			}
			if (!ExternalCommunication.Validate())
			{
				_logger.LogError("External communication configuration is invalid. Please check your configuration.");
			}

			_logger.LogInformation("All keys are present. Continuing with app startup.");
		}
	}

	public struct JwtConfig
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }

		public readonly bool Validate()
		{
			return !string.IsNullOrEmpty(Key) && !string.IsNullOrEmpty(Issuer) && !string.IsNullOrEmpty(Audience);
		}
	}
	public struct ContentSafety
	{
		public string Key { get; set; }
		public string Endpoint { get; set; }
		public string ClientServiceKey { get; set; }

		public readonly bool Validate()
		{
			return !string.IsNullOrEmpty(Key) && !string.IsNullOrEmpty(Endpoint) && !string.IsNullOrEmpty(ClientServiceKey);
		}
	}
	public struct BlobStorage
	{
		public string URL { get; set; }
		public string ConnectionString { get; set; }

		public readonly bool Validate()
		{
			return !string.IsNullOrEmpty(URL) && !string.IsNullOrEmpty(ConnectionString);
		}
	}
	public struct Persistence
	{
		public string ConnectionString { get; set; }

		public readonly bool Validate()
		{
			return !string.IsNullOrEmpty(ConnectionString);
		}
	}
	public struct ExternalCommunication
	{
		public string ConnectionString { get; set; }

		public readonly bool Validate()
		{
			return !string.IsNullOrEmpty(ConnectionString);
		}
	}
}