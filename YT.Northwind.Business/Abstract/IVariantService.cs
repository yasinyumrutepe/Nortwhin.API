using Northwind.Core.Models.Response.Variant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Abstract
{
    public interface IVariantService
    {
        Task<List<VariantResponseModel>> GetAllVariant();
    }
}
