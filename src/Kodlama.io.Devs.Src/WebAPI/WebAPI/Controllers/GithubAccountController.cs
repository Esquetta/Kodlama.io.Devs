﻿using Application.Features.GithubAccounts.Commands.CreateGithubAccount;
using Application.Features.GithubAccounts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class GithubAccountController : BaseController
    {

        private readonly IHttpContextAccessor httpContextAccessor;
        public GithubAccountController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        
        [HttpPost]
        [Authorize, AllowAnonymous]
        public async Task<IActionResult> Add([FromBody] CreateGithubAccountCommand createGithubAccountCommand)
        {
            CreatedGithibAccountDto createdGithibAccountDto = await Mediator.Send(createGithubAccountCommand);

            return Ok(createdGithibAccountDto);
        }
    }
}
