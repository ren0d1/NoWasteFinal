using NoWaste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Services
{
    public static class GpsHelper
    {
        public static bool CheckInRange(GPSCoord centerCoord, GPSCoord elemCoord, int km)
        {
            var ky = 40000 / 360;
            var kx = Math.Cos(Math.PI * centerCoord.Lat / 180.0) * ky;
            var dx = Math.Abs(centerCoord.Lng - elemCoord.Lng) * kx;
            var dy = Math.Abs(centerCoord.Lat - elemCoord.Lat) * ky;
            return Math.Sqrt(dx * dx + dy * dy) <= km;
        }
        public static double GetDistanceBetweenCorrds(GPSCoord point1, GPSCoord point2)
        {
            var R = 6371; // km
            var dLat = ConvertToRadians(point2.Lat - point1.Lat);
            var dLon = ConvertToRadians(point2.Lng - point1.Lng);
            var lat1 = ConvertToRadians(point1.Lat);
            var lat2 = ConvertToRadians(point2.Lat);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return d;
        }
        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
