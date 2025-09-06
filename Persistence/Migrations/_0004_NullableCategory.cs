using FluentMigrator;

namespace Persistence.Migrations;

[Migration(0004)]
public class _0004_NullableCategory : Migration
{
    public override void Up()
    {
        Alter.Table("products")
            .InSchema("catalog")
            .AlterColumn("category_id").AsGuid().Nullable();
    }

    public override void Down()
    {
       Alter.Table("products")
           .InSchema("catalog")
           .AlterColumn("category_id").AsGuid().NotNullable(); 
    }
}