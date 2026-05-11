using ACME.CargoExpress.API.IAM.Application.Internal.OutboundServices;
using ACME.CargoExpress.API.IAM.Domain.Model.Commands;
using ACME.CargoExpress.API.IAM.Domain.Repositories;
using ACME.CargoExpress.API.IAM.Domain.Services;
using ACME.CargoExpress.API.Shared.Domain.Repositories;

namespace ACME.CargoExpress.API.IAM.Application.Internal.CommandServices;

/**
 * <summary>
 *     The user command service
 * </summary>
 * <remarks>
 *     This class is used to handle user commands
 * </remarks>
 */
public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork)
    : IUserCommandService
{
    /**
     * <summary>
     *     Handle sign in command
     * </summary>
     * <param name="command">The sign in command</param>
     * <returns>The authenticated user and the JWT token</returns>
     */
    public async Task<(Domain.Model.Aggregates.User user, string token)> Handle(SignInCommand command)
    {
        var normalizedUsername = command.Username.Trim().ToLowerInvariant();
        var user = await userRepository.FindByUsernameAsync(normalizedUsername);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid username or password");

        var token = tokenService.GenerateToken(user);

        return (user, token);
    }

    public async Task<(Domain.Model.Aggregates.User user, string token)> HandleGoogleSignIn(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Google email is required");

        var normalizedEmail = email.Trim().ToLowerInvariant();
        var user = await userRepository.FindByUsernameAsync(normalizedEmail);

        if (user == null)
        {
            var randomPassword = Guid.NewGuid().ToString("N");
            var hashedPassword = hashingService.HashPassword(randomPassword);
            user = new Domain.Model.Aggregates.User(normalizedEmail, hashedPassword);
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }

        var token = tokenService.GenerateToken(user);
        return (user, token);
    }

    /**
     * <summary>
     *     Handle sign up command
     * </summary>
     * <param name="command">The sign up command</param>
     * <returns>A confirmation message on successful creation.</returns>
     */
    public async Task Handle(SignUpCommand command)
    {
        var normalizedUsername = command.Username.Trim().ToLowerInvariant();

        if (userRepository.ExistsByUsername(normalizedUsername))
            throw new Exception($"Username {normalizedUsername} is already taken");

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new Domain.Model.Aggregates.User(normalizedUsername, hashedPassword);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating user: {e.Message}");
        }
    }
}
