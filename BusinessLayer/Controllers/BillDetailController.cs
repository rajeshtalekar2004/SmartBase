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
    /// This controller is used for CRUD and paging for Bill Detail matching with voucher.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class BillDetailController : ControllerBase

    {
        private IBillDetailService _billDetailService { get; }
        private readonly ILogger<BillDetailController> _logger;

        public BillDetailController(IBillDetailService billDetailService, ILogger<BillDetailController> logger)
        {
            _billDetailService = billDetailService;
            _logger = logger;
        }

        /// <summary>
        /// Add new bill detail record. CompId+AccYear+BillId+ItemSr required field
        /// </summary>
        /// <param name="newBillDetailModell"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] BillDetailModel newBillDetailModell)
        {
            ServiceResponseModel<BillDetailModel> response = new ServiceResponseModel<BillDetailModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(newBillDetailModell.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(newBillDetailModell.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(newBillDetailModell.BillId))
                {
                    throw new ArgumentNullException("BillId is required");
                }
                if (newBillDetailModell.ItemSr < 0)
                {
                    throw new ArgumentNullException("ItemSr is required");
                }
                response = await _billDetailService.Add(newBillDetailModell);
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
        /// Delete bill detail record. CompId+AccYear+BillId+ItemSr required field
        /// </summary>
        /// <param name="billBillDetailMode"></param>
        /// <returns></returns>
         [HttpDelete("{Delete}")]
        public async Task<IActionResult> Delete([FromBody] BillDetailModel billBillDetailMode)
        {
            ServiceResponseModel<BillDetailModel> response = new ServiceResponseModel<BillDetailModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(billBillDetailMode.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(billBillDetailMode.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(billBillDetailMode.BillId))
                {
                    throw new ArgumentNullException("BillId is required");
                }
                if (billBillDetailMode.ItemSr < 0)
                {
                    throw new ArgumentNullException("ItemSr is required");
                }
                response = await _billDetailService.Delete(billBillDetailMode);
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
        /// Edit bill detail record. CompId+AccYear+BillId+ItemSr required field
        /// </summary>
        /// <param name="editBillDetailModel"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] BillDetailModel editBillDetailModel)
        {
            ServiceResponseModel<BillDetailModel> response = new ServiceResponseModel<BillDetailModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(editBillDetailModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editBillDetailModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(editBillDetailModel.BillId))
                {
                    throw new ArgumentNullException("BillId is required");
                }
                if (editBillDetailModel.ItemSr < 0 )
                {
                    throw new ArgumentNullException("ItemSr is required");
                }

                response = await _billDetailService.Edit(editBillDetailModel);
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
        /// Get all records from Bill details. CompId+AccYear required field
        /// </summary>
        /// <param name="getBillMasterModel"></param>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAll(BillDetailModel getBillMasterModel)
        {
            ServiceResponseModel<IEnumerable<BillDetailModel>> response = new ServiceResponseModel<IEnumerable<BillDetailModel>>();
            try
            {
                if (string.IsNullOrWhiteSpace(getBillMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(getBillMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                response = await _billDetailService.GetAll(getBillMasterModel);
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
        /// Get bill detail with paging. Required =>CompCode+AccYear
        /// </summary>
        /// <param name="pageParams"></param>
        /// <param name="getBillDetailModel"></param>
        /// <returns></returns>
        [Route("GetAllByPage")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromQuery] PageParams pageParams, [FromBody] BillDetailModel getBillDetailModel)
        {
            ServiceResponseModel<IEnumerable<BillDetail>> response = new ServiceResponseModel<IEnumerable<BillDetail>>();
            try
            {
                if (string.IsNullOrWhiteSpace(getBillDetailModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(getBillDetailModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }

                var billDetailList = await _billDetailService.GetAll(pageParams, getBillDetailModel);
                Response.AddPaginationHeader(billDetailList.CurrentPage, billDetailList.PageSize, billDetailList.TotalCount, billDetailList.TotalPages);
                response.Data = billDetailList;
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
        /// Get all bill detail records for selected Bill. CompId+AccYear+BillId required field
        /// </summary>
        /// <param name="billMasterId"></param>
        /// <returns></returns>
        [Route("GetBillId")]
        [HttpPost]
        public async Task<IActionResult> GetBillId(BillDetailModel billMasterId)
        {
            ServiceResponseModel<IEnumerable<BillDetailModel>> response = new ServiceResponseModel<IEnumerable<BillDetailModel>>();
            try
            {
                if (string.IsNullOrWhiteSpace(billMasterId.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(billMasterId.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(billMasterId.BillId))
                {
                    throw new ArgumentNullException("BillId is required");
                }

                response = await _billDetailService.GetBillId(billMasterId);
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
