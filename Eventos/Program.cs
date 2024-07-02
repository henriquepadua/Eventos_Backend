using csharp_Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (!File.Exists(DalHelper.DatabasePath))
{
    DalHelper.CriarBancoSQLite();
    DalHelper.CriarTabelaSQlite();
    Console.WriteLine("Banco de dados e tabela criados com sucesso.");
}
else
{
    Console.WriteLine("Banco de dados já existe.");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
