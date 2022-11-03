
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class Departement
    {

        public Departement(int Id, string Nama, int DivisionID)
        {
            this.Id = Id;
            this.Nama = Nama;
            this.DivisionID = DivisionID;
        }

        public Departement()
        {

        }

        [Key]
        public int Id { get; set; }
        public string Nama { get; set; }

        [ForeignKey("Division")]
        public int DivisionID { get; set; }
        [JsonIgnore]
        public Division? Division { get; set; }
    }
}
