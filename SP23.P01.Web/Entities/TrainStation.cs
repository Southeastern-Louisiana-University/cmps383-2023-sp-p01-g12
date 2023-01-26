using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SP23.P01.Web.Entities
{
    public class TrainStation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
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
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class TrainStationCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class TrainStationUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}