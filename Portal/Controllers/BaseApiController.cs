using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Angular.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected IMapper mapper { get; }
        protected ILogger<BaseApiController> logger { get; }

        protected BaseApiController(ILogger<BaseApiController> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        protected void LogError(Exception ex) => logger.LogError($"Exception: {ex.Message}, stacktrace: {ex.StackTrace}");

    }
}