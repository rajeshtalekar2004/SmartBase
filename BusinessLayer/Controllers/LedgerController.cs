﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Controllers
{
    /// <summary>
    /// This controller is used for CRUD and paging for Ledger. This is not interacted by user.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class LedgerController : ControllerBase
    {
        private ILedgerService _ledgerService;
        public ILogger<LedgerController> _logger { get; }

        public LedgerController(ILedgerService ledgerService, ILogger<LedgerController> logger)
        {
            _ledgerService = ledgerService;
            _logger = logger;
        }

        /// <summary>
        /// Add new ledger. CompCode+Accyear+VouNo+ItemSr required fields.
        /// </summary>
        /// <param name="newLedgerModel"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] LedgerModel newLedgerModel)
        {
            ServiceResponseModel<LedgerModel> response = new ServiceResponseModel<LedgerModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(newLedgerModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(newLedgerModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(newLedgerModel.VouNo))
                {
                    throw new ArgumentNullException("VouNo is required");
                }
                if (newLedgerModel.ItemSr < 0)
                {
                    throw new ArgumentNullException("ItemSr is required");
                }
                response = await _ledgerService.Add(newLedgerModel);
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
        /// Delete ledger record. CompCode+Accyear+VouNo+ItemSr required fields.
        /// </summary>
        /// <param name="delLedgerModel"></param>
        /// <returns></returns>
        [HttpDelete("{Delete}")]
        public async Task<IActionResult> Delete([FromBody] LedgerModel delLedgerModel)
        {
            ServiceResponseModel<LedgerModel> response = new ServiceResponseModel<LedgerModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(delLedgerModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(delLedgerModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(delLedgerModel.VouNo))
                {
                    throw new ArgumentNullException("VouNo is required");
                }
                if (delLedgerModel.ItemSr < 0)
                {
                    throw new ArgumentNullException("ItemSr is required");
                }
                response = await _ledgerService.Delete(delLedgerModel);
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
        /// Edit ledger record. CompCode+Accyear+VouNo+ItemSr required fields.
        /// </summary>
        /// <param name="editLedgerModel"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] LedgerModel editLedgerModel)
        {

            ServiceResponseModel<LedgerModel> response = new ServiceResponseModel<LedgerModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(editLedgerModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editLedgerModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(editLedgerModel.VouNo))
                {
                    throw new ArgumentNullException("VouNo is required");
                }
                if (editLedgerModel.ItemSr < 0)
                {
                    throw new ArgumentNullException("ItemSr is required");
                }
                response = await _ledgerService.Edit(editLedgerModel);
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
        /// Get all ledger records. CompCode+Accyear required fields.
        /// </summary>
        /// <param name="ledgerModel"></param>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] LedgerModel ledgerModel)
        {

            ServiceResponseModel<IEnumerable<LedgerModel>> response = new ServiceResponseModel<IEnumerable<LedgerModel>>();
            try
            {
                if (string.IsNullOrWhiteSpace(ledgerModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(ledgerModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                response =  await _ledgerService.GetAll(ledgerModel);
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
        /// Get all ledger by vouNo,vouDate,accountId
        /// </summary>
        /// <returns></returns>
        [Route("GetAllByPage")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromQuery] PageParams pageParams, [FromBody] LedgerModel getledgerModel)
        {
            ServiceResponseModel<IEnumerable<Ledger>> response = new ServiceResponseModel<IEnumerable<Ledger>>();
            try
            {
                if (string.IsNullOrWhiteSpace(getledgerModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(getledgerModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                var ledgerList = await _ledgerService.GetAll(pageParams, getledgerModel);
                Response.AddPaginationHeader(ledgerList.CurrentPage, ledgerList.PageSize, ledgerList.TotalCount, ledgerList.TotalPages);
                response.Data = ledgerList;
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
        /// Get all ledger records. CompCode+Accyear+AccountId required fields.
        /// </summary>
        /// <param name="editLedgerModel"></param>
        /// <returns></returns>
        [Route("GetByCode")]
        [HttpPost]
        public async Task<IActionResult> GetByCode([FromBody] LedgerModel editLedgerModel)
        {

            ServiceResponseModel<IEnumerable<LedgerModel>> response = new ServiceResponseModel<IEnumerable<LedgerModel>>();
            try
            {
                if (string.IsNullOrWhiteSpace(editLedgerModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editLedgerModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(editLedgerModel.AccountId))
                {
                    throw new ArgumentNullException("AccountId is required");
                }
                response =  await _ledgerService.GetByCode(editLedgerModel);
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
        /// This process will recreate ledger file from VoucherMaster+VoucherDetail
        /// </summary>
        /// <param name="newLedgerModel"></param>
        /// <returns></returns>
        [HttpPost("Receate")]
        public async Task<IActionResult> Receate([FromBody] LedgerModel newLedgerModel)
        {
            ServiceResponseModel<string> response = new ServiceResponseModel<string>();
            try
            {
                if (string.IsNullOrWhiteSpace(newLedgerModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(newLedgerModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                response = await _ledgerService.Receate(newLedgerModel);
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
        /// Generate ledger. Required => CompCode+AccYear+StartDate+StartAccount+FinishAccount
        /// </summary>
        /// <param name="reportModel"></param>
        /// <returns></returns>
        [HttpPost("GeneralLedger")]
        public async Task<IActionResult> GeneralLedger([FromBody] ReportModel reportModel)
        {
            ServiceResponseModel<LedgerReportModel> response = new ServiceResponseModel<LedgerReportModel>();
            try
            {
                
                if (string.IsNullOrWhiteSpace(reportModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(reportModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (reportModel.FinishDate.CompareTo(reportModel.StartDate) < 0 ) 
                {
                    throw new ArgumentNullException("Invalid StartDate Or Finish date");
                }
                if (string.IsNullOrWhiteSpace(reportModel.StartAccount))
                {
                    throw new ArgumentNullException("Start Account is required");
                }
                if (string.IsNullOrWhiteSpace(reportModel.FinishAccount))
                {
                    throw new ArgumentNullException("Finish Account is required");
                }

                response = await _ledgerService.GeneralLedger(reportModel);
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
        /// Generate Sales or Purchase Register. Required=>CompCode+AccYear+StartDate+SaleOrPurchaseType.
        /// SaleOrPurchaseType = 'P' for purchase register
        /// SaleOrPurchaseType = 'S' for sales register
        /// </summary>
        /// <param name="reportModel"></param>
        /// <returns></returns>
        [HttpPost("SaleOrPurchaseRegister")]
        public async Task<IActionResult> SaleRegister([FromBody] ReportModel reportModel)
        {
            ServiceResponseModel<SalePurchaseReportModel> response = new ServiceResponseModel<SalePurchaseReportModel>();
            try
            {

                if (string.IsNullOrWhiteSpace(reportModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(reportModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (reportModel.FinishDate.CompareTo(reportModel.StartDate) < 0)
                {
                    throw new ArgumentNullException("Invalid StartDate Or Finish date");
                }
                if (string.IsNullOrWhiteSpace(reportModel.SaleOrPurchaseType) || (reportModel.SaleOrPurchaseType != "S" && reportModel.SaleOrPurchaseType != "P"))
                {
                    throw new ArgumentNullException("SaleOrPurchaseType is required and value should be S or P");
                }
                response = await _ledgerService.SaleRegister(reportModel);
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
