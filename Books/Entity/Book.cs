using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Books.Entity
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }

        public int Pages { get; set; }
        /*[ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }*/
    }
}
