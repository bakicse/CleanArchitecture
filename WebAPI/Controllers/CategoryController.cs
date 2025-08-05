using Application.Common.Model;
using Application.Master;
using Application.Master.Dto;
using Application.Master.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/Category")]
[ApiController]
public class CategoryController (ICategoryService categoryService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ResponseModel>> Upsert([FromBody] CategoryDto categoryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ResponseModel.FailureResponse("Invalid data provided."));
        }
        var response = await categoryService.UpsertAsync(categoryDto);
        if (response.Success)
        {
            return Ok(response);
        }
        return StatusCode(500, response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseModel>> Delete(int id)
    {
        var response = await categoryService.DeleteAsync(id);
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
        var response = await categoryService.GetAsync();
        if (response.Success)
        {
            return Ok(response);
        }
        return StatusCode(500, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseModel>> GetById(int id)
    {
        var response = await categoryService.GetByIdAsync(id);
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
