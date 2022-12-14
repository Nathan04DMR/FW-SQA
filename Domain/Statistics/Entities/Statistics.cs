/* Statistics module - Asociación Lista y Valiente
 * Collaborators:
 * - Jimena Gdur
 * - Rodrigo Li
 * - Daniela Murillo
 * - Milen Rodriguez
 * - Jorim Wilson
 * 
 * - Summary: Implementation of Statistics class
 *   Stores all the user's statistics information.
 */

namespace Domain.Statistics.Entities
{
	public class Statistic 
	{
		/**  Attributes **/

		// Basic attributes

		public string User_Id { get; set; }
		public int  Donation_Amount{ get; set; }
		public int Order_Amount{ get; set; }
		public double Donated_Weight { get; set; }
		//public int App_Total_Donations { get; set; }

		/**  Methods **/

		// Basic constructor for Statistic
		public Statistic(string user_id, int donation_amount, int order_amount, double donated_weight)
		{
			User_Id = user_id;
			Donation_Amount = donation_amount;
			Order_Amount = order_amount;
			Donated_Weight = donated_weight;
			//App_Total_Donations = total_app_donations;
		}

		// Empty constructor for EFCore
		public Statistic()
		{
			User_Id = "";
			Donation_Amount = 0;
			Order_Amount = 0;
			Donated_Weight = 0;
		}
	}
}
