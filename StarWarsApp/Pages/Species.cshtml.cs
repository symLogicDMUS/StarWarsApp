using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StarWarsApp.Pages
{
    public class Species
    {
        public string name { get; set; }
        public string classification { get; set; }
        public string designation { get; set; }
        public string average_height { get; set; }
        public string skin_colors { get; set; }
        public string hair_colors { get; set; }
        public string eye_colors { get; set; }
        public string average_lifespan { get; set; }
        public string language { get; set; }

    }

    public class SpeciesPage
    {
        public string next { get; set; }
        public string previous { get; set; }
        public List<Species> results { get; set; }
    }

    public class SpeciesModel : PageModel
    {
        static HttpClient client = new HttpClient();
        public List<SpeciesPage> Species { get; set; }

        public async Task<SpeciesPage> GetSpeciesPageAsync(string path)
        {
            SpeciesPage speciesPage = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                speciesPage = await response.Content.ReadAsAsync<SpeciesPage>();

            }
            return speciesPage;
        }
        public async Task OnGetAsync()
        {
            SpeciesPage speciesPage = null;
            List<SpeciesPage> species = new List<SpeciesPage>();
            int count = 0;
            do
            {
                count++;
                speciesPage = await this.GetSpeciesPageAsync($"https://swapi.dev/api/species/?page={count}");
                species.Add(
                    new SpeciesPage
                    {
                        next = speciesPage.next,
                        previous = speciesPage.previous,
                        results = speciesPage.results
                    });
            } while (speciesPage.next != null);
            Species = species;
        }

    }
}
