using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CapstoneProject.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CapstoneProject.Data;

public class CapstoneData {

    public static async Task<IEnumerable<Capstone>> GetAllData (ILogger logger) {
        var connectionString = Environment.GetEnvironmentVariable ("AZURE_SQL_CONNECTION_STRING");
        SqlConnection connection = null;

        try {
            connection = new SqlConnection (connectionString);
            await connection.OpenAsync ();

            List<Capstone> result = new ();
            using SqlCommand command = new (GetAllQuery (), connection);
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
        catch (SqlException e) {
            logger.LogError (e, 
                "An error occured while querying database", 
                new object[] {connectionString});

            throw;
        }
        finally {
            if (connection?.State != ConnectionState.Closed) {
                connection?.Close ();
            }
        }
    }

    public static async Task<IEnumerable<CapstoneSummarized>> GetDataByDates (DateTime start, DateTime end, 
        bool summarize, ILogger logger) {
        var connectionString = Environment.GetEnvironmentVariable ("AZURE_SQL_CONNECTION_STRING");
        SqlConnection connection = null;        
        
        try {
            connection = new SqlConnection (connectionString);
            await connection.OpenAsync ();            
            
            using SqlCommand command = new ("dbo.usp_GetDataByDates", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add (new SqlParameter ("@p_StartDate", SqlDbType.DateTime));
            command.Parameters.Add (new SqlParameter ("@p_EndDate", SqlDbType.DateTime));
            command.Parameters.Add (new SqlParameter ("@p_Summarize", SqlDbType.Int));
            command.Parameters ["@p_StartDate"].Value = start;
            command.Parameters ["@p_EndDate"].Value = end;
            command.Parameters ["@p_Summarize"].Value = summarize;

            var data = new List<CapstoneSummarized>();
            var reader = await command.ExecuteReaderAsync (CommandBehavior.CloseConnection);
            
            while (reader.Read ()) {
                var item = new CapstoneSummarized (
                    Date: (DateTime) reader ["date"],
                    Temperature: (long) reader ["temperature"],
                    Humidity: (long) reader ["humidity"]
                );

                data.Add (item);
            }

            return data;
        }
        catch (SqlException e) {
            logger.LogError (e, 
                "An error occured while executing stored procedure: dbo.GetDataByDates", 
                new object[] {start, end, summarize});

            throw;
        }
        finally {
            if (connection?.State != ConnectionState.Closed) {
                connection?.Close ();
            }
        }
    }

    private static string GetAllQuery () {
        return @"
            SELECT id, deviceId, timestamp, temperature, humidity, latitude, longitude 
            FROM temperature_humidity
        ";
    }    

}