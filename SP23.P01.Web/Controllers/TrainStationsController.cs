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

        [HttpPost]
        public ActionResult Create([FromBody] TrainStationCreateDto trainStationCreateDto)
        {
            if(String.IsNullOrWhiteSpace(trainStationCreateDto.Name))
            {
                return BadRequest(new Error("name", "Name cannot be empty."));
            }

            if(String.IsNullOrWhiteSpace(trainStationCreateDto.Address))
            {
                return BadRequest(new Error("address", "Address cannot be empty."));
            }

            if(trainStationCreateDto.Name.Length > 120)
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

            return CreatedAtAction(nameof(GetById), new { trainStationId = trainStationToReturn.Id  }, trainStationToReturn);
        }
        [HttpDelete("{trainStationId}")]
        public ActionResult Delete([FromRoute] int trainStationId) 
        {

            var trainStationToDelete = _dataContext.TrainStations.FirstOrDefault(x => x.Id == trainStationId);


            if (trainStationToDelete == null) 
            { 
                    return NotFound();
            
            }

            _dataContext.TrainStations.Remove(trainStationToDelete);
            _dataContext.SaveChanges();

            return Ok(); 
        }
    }
}
