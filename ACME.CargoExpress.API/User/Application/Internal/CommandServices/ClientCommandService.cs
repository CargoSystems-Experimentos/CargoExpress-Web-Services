using ACME.CargoExpress.API.IAM.Domain.Repositories;
using ACME.CargoExpress.API.Shared.Domain.Repositories;
using ACME.CargoExpress.API.User.Domain.Model.Aggregates;
using ACME.CargoExpress.API.User.Domain.Model.Commands;
using ACME.CargoExpress.API.User.Domain.Model.Entities;
using ACME.CargoExpress.API.User.Domain.Repositories;
using ACME.CargoExpress.API.User.Domain.Services;

namespace ACME.CargoExpress.API.User.Application.Internal.CommandServices;

public class ClientCommandService(IClientRepository clientRepository, IUserRepository userRepository, IUnitOfWork unitOfWork) : IClientCommandService
{
    public async Task<Client?> Handle(CreateClientCommand command)
    {
        var user = await userRepository.FindByIdAsync(command.UserId);
        if (user == null)
        {
            throw new ArgumentException("UserId not found.");
        }
        // Create the client
        var client = new Client(command, user);
        try
        {
            await clientRepository.AddAsync(client);
            await unitOfWork.CompleteAsync();
            return client;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the client: {e.Message}");
            return null;
        }
    }
    
    public async Task<Client?> Handle(UpdateClientCommand command)
    {
        var client = await clientRepository.FindByIdAsync(command.ClientId);
        if (client == null)
        {
            throw new ArgumentException("Client not found.");
        }
        // Update the client
        client.Update(command);
        try
        {
            await unitOfWork.CompleteAsync();
            return client;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the client: {e.Message}");
            return null;
        }
    }
}