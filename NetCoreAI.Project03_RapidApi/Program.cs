

using NetCoreAI.Project03_RapidApi.ViewModels;
using Newtonsoft.Json;

var client = new HttpClient();

List<ApiSeriesViewModel> apiSeriesViewModels = new List<ApiSeriesViewModel>();

var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/series/"),
    Headers =
    {
        { "x-rapidapi-key", "2cd163a4d1mshadfc684ea01fde1p1192b3jsn7f0a16cc3b4d" },
        { "x-rapidapi-host", "imdb-top-100-movies.p.rapidapi.com" },
    },
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    // Json gelen veriyi, metne dönüştürme
    apiSeriesViewModels = JsonConvert.DeserializeObject<List<ApiSeriesViewModel>>(body);
    foreach (var series in apiSeriesViewModels)
    {
        Console.WriteLine(series.rank + " - " + series.title + " - Rating:" + series.rating + " Year:" + series.rating);
    }
}

