using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StarWarsApp.Pages
{

    public class Spaceship
    {
        public string name { get; set; }
        public string model { get; set; }
        public string manufacturer { get; set; }
        public string cost_in_credits { get; set; }
        public string length { get; set; }
        public string max_atmosphering_speed { get; set; }
        public string crew { get; set; }
        public string passengers { get; set; }
        public string cargo_capacity { get; set; }
        public string consumables { get; set; }
        public string hyperdrive_rating { get; set; }
        public string MGLT { get; set; }
        public string starship_class { get; set; }
    }
    public class SpaceshipsPage
    {
        public string next { get; set; }
        public string previous { get; set; }
        public List<Spaceship> results { get; set; }

    }
    public class SpaceshipsModel : PageModel
    {
        static HttpClient client = new HttpClient();

        public List<SpaceshipsPage> Spaceships { get; set; }

        public async Task<SpaceshipsPage> GetSpaceshipsPageAsync(string path)
        {
            SpaceshipsPage spaceshipsPage = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                spaceshipsPage = await response.Content.ReadAsAsync<SpaceshipsPage>();
            }
            return spaceshipsPage;
        }
        public async Task OnGetAsync()
        {
            int count = 0;
            List<SpaceshipsPage> spaceships = new List<SpaceshipsPage>();
            SpaceshipsPage spaceshipsPage = null;
            do
            {
                count++;
                spaceshipsPage = await this.GetSpaceshipsPageAsync($"https://swapi.dev/api/starships/?page={count}");
                spaceships.Add(
                    new SpaceshipsPage { 
                        next = spaceshipsPage.next, 
                        previous = spaceshipsPage.previous,
                        results = spaceshipsPage.results,
                });
            } while (spaceshipsPage.next != null);
            Spaceships = spaceships;
        }
    }
}
