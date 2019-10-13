using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.DataProviders;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/Company")]
    [ApiController]
    public class CompanyController : BaseDataController<Company, CompanyProvider>
    {

    }

}
