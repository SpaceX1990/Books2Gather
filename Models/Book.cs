using System;
using System.Collections.Generic;
using System.Linq;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books2Gather.Models
{
    [Table("Buecher")] 
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("BuchID")] 
        public int BookId { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Titel")]
        public string Title { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("ISBN")]
        public string ISBN { get; set; }

        [Column("Erscheinungsdatum")]
        public DateOnly PublishingDate { get; set; }

        [Column("Preis")]
        public decimal Prize { get; set; }

        public List<Author> Authors { get; set; } = new List<Author>();
        
        public List<Genre> Genres { get; set; } = new List<Genre>();


        public string AuthorNames => string.Join(", ", AuthorList.Select(a => $"{a.Firstname} {a.Lastname}"));
        public string GenreNames => string.Join(", ", GenreList.Select(g => g.Description));
    }
}
