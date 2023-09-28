namespace USTM_Library.Models
{
	public class Reservation
	{
		public string Id { get; set; }
		public string StudentCode { get; set; }
		public string BookTitle { get; set; }
		public DateTime date { get; set; }

		public Reservation() { }
	}
}
