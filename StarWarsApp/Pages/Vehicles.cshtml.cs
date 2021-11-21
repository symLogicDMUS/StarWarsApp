using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StarWarsApp.Pages
{
    public class Vehicles
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
        public string vehicle_class { get; set; }

    }

    public class VehiclesPage
    {
        public string next { get; set; }
        public string previous { get; set; }
        public List<Vehicles> results { get; set; }
    }

    public class VehiclesModel : PageModel
    {
        static HttpClient client = new HttpClient();
        public List<VehiclesPage> Vehicles { get; set; }

        public async Task<VehiclesPage> GetVehiclesPageAsync(string path)
        {
            VehiclesPage vehiclesPage = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                vehiclesPage = await response.Content.ReadAsAsync<VehiclesPage>();

            }
            return vehiclesPage;
        }
        public async Task OnGetAsync()
        {
            VehiclesPage vehiclesPage = null;
            List<VehiclesPage> vehicles = new List<VehiclesPage>();
            int count = 0;
            do
            {
                count++;
                vehiclesPage = await this.GetVehiclesPageAsync($"https://swapi.dev/api/vehicles/?page={count}");
                vehicles.Add(
                    new VehiclesPage
                    {
                        next = vehiclesPage.next,
                        previous = vehiclesPage.previous,
                        results = vehiclesPage.results
                    });
            } while (vehiclesPage.next != null);
            Vehicles = vehicles;
        }

    }
}
