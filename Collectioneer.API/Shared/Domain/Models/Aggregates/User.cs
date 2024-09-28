using System.Text.RegularExpressions;
using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Shared.Domain.Exceptions;
using Collectioneer.API.Shared.Domain.Models.Entities;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Shared.Domain.Models.Aggregates;

public class User
{
	// Entity identifier
	public int Id { get; private set; }
	// Entity properties
	public string Username { get; private set; } = string.Empty;
	public string Email { get; private set; } = string.Empty;
	public string Name { get; private set; } = string.Empty;
	public string Password { get; private set; } = string.Empty;

	// Navigation properties
	public ICollection<Role> Roles { get; set; } = [];
	public ICollection<Collectible> Collectibles { get; set; } = [];
	public ICollection<Auction> Auctions { get; set; } = [];
	public ICollection<Bid> Bids { get; set; } = [];
	public ICollection<Sale> Sales { get; set; } = [];
	public ICollection<Sale> Purchases { get; set; } = [];
	public ICollection<Exchange> Exchanges { get; set; } = [];
	public ICollection<Reaction> Reactions { get; set; } = [];
	public ICollection<Post> Posts { get; set; } = [];
	public ICollection<Comment> Comments { get; set; } = [];
	public ICollection<Review> Reviews { get; set; } = [];
	public ICollection<MediaElement> MediaElements { get; set; } = [];
	public ICollection<MediaElement> ProfileMediaElements { get; set; } = [];
	public User(
		string username,
		string email,
		string name,
		string password
	)
	{
		SetUsername(username);
		SetEmail(email);
		SetName(name);
		SetPassword(password);
	}

	public void SetPassword(string hashedPassword)
	{
		if (hashedPassword.Length != 64)
		{
			throw new UserInputException("Hashed password is not of expected length. This is likely an internal service error. Contact app support");
		}

		Password = hashedPassword;
	}

	private void SetName(string name)
	{
		if (name.Length < 3 || name.Length > 50)
		{
			throw new UserInputException("Name must be between 3 and 50 characters long.");
		}

		Name = name;
	}

	private void SetEmail(string email)
	{
		if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
		{
			throw new UserInputException("Email is not in a valid format.");
		}

		Email = email;
	}

	private void SetUsername(string username)
	{
		if (username.Length < 3 || username.Length > 50)
		{
			throw new UserInputException("Username must be between 3 and 50 characters long.");
		}

		if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_.]+$"))
		{
			throw new UserInputException("Username can only contain alphanumeric characters, dots, and underscores.");
		}
		Username = username;
	}

	public bool CheckPassword(string password)
	{
		if (password.Length != 64)
		{
			throw new UserInputException("Argument doesn't have expected length. Verify if properly hashed before using this method.");
		}

		return Password == password;
	}
}