using AutoMapper;
using Nasa.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.BLL.Services.Abstract
{
    public abstract class BaseService
    {
        private protected readonly NasaContext _context;
        private protected readonly IMapper _mapper;

        public BaseService(NasaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
