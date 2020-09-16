using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace API.Base
{
    public class BaseController<TEntity, TRepository> : ControllerBase
        where TEntity : class
        where TRepository : InterfaceRepo<TEntity>
    {
        private InterfaceRepo<TEntity> _repo;

        public BaseController(TRepository repository)
        {
            this._repo = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<TEntity>> GetAll() => await _repo.GetAll();

        [HttpGet("{Id}")]
        public async Task<ActionResult<TEntity>> GetById(int Id) => await _repo.GetById(Id);

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post (TEntity entity)
        {
            var data = await _repo.Create(entity);
            if(data > 0)
            {
                return Ok("Data saved!");
            }
            return BadRequest("Data failed to save");
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int Id)
        {
            var deleted = await _repo.Delete(Id);
            if(deleted.Equals(null))
            {
                return NotFound("Data is not found");
            }
            return deleted;
        }
    }
}
