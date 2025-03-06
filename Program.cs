using ExClockify.Services;
using ExClockify.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

// could write a CustomService extenions method if more services are added 
builder.Services.AddControllers();
builder.Services.AddSingleton<DtoService>();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddJwtAuthentcationService(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerWithAuthorization();

var app = builder.Build();

// could also write middleware extensions method if more middleware are added
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

