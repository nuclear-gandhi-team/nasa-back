using Nasa.BLL.ServicesContracts;
using Nasa.Common.DTO.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.BLL.Services
{
    public class CoordinatesService : ICoordinatesService
    {
        public double ComputeDistance(CoordinatesDto left, CoordinatesDto right)
        {
            if ((left.Latitude == right.Latitude) && (left.Longitude == right.Longitude))
            {
                return 0;
            }
            else
            {
                double theta = left.Longitude - right.Longitude;
                double dist = Math.Sin(DegToRad(left.Latitude)) * Math.Sin(DegToRad(right.Latitude)) + Math.Cos(DegToRad(left.Latitude)) * Math.Cos(DegToRad(right.Latitude)) * Math.Cos(DegToRad(theta));
                dist = Math.Acos(dist);
                dist = RadToDeg(dist);
                dist = dist * 60 * 1.1515;
                dist = dist * 1.609344;

                return (dist);
            }
        }

        public CoordinatesDto GetCoordinatesInstance(string coordinates)
        {
            var coords = coordinates.Split(',');

            if (coords.Length != 2)
                throw new ArgumentException();

            if (!double.TryParse(coords[0], out double latitude) || !double.TryParse(coords[1], out double longitude))
                throw new ArgumentException();

            return new CoordinatesDto { Latitude = latitude, Longitude = longitude };
        }

        private double DegToRad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double RadToDeg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
