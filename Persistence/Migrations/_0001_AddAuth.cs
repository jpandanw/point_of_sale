using FluentMigrator.Builders;

namespace Persistence.Migrations;

using FluentMigrator;


[Migration( 0001)]
public class AddAuth : Migration{
    public override void Up()
    {
        Create.Schema("auth");

        Create.Table("users").InSchema("auth")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("username").AsString(255).NotNullable()
            .WithColumn("created_at").AsDateTime()
            .WithColumn("updated_at").AsDateTime();

        Create.Table("password_hashes").InSchema("auth")
            .WithColumn("user_id").AsGuid().ForeignKey("fk_user_password_hashes","auth", "users", "id")
            .WithColumn("hash").AsString(255).NotNullable();

        Create.Table("profiles").InSchema("auth")
            .WithColumn("user_id").AsGuid().ForeignKey("fk_user_profiles","auth", "users", "id").PrimaryKey()
            .WithColumn("first_name").AsString(255).NotNullable()
            .WithColumn("last_name").AsString(255).NotNullable()
            .WithColumn("birthdate").AsDate();

        Create.Table("roles").InSchema("auth")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("role_name").AsString(255).NotNullable()
            .WithColumn("created_at").AsDateTime()
            .WithColumn("updated_at").AsDateTime();


        Create.Table("permissions").InSchema("auth")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("name").AsString(127).NotNullable();

        Create.Table("role_permissions").InSchema("auth")
            .WithColumn("role_id").AsGuid().ForeignKey("fk_role_permission","auth" ,"roles", "id")
            .WithColumn("permission_id").AsGuid().ForeignKey("fk_permission_role", "auth" ,"permissions", "id");

        Create.Table("user_roles").InSchema("auth")
            .WithColumn("user_id").AsGuid().ForeignKey("fk_users_user_roles", "auth","users", "id")
            .WithColumn("role_id").AsGuid().ForeignKey("fk_roles_user_roles", "auth" , "roles", "id");
        
    }

    public override void Down()
    {

        
        // Drop foreign keys first (important for order)
        Delete.ForeignKey("fk_user_password_hashes").OnTable("password_hashes").InSchema("auth");
        Delete.ForeignKey("fk_user_profiles").OnTable("profiles").InSchema("auth");
        Delete.ForeignKey("fk_role_permission").OnTable("role_permissions").InSchema("auth");
        Delete.ForeignKey("fk_permission_role").OnTable("role_permissions").InSchema("auth");
        Delete.ForeignKey("fk_users_user_roles").OnTable("user_roles").InSchema("auth");
        Delete.ForeignKey("fk_roles_user_roles").OnTable("user_roles").InSchema("auth");

// Drop tables in reverse order of dependencies
        Delete.Table("user_roles").InSchema("auth");
        Delete.Table("role_permissions").InSchema("auth");
        Delete.Table("permissions").InSchema("auth");
        Delete.Table("roles").InSchema("auth");
        Delete.Table("profiles").InSchema("auth");
        Delete.Table("password_hashes").InSchema("auth");
        Delete.Table("users").InSchema("auth");

// Drop schema
        Delete.Schema("auth");

    }
}



