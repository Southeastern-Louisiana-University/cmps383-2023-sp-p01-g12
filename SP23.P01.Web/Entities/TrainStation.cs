using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SP23.P01.Web.Entities
{
    public class TrainStation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class TrainStationConfiguration : IEntityTypeConfiguration<TrainStation>
    {
        public void Configure(EntityTypeBuilder<TrainStation> builder)
        {
            builder
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(120);

            builder
                .Property(t => t.Address)
                .IsRequired();
        }
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