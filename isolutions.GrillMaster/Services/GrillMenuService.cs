using isolutions.GrillMaster.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace isolutions.GrillMaster.Services
{
    public class GrillMenuService : IGrillMenuService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public GrillMenuService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<GrillMenu>> GetGrillMenus()
        {
            var httpClient = httpClientFactory.CreateClient();
            var responseString =
                await httpClient.GetStringAsync("http://isol-grillassessment.azurewebsites.net/api/GrillMenu");
            var menus = JsonConvert.DeserializeObject<IEnumerable<GrillMenu>>(responseString)!.OrderBy(p => p.Menu);

            return menus;
        }
    }
}