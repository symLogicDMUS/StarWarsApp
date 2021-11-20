using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;


namespace StarWarsApp.Pages
{
    public class Planet
    {
        public string name { get; set; }

        public string rotation_period { get; set; }

        public string orbital_period { get; set; }

        public string diameter { get; set; }

        public string climate { get; set; }

        public string gravity { get; set; }

        public string terrain { get; set; }

        public string surface_water { get; set; }

        public string population { get; set; }
    }

    public class PlanetPage
    {
        public string next { get; set; }
        public string previous { get; set; }
        public List<Planet> results { get; set; }
    }


    public class PlanetsModel : PageModel
    {
        static HttpClient client = new HttpClient();
        public List<PlanetPage> Planets {get; set;}

        public async Task<PlanetPage> GetPlanetPageAsync(string path)
        {
            PlanetPage planetPage = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                planetPage = await response.Content.ReadAsAsync<PlanetPage>();

            }
            return planetPage;
        }
        public async Task OnGetAsync()
        {
            PlanetPage planet = null;
            List <PlanetPage> planets = new List<PlanetPage>();
            int count = 0;
            do
            {
                count++;
                planet = await this.GetPlanetPageAsync($"https://swapi.dev/api/planets/?page={count}");
                planets.Add(
                    new PlanetPage { 
                        next = planet.next, 
                        previous = planet.previous, 
                        results = planet.results 
                    });
            } while (planet.next != null);
            Planets = planets;
        }
    }
}
