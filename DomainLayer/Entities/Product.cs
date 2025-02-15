using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.Entities;

[Table("products")]

[Index(nameof(Name), IsUnique = false)]
public class Product : BaseEntity
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    public string? VideoUrl { get; set; }

    
    // SP that will make it auto increment
    // CREATE PROCEDURE GenerateSKU
    //     @ProductId INT
    //     AS
    // BEGIN
    //     DECLARE @sku NVARCHAR(50)
    // SET @sku = 'Prod-' + CAST(@ProductId AS NVARCHAR(50))
    //
    // UPDATE Products
    // SET SKU = @sku
    // WHERE Id = @ProductId
    // END
    
    // Trigger that will call the SP
        // CREATE TRIGGER trgCallGenerateSKU
        //     ON Products
        //     AFTER INSERT
        //     AS
        // BEGIN
        //     DECLARE @id INT
        // SELECT @id = INSERTED.Id FROM INSERTED
        //
        // EXEC GenerateSKU @ProductId = @id
        //     END
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string? SKU { get; set; } = string.Empty;


    public string? ImgUrl { get; set; }
    public string? PublicId { get; set; }

    public int CategoryId { get; set; }
    public List<Category> Categories { get; set; }
    
    public List<Bundle> Bundles { get; set; }

    
    
}