using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace StarWarsApp.Pages
{
 
    public class Person
    {
        public string name { get; set; }
        public string gender { get; set; }
        public string eye_color { get; set; }
        public string mass { get; set; }

        public string hair_color { get; set;}

        public string birth_year { get; set; }

        public string height { get; set; }

        public List<string> films { get; set; }

        public List<string> species { get; set; }

        public List<string> vehicles { get; set; }

        public List<string> starships { get; set; }

        public string url { get; set; }

    }

    public class PersonPage
    {
        public string next { get; set; }
        public string previous { get; set; }
        public List<Person> results { get; set; }

    }

    public class PeopleModel : PageModel
    {
        static HttpClient client = new HttpClient();

        public List<PersonPage> People {get; set;}

        public async Task<PersonPage> GetPeoplePageAsync(string path)
        {
            PersonPage personPage = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                personPage = await response.Content.ReadAsAsync<PersonPage>();
            }
            return personPage;
        }
        
        public async Task OnGetAsync()
        {
            int count = 0;
            PersonPage personPage = null;
            List<PersonPage> people = new List<PersonPage>();
            do
            {
                count++;
                personPage = await this.GetPeoplePageAsync($"https://swapi.dev/api/people/?page={count}");
                people.Add(new
                    PersonPage
                {
                    results = personPage.results,
                    next = personPage.next,
                    previous = personPage.previous,
                });
            } while (personPage.next != null);
            People = people;
        }
    }
}