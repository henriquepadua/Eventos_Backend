using csharp_Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Verifica e cria o banco de dados e tabelas antes de construir o aplicativo
if (!File.Exists(DalEvento.DatabasePath))
{
    DalEvento.CriarBancoSQLite();
    DalEvento.CriarTabelaSQlite();
    DalParticipante.CriarTabelaSQlite();
    Console.WriteLine("Banco de dados e tabelas criados com sucesso.");
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
