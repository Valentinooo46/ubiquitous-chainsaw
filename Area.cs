using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NovaPoshta
{
    [Table("areas")]
    public class Area
    {
        [Key] public int id { get; set; }
        [Required, StringLength(36)][JsonProperty("Ref")] public string _ref { get; set; } = null!;
        [Required, StringLength(36)][JsonProperty("AreasCenter")] public string areasCenter { get; set; } = null!;
        [Required, StringLength(36)][JsonProperty("Description")] public string description { get; set; } = null!;
    }
    [Table("cities")]
    public class City
    {
        [Key] public int id { get; set; }
        [Required, StringLength(36)][JsonProperty("Ref")] public string _ref { get; set; } = null!;
        [Required, StringLength(50)][JsonProperty("Description")] public string description { get; set; } = null!;
        [Required, StringLength(36)][JsonProperty("Area")] public string areaRef { get; set; } = null!;
        [Required, StringLength(36)][JsonProperty("SettlementTypeDescription")] public string areaDescription { get; set; } = null!;
    }
    [Table("warehouses")]
    public class Warehouse
    {
        [Key] public int id { get; set; }
        [Required, StringLength(36)][JsonProperty("Ref")] public string _ref { get; set; } = null!;
        [Required, StringLength(50)][JsonProperty("Description")] public string description { get; set; } = null!;
        [Required, StringLength(10)][JsonProperty("SiteKey")] public string SiteKey { get; set; } = null!;
        [Required, StringLength(36)][JsonProperty("Number")] public string number { get; set; } = null!;
    }
}