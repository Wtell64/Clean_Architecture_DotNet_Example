using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sm.Crm.Application.Features.UserAddresses.Commands.CreateUserAddresses;
using Sm.Crm.Application.Features.UserAddresses.Commands.DeleteUserAddresses;
using Sm.Crm.Application.Features.UserAddresses.Commands.UpdateUserAddresses;
using Sm.Crm.Application.Features.UserAddresses.Queries.GetAllUserAddresses;
using Sm.Crm.Application.Features.UserAddresses.Queries.GetByIdUserAddresses;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.WebApi.Controllers;

public class UserAddresesController : FeatureController
{
    private readonly IMediator _mediator;
    readonly IUserRepository _userRepository;

    public UserAddresesController(IMediator mediator, IUserRepository userRepository)
    {
        _mediator = mediator;
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPaginationUserAddressesQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediator.Send(new GetByIdUserAddressesQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateUserAddressesCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateUserAdressesCommand command)
    {
        if (id == command.Id)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        return Ok(false);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediator.Send(new DeleteuserAdressesCommand(id));
        return Ok(response);
    }
    [HttpGet("users")]
    public async Task<IActionResult> Users()
    {
        var users =await _userRepository.GetAll().ToListAsync();
        return Ok(users);
    }
}

