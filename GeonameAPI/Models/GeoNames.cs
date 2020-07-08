using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeonameAPI.Models
{
    public class GeoNames
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int geonameid { get; set; }
        public string name { get; set; }
        public string asciiname { get; set; }
        public string alternatenames { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string feature_class { get; set; }
        public string feature_code { get; set; }
        public string country_code { get; set; }
        public string cc2 { get; set; }
        public string admin1_code { get; set; }
        public string admin2_code { get; set; }
        public string admin3_code { get; set; }
        public string admin4_code { get; set; }
        public long population { get; set; }
        public int elevation { get; set; }
        public int gtopo30 { get; set; }
        public int timezone { get; set; }
        public DateTime modification_date { get; set; }
       
    }
    [NotMapped]
    public class JsonSearchCities
    {
        public int geonameid { get; set; }
        public string name { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public float Score { get; set; }
    }
}