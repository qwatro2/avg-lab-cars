using System.Globalization;
using Dapper;
using Npgsql;

namespace UtilsLib;

public static class DBConverter
{
    private const string InsertQuery = @"
                INSERT INTO public.cars (id_by_model, model, year, status, mileage, price, msrp)
                VALUES (@Id, @Model, @Year, @Status, @Mileage, @Price, @MSRP);";
    
    public static async Task ConvertDB(string dbPath, string connectionString)
    {
        
        var reader = new CsvReader(dbPath);
        var models = (await reader.Read()).Select(DBModel.Create).ToArray();
        using (var conn = new NpgsqlConnection(connectionString))
        {
            await conn.OpenAsync();
            foreach (var model in models)
            {
                await conn.ExecuteAsync(InsertQuery, model);
            }

            await conn.DisposeAsync();
        }
    }
    
    public static async Task InitDB(string connectionString)
    {
        const string query = @"
            CREATE TABLE IF NOT EXISTS public.cars (
                id BIGSERIAL PRIMARY KEY,
                id_by_model BIGINT NOT NULL,
                model VARCHAR(255) NOT NULL,
                year INTEGER NOT NULL,
                status VARCHAR(255) NOT NULL,
                mileage INTEGER,
                price INTEGER,
                msrp VARCHAR(255)
            );";
        var conn = new NpgsqlConnection(connectionString);
        await conn.ExecuteAsync(query);
    }
}