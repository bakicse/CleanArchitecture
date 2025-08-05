using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Error;
public interface IErrorMessageLog
{
    bool LogError(string layerName, string className, string methodName, string msg);

}
