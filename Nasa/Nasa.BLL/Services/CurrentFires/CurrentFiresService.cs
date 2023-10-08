using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using Nasa.BLL.Exceptions;
using Nasa.BLL.ServicesContracts;
using Nasa.Common.DTO.Coordinates;
using System.Globalization;
using System.Net.Http.Headers;
using Nasa.BLL.Extensions;

namespace Nasa.BLL.Services.CurrentFires
{
    public class CurrentFiresService : ICurrentFiresService
    {
        private readonly HttpClient _client = new HttpClient();
        private const string API_PATH = "https://firms.modaps.eosdis.nasa.gov/api/area/csv";
        private const string SOURCE_AND_AREA = "VIIRS_NOAA20_NRT/world";
        private readonly IConfiguration _configuration;

        public CurrentFiresService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<CoordinatesDto>> GetCurrentFires(DateTime date, int numberDays)
        {
            string mapKey = _configuration.GetValue<string>("MapKey");

            var path = string.Join("/", API_PATH, mapKey, SOURCE_AND_AREA, numberDays.ToString(), date.ToString("yyyy-MM-dd"));

            IEnumerable<CoordinatesDto> currentFires;

            using (var msg = new HttpRequestMessage(HttpMethod.Get, new Uri(path)))
            {
                msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/csv"));
                using (var resp = await _client.SendAsync(msg))
                {
                    if (!resp.IsSuccessStatusCode)
                        throw new CurrentFiresApiException();

                    using (var s = await resp.Content.ReadAsStreamAsync())
                    using (var sr = new StreamReader(s))
                    using (var futureoptionsreader = new CsvReader(sr, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        TrimOptions = TrimOptions.Trim,
                        Delimiter = ",",
                        PrepareHeaderForMatch = (PrepareHeaderForMatchArgs args) => args.Header.FirstCharacterToUpper()
                    }))
                    {
                        currentFires = futureoptionsreader.GetRecords<CoordinatesDto>().ToList();
                    }
                }
            }

            return currentFires;
        }
    }
}
