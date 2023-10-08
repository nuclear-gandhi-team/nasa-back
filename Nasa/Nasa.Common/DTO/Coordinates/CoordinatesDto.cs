namespace Nasa.Common.DTO.Coordinates
{
    public class CoordinatesDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"{Latitude};{Longitude};{DateTime.UtcNow:yyyy-MM-ddTHH:mm}";
        }
    }
}
