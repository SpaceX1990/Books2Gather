﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books2Gather.Models {
    [Table("Books")] 
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("BookId")] 
        public int? BookId { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Title")]
        public string Title { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("ISBN")]
        public string ISBN { get; set; }

        [Column("PublishingDate")]
        public DateTime PublishingDate { get; set; }

        [Column("Prize")]
        public decimal Prize { get; set; }

        public Author Author { get; set; }

        public Genre Genre { get; set; }

        [Column("AuthorId")]
        public int? AuthorId { get; set; }

        [Column("GenreId")]
        public int? GenreId { get; set; }

        public string FullName => Author != null ? $"{Author.FirstName} {Author.LastName}".Trim() : "Unbekannter Autor";


    }
}