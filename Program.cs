var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookLoonService, BookLoonService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ApplicationDBContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.SetMinimumLevel(LogLevel.Information);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();