namespace USTM_Library.Models
{
	public class ReservationIDGenerator
	{
		private static Random random = new Random();

		public static string GenerateReservationId()
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			char[] idChars = new char[8];

			for (int i = 0; i < idChars.Length; i++)
			{
				idChars[i] = chars[random.Next(chars.Length)];
			}

			return new string(idChars);
		}
	}
}
