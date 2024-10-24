using AutoMapper;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Response.Variant;
using Northwind.DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Concrete
{
    public class VariantService : IVariantService
    {
        private readonly IVariantRepository _variantRepository;
        private readonly IMapper _mapper;

        public VariantService(IVariantRepository variantRepository, IMapper mapper)
        {
            _variantRepository = variantRepository;
            _mapper = mapper;
        }

        public async Task<List<VariantResponseModel>> GetAllVariant()
        {
            var variants =  await _variantRepository.GetAllAsync();
            return _mapper.Map<List<VariantResponseModel>>(variants);



        }
    }
}
