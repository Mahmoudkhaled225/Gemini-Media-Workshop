using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;


[Table("images")]
public class Img : BaseEntity
{
    public string ImgURL { get; set; }
    public string PublicId { get; set; }

}