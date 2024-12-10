using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Serilog;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseSerilog((ctx, conf) =>
//{
//    conf.ReadFrom.Configuration(ctx.Configuration);
//});

builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddPolicy("MyTestPolicy", policy =>
{
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));

builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyTestPolicy");
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (!context.User.Identity?.IsAuthenticated ?? false)
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Not Authenticated");
    }
    else await next();

});

app.Run();


