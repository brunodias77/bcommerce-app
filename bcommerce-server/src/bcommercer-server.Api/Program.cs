
using bcommercer_server.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// ====================
// CONFIGURAÇÃO DE SERVIÇOS
// ====================

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// ====================
// PIPELINE HTTP
// ====================

ConfigureMiddleware(app);

app.Run();


// ====================
// MÉTODOS LOCAIS
// ====================

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Serviços da camada de infraestrutura e aplicação
    services.AddInfrastructure(builder.Configuration);
    services.AddApplication(builder.Configuration);

    // Serviços MVC e Swagger
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    // Configuração de CORS
    services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend", policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
            // .AllowCredentials(); // Descomente se usar autenticação via cookies
        });
    });

    // Configuração global do Dapper
    Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
}

void ConfigureMiddleware(WebApplication app)
{
    // Aplicação da política de CORS
    app.UseCors("AllowFrontend");

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}

// using bcommercer_server.Api.Configurations;
//
// var builder = WebApplication.CreateBuilder(args);
//
// // ⬇️ Adiciona os serviços da infraestrutura e da aplicação
// builder.Services.AddInfrastructure(builder.Configuration);
// builder.Services.AddApplication(builder.Configuration);
//
// // ⬇️ Adiciona os serviços de Controllers, API Explorer e Swagger
// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
//
// // ✅ CONFIGURAÇÃO DE CORS
// // Aqui definimos uma política de CORS chamada "AllowFrontend"
// // que permite requisições da origem http://localhost:3000 (frontend React)
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowFrontend", policy =>
//     {
//         policy.WithOrigins("http://localhost:3000") // Altere se seu frontend estiver em outro domínio
//             .AllowAnyHeader()
//             .AllowAnyMethod();
//         // .AllowCredentials(); // Descomente se estiver usando autenticação via cookies
//     });
// });
// Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
//
// var app = builder.Build();
//
// // ✅ APLICA A POLÍTICA DE CORS NO PIPELINE HTTP
// app.UseCors("AllowFrontend");
//
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// app.UseHttpsRedirection();
//
// app.UseAuthorization();
//
// app.MapControllers();
//
// app.Run();
//
