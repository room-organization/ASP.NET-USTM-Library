using Microsoft.AspNetCore.Mvc;
using USTM_Library.Models;

namespace USTM_Library.Controllers
{


	public class BibliographyController : Controller
	{

		private List<Bibliography> bibliographies = Bibliography.bibliographies;
		public IActionResult Index()
		{
			return View();
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

	}
}
