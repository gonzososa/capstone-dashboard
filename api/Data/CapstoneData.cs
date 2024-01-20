using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CapstoneProject.Models;
using Microsoft.Data.SqlClient;

namespace CapstoneProject.Data;

public class CapstoneData {

    public static async Task<IEnumerable<Capstone>> GetAllData () {
        var connectionString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTION_STRING");
        var connection = new SqlConnection (connectionString);
        await connection.OpenAsync ();

        List<Capstone> result = new List<Capstone>();
        using var command = new SqlCommand (GetAllQuery (), connection);
        var reader = await command.ExecuteReaderAsync (CommandBehavior.CloseConnection);

        while (reader.Read ()) {
            var item = new Capstone (
                Id: (int) reader ["id"],
                DeviceId: (long) reader ["deviceId"],
                TimeStamp: (DateTime) reader ["timestamp"],
                Temperature: (long) reader ["temperature"],
                Humidity: (long) reader ["humidity"],
                Latitude: (double) reader["latitude"],
                Longitude: (double) reader ["longitude"]
            );

            result.Add (item);
        }

        await reader.CloseAsync ();
        return result;
    }

    private static string GetAllQuery () {
        return @"
            SELECT id, deviceId, timestamp, temperature, humidity, latitude, longitude 
            FROM temperature_humidity
        ";
    }

}