using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text;
using USTM_Library.Data;
using USTM_Library.Models;

namespace USTM_Library.Controllers
{
    public class HomeController : Controller
    {
     
        private readonly ILogger<HomeController> _logger;

       private readonly string endpoint = "https://localhost:7213/api/Bibliographies";
       private readonly HttpClient client = null;

		public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

           client = new HttpClient();
            client.BaseAddress = new Uri(endpoint);
        }

        public async Task<IActionResult> IndexAsync()
        {
             try
             {
                 var response = await client.GetAsync(endpoint);

                 if (response.IsSuccessStatusCode)
                 {
                     var jsonString = await response.Content.ReadAsStringAsync();

                     //converte o json q recebe da api e converte em um objeto
                     Bibliography.bibliographies = JsonConvert.DeserializeObject<List<Bibliography>>(jsonString);

                 }
                 else
                 {
                     ModelState.AddModelError(null, "Erro");
                 }
                 return View(Bibliography.bibliographies);
             }
             catch (Exception ex)
             {
                 string message = ex.Message;
                 throw ex;
             }
         

        }
      

        public IActionResult About()
        {
            return View();
        }  public IActionResult ReadingPage(int? id)
        {
			Bibliography book = null;

			foreach (Bibliography b in Bibliography.bibliographies)
			{

				if (b.Id == id)
				{
					book = b;
				}
			}
			return View(book);
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




		[HttpPost]
		public async Task<IActionResult> PostReservationAsync([FromForm] Reservation reservation)
		{
			// Configure o URL da API Swagger
			string apiUrl = "https://localhost:7213/api/Reservations";
			client.BaseAddress = new Uri(apiUrl);

			// Configure os cabeçalhos, como token de autenticação
			client.DefaultRequestHeaders.Add("Authorization", "Bearer seu-token-de-autenticacao");

			// Converta seu objeto em JSON
			string jsonData = JsonConvert.SerializeObject(reservation);

			// Configure o conteúdo da solicitação
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			// Envie a solicitação POST
			HttpResponseMessage response = await client.PostAsync(apiUrl, content);

			// Verifique a resposta da API (códigos de status, etc.)
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return RedirectToAction("About");
			}

		}

	}
}