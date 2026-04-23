using Gym.Domain.Contexts.AccountContext.Entities;
using Gym.Domain.Contexts.AccountContext.UseCases.Edit;
using Gym.Domain.Contexts.AccountContext.UseCases.Edit.Contracts;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Application.Contexts.AccountContext.UseCases.Edit
{
    internal class Handler : IHandler<Request, Result<Response?>>
    {

        private readonly IRepository _repository;

        public Handler(IRepository repo)
        {
            _repository = repo;
        }

        public async Task<Result<Response?>> HandlerAsync(Request request, CancellationToken cancellationToken = default)
        {

            //01 - Get user by id
            Member? member = await _repository.GetByIdAsync(request.Id, cancellationToken);

            //02 - Validate if user exists 
            if (member == null)
            {
                return new Result<Response?>(null, 404, "User Not found.");
            }
            ;


            //03 - Validate if the user is the same as the one in the token

            if (member.Id != request.Id
                || member.Email != request.Email)
                return new Result<Response?>(null, 400, "Unable to update Member.");
            //04 - Update user

            await _repository.UpdateAsync(request, cancellationToken);


            return new Result<Response?>(new Response(
                 request.Name,
                 request.Email,
                 request.Image
             ), 200, "Member updated successfully.");

        }
    }
}
