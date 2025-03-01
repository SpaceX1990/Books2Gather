using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books2Gather.Models
{
    [Table("Genres")]
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("GenreId")]
        public int? GenreId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Description")]
        public string Description { get; set; }
    }
}