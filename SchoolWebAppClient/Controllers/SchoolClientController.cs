using Microsoft.AspNetCore.Mvc;
using SchoolWebAppClient.Models;

namespace SchoolWebAppClient.Controllers
{
    public class SchoolClientController : Controller
    {
        HttpClient _client;

        
        public SchoolClientController(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://localhost:7158/");
        }

        // Action GetAllSchools - Étape 4 du TP
        public async Task<IActionResult> GetAllSchools()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/Schools/get-all-schools");

                if (response.IsSuccessStatusCode)
                {
                    var schools = await response.Content.ReadFromJsonAsync<IEnumerable<SchoolClient>>();

                    if (schools == null || !schools.Any())
                    {
                        ViewBag.ErrorMessage = "L'API a répondu mais aucune école n'a été trouvée.";
                        return View(new List<SchoolClient>());
                    }

                    return View(schools);
                }
                else
                {
                    ViewBag.ErrorMessage = $"Erreur API : {response.StatusCode} - {response.ReasonPhrase}";
                    return View(new List<SchoolClient>());
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.ErrorMessage = $"Impossible de se connecter à l'API. Vérifiez que l'API est en cours d'exécution. Erreur : {ex.Message}";
                return View(new List<SchoolClient>());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Erreur inattendue : {ex.Message}";
                return View(new List<SchoolClient>());
            }
        }

        // Action GetSchoolById - Étape 5 du TP
        public async Task<IActionResult> GetSchoolById(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/Schools/get-school-by-id/{id}");

            if (response.IsSuccessStatusCode)
            {
                var school = await response.Content.ReadFromJsonAsync<SchoolClient>();
                return View(school);
            }

            return NotFound();
        }

        // Action GetSchoolByName - Étape 6 du TP
        public async Task<IActionResult> GetSchoolByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction(nameof(GetAllSchools));
            }

            HttpResponseMessage response = await _client.GetAsync($"api/Schools/search-by-name?name={name}");

            if (response.IsSuccessStatusCode)
            {
                var schools = await response.Content.ReadFromJsonAsync<IEnumerable<SchoolClient>>();
                return View(schools);
            }

            return View(new List<SchoolClient>());
        }

        // GET: CreateSchool - Étape 7 du TP
        public IActionResult CreateSchool()
        {
            return View();
        }

        // POST: CreateSchool - Étape 7 du TP
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSchool(SchoolClient school)
        {
            try
            {
                // Retirer la validation ModelState temporairement pour déboguer
                ModelState.Remove("Id"); // L'Id n'est pas requis pour la création

                if (!ModelState.IsValid)
                {
                    // Afficher les erreurs de validation
                    ViewBag.ErrorMessage = "Veuillez corriger les erreurs de validation.";
                    return View(school);
                }

                HttpResponseMessage response = await _client.PostAsJsonAsync("api/schools/create-school", school);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(GetAllSchools));
                }
                else
                {
                    ViewBag.ErrorMessage = $"Erreur lors de la création : {response.StatusCode}";
                    return View(school);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Erreur : {ex.Message}";
                return View(school);
            }
        }

        // GET: EditSchool - Étape 8 du TP (appelle GetSchoolById)
        public async Task<IActionResult> EditSchool(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/Schools/get-school-by-id/{id}");

            if (response.IsSuccessStatusCode)
            {
                var school = await response.Content.ReadFromJsonAsync<SchoolClient>();
                return View(school);
            }

            return NotFound();
        }

        // POST: EditSchool - Étape 8 du TP
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSchool(SchoolClient school)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("api/schools/edit-school/" + school.Id, school);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetAllSchools));
            }

            return View();
        }

        // GET: DeleteSchool - Étape 9 du TP (appelle GetSchoolById)
        public async Task<IActionResult> DeleteSchool(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/Schools/get-school-by-id/{id}");

            if (response.IsSuccessStatusCode)
            {
                var school = await response.Content.ReadFromJsonAsync<SchoolClient>();
                return View(school);
            }

            return NotFound();
        }

        // POST: DeleteSchool - Étape 9 du TP
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSchool(SchoolClient school)
        {
            HttpResponseMessage response = await _client.DeleteAsync("api/schools/delete-school/" + school.Id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetAllSchools));
            }

            return View();
        }
    }
}