using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.DataProviders;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/Borehole")]
    [ApiController]
    public class BoreholeController : BaseDataController<Borehole, BoreholeProvider>
    {

    }
}

