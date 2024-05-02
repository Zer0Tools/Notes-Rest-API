
using Zer0Tools.NotesWebAPI.API;
using Zer0Tools.NotesWebAPI.Repositories;
using Zer0Tools.NotesWebAPI.Mappings;


var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services);

var app = builder.Build();
Configure(app);

var apis = app.Services.GetServices<API>();
foreach(var api in apis)
{
    if(api is null) throw new InvalidProgramException("Api not found");
    api.Register(app);
}
app.Run();


void RegisterServices(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddDbContext<ApplicationContext>(options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
    });    

    services.AddScoped<NotesRepository>();  
    services.AddTransient<API, NoteAPI>(); 
    services.AddAutoMapper(typeof(NoteMappingProfile));
    services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });



}

void Configure(WebApplication app)
{
    
    if(app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        using var scope = app.Services.CreateScope();

        var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        applicationContext.Database.EnsureCreated();              
    } 
    app.UseHttpsRedirection();
}
