using Microsoft.AspNetCore.Mvc;
using SP23.P01.Web.Common;
using SP23.P01.Web.Data;
using SP23.P01.Web.Entities;

namespace SP23.P01.Web.Controllers
{
    [ApiController]
    [Route("/api/stations")]
    public class TrainStationsController : Controller
    {
        private readonly DataContext _dataContext;

        public TrainStationsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public ActionResult GetTrainStations()
        {
            // var response = new Response();

            var trainStationToReturn = _dataContext
                .TrainStations
                .Select(x => new TrainStationGetDto {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
            })
            .ToList();

            // response.Data = trainStationToReturn;

            return Ok(trainStationToReturn);
        }

        [HttpGet("{trainStationId}")]
        public ActionResult GetById([FromRoute] int trainStationId)
        {
            // var response = new Response();

            var trainStationFromDatabase = _dataContext.TrainStations.FirstOrDefault(x => x.Id == trainStationId);

            if (trainStationFromDatabase == null)
            {
                // response.AddError("trainStationId", "No trainStation found with that Id");
                return NotFound(new Error("trainStationId", "No trainStation found with that Id"));
            }

            //if (response.HasErrors)
            //{
            //    return BadRequest(response);
            //}

            var trainStationToReturn = new TrainStationGetDto
            {
                Id = trainStationFromDatabase.Id,
                Name = trainStationFromDatabase.Name,
                Address = trainStationFromDatabase.Address
            };

            //throws error?
            //response.Data = trainStationToReturn;

            return Ok(trainStationToReturn);
        }
    }
}
