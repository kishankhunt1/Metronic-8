using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Metronic_8.Areas.LOC_City.Models
{
	public class LOC_CityModel
	{
		public int? CityID { get; set; }
		[Required]
		[DisplayName("City Name")]
		public string? CityName { get; set; }
		[Required]
		[DisplayName("Country Name")]
		public int? CountryID { get; set; }
		public string? CountryName { get; set; }
		[Required]
		[DisplayName("State Name")]
		public int? StateID { get; set; }
		public string? StateName { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
        public int UserID { get; set; }
    }
}
