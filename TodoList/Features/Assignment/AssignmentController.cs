﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.Features.Assignment
{
    [Route("api/[controller]")]
    public class AssignmentController : Controller
    {
        private IMediator mediator;

        public AssignmentController(IMediator mediator)
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
    }
}