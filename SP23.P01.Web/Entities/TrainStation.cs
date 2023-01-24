namespace SP23.P01.Web.Entities
{
    public class TrainStation
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }

    public class TrainStationGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }

    public class TrainStationCreateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }

    public class TrainStationUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }

    public class TrainStationDeleteDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}