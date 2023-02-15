using UtilsLib;

const string connectionString = "Username=postgres;Password=postgres;Server=localhost;Database=postgres;Port=5432";
const string dbPath = "car_data.csv"; // должно рядом с бинарником лежать
await DBConverter.InitDB(connectionString);
await DBConverter.ConvertDB(dbPath, connectionString);