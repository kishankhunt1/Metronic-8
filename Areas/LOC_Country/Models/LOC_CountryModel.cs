

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Metronic_8.Areas.LOC_Country.Models
{
	public class LOC_CountryModel
	{
		public int? CountryID { get; set; }
		[Required]
        [DisplayName("Country Name")]
        public string CountryName { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public int StateCount { get; set; }
		public int CityCount { get; set; }
        public int UserID { get; set; }
    }

	public class CountryDropDown
	{
		public int CountryID { get; set; }
		public string? CountryName { get; set; }
	}
}
