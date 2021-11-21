using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StarWarsApp.Pages
{
    public class Film
    {
        public string title { get; set; }
        public string episode_id { get; set; }
        public string opening_crawel { get; set; }
        public string director { get; set; }
        public string producer { get; set; }

        public string release_date { get; set; }

    }

    public class FilmsPage
    {
        public string next { get; set; }
        public string previous { get; set; }
        public List<Film> results { get; set; }
    }

    public class FilmsModel : PageModel
    {
        static HttpClient client = new HttpClient();
        public List<FilmsPage> Films { get; set; }

        public async Task<FilmsPage> GetFilmsPageAsync(string path)
        {
            FilmsPage filmsPage = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                filmsPage = await response.Content.ReadAsAsync<FilmsPage>();

            }
            return filmsPage;
        }
        public async Task OnGetAsync()
        {
            FilmsPage filmsPage = null;
            List<FilmsPage> films = new List<FilmsPage>();
            int count = 0;
            do
            {
                count++;
                filmsPage = await this.GetFilmsPageAsync($"https://swapi.dev/api/films/?page={count}");
                films.Add(
                    new FilmsPage
                    {
                        next = filmsPage.next,
                        previous = filmsPage.previous,
                        results = filmsPage.results
                    });
            } while (filmsPage.next != null);
            Films = films;
        }

    }
}
