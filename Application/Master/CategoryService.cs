using Application.Common.Error;
using Application.Common.Interface;
using Application.Common.Model;
using AutoMapper;
using Domain.Master;
using Microsoft.Extensions.Logging;
using Application.Master.Dto;
using Microsoft.EntityFrameworkCore;

namespace Application.Master;
internal class CategoryService : ICategoryService
{
    #region Properties
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryService> logger;
    private readonly IMapper mapper;
    private readonly IErrorMessageLog errorMessageLog;
    #endregion

    #region ctor
    public CategoryService(IUnitOfWork unitOfWork, ILogger<CategoryService> logger, IMapper mapper, IErrorMessageLog errorMessageLog)
    {
        _unitOfWork = unitOfWork;
        this.logger = logger;
        this.mapper = mapper;
        this.errorMessageLog = errorMessageLog;
    }
    #endregion
    #region Methods
    public async Task<ResponseModel> GetAsync()
    {
        try
        {
            var categories = await _unitOfWork.Repository<Category>()
                .TableNoTracking
                .OrderBy(t => t.Id)
                .ToListAsync();
            var categoryVms = mapper.Map<List<CategoryVm>>(categories);
            return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, categoryVms);
        }
        catch (Exception ex)
        {
            Log(nameof(GetAsync), ex.Message);
            logger?.LogError(ex.ToString());
            return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
        }
    }

    public async Task<ResponseModel> GetByIdAsync(int Id)
    {
        try
        {
            var categories = await _unitOfWork.Repository<Category>().Get(Id);
            if (categories != null)
            {
                var categoryVms = mapper.Map<CategoryVm>(categories);
                return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, categoryVms);
            }
            else
                return ResponseModel.FailureResponse("Not Found");
        }
        catch (Exception ex)
        {
            Log(nameof(GetByIdAsync), ex.Message);
            logger?.LogError(ex.ToString());
            return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
        }
    }

    public async Task<ResponseModel> UpsertAsync(CategoryVm categoryVm)
    {
        var category = mapper.Map<Category>(categoryVm);
        try
        {
            if (category.Id > 0)
                _unitOfWork.Repository<Category>().Update(category);
            else
                await _unitOfWork.Repository<Category>().AddAsync(category);

            await _unitOfWork.SaveAsync();
            return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, mapper.Map<CategoryVm>(category));
        }
        catch (Exception ex)
        {
            Log(nameof(UpsertAsync), ex.Message);
            logger?.LogError(ex.ToString());
            return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
        }
    }

    public async Task<ResponseModel> DeleteAsync(int id)
    {
        try
        {
            var categories = await _unitOfWork.Repository<Category>().Get(id);
            if (categories != null)
            {
                categories.IsDeleted = true;
                await _unitOfWork.SaveAsync();
                return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, null);
            }
            else
                return ResponseModel.FailureResponse("Not Found");
        }
        catch (Exception ex)
        {
            Log(nameof(DeleteAsync), ex.Message);
            logger?.LogError(ex.ToString());
            return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
        }
    }
    #endregion
    #region Error
    private void Log(string method, string msg)
    {
        errorMessageLog.LogError("Application", "CategoryService", method, msg);
    }

    #endregion
}
