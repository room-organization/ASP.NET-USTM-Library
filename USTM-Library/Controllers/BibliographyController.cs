using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using USTM_Library.Models;

namespace USTM_Library.Controllers
{


	public class BibliographyController : Controller
	{

		private readonly HttpClient client = null;

		private List<Bibliography> bibliographies = Bibliography.bibliographies;

		Reservation reserve; 

		public BibliographyController()
		{
		client = new HttpClient();
		reserve = new Reservation();
		}

		

		//method to show the details of the book
		public IActionResult Details(int? id)
		{
			Bibliography book = null;

			foreach (Bibliography b in bibliographies)
			{

				if (b.Id == id)
				{
					book = b;
				}
			}
			return View(book);
		}

		//method to show the read book page
		public IActionResult ReadingPage(int? id)
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


		//Method to add new Book reservation
		[HttpPost]
		public async Task<IActionResult> PostReservationAsync([FromForm] Reservation reservation)
		{
			

			string apiUrl = "https://localhost:7213/api/Reservations";
			client.BaseAddress = new Uri(apiUrl);

			client.DefaultRequestHeaders.Add("Authorization", "Bearer seu-token-de-autenticacao");

			string jsonData = JsonConvert.SerializeObject(reservation);

			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await client.PostAsync(apiUrl, content);

			if (response.IsSuccessStatusCode)
			{
				reserve.Id = reservation.Id;
				reserve.StudentCode = reservation.StudentCode;
				reserve.BookTitle = reservation.BookTitle;
				reserve.date = DateTime.UtcNow;

				TempData["Reserve"] = JsonConvert.SerializeObject(reserve);

				return RedirectToAction(nameof(ReservationDone));
			}
			else
			{
				return RedirectToAction(nameof(Details));
			}

		}

		public IActionResult ReservationDone()
		{
			var reserveJson = TempData["Reserve"] as string;

			if (!string.IsNullOrEmpty(reserveJson))
			{
				// Deserializa o JSON de volta para um objeto 'Reservation'
				var reserve = JsonConvert.DeserializeObject<Reservation>(reserveJson);

				// Faça o que você precisa com 'reserve' aqui
				return View(reserve);
			}
			else
			{
				// Caso não haja dados em TempData, lide com isso de acordo com sua lógica
				return RedirectToAction(nameof(Details));
			}
		}
}
}

