using Microsoft.AspNetCore.Mvc;
using SP23.P01.Web.Common;
using SP23.P01.Web.Data;

namespace SP23.P01.Web.Controllers
{
    [ApiController]
    [Route("/api/trainstations")]
    public class TrainStationsController : Controller
    {
        private readonly DataContext _dataContext;

        public TrainStationsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
