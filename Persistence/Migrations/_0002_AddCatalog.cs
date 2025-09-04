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
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("description").AsString().NotNullable();
        
            
        Create.Table("products").InSchema("catalog")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("description").AsString(255).NotNullable()
            .WithColumn("category_id").AsGuid().ForeignKey("fk_product_categories", "catalog", "categories","id")
            .WithColumn("status").AsString(16).NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("updated_at").AsDateTime().NotNullable();
        
        Create.Table("variants").InSchema("catalog")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("product_id").AsGuid().NotNullable().ForeignKey("fk_product_variants","catalog","products", "id")
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("description").AsString(255).NotNullable()
            .WithColumn("base_price").AsDecimal(10,2).NotNullable()
            .WithColumn("sku").AsString().Unique().Indexed("ix_variant_sku")
            .WithColumn("bar_code").AsString().Unique().Indexed("ix_variant_bar_code")
            .WithColumn("current_stocks").AsInt32().NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("updated_at").AsDateTime().NotNullable();
        
        Create.Table("price_modifiers").InSchema("catalog")
            .WithColumn("id").AsGuid().PrimaryKey()
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
        
        Create.Table("inventories").InSchema("catalog")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("quantity_change").AsInt32().NotNullable()
            .WithColumn("reason").AsInt32().NotNullable()
            .WithColumn("variant_id").AsGuid().ForeignKey("fk_product_inventory", "catalog", "variants", "id")
            .WithColumn("created_by").AsGuid().ForeignKey("fk_inventory_user", "auth", "users", "id")
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("updated_at").AsDateTime().NotNullable();
    }

    public override void Down()
    {

        // Drop foreign keys (must come before tables)
        Delete.ForeignKey("fk_product_categories").OnTable("products").InSchema("catalog");
        Delete.ForeignKey("fk_product_variants").OnTable("variants").InSchema("catalog");
        Delete.ForeignKey("fk_variant_price_modifiers").OnTable("variant_price_modifiers").InSchema("catalog");
        Delete.ForeignKey("fk_price_modifier_variant").OnTable("variant_price_modifiers").InSchema("catalog");
        Delete.ForeignKey("fk_product_inventory").OnTable("inventory").InSchema("catalog");

        // Drop indexes
        Delete.Index("ix_variant_sku").OnTable("variants").InSchema("catalog");
        Delete.Index("ix_variant_bar_code").OnTable("variants").InSchema("catalog");

        // Drop tables (reverse order of creation and FK dependency)
        Delete.Table("inventory").InSchema("catalog");
        Delete.Table("variant_price_modifiers").InSchema("catalog");
        Delete.Table("price_modifiers").InSchema("catalog");
        Delete.Table("variants").InSchema("catalog");
        Delete.Table("products").InSchema("catalog");
        Delete.Table("categories").InSchema("catalog");

        // Drop schema
        Delete.Schema("catalog");

    }

}