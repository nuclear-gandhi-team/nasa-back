using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.BLL.Services.Abstract
{
    public abstract class BaseService
    {
        private protected readonly DbContext _context;
        private protected readonly IMapper _mapper;

        public BaseService(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
