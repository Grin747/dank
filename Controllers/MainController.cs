using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DankContainers.Data;
using DankContainers.Models;
using DankContainers.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DankContainers.Controllers
{
    [ApiController, Route("/")]
    [Produces("application/json"), Consumes("application/json")]
    public class MainController : Controller
    {
        private readonly HelloContext _context;

        public MainController(HelloContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ICollection<Model>> ListData(CancellationToken cancellationToken) =>
            await _context.Models.ToListAsync(cancellationToken);

        [HttpGet("{id:guid}")]
        public async Task<Model> GetData([FromRoute] Guid id,
                                         CancellationToken cancellationToken) =>
            await _context.Models
                          .AsNoTracking()
                          .FirstOrDefaultAsync(x => x.Id == id,
                               cancellationToken);

        [HttpPost]
        public async Task AddData([FromBody] AddDataRequest request,
                                  CancellationToken cancellationToken)
        {
            await _context.Models.AddAsync(new Model { Data = request.Data }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        [HttpPost("migrate")]
        public async Task Migrate(CancellationToken cancellationToken)
        {
            await _context.Database.MigrateAsync(cancellationToken);
        }
    }
}