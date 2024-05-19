
using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Entities.Mappers;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Domain.Interfaces.Atividade;
using Gerenciador.Domain.Interfaces.Projeto;
using Gerenciador.Domain.Interfaces.TarefasComentario;
using Gerenciador.Infra.Data.Context;
using Gerenciador.Infra.Data.Repository;
using Gerenciador.Service.Services;
using Gerenciador.Service.Utils;
using Gerenciador.Service.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

builder.Services.AddDbContext<GerenciadorContext>(options =>
{
    options.UseNpgsql(config.GetConnectionString("postgres"));
    options.EnableSensitiveDataLogging(); 
    options.EnableDetailedErrors();
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = false
    };
});
builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAuthorization();

//validation
builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<Tarefa>, TarefaValidator>();
builder.Services.AddScoped<IValidator<Projeto>, ProjetoValidator>();

//mapper
builder.Services.AddSingleton<DtoEntityMapper<TarefaDto, Tarefa>>();
builder.Services.AddScoped<IEntityDtoMapper<Tarefa, TarefaDto>, TarefaMapper>();
builder.Services.AddScoped<IEntityDtoMapper<TarefaComentario, TarefaComentarioDto>, TarefaComentarioMapper>();
builder.Services.AddScoped<IEntityDtoMapper<Atividade, AtividadeDto>, AtividadeMapper>();
builder.Services.AddScoped<IEntityDtoMapper<User, CadastroDto>, CadastroMapper>();
builder.Services.AddScoped<IEntityDtoMapper<Projeto, ProjetoDto>, ProjetoMapper>();

//repository
builder.Services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<ITarefaComentarioRepository, TarefaComentarioRepository>();
builder.Services.AddScoped<IAtividadeRepository, AtividadeRepository>();
builder.Services.AddScoped<IProjetoRepository, ProjetoRepository>();

//services
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IBaseService<User>, BaseService<User>>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITarefaService, TarefaService>();
builder.Services.AddScoped<ITarefaComentarioService, TarefaComentarioService>();
builder.Services.AddScoped<IAtividadeService, AtividadeService>();
builder.Services.AddScoped<IProjetoService, ProjetoService>();

var app = builder.Build();
app.UseCors("AllowAnyOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();