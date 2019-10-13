using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.DataProviders;

namespace WebApp.Controllers
{
    public class ServiceResult : object
    {
        public string error;
        public ServiceResult(string _error) { error = _error; }
    }
    public class BaseDataController <T, P> : ControllerBase 
        where P : BaseProvider<T>, new() 
        where T : BaseEntity
    {
        readonly P Provider;
        public BaseDataController()
        {
            Provider = new P();
        }

        // GET: api/T
        [HttpGet]
        public IActionResult Get()
        {
            var res = Provider.GetAll(out IEnumerable<T> companies);
            return Ok(res == null ? companies : (object) new ServiceResult(res.Message));
        }

        // GET: api/T/5
        [HttpGet("{_id}")]
        public IActionResult Get(long _id)
        {
            var res = Provider.Get(out T company, _id);
            if ((res == null) && (company == null))
                return NotFound();
            else
                return Ok(res == null ? company : (object) new ServiceResult(res.Message));
        }

        // POST: api/T
        [HttpPost]
        public IActionResult Post([FromBody] T _value)
        {
            var res = Provider.Add(out long id, _value);
            if ((res == null) && (id == 0))
                return BadRequest();
            else
                return Ok(new ServiceResult(res == null ?  "Добавлено успешно!" : res.Message));
        }

        // PUT: api/T/5
        [HttpPut]
        public IActionResult Put([FromBody] T _value)
        {
            var res = Provider.Set(_value);
            return Ok(new ServiceResult(res == null ? "Обновлено успешно!" : res.Message));
        }

        // DELETE: api/T/5
        [HttpDelete("{_id}")]
        public IActionResult Delete(long _id)
        {
            var res = Provider.Del(_id);
            return Ok(new ServiceResult(res == null ? "Удалено успешно!" : res.Message));
        }
    }
}