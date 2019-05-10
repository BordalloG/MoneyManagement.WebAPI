using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MMWebAPI.Application.InOut.FinancialTransactionGroup;
using MMWebAPI.Application.Interfaces;

namespace MMWebAPI.WebAPI.Controllers
{
    /// <summary>
    /// Controller For Groups Of Transacations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialTransactionGroupController : ControllerBase
    {
        private readonly IFinancialTransactionGroupApplicationService _appService;
        /// <summary>
        /// Constructor with gets the application service. 
        /// </summary>
        /// <param name="appService"></param>
        public FinancialTransactionGroupController(IFinancialTransactionGroupApplicationService appService)
        {
            _appService = appService;
        }
        /// <summary>
        /// Inserts a new Group of Financial Transactions.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddAsync([FromBody]FinancialTransactionGroupRequest group)
        {
            var groupResponse = await _appService.AddAsync(group);
            return Created("", groupResponse);

        }
        /// <summary>
        /// Remove a Group of Financial Transactions.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [Route("{groupId}")]
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int groupId)
        {
            await _appService.DeleteAsync(groupId);
            return Ok("The Group and Its Transactions have been deleted successfully");
        }

       /// <summary>
       /// Retrieves a List of The Groups and its Transactions
       /// </summary>
       /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var financialTransactionGroup = await _appService.GetAllAsync();

            return Ok(financialTransactionGroup);
        }

        /// <summary>
        /// Retrieves a List of The Groups without its Transactions
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [Route("GetAllSimple")]
        [HttpGet]
        public async Task<IActionResult> GetAllSimple()
        {
            var financialTransactionGroup = await _appService.GetAllSimpleAsync();

            return Ok(financialTransactionGroup);
        }

        /// <summary>
        /// Retrieves a Group of Transaction by Id
        /// </summary>
        /// <returns></returns>

        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [Route("{entityId}")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute] int entityId)
        {
            var trasnactionGroup = await _appService.GetByIdAsync(entityId);
            return Ok(trasnactionGroup);
        }

        /// <summary>
        /// Update a transaction Group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="transactionGroup"></param>
        /// <returns></returns>
        [Route("{groupId}")]
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int groupId, [FromBody] FinancialTransactionGroupRequest transactionGroup)
        {
            var resopnse = await _appService.UpdateAsync(transactionGroup, groupId);
            return Ok(resopnse);
        }
    }
}