using ACME.CargoExpress.API.IAM.Application.Internal.OutboundServices;
using Google.Apis.Auth;

namespace ACME.CargoExpress.API.IAM.Infrastructure.Authentication.Google.Services;

public class GoogleTokenVerifierService(
    IConfiguration configuration,
    ILogger<GoogleTokenVerifierService> logger) : IGoogleTokenVerifierService
{
    private readonly string _webClientId = configuration["Google:WebClientId"]
                                          ?? throw new InvalidOperationException("Google WebClientId is not configured.");

    public async Task<string> VerifyAndGetEmailAsync(string idToken)
    {
        if (string.IsNullOrWhiteSpace(idToken))
            throw new ArgumentException("Google idToken is required.");

        logger.LogInformation("Validating Google idToken with length {Length}", idToken.Length);

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = [_webClientId]
        });

        if (!payload.EmailVerified)
            throw new UnauthorizedAccessException("Google account email is not verified.");

        if (string.IsNullOrWhiteSpace(payload.Email))
            throw new UnauthorizedAccessException("Google token does not contain an email.");

        logger.LogInformation("Google token validated for subject {Subject}", payload.Subject);

        return payload.Email.Trim().ToLowerInvariant();
    }
}
