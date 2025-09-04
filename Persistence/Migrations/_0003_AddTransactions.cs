using FluentMigrator;

namespace Persistence.Migrations;

[Migration(0003)]
public class _0003_AddSales :  Migration
{
    public override void Up()
    {
        Create.Schema("transactions");

        Create.Table("sales").InSchema("transactions")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("user_id").AsGuid().NotNullable().ForeignKey("fk_sale_user", "auth", "users", "id")
            .WithColumn("total_amount").AsDecimal(10, 2).NotNullable()
            .WithColumn("status").AsString(32).NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("updated_at").AsDateTime().NotNullable() ;

        Create.Table("sale_items").InSchema("transactions")
            .WithColumn("product_id").AsGuid().ForeignKey("fk_sales_sale_item","catalog", "products", "id").NotNullable()
            .WithColumn("quantity").AsInt32().NotNullable()
            .WithColumn("sale_id").AsGuid().ForeignKey("fk_sale_items_sale","transactions", "sales", "id").NotNullable()
            .WithColumn("sale_price").AsDecimal(10, 2).NotNullable()
            .WithColumn("modifier_value").AsDecimal(10, 2);
        
        
        Create.Table("total_modifiers").InSchema("transactions")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("name").AsString(100).NotNullable()
            .WithColumn("description").AsString().NotNullable()
            .WithColumn("modifier_value").AsDecimal(10, 2).NotNullable()
            .WithColumn("modifier_type").AsString(32).NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("updated_at").AsDateTime().NotNullable();


        Create.Table("sales_total_modifier").InSchema("transactions")
            .WithColumn("sale_id").AsGuid().ForeignKey("fk_sale_total_modifier", "transactions", "sales", "id")
            .WithColumn("modifier_id").AsGuid().ForeignKey("fk_total_modifier_sale", "transactions", "sales", "id");
    }

    public override void Down()
    {
        // Drop foreign keys first
        Delete.ForeignKey("fk_sale_user").OnTable("sales").InSchema("transactions");
        Delete.ForeignKey("fk_sales_sale_item").OnTable("sale_items"); // No schema for sale_items table, assuming default schema
        Delete.ForeignKey("fk_sale_items_sale").OnTable("sale_items");
        Delete.ForeignKey("fk_sale_total_modifier").OnTable("sales_total_modifier").InSchema("transactions");
        Delete.ForeignKey("fk_total_modifier_sale").OnTable("sales_total_modifier").InSchema("transactions");

// Drop tables in reverse order of dependencies
        Delete.Table("sales_total_modifier").InSchema("transactions");
        Delete.Table("total_modifiers").InSchema("transactions");
        Delete.Table("sale_items"); // No schema was explicitly defined in the creation, assuming default schema
        Delete.Table("sales").InSchema("transactions");

// Drop schema
        Delete.Schema("transactions");
 
        
    }
}