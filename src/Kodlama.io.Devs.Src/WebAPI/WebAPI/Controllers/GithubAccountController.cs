using Application.Features.GithubAccounts.Commands.CreateGithubAccount;
using Application.Features.GithubAccounts.Commands.DeleteGithubAccount;
using Application.Features.GithubAccounts.Commands.UpdateGithubAccount;
using Application.Features.GithubAccounts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class GithubAccountController : BaseController
    {
       

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGithubAccountCommand createGithubAccountCommand)
        {
            
            CreatedGithibAccountDto createdGithibAccountDto = await Mediator.Send(createGithubAccountCommand);

            return Ok(createdGithibAccountDto);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGithubAccountCommand updateGithubAccountCommand)
        {
            UpdatedGithubAccountDto  updatedGithubAccountDto = await Mediator.Send(updateGithubAccountCommand);

            return Ok(updatedGithubAccountDto);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteGithubAccountCommand deleteGithubAccountCommand)
        {
            DeletedGithubAccountDto deletedGithubAccountDto = await Mediator.Send(deleteGithubAccountCommand);

            return Ok(deletedGithubAccountDto);
        }

    }
}
