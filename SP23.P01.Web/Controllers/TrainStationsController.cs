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

        [HttpPost]
        public ActionResult Create([FromBody] TrainStationCreateDto trainStationCreateDto)
        {
            if (String.IsNullOrWhiteSpace(trainStationCreateDto.Name))
            {
                return BadRequest(new Error("name", "Name cannot be empty."));
            }

            if (String.IsNullOrWhiteSpace(trainStationCreateDto.Address))
            {
                return BadRequest(new Error("address", "Address cannot be empty."));
            }

            if (trainStationCreateDto.Name.Length > 120)
            {
                return BadRequest(new Error("name", "Name cannot be longer than 120 characters."));
            }

            var trainStationToCreate = new TrainStation
            {
                Name = trainStationCreateDto.Name,
                Address = trainStationCreateDto.Address,
            };

            _dataContext.TrainStations.Add(trainStationToCreate);
            _dataContext.SaveChanges();

            var trainStationToReturn = new TrainStationGetDto
            {
                Id = trainStationToCreate.Id,
                Name = trainStationToCreate.Name,
                Address = trainStationToCreate.Address,
            };

            return CreatedAtAction(nameof(GetById), new { trainStationId = trainStationToReturn.Id }, trainStationToReturn);
        }

        [HttpPut("/api/stations/{id}")]
        public ActionResult<TrainStationsController> UpdateStation([FromBody]TrainStationUpdateDto trainStationUpdateDto, int id)
        {
            var updateStations = _dataContext.TrainStations.FirstOrDefault(x => x.Id == id);

            //name must be provided - return 400 (bad request)
            if (String.IsNullOrWhiteSpace(trainStationUpdateDto.Name))
            {
                return BadRequest(new Error("Name", "Name cannot be empty"));
            }

            //address must be provided - return 400
            if (String.IsNullOrWhiteSpace(trainStationUpdateDto.Address))
            {
                return BadRequest(new Error("Address", "Address cannot be empty"));
            }

            //name cannot be more than 120 chars - return 400
            if (trainStationUpdateDto.Name.Length > 120)
            {
                return BadRequest(new Error("Name", "Name cannot be longer than 120 characters"));
            }

            //update the trainStationUpdateDto with new info
            /*
            var trainStationToUpdate = new TrainStation
            {
                Name = trainStationUpdateDto.Name,
                Address = trainStationUpdateDto.Address,
            };

           //_dataContext.SaveChanges();

            var trainStationToReturn = new TrainStation
            {
                Id = trainStationToUpdate.Id,
                Name = trainStationToUpdate.Name,
                Address = trainStationToUpdate.Address
            };

            _dataContext.SaveChanges();
            */

            return Ok();
        }
    }
}
