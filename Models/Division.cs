using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Division
    {
        public Division(int Id, string Nama)
        {
            this.Id = Id;
            this.Nama = Nama;
        }

        public Division()
        {

        }

        [Key]
        public int Id { get; set; }
        public string Nama { get; set; }
    }
}
