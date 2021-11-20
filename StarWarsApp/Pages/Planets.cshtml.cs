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
    public class PlanetsModel : PageModel
    {    
        static HttpClient client = new HttpClient();

        public async Task<Planet> GetPlanetAsync(string path)
        {
            Planet planet = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                planet = await response.Content.ReadAsAsync<Planet>();

            }
            return planet;
        }
        public async Task OnGetAsync()
        {
            Planet planet = null;
            int count = 0;
            do
            {
                count++;
                planet = await this.GetPlanetAsync($"https://swapi.dev/api/planets/{count}");
                if (planet != null)
                {
                    Console.WriteLine(planet.name);
                }
            } while (planet != null);
        }
    }
}
