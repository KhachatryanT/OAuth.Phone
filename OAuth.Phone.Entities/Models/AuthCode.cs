namespace OAuth.Phone.Entities.Models;

public sealed class AuthCode
{
	public AuthCode(string clientId, string codeChallenge, string codeChallengeMethod, string redirectUri, DateTime expiry)
	{
		ClientId = clientId;
		CodeChallenge = codeChallenge;
		CodeChallengeMethod = codeChallengeMethod;
		RedirectUri = redirectUri;
		Expiry = expiry;
	}

	public string ClientId { get;  }
	public string CodeChallenge { get;  }
	public string CodeChallengeMethod { get;  }
	public string RedirectUri { get;  }
	public DateTime Expiry { get;  }
}