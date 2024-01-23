using System;

namespace CapstoneProject.Models;

public record CapstoneSummarized (
    DateTime Date, 
    long Temperature, 
    long Humidity
);