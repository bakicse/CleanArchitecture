using Application.Common.Error;
using Application.Common.Interface;
using Application.Common.Model;
using Application.Master.Dto;
using Application.Master.ViewModel;
using AutoMapper;
using Domain.Master;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Application.Master;
internal class SubCategoryService : ISubCategoryService
{
    #region Properties
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SubCategoryService> logger;
    private readonly IMapper mapper;
    private readonly IErrorMessageLog errorMessageLog;
    #endregion

    #region ctor
    public SubCategoryService(IUnitOfWork unitOfWork, ILogger<SubCategoryService> logger, IMapper mapper, IErrorMessageLog errorMessageLog)
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
            var subCategories = await _unitOfWork.Repository<SubCategory>()
                .TableNoTracking
                .OrderBy(t => t.Id)
                .ToListAsync();
            var categoryVms = mapper.Map<List<SubCategoryVm>>(subCategories);
            return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, categoryVms);
        }
        catch (Exception ex)
        {
            Log(nameof(GetAsync), ex);
            logger?.LogError(ex.ToString());
            return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
        }
    }

    public async Task<ResponseModel> GetByIdAsync(int Id)
    {
        try
        {
            //var subCategory = await _unitOfWork.Repository<SubCategory>().Get(Id);
            var subCategory = await _unitOfWork.Repository<SubCategory>().GetWithInclude(Id, s => s.Category);
            if (subCategory != null)
            {
                var categoryVms = mapper.Map<SubCategoryVm>(subCategory);
                return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, categoryVms);
            }
            else
                return ResponseModel.FailureResponse("Not Found");
        }
        catch (Exception ex)
        {
            Log(nameof(GetByIdAsync), ex);
            logger?.LogError(ex.ToString());
            return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
        }
    }

    public async Task<ResponseModel> UpsertAsync(SubCategoryDto subCategoryDto)
    {
        var subCategory = mapper.Map<SubCategory>(subCategoryDto);
        try
        {
            _unitOfWork.BeginTransaction();
            if (subCategory.Id > 0)
                _unitOfWork.Repository<SubCategory>().Update(subCategory);
            else
                await _unitOfWork.Repository<SubCategory>().AddAsync(subCategory);

            await _unitOfWork.SaveAsync();
            _unitOfWork.CommitTransaction();
            return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, mapper.Map<SubCategoryVm>(subCategory));
        }
        catch (Exception ex)
        {
            _unitOfWork.RollbackTransaction();
            Log(nameof(UpsertAsync), ex);
            logger?.LogError(ex.ToString());
            return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
        }
    }

    public async Task<ResponseModel> DeleteAsync(int id)
    {
        try
        {
            var subCategories = await _unitOfWork.Repository<SubCategory>().Get(id);
            if (subCategories != null)
            {
                subCategories.IsDeleted = true;
                await _unitOfWork.SaveAsync();
                return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, null);
            }
            else
                return ResponseModel.FailureResponse("Not Found");
        }
        catch (Exception ex)
        {
            Log(nameof(DeleteAsync), ex);
            logger?.LogError(ex.ToString());
            return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
        }
    }
    #endregion
    #region Error
    private void Log(string method, Exception ex)
    {
        errorMessageLog.LogError("Application", "AppSettingService", method, ex);
    }

    #endregion
}
