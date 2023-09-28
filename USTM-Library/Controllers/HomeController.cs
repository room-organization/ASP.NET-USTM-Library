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
        }  
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




	}
}