using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using service_billing.services.MessageProducer;
using service_billing.services.TransactionService;
using TransactionModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace service_billing.Controllers
{

    [Route("api/[controller]")]
    public class TransactionController : Controller
    {

        //TODO: Fix connection with SQL Server
        // private readonly ITransactionService _transactionService;
        private readonly IPublishEndpoint _publishEndpoint;

        public TransactionController(ITransactionService transactionService, IMessageProducer messageProducer, IPublishEndpoint publishEndpoint)
        {
            //TODO: Fix connection with SQL Server
            // _transactionService = transactionService;
            
            //_messageProducer = messageProducer;
            _publishEndpoint = publishEndpoint;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [Authorize("write:transaction")]
        [EnableCors("MyAllowSpecificOrigins")]
        public async Task<ActionResult> Post([FromBody]TransactionModel.Transaction transaction)
        {
            // TEMP LEFT OUT 
            // var result = await _transactionService.handleTransaction(transaction);

            await _publishEndpoint.Publish<TransactionModel.Transaction>(transaction);

            Console.WriteLine(transaction);

            return Ok(transaction);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

