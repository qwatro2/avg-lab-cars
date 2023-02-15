namespace UtilsLib;

public class CsvReader
{
    private string _path;

    public CsvReader(string path)
    {
        _path = path;
    }
    
    public async Task<List<string []>> Read()
    {
        var models = new List<string []>();
        using (var sr = new StreamReader(_path))
        {
            await sr.ReadLineAsync();
            while (await sr.ReadLineAsync() is { } line)
            {
                var data = new string[7];
                var dataIndex = 0;
                var i = 0;
                var cur = "";
                var inQuotes = false;
                while (i < line.Length)
                {
                    switch (line[i])
                    {
                        case ',' when !inQuotes:
                            data[dataIndex] = cur;
                            dataIndex++;
                            cur = "";
                            break;
                        case '"':
                            inQuotes = !inQuotes;
                            break;
                        default:
                            cur += line[i];
                            break;
                    }

                    i++;
                }

                data[dataIndex] = cur;
                models.Add(data);
            }
        }

        return models;
    }
}