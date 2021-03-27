﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Persistence.PageParams;
using SmartBase.BusinessLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionMasterController : ControllerBase
    {
        private readonly ILogger<TransactionMasterController> _logger;
        private ITransactionMasterService _transactionMasterService { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionMasterService"></param>
        public TransactionMasterController(ITransactionMasterService transactionMasterService, ILogger<TransactionMasterController> logger)
        {
            _transactionMasterService = transactionMasterService;
            _logger = logger;
        }

        /// <summary>
        /// Add new transaction type. TrxId+DrCr is required
        /// </summary>
        /// <param name="newTransactionMaster"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] TransactionMasterModel newTransactionMaster)
        {
            ServiceResponseModel<TransactionMasterModel> response = new ServiceResponseModel<TransactionMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(newTransactionMaster.TrxId))
                {
                    throw new ArgumentNullException("TrxId is required");
                }
                response = await _transactionMasterService.Add(newTransactionMaster);
                if (response.Data == null)
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                response.Success = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Delete transaction type. TrxId is required
        /// </summary>
        /// <param name="trxId"></param>
        /// <returns></returns>
        [HttpDelete("{trxId}")]
        public async Task<IActionResult> Delete(string trxId)
        {
            ServiceResponseModel<TransactionMasterModel> response = new ServiceResponseModel<TransactionMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(trxId))
                {
                    throw new ArgumentNullException("TrxId is required");
                }
                response = await _transactionMasterService.Delete(trxId);
                if (response.Data == null)
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                response.Success = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Edit transaction type. TrxId+DrCr is required
        /// </summary>
        /// <param name="editTransactionMasterModel"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(TransactionMasterModel editTransactionMasterModel)
        {
            ServiceResponseModel<TransactionMasterModel> response = new ServiceResponseModel<TransactionMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(editTransactionMasterModel.TrxId))
                {
                    throw new ArgumentNullException("TrxId is required");
                }
                response = await _transactionMasterService.Edit(editTransactionMasterModel);
                if (response.Data == null)
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                response.Success = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Get all transaction types.
        /// </summary>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ServiceResponseModel<IEnumerable<TransactionMasterModel>> response = new ServiceResponseModel<IEnumerable<TransactionMasterModel>>();
            try
            {
                response = await _transactionMasterService.GetAll();
                if (response.Data == null)
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                response.Success = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Get all TransactionMaster trxId
        /// </summary>
        /// <returns></returns>
        [Route("GetAllByPage")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TransactionParams transactionParams)
        {
            var transactionList = await _transactionMasterService.GetAll(transactionParams);
            Response.AddPaginationHeader(transactionList.CurrentPage, transactionList.PageSize, transactionList.TotalCount, transactionList.TotalPages);
            return Ok(transactionList);
        }

        /// <summary>
        /// Get transaction types by type.
        /// </summary>
        /// <param name="trxId"></param>
        /// <returns></returns>
        [Route("{trxId}")]
        [HttpGet]
        public async Task<IActionResult> GetByCode(string trxId)
        {
            ServiceResponseModel<TransactionMasterModel> response = new ServiceResponseModel<TransactionMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(trxId))
                {
                    throw new ArgumentNullException("TrxId is required");
                }
                response = await _transactionMasterService.GetByCode(trxId);
                if (response.Data == null)
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                response.Success = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
    }
}
