using System;

namespace CapstoneProject.Models;
public record Capstone (
    int Id,
    long DeviceId, 
    DateTime TimeStamp, 
    long Temperature, 
    long Humidity,
    double Latitude,
    double Longitude
);