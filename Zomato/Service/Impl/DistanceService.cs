using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using Zomato.Service;

public class DistanceServiceOSRMImpl: IDistanceService
{
    private static readonly string OSRM_API_BASE_URL = "https://router.project-osrm.org/route/v1/driving/";
    private readonly HttpClient _httpClient;

    public DistanceServiceOSRMImpl(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<double> CalculateDistance(Point src, Point dest)
    {
        try
        {
            string uri = $"{OSRM_API_BASE_URL}{src.X},{src.Y};{dest.X},{dest.Y}?overview=false";

            Console.WriteLine($"OSRM API Request URI: {uri}");

            // Fetch the response using HttpClient
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode(); // Throws if not successful

            string rawJsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Raw OSRM Response: {rawJsonResponse}");

            // Deserialize JSON response
            OSRMResponseDto responseDto = JsonConvert.DeserializeObject<OSRMResponseDto>(rawJsonResponse);

            if (responseDto?.Routes == null || responseDto.Routes.Count == 0)
            {
                throw new Exception("No routes found in OSRM response.");
            }

            return responseDto.Routes[0].Distance / 1000.0; // Convert meters to kilometers
        }
        catch (Exception e)
        {
            throw new Exception("Error getting data from OSRM: " + e.Message, e);
        }
    }
}
public class OSRMResponseDto
{
    [JsonProperty("routes")]
    public List<OSRMRoute> Routes { get; set; }
}

public class OSRMRoute
{
    [JsonProperty("distance")]
    public double Distance { get; set; }
}
