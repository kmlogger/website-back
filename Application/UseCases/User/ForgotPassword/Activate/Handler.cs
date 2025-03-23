using System;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Cold;
using Domain.Records;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.User.ForgotPassword.Activate;

public  class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IDbCommit _dbCommit;

    public Handler(IUserRepository userRepository, IDbCommit dbCommit)
    {
        _userRepository = userRepository;
        _dbCommit = dbCommit;
    }

    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        var userFromDb = await _userRepository.GetByEmail(request.email, cancellationToken);
        if (userFromDb == null || !userFromDb.TokenActivate.Equals(request.token))
            return new BaseResponse(404, "User not found or invalid token");

        userFromDb.UpdatePassword(new Password(request.newPassword));
        if (userFromDb.Notifications.Any())
            return new BaseResponse(400, "Request invalid", userFromDb.Notifications.ToList());

        _userRepository.Update(userFromDb);
        await _dbCommit.Commit(cancellationToken);

        return new BaseResponse(200, "Password changed successfully");
    }
}