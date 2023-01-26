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

            var trainStationToReturn = _dataContext
                .TrainStations
                .Select(x => new TrainStationGetDto {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
            })
            .ToList();

            return Ok(trainStationToReturn);
        }

        [HttpGet("{trainStationId}")]
        public ActionResult GetById([FromRoute] int trainStationId)
        {

            var trainStationFromDatabase = _dataContext.TrainStations.FirstOrDefault(x => x.Id == trainStationId);

            if (trainStationFromDatabase == null)
            {
                return NotFound(new Error("trainStationId", "No trainStation found with that Id"));
            }

            var trainStationToReturn = new TrainStationGetDto
            {
                Id = trainStationFromDatabase.Id,
                Name = trainStationFromDatabase.Name,
                Address = trainStationFromDatabase.Address
            };

            return Ok(trainStationToReturn);
        }

        [HttpPut("edit-trainStation/{trainStationId:int}")]
        public ActionResult EditTrainStation([FromRoute] int trainStationId)
        {
            var trainStationToEdit = _dataContext.TrainStations.FirstOrDefault(x => x.Id == trainStationId);

            //check if null
            if (trainStationToEdit == null)
            {
                return NotFound(new Error("trainStationId", "Id not found"));
            }

            return Ok(trainStationToEdit);
        }
    }
}
