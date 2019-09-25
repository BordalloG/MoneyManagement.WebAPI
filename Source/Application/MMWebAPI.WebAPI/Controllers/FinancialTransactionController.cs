using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MMWebAPI.Application.InOut.FinancialTransaction;
using MMWebAPI.Application.Interfaces;

namespace MMWebAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinancialTransactionController : ControllerBase
    {
        private readonly IFinancialTransactionApplicationService _appService;

        public FinancialTransactionController(IFinancialTransactionApplicationService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Inserts a Financial Transactions into a group.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> InsertFinancialTransactionAsync([FromBody]FinancialTransactionRequest request)
        {
            var financialResponse = await _appService.AddAsync(request);
            return Created("", financialResponse);

        }

        /// <summary>
        /// Remove a Financial Transactions.
        /// </summary>
        /// <param name="transactionsId"></param>
        /// <returns></returns>
        [Route("{transactionsId}")]
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int transactionsId)
        {
            await _appService.DeleteAsync(transactionsId);
            return Ok("The Transaction has been deleted successfully");
        }

        /// <summary>
        /// Retrieve all transactions for a specific group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>

        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [Route("Group/{groupId}/GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAllByGroup(int groupId)
        {
            var financialTransaction = await _appService.GetAllByGroup(groupId);

            return Ok(financialTransaction);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var financialTransaction = await _appService.GetAllAsync();

            return Ok(financialTransaction);
        }


        /// <summary>
        /// Retrieves a Transaction by Id
        /// </summary>
        /// <returns></returns>

        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [Route("{entityId}")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute] int entityId)
        {
            var trasnaction = await _appService.GetByIdAsync(entityId);
            return Ok(trasnaction);
        }

        /// <summary>
        /// Update a transaction
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [Route("{transactionId}")]
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int transactionId, [FromBody] FinancialTransactionRequest transaction)
        {
            var resopnse = await _appService.UpdateAsync(transaction, transactionId);
            return Ok(resopnse);
        }
    }
}