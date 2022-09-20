namespace CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Courses")]
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public DateTime? PublishedDate { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public int Level { get; set; }

        public decimal FullPrice { get; set; }

        [ForeignKey("Author")]
        public int? Author_Id { get; set; }
        [ForeignKey("Category")]
        public int? Category_Id { get; set; }
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
