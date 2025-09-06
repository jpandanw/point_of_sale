using FastEndpoints;
using Infrastructure.Db;
using Persistence;

using Microsoft.Data.SqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddMigrationService(connectionString ?? "");

builder.Services.AddTransient<AppDbContext>((_) => {

    // In real life you may read the configuration dynamically
    var connection = new SqlConnection(
        connectionString
    );

    var compiler = new SqlServerCompiler();

    var qf = new QueryFactory(connection, compiler);

    
    return new AppDbContext(qf);


});


builder.Services.AddFastEndpoints();

var app = builder.Build();

app.UseFastEndpoints();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    serviceProvider.MigrationServiceUp();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();