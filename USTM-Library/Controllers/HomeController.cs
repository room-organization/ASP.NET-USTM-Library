using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using USTM_Library.Data;
using USTM_Library.Models;

namespace USTM_Library.Controllers
{
    public class HomeController : Controller
    {
     
        private readonly ILogger<HomeController> _logger;

       private readonly string endpoint = "https://localhost:7213/api/Bibliographies";
       private readonly HttpClient client = null;
		private static List<bibliography> bibliographies = null;

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
                     bibliographies = JsonConvert.DeserializeObject<List<bibliography>>(jsonString);

                 }
                 else
                 {
                     ModelState.AddModelError(null, "Erro");
                 }
                 return View(bibliographies);
             }
             catch (Exception ex)
             {
                 string message = ex.Message;
                 throw ex;
             }
         

        }
        //method to show the details of the book
		public IActionResult Details(int? id)
		{
            bibliography book = null;

			foreach (bibliography b in bibliographies)
            {

                if(b.Id == id){
                     book = b;
				} 
            }
            return View(book);
		}

        public IActionResult About()
        {
            return View();
        }  public IActionResult ReadingPage(int? id)
        {
			bibliography book = null;

			foreach (bibliography b in bibliographies)
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
    }
}