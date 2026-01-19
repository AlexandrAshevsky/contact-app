using Contact.API.Extensions;
using Contact.Application;
using Contact.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddContactCors();

builder.Services.AddContactDbContext(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseCors(ContactCorsExtension.AllowAllPolicyName);

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactApi");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
