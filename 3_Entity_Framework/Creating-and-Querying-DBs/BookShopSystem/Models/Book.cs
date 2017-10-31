using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.Models
{
    public enum EditionTypeEnum
    {
        Normal,
        Promo,
        Gold
    }
    public enum AgeRestEnum
    {
        Minor,
        Teen,
        Adult
    }
    public class Book
    {
        public Book()
        {
            this.Categories = new HashSet<Category>();
            this.RelatedBooks = new HashSet<Book>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public EditionTypeEnum EditionType { get; set; }

        public decimal Price { get; set; }

        public int Copies { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [Required]
        public virtual Author Author { get; set; }

        public AgeRestEnum AgeRestriction { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        [ForeignKey("Author")]
        public int Author_Id { get; internal set; }

        public virtual ICollection<Book> RelatedBooks { get; set; }
    }
}
