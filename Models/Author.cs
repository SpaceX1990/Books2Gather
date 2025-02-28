using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books2Gather.Models
{
    [Table("Authors")] 
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("AuthorId")] 
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("FirstName")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("LastName")]
        public string LastName { get; set; }

        //[Column("Geburtsdatum")] public DateOnly? BirthDate { get; set; }

        //[MaxLength(50)]
        //[Column("Nationalitaet")]
        //public string Nationality { get; set; }

        //[Column("Biografie")] public string Biography { get; set; }

        //[NotMapped]
        //public string FullName
        //{
        //    get { return $"{FirstName} {LastName}"; }
        //}

        //public List<Book> Books { get; set; } = new List<Book>();

    }
}