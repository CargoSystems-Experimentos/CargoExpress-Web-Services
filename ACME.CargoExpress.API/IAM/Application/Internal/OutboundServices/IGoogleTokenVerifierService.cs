namespace ACME.CargoExpress.API.IAM.Application.Internal.OutboundServices;

public interface IGoogleTokenVerifierService
{
    Task<string> VerifyAndGetEmailAsync(string idToken);
}
