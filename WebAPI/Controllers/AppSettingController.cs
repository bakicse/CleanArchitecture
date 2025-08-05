using Application.Common.Model;
using Application.Master;
using Application.Master.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/AppSetting")]
[ApiController]
public class AppSettingController(IAppSettingService appSettingService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ResponseModel>> Upsert([FromBody] AppSettingVm appSettingVm)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ResponseModel.FailureResponse("Invalid data provided."));
        }
        var response = await appSettingService.UpsertAsync(appSettingVm);
        if (response.Success)
        {
            return Ok(response);
        }
        return StatusCode(500, response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseModel>> Delete(int id)
    {
        var response = await appSettingService.DeleteAsync(id);
        if (response.Success)
        {
            return Ok(response);
        }
        else if (response.Message == "Not Found")
        {
            return NotFound(response);
        }
        return StatusCode(500, response);
    }

    [HttpGet]
    public async Task<ActionResult<ResponseModel>> GetAll()
    {
        var response = await appSettingService.GetAsync();
        if (response.Success)
        {
            return Ok(response);
        }
        return StatusCode(500, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseModel>> GetById(int id)
    {
        var response = await appSettingService.GetByIdAsync(id);
        if (response.Success)
        {
            return Ok(response);
        }
        else if (response.Message == "Not Found")
        {
            return NotFound(response);
        }
        return StatusCode(500, response);
    }
}
