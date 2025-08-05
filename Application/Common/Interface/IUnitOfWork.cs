using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interface;
public interface IUnitOfWork
{
    IRepository<T> Repository<T>() where T : class;
    Task<int> SaveAsync();
    int Save();
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
