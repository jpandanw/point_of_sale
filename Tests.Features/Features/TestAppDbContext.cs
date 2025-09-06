using Infrastructure.Db;
using Microsoft.Data.SqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace Tests.Features.Features;

public class TestAppDbContext
{
    public static AppDbContext Get()
    {
        var sql = new SqlConnection("Server=localhost;Database=master;User Id=sa;Password=Password1234!;Encrypt=False");
        var db = new QueryFactory(sql, new SqlServerCompiler());
        return new AppDbContext(db);
    }
}