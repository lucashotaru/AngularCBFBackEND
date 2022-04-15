using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AngularCBFBackEND.conteudo.DataService
{
    [Route("[controller]")]
    public class DataServiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DataServiceController(ApplicationDbContext context)
        {
            this._context = context;
        }
    }
}