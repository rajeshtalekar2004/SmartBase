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
    /// This controller is used for CRUD and paging for Voucher Detail. But it will never required user interaction
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class VoucherDetailController : ControllerBase
    {
        private IVoucherDetailService _voucherDetailService;
        private readonly ILogger<TransactionMasterController> _logger;

        public VoucherDetailController(IVoucherDetailService voucherDetailService,ILogger<TransactionMasterController> logger)
        {
            _voucherDetailService = voucherDetailService;
            _logger = logger;
        }

        /// <summary>
        /// Add new voucher detail record CompCode+AccYear+VouNo+ItemSr required
        /// </summary>
        /// <param name="newVoucherDetailModel"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult>  Add(VoucherDetailModel newVoucherDetailModel)
        {
            ServiceResponseModel<VoucherDetailModel> response = new ServiceResponseModel<VoucherDetailModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(newVoucherDetailModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(newVoucherDetailModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(newVoucherDetailModel.VouNo))
                {
                    throw new ArgumentNullException("UserPassword is required");
                }
                if (newVoucherDetailModel.ItemSr<0)
                {
                    throw new ArgumentNullException("VouNo is required");
                }
                response = await _voucherDetailService.Add(newVoucherDetailModel);
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
        /// Delete voucher detail record CompCode+AccYear+VouNo+ItemSr required
        /// </summary>
        /// <param name="delVoucherDetailModel"></param>
        /// <returns></returns>
        [HttpDelete("{Delete}")]
        public  async Task<IActionResult> Delete(VoucherDetailModel delVoucherDetailModel)
        {
            ServiceResponseModel<VoucherDetailModel> response = new ServiceResponseModel<VoucherDetailModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(delVoucherDetailModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(delVoucherDetailModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(delVoucherDetailModel.VouNo))
                {
                    throw new ArgumentNullException("VouNo is required");
                }
                if (delVoucherDetailModel.ItemSr < 0)
                {
                    throw new ArgumentNullException("UserPassword is required");
                }
                response = await _voucherDetailService.Delete(delVoucherDetailModel);
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
        /// Edit voucher detail record CompCode+AccYear+VouNo+ItemSr required
        /// </summary>
        /// <param name="editVoucherDetailModel"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(VoucherDetailModel editVoucherDetailModel)
        {
            ServiceResponseModel<VoucherDetailModel> response = new ServiceResponseModel<VoucherDetailModel>();

            try
            {
                response = await _voucherDetailService.Edit(editVoucherDetailModel);
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
        /// Get all Voucher Detail. CompCode+AccYear is required.
        /// </summary>
        /// <param name="editVoucherDetailModel"></param>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAll(VoucherDetailModel editVoucherDetailModel)
        {
            ServiceResponseModel<IEnumerable<VoucherDetailModel>> response = new ServiceResponseModel<IEnumerable<VoucherDetailModel>>();
            try
            {
                response = await _voucherDetailService.GetAll(editVoucherDetailModel);
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
        /// Get all Voucher Details by vouNo,accountId
        /// </summary>
        /// <param name="pageParams"></param>
        /// <param name="getVoucherDetailModel"></param>
        /// <returns></returns>
        [Route("GetAllByPage")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromQuery] PageParams pageParams, VoucherDetailModel getVoucherDetailModel)
        {
            ServiceResponseModel<IEnumerable<VoucherDetail>> response = new ServiceResponseModel<IEnumerable<VoucherDetail>>();
            try
            {
                if (string.IsNullOrWhiteSpace(getVoucherDetailModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(getVoucherDetailModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                var vouDetailList = await _voucherDetailService.GetAll(pageParams, getVoucherDetailModel);
                Response.AddPaginationHeader(vouDetailList.CurrentPage, vouDetailList.PageSize, vouDetailList.TotalCount, vouDetailList.TotalPages);
                response.Data = vouDetailList;
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
        /// Get all Voucher Detail. CompCode+AccYear+VouNo is required.
        /// </summary>
        /// <param name="getVoucherDetailModel"></param>
        /// <returns></returns>
        [Route("GetByVouNo")]
        [HttpPost]
        public  async Task<IActionResult>  GetByVouNo(VoucherDetailModel getVoucherDetailModel)
        {
            ServiceResponseModel<IEnumerable<VoucherDetailModel>> response = new ServiceResponseModel<IEnumerable<VoucherDetailModel>>();
            try
            {
                if (string.IsNullOrWhiteSpace(getVoucherDetailModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(getVoucherDetailModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(getVoucherDetailModel.VouNo))
                {
                    throw new ArgumentNullException("VouNo is required");
                }
                if (getVoucherDetailModel.ItemSr < 0)
                {
                    throw new ArgumentNullException("UserPassword is required");
                }
                response = await _voucherDetailService.GetByVouNo(getVoucherDetailModel);
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
        /// Get all Voucher Detail. CompCode+AccYear+VouNo+ItemSr is required.
        /// </summary>
        /// <param name="getVoucherDetailModel"></param>
        /// <returns></returns>
        [Route("GetByVouNoItemSr")]
        [HttpPost]
        public  async Task<IActionResult>  GetByVouNoItemSr(VoucherDetailModel getVoucherDetailModel)
        {
            ServiceResponseModel<VoucherDetailModel> response = new ServiceResponseModel<VoucherDetailModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(getVoucherDetailModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(getVoucherDetailModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(getVoucherDetailModel.VouNo))
                {
                    throw new ArgumentNullException("VouNo is required");
                }
                if (getVoucherDetailModel.ItemSr < 0)
                {
                    throw new ArgumentNullException("UserPassword is required");
                }
                response = await _voucherDetailService.GetByVouNoItemSr(getVoucherDetailModel);
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