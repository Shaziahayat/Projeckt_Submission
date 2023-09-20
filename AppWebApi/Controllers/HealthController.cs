﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;
using Models.DTO;
using Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HealthController : Controller
    {
        IPersonsService _service = null;
        ILogger<PersonController> _logger = null;

        // GET: health/hello
        [HttpGet()]
        [ActionName("Hello")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Hello()
        {
            //to verify the layers are accessible
            string sRet = $"\nLayer access:\n{csAppConfig.Hello}" +
                $"\n{csPerson.Hello}" +
                $"\n{csLoginService.Hello}" +
                $"\n{csJWTService.Hello}" +
                $"\n{csPersonServiceModel.Hello}";


            //to verify connection strings can be read from appsettings.json
            sRet += $"\n\nDbConnections:\nDbLocation: {csAppConfig.DbSetActive.DbLocation}" +
                $"\nDbServer: {csAppConfig.DbSetActive.DbServer}";

            sRet += "\nDbUserLogins in DbSet:";
            foreach (var item in csAppConfig.DbSetActive.DbLogins)
            {
                sRet += $"\n   DbUserLogin: {item.DbUserLogin}" +
                    $"\n   DbConnection: {item.DbConnection}\n   ConString: <secret>";
            }

            //to verify usersecret access
            sRet += $"\n\nUser secrets:\n{csAppConfig.SecretMessage}";

            //to verify Services added via DI
            sRet += $"\n\nDependency Injection:";
            if (_service == null)
                sRet += $"\nNo Services added";
            else
                sRet += $"\n{_service.InstanceHello}";

            //koppling av models to appwebApi
            sRet += $"\n\n seeding test\n\n";

            //var s = new csSeedGenerator();
            //var person = new List<csPerson>();
            //for (int i = 0; i < 100; i++)
            //{
            //    var p = new csPerson();
            //    p.Seed(s);
            //    person.Add(p);
            //}

            //hård koppling av Service model och appWebApi
            //var service = new csPersonServiceModel();
         
            //sRet += person.Count.ToString() + "\n";
            //sRet += person.First() + "\n";
            //sRet += person.Last() + "\n";

            //sRet += $"\n\n****************************";



            return Ok(sRet);
        }

        //GET: health/log
        [HttpGet()]
        [ActionName("Log")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<csLogMessage>))]
        public async Task<IActionResult> Log([FromServices] ILoggerProvider _loggerProvider)
        {
            //Note the way to get the LoggerProvider, not the logger from Services via DI
            if (_loggerProvider is csInMemoryLoggerProvider cl)
            {
                return Ok(await cl.MessagesAsync);
            }
            return Ok("No messages in log");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        #region constructors
        //Controllers do not specify default constructor
        public HealthController(IPersonsService service, ILogger<PersonController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion
    }
}

