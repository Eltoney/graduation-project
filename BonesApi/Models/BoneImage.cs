using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonesApi.Models
{
    [Table("BoneImages")]
    public class BoneImage {
        
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        public string imageName { get; set; } = null!;

        [StringLength(50)]
        public string path { get; set; } = null!;
    }
}