using System.Data;
using SqlKata;
using SqlKata.Execution;

namespace Infrastructure.Db;

public class AppDbContext(QueryFactory queryFactory)
{
    private readonly QueryFactory _qFactory = queryFactory;

    public AuthQf Auth { get; } = new(queryFactory);
    public CatalogQf Catalog { get; } = new(queryFactory);
    public TransactionsQf Transactions { get; } = new(queryFactory);
    

    public IDbConnection GetDbConnection() => _qFactory.Connection;

    public class AuthQf (QueryFactory q)
    {
        public Query Users => q.Query("auth.users");
        public Query Roles => q.Query("auth.roles");
        public Query UserRoles => q.Query("auth.user_roles");
        public Query RolePermissions => q.Query("auth.role_permissions");
        public Query Profiles =>  q.Query("auth.profiles");
        public Query Permissions =>  q.Query("auth.permissions");
        public Query PasswordHashes => q.Query("auth.password_hashes");
        
    }

    public class CatalogQf (QueryFactory q)
    {
        public Query Categories => q.Query("catalog.categories");
        public Query Inventories => q.Query("catalog.inventories");
        public Query Products => q.Query("catalog.products");
        public Query PriceModifier => q.Query("catalog.price_modifiers");
        public Query Variants => q.Query("catalog.variants");
        public Query VariantPriceModifiers => q.Query("catalog.variant_price_modifiers");
    }
    
    public class TransactionsQf (QueryFactory q)
    {
        public Query SaleItems => q.Query("transactions.sale_items");
        public Query Sales => q.Query("transactions.sales");
        public Query SalesTotalModifier => q.Query("transactions.sales_total_modifier");
        public Query SalesTotal => q.Query("transactions.sales_total");
    }

}