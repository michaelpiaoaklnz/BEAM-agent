namespace BeamApi.Services;

public enum EmailDomainStatus
{
    Verified,
    Disposable,
    Unverified
}

public class EmailDomainPolicy
{
    private static readonly HashSet<string> DisposableDomains = new(StringComparer.OrdinalIgnoreCase)
    {
        "mailinator.com",
        "tempmail.com",
        "10minutemail.com",
        "guerrillamail.com"
    };

    private static readonly HashSet<string> VerifiedCompanyDomains = new(StringComparer.OrdinalIgnoreCase)
    {
        "company.com",
        "beam.com",
        "enterprise.co.nz"
    };

    public EmailDomainStatus Evaluate(string email)
    {
        var domain = ExtractDomain(email);
        if (domain is null)
        {
            return EmailDomainStatus.Unverified;
        }

        if (DisposableDomains.Contains(domain))
        {
            return EmailDomainStatus.Disposable;
        }

        if (VerifiedCompanyDomains.Contains(domain))
        {
            return EmailDomainStatus.Verified;
        }

        return EmailDomainStatus.Unverified;
    }

    private static string? ExtractDomain(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return null;
        }

        var atIndex = email.LastIndexOf('@');
        if (atIndex < 0 || atIndex == email.Length - 1)
        {
            return null;
        }

        return email[(atIndex + 1)..].Trim().ToLowerInvariant();
    }
}
