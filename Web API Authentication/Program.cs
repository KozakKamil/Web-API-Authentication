using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_API_Authentication.Model;
using Web_API_Authentication.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme="Bearer",
        BearerFormat="JWT",
        In=Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name="Authorization",
        Description="Auth with JWT",
        Type=Microsoft.OpenApi.Models.SecuritySchemeType.Http
    });
    options.AddSecurityRequirement( new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IUserInterface, UserService>();
builder.Services.AddSingleton<IGameService, GameService>();

var app = builder.Build();
app.UseSwagger();
app.UseAuthorization();
app.UseAuthentication();

// Swagger UI
app.UseSwaggerUI();

// Map endpoints
app.MapGet("/", () => "Hello World!").ExcludeFromDescription();

app.MapPost("/login",
    ([FromBody] UserLogin user, [FromServices] IUserInterface service) => Login(user, service)).Accepts<UserLogin>("application/json").Produces<string>();
app.MapPost("/create", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    ([FromBody] Game game, [FromServices] IGameService service) => Create(game, service)).Accepts<Game>("application/json").Produces<Game>(statusCode:200, contentType:"application/json");
app.MapGet("/get/{id}", 
    (int id, [FromServices] IGameService service) => Get(id, service)).Produces<Game>();
app.MapGet("/list", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, User")]
([FromServices] IGameService service) => List(service)).Produces<List<Game>>(statusCode:200,contentType:"application/json");
app.MapPut("/update", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
([FromBody] Game game, [FromServices] IGameService service) => Update(game, service)).Accepts<Game>("application/json");
app.MapDelete("/delete/{id}", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
(int id, [FromServices] IGameService service) => Delete(id, service));

// Methods for API logic
IResult Login(UserLogin user, IUserInterface service)
{
    if(!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
    {
        var loggedInUser = service.Get(user);
        if (loggedInUser is null)
        {
            return Results.NotFound("Error");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, loggedInUser.UserName),
            new Claim(ClaimTypes.Role, loggedInUser.Role),
            new Claim(ClaimTypes.Email, loggedInUser.EmailAddress),
            new Claim(ClaimTypes.GivenName, loggedInUser.GivenName),
            new Claim(ClaimTypes.Surname, loggedInUser.SurName)
        };

        var token = new JwtSecurityToken(
            issuer: builder.Configuration["Jwt:Issuer"],
            audience: builder.Configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256)
            );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Results.Ok(tokenString);
    }

    return Results.NotFound("Error");
}

IResult Create(Game game, IGameService service)
{
    var result = service.Create(game);
    return Results.Ok(result);
}

IResult Get(int id, IGameService service)
{
    var game = service.Get(id);
    return game is null
        ? Results.NotFound("Game not found")
        : Results.Ok(game);
}

IResult List(IGameService service)
{
    var games = service.List();
    return Results.Ok(games);
}

IResult Update(Game game, IGameService service)
{
    var updatedGame = service.Update(game);
    return updatedGame is null
        ? Results.NotFound("Game not found")
        : Results.Ok(updatedGame);
}

IResult Delete(int id, IGameService service)
{
    var result = service.Delete(id);
    return result
        ? Results.Ok()
        : Results.NotFound("Game not found");
}

// Run the application
app.Run();
