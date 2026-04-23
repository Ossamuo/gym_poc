using Gym.Application.Contexts.SharedContext.Results;
using Gym.Domain.Contexts.AccountContext.Entities;
using Gym.Domain.Contexts.AccountContext.UseCases.Create;
using Gym.Domain.Contexts.AccountContext.UseCases.Create.Contracts;
using Gym.Domain.Contexts.AccountContext.ValueObjects;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;

namespace Gym.Application.Contexts.AccountContext.UseCases.Create;

public class Handler : IHandler<Request, Result<Response?>>
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handler(IRepository repo, IService service)
    {
        _repository = repo;
        _service = service;
    }

    public async Task<Result<Response?>> HandlerAsync(Request request, CancellationToken cancellationToken = default)
    {
        //01 - TODO Validate the request.

        //02 - Create the Value Objects
        var email = Email.Create(request.Email);
        var password = new Password(request.Password);
        var member = Member.Create(request.Name, email, password);


        //03 - Check if the Member exists in Database
        var exists = await _repository.AnyAsync(request.Name, email.Address, cancellationToken);
        if (exists)
            return new Result<Response?>(null, 500, "E-mail already exists");
        //04 - Insert Member in DataBase
        await _repository.SaveAsync(member, cancellationToken);

        //05 - Send Email for activation
        // Console.WriteLine($"Test of the mediator {nameof(Handler)} - {request.Email}");
        //await _service.SendEmailVerificationAsync(member, cancellationToken);
         var response = new Response(member.Id, member.Name, member.Email.Address, member.Email.Verification.Code);
        return new Result<Response?>(response,201, "Member created");
    }
}