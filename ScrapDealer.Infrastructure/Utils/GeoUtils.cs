using ScrapDealer.Domain.Consts;
using System;
using System.Collections.Generic;
using System.Linq;

public static class GeoUtils
{
    // simple ray-casting algorithm for point-in-polygon (works for simple polygons)
    public static bool IsPointInPolygon(double lat, double lon, IList<(double Lat, double Lon)> polygon)
    {
        var inside = false;
        int n = polygon.Count;
        for (int i = 0, j = n - 1; i < n; j = i++)
        {
            var xi = polygon[i].Lat;
            var yi = polygon[i].Lon;
            var xj = polygon[j].Lat;
            var yj = polygon[j].Lon;

            var intersect = ((yi > lon) != (yj > lon)) &&
                            (lat < (xj - xi) * (lon - yi) / (yj - yi + double.Epsilon) + xi);
            if (intersect) inside = !inside;
        }
        return inside;
    }
}

public static class TehranPolygonsAreaHelper
{
    // TODO: Replace the placeholders below with real polygon coordinate lists for Tehran.
    // Polygons are lists of (Lat, Lon) points forming a closed ring (first != last is fine).
    private static readonly List<(double Lat, double Lon)> NorthPolygon = new()
    {
        // example placeholder points - REPLACE with real coords
        (35.80, 51.00),
        (35.85, 51.60),
        (35.70, 51.60),
        (35.65, 51.10)
    };

    private static readonly List<(double Lat, double Lon)> SouthPolygon = new()
    {
        // placeholder
        (35.50, 50.95),
        (35.60, 51.60),
        (35.65, 51.60),
        (35.55, 51.00)
    };

    private static readonly List<(double Lat, double Lon)> EastPolygon = new()
    {
        // placeholder
        (35.60, 51.40),
        (35.85, 51.70),
        (35.50, 51.70),
        (35.45, 51.40)
    };

    private static readonly List<(double Lat, double Lon)> WestPolygon = new()
    {
        // placeholder
        (35.45, 50.90),
        (35.80, 51.10),
        (35.60, 51.10),
        (35.50, 50.95)
    };

    private static readonly List<(double Lat, double Lon)> CenterPolygon = new()
    {
        // small center polygon - placeholder
        (35.68, 51.35),
        (35.70, 51.45),
        (35.66, 51.45),
        (35.65, 51.33)
    };

    public static ActivityArea GetActivityAreaFromPolygons(double lat, double lon)
    {
        if (GeoUtils.IsPointInPolygon(lat, lon, CenterPolygon)) return ActivityArea.Center;
        if (GeoUtils.IsPointInPolygon(lat, lon, NorthPolygon)) return ActivityArea.North;
        if (GeoUtils.IsPointInPolygon(lat, lon, SouthPolygon)) return ActivityArea.South;
        if (GeoUtils.IsPointInPolygon(lat, lon, EastPolygon)) return ActivityArea.East;
        if (GeoUtils.IsPointInPolygon(lat, lon, WestPolygon)) return ActivityArea.West;

        return ActivityArea.Whole; // not matched — outside or unknown
    }
}
