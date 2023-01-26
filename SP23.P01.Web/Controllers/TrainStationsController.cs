using Microsoft.AspNetCore.Mvc;
using SP23.P01.Web.Common;
using SP23.P01.Web.Data;
using SP23.P01.Web.Entities;
using System.Net.Mail;

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

        [HttpPut("")]
        public ActionResult EditTrainStation([FromRoute] int trainStationId, TrainStationUpdateDto trainStationUpdateDto)
        {
            var trainStationToEdit = _dataContext.TrainStations.FirstOrDefault(x => x.Id == trainStationId);

            //check if null
            if (trainStationToEdit == null)
            {
                return NotFound(new Error("trainStationId", "Id not found"));
            }

            //name must be provided - return 400
            if (string.IsNullOrEmpty(trainStationUpdateDto.Name.Trim()))
            {
                return BadRequest("Name cannot be empty");
            }

            //name cannot be more than 120 chars

            //address must be provided - return 400

            //return updated Dto
            trainStationToEdit.Name = trainStationUpdateDto.Name;
            trainStationToEdit.Address = trainStationUpdateDto.Address;

            _dataContext.SaveChanges();

            var trainStationToReturn = new TrainStationUpdateDto
            {
                Name = trainStationUpdateDto.Name,
                Address = trainStationUpdateDto.Address
            };

            return Ok(trainStationToReturn);
        }
    }
}
