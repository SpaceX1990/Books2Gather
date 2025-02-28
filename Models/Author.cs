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

        public List<Book> Books { get; set; } = new();
    }
}
