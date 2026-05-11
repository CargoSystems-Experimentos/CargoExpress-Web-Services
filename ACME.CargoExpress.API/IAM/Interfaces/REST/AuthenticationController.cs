using System.Net.Mime;
using ACME.CargoExpress.API.IAM.Application.Internal.OutboundServices;
using ACME.CargoExpress.API.IAM.Domain.Services;
using ACME.CargoExpress.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using ACME.CargoExpress.API.IAM.Interfaces.REST.Resources;
using ACME.CargoExpress.API.IAM.Interfaces.REST.Transform;
using ACME.CargoExpress.API.Shared.Interfaces.ASP.Configuration.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ACME.CargoExpress.API.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthenticationController(
    IUserCommandService userCommandService,
    IGoogleTokenVerifierService googleTokenVerifierService,
    ILogger<AuthenticationController> logger) : ControllerBase
{
    /**
     * <summary>
     *     Sign in endpoint. It allows to authenticate a user
     * </summary>
     * <param name="signInResource">The sign in resource containing username and password.</param>
     * <returns>The authenticated user resource, including a JWT token</returns>
     */
    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource)
    {
        if (!signInResource.Username.IsValidEmail())
            return BadRequest(new { message = "Username must be a valid email" });

        if (string.IsNullOrWhiteSpace(signInResource.Password))
            return BadRequest(new { message = "Password is required" });

        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
        var authenticatedUser = await userCommandService.Handle(signInCommand);
        var resource =
            AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticatedUser.user,
                authenticatedUser.token);
        return Ok(resource);
    }

    /**
     * <summary>
     *     Sign up endpoint. It allows to create a new user
     * </summary>
     * <param name="signUpResource">The sign up resource containing username and password.</param>
     * <returns>A confirmation message on successful creation.</returns>
     */
    [HttpPost("sign-up")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource signUpResource)
    {
        if (!signUpResource.Username.IsValidEmail())
            return BadRequest(new { message = "Username must be a valid email" });

        if (!signUpResource.Password.IsValidPassword())
            return BadRequest(new
            {
                message = "Password must have at least 8 characters, one uppercase letter, one number, and one special character"
            });

        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource);
        await userCommandService.Handle(signUpCommand);
        return Ok(new { message = "User created successfully" });
    }

    [HttpPost("google-sign-in")]
    [AllowAnonymous]
    public async Task<IActionResult> GoogleSignIn([FromBody] GoogleSignInResource googleSignInResource)
    {
        if (string.IsNullOrWhiteSpace(googleSignInResource.IdToken))
            return BadRequest(new { message = "IdToken is required" });

        try
        {
            logger.LogInformation("Google sign-in request received");
            var email = await googleTokenVerifierService.VerifyAndGetEmailAsync(googleSignInResource.IdToken);
            logger.LogInformation("Google token validated for email {Email}", email);
            var authenticatedUser = await userCommandService.HandleGoogleSignIn(email);
            var resource = AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(
                authenticatedUser.user,
                authenticatedUser.token);

            logger.LogInformation("Google sign-in completed for userId {UserId}", authenticatedUser.user.Id);
            return Ok(resource);
        }
        catch (UnauthorizedAccessException e)
        {
            logger.LogWarning(e, "Google sign-in unauthorized");
            return Unauthorized(new { message = e.Message });
        }
        catch (ArgumentException e)
        {
            logger.LogWarning(e, "Google sign-in invalid request");
            return BadRequest(new { message = e.Message });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Google sign-in unexpected error");
            return StatusCode(500, new { message = "Unexpected error during Google sign-in" });
        }
    }
}
