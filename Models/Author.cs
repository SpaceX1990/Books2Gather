using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books2Gather.Models
{
    [Table("Autoren")] // Entspricht der SQL-Tabelle
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("AutorID")] // Entspricht der SQL-Spalte
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Vorname")] // Deutsche Spaltenbezeichnung
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Nachname")]
        public string LastName { get; set; }

        [Column("Geburtsdatum")] public DateTime? BirthDate { get; set; }

        [MaxLength(50)]
        [Column("Nationalitaet")]
        public string Nationality { get; set; }

        [Column("Biografie")] public string Biography { get; set; }

        [NotMapped] // Diese Eigenschaft wird NICHT in die Datenbank geschrieben
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
        
        public List<Book> Books { get; set; } = new List<Book>();

    }
}