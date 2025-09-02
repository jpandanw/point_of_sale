using FluentMigrator.Builders;

namespace Persistence.Migrations;

using FluentMigrator;


public class AddAuth : Migration{
    public override void Up()
    {
        Create.Schema("auth");

        Create.Table("users").InSchema("auth")
            .WithColumn("id").AsGuid().PrimaryKey().Identity()
            .WithColumn("username").AsString(255).NotNullable()
            .WithColumn("created_at").AsDateTime()
            .WithColumn("updated_at").AsDateTime();

        Create.Table("password_hashes").InSchema("auth")
            .WithColumn("user_id").AsGuid().ForeignKey("fk_user_password_hashes","auth", "users", "auth")
            .WithColumn("hash").AsString(255).NotNullable();

        Create.Table("profiles").InSchema("auth")
            .WithColumn("user_id").AsGuid().ForeignKey("fk_user_profiles","auth", "users", "id").PrimaryKey()
            .WithColumn("first_name").AsString(255).NotNullable()
            .WithColumn("last_name").AsString(255).NotNullable()
            .WithColumn("birthdate").AsDate();

        Create.Table("roles").InSchema("auth")
            .WithColumn("id").AsGuid().PrimaryKey().Identity()
            .WithColumn("role_name").AsString(255).NotNullable()
            .WithColumn("created_at").AsDateTime()
            .WithColumn("updated_at").AsDateTime();

        Create.Table("role_permissions").InSchema("auth")
            .WithColumn("id").AsGuid().PrimaryKey().Identity()
            .WithColumn("role_id").AsGuid().ForeignKey("fk_role_permission","auth" ,"role", "id")
            .WithColumn("permission_id").AsGuid().ForeignKey("fk_permission_role", "auth" ,"permission", "id");

        Create.Table("user_roles").InSchema("auth")
            .WithColumn("id").AsGuid().PrimaryKey().Identity()
            .WithColumn("user_id").AsGuid().ForeignKey("users", "auth", "id")
            .WithColumn("role_id").AsGuid().ForeignKey("roles", "auth", "id");
        
    }

    public override void Down()
    {
        Delete.Table("user_roles").IfExists().InSchema("auth");
        Delete.Table("role_permissions").IfExists().InSchema("auth");
        Delete.Table("roles").IfExists().InSchema("auth");
        Delete.Table("profiles").IfExists().InSchema("auth");
        Delete.Table("password_hashes").IfExists().InSchema("auth");
        Delete.Table("users").InSchema("auth");
        Delete.Schema("auth");
    }
}



