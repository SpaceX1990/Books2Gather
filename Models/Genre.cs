using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books2Gather.Models
{
    [Table("Genres")]
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("GenreID")]
        public int GenreId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Beschreibung")]
        public string Description { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}