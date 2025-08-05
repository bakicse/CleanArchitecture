using Application.Common.Model;
using Application.Master;
using Application.Master.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/SubCategory")]
[ApiController]
public class SubCategoryController (ISubCategoryService subCategoryService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ResponseModel>> Upsert([FromBody] SubCategoryVm subCategoryVm)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ResponseModel.FailureResponse("Invalid data provided."));
        }
        var response = await subCategoryService.UpsertAsync(subCategoryVm);
        if (response.Success)
        {
            return Ok(response);
        }
        return StatusCode(500, response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseModel>> Delete(int id)
    {
        var response = await subCategoryService.DeleteAsync(id);
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
        var response = await subCategoryService.GetAsync();
        if (response.Success)
        {
            return Ok(response);
        }
        return StatusCode(500, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseModel>> GetById(int id)
    {
        var response = await subCategoryService.GetByIdAsync(id);
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
