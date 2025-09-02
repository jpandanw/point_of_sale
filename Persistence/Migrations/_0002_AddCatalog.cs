using System.ComponentModel.DataAnnotations.Schema;
using FluentMigrator;

namespace Persistence.Migrations;

[Migration(0002)]
public class _0002_AddCatalog : Migration
{
    public override void Up()
    {
        Create.Schema("catalog");

        Create.Table("categories").InSchema("catalog")
            .WithColumn("id").AsGuid().Identity().PrimaryKey()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("description").AsString().NotNullable();
        
            
        Create.Table("products").InSchema("catalog")
            .WithColumn("id").AsGuid().Identity().PrimaryKey()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("description").AsString(255).NotNullable()
            .WithColumn("category").AsGuid().ForeignKey("fk_product_categories", "categories", "id","id")
            .WithColumn("status").AsString(16).NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("updated_at").AsDateTime().NotNullable();
        
        Create.Table("variants").InSchema("catalog")
            .WithColumn("id").AsGuid().Identity().PrimaryKey()
            .WithColumn("product_id").AsGuid().NotNullable().ForeignKey("fk_product_variants","catalog","products", "id")
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("description").AsString(255).NotNullable()
            .WithColumn("base_price").AsDecimal(10,2).NotNullable()
            .WithColumn("sku").AsString().Unique().Indexed()
            .WithColumn("bar_code").AsString().Unique().Indexed()
            .WithColumn("current_stocks").AsInt32().NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("updated_at").AsDateTime().NotNullable();
        
        Create.Table("price_modifiers").InSchema("catalog")
            .WithColumn("id").AsGuid().Identity().PrimaryKey()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("description").AsString(255).NotNullable()
            .WithColumn("is_default").AsBoolean().WithDefaultValue(false)
            .WithColumn("modifier_value").AsDecimal(10,2).NotNullable()
            .WithColumn("modifier_type").AsString(16).NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("updated_at").AsDateTime().NotNullable();

        Create.Table("variant_price_modifiers").InSchema("catalog")
            .WithColumn("variant_id").AsGuid().ForeignKey("fk_variant_price_modifiers", "catalog", "variants", "id")
            .WithColumn("price_modifier_id").AsGuid()
            .ForeignKey("fk_price_modifier_variant", "catalog", "price_modifiers", "id");
        
        Create.Table("inventory").InSchema("catalog")
            .WithColumn("id").AsGuid().Identity().PrimaryKey()
            .WithColumn("quantity_change").AsInt32().NotNullable()
            .WithColumn("reason").AsInt32().NotNullable()
            .WithColumn("variant_id").AsGuid().ForeignKey("fk_product_variants", "catalog", "variants", "id")
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("updated_at").AsDateTime().NotNullable();
    }

    public override void Down()
    {
       
        Delete.Table("inventory").IfExists().InSchema("catalog");
        Delete.Table("variant_price_modifiers").IfExists().InSchema("catalog");
        Delete.Table("price_modifiers").IfExists().InSchema("catalog");
        Delete.Table("variants").IfExists().InSchema("catalog");
        Delete.Table("products").IfExists().InSchema("catalog");
        Delete.Table("categories").IfExists().InSchema("catalog");

        Delete.Schema("catalog");
    } 
    
}