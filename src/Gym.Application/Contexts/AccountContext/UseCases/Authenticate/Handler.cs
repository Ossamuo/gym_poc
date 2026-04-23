using Gym.Application.Contexts.SharedContext.Results;
using Gym.Domain.Contexts.AccountContext.Entities;
using Gym.Domain.Contexts.AccountContext.UseCases.Authenticate;
using Gym.Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;

namespace Gym.Application.Contexts.AccountContext.UseCases.Authenticate;

public class Handler: IHandler<Request, Result<Response?>>
{
    private  readonly IRepository _repository;

    public Handler(IRepository repository)=> _repository = repository;
    
    public async Task<Result<Response?>> HandlerAsync(Request request, CancellationToken cancellationToken = default)
    {
        //01 - Validate the request

        Member? member;
        try
        {
            member = await _repository.GetMemberByEmailAsync(request.Email, cancellationToken);
            if (member is null)
                return new Result<Response?>(null, 400, "Email or Password invalid.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        if(!member.Password.Challenge(request.Password))
            return new Result<Response?>(null, 400, "Email or Password invalid.");

        try
        {
            if (!member.Email.Verification.IsActive)
                return new Result<Response?>(null, 400, "Email is not verified.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        var response = new Response
        {
            Id = member.Id.ToString(),
            Email =  member.Email,
            Name =  member.Name,
            Roles = member.Roles.Select(x=>x.Name).ToArray()
        };
        
        return new Result<Response?>(response,201);
    }
}