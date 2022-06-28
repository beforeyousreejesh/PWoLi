using Microsoft.AspNetCore.Http;
using PWoLi.Application.Contracts;
using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWoLi.Web.Services
{
    public class ClientSystemService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ISystemService _systemService;

        public ClientSystemService(
            IHttpContextAccessor httpContextAccessor,
            ISystemService systemService)
        {
            if (httpContextAccessor == null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            if (systemService == null)
            {
                throw new ArgumentNullException(nameof(systemService));
            }

            _httpContextAccessor = httpContextAccessor;
            _systemService = systemService;
        }

        public Task InsertIntoSystemsAsync(SystemModel systemModel)
        {
            var createdby = _httpContextAccessor.HttpContext.User?.Identity?.Name;

            if (string.IsNullOrEmpty(createdby))
            {
                throw new Exception("Invalid user");
            }

            return _systemService.InsertIntoSystemsAsync(systemModel, createdby);
        }

        public Task UpdateSystemsAsync(SystemModel systemModel)
        {
            var updatedBy = _httpContextAccessor.HttpContext.User?.Identity?.Name;

            if (string.IsNullOrEmpty(updatedBy))
            {
                throw new Exception("Invalid user");
            }

            return _systemService.UpdateSystemsAsync(systemModel, updatedBy);
        }

        public Task DeleteSystemsAsync(SystemModel systemModel)
        {
            var updatedBy = _httpContextAccessor.HttpContext.User?.Identity?.Name;

            if (string.IsNullOrEmpty(updatedBy))
            {
                throw new Exception("Invalid user");
            }

            return _systemService.DeleteSystemsAsync(systemModel, updatedBy);
        }
    }
}
