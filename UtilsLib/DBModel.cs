namespace UtilsLib;

public record DBModel(long Id,
    string Model,
    int Year,
    string Status,
    int? Mileage,
    int? Price,
    string? MSRP)
{
    public DBModel(long id, string model, int year, string status, string mileage, string price, string msrp) :
        this(id,
            model,
            year,
            status,
            mileage == "Not available" ? null : int.Parse(mileage),
            int.Parse(price[2..^1]),
            msrp == "Not specified" ? null : msrp) { }
    
    
    public static DBModel Create(string[] data)
    {
        Console.WriteLine(string.Join('\t', data));
        
        var id = long.Parse(data[0]);
        var model = data[1];
        var year = int.Parse(data[2]);
        var status = data[3];
        int? mileage = data[4] == "Not available" ? null : int.Parse(data[4][..^4].Replace(",",""));
        int? price = data[5] == "Not Priced" ? null : int.Parse(data[5][1..].Replace(",", ""));
        var msrp = data[6] == "Not specified" ? null : data[6];
        
        return new DBModel(id, model, year, status, mileage, price, msrp);
    }
    
}