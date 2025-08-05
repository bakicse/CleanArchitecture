using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Model;

namespace Application.Common.Interface;
public interface IRepositoryService<T> where T : class
{
    Task<ResponseModel> GetAsync();
    Task<ResponseModel> GetByIdAsync(int Id);
    Task<ResponseModel> UpsertAsync(T modelToUpdate);
    Task<ResponseModel> DeleteAsync(int Id);
}
