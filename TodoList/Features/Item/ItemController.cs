﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.Features.Item
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private IMediator mediator;

        public ItemController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Create.Command command)
        {
            await mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        public async Task<IList<List.Result>> List()
        {
            var result = await mediator.Send(new List.Query());

            return result;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Read.Result> Read(Read.Query query)
        {
            var result = await mediator.Send(query);

            return result;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Delete.Command command)
        {
            await mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Update.Command command)
        {
            command.Id = id;
            await mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [Route("finished")]
        public async Task<IList<FinishedItens.Result>> FinishedItens()
        {
            var result = await mediator.Send(new FinishedItens.Query());

            return result;
        }
    }
}