using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Metronic_8.Areas.LOC_State.Models
{
	public class LOC_StateModel
	{
		public int? StateID { get; set; }
		[Required]
		[DisplayName("State Name")]
		public string? StateName { get; set; }
		[Required]
		[DisplayName("Country Name")]
		public int? CountryID { get; set; }
		public string? CountryName { get; set; }
		public DateTime? Created { get; set; }
		public DateTime? Modified { get; set; }

		public int? UserID {  get; set; }
	}

	public class StateDropDown
	{
		public int StateID { get; set; }
		public string? StateName { get; set; }
	}
}
