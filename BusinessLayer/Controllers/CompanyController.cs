﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using SmartBase.BusinessLayer.Persistence.PageParams;
using SmartBase.BusinessLayer.Persistence;

namespace SmartBase.BusinessLayer.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        public ICompanyService _companyService { get; }

        /// <summary>
        /// Initialize company controller
        /// </summary>
        /// <param name="companyService"></param>
        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        /// <summary>
        /// Get list of companies
        /// </summary>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ServiceResponseModel<IEnumerable<CompanyModel>> response = new ServiceResponseModel<IEnumerable<CompanyModel>>();
            try
            {
                response = await _companyService.GetAll();
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
        /// Get all Company information by compCode,name
        /// </summary>
        /// <returns></returns>
        [Route("GetAllByPage")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CompInfoParams compInfoParams)
        {
            if (string.IsNullOrWhiteSpace(compInfoParams.CompCode))
            {
                throw new ArgumentNullException("CompCode is required");
            }
            var compList = await _companyService.GetAll(compInfoParams);
            Response.AddPaginationHeader(compList.CurrentPage, compList.PageSize, compList.TotalCount, compList.TotalPages);
            return Ok(compList);
        }



        /// <summary>
        /// Get company by CompId. 
        /// </summary>
        /// <param name="compId"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("GetCompanyByCode")]
        public async Task<IActionResult> GetCompanyByCode([FromBody] CompanyModel editCompany)
        {
            ServiceResponseModel<CompanyModel> response = new ServiceResponseModel<CompanyModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(editCompany.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                response = await _companyService.GetCompanyByCode(editCompany.CompCode);
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
        /// Delete company by Id
        /// </summary>
        /// <param name="compId"></param>
        /// <returns></returns>
        [HttpDelete("{ByCode}")]
        public async Task<IActionResult> Delete(string ByCode)
        {
            ServiceResponseModel<CompanyModel> response = new ServiceResponseModel<CompanyModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(ByCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                response = await _companyService.Delete(ByCode);
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
        /// Add new company
        /// </summary>
        /// <param name="newCompany"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CompanyModel newCompany)
        {
            ServiceResponseModel<CompanyModel> response = new ServiceResponseModel<CompanyModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(newCompany.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(newCompany.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(newCompany.Name))
                {
                    throw new ArgumentNullException("Name is required");
                }
                response = await _companyService.Add(newCompany);
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
        /// Edit Company
        /// </summary>
        /// <param name="editCompany"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] CompanyModel editCompany)
        {

            ServiceResponseModel<CompanyModel> response = new ServiceResponseModel<CompanyModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(editCompany.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editCompany.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(editCompany.Name))
                {
                    throw new ArgumentNullException("Name is required");
                }
                response = await _companyService.Edit(editCompany);
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

        [HttpPost("NewYear")]
        public async Task<IActionResult> NewYear([FromBody] CompanyNewYearModel newCompany)
        {
            ServiceResponseModel<CompanyNewYearModel> response = new ServiceResponseModel<CompanyNewYearModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(newCompany.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(newCompany.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(newCompany.NewYear))
                {
                    throw new ArgumentNullException("NewYear is required");
                }
                if (string.IsNullOrWhiteSpace(newCompany.BalanceTransfer))
                {
                    throw new ArgumentNullException("BalanceTransfer is required");
                }

                response = await _companyService.NewYear(newCompany);
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
