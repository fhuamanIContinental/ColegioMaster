using ColegioMaster.Api.Configuracion;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurarBaseDeDatos(builder.Configuration);
builder.Services.ConfigurarCifradoAes(builder.Configuration);

builder.Services.ConfigurarJwt(builder.Configuration);
builder.Services.ConfigurarFluentValidation();
builder.Services.ConfigurarDependenciasAplicacion();
builder.Services.ConfigurarSwagger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

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
