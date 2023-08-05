using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiOnionAchitecture.API.Business;
using WebApiOnionAchitecture.API.Hubs;
using WebApiOnionArchitecture.Application.Abstraction.Services;
using WebApiOnionArchitecture.Domain.Entities.Identity;
using WebApiOnionArchitecture.Persistence.Contexts;
using WebApiOnionArchitecture.Persistence.Implementations.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Services.BuildServiceProvider().GetService<IConfiguration>().GetConnectionString("Default")
    )
);
builder.Services.AddIdentity<AppUser, IdentityRole>(identityOptions =>
{
    identityOptions.User.RequireUniqueEmail = true;
    identityOptions.Password.RequiredLength = 8;
    identityOptions.Lockout.MaxFailedAccessAttempts = 3;
    //identityOptions.SignIn.
})
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddSignalR();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AppDbContextInitialiser>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<MyBusiness>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(origin => true)));


//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowReactApp",
//        builder => builder.WithOrigins("http://localhost:3000")  // assuming your React app is running on port 3000
//                           .AllowAnyMethod()
//                           .AllowAnyHeader());
//});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var instance = scope.ServiceProvider.GetRequiredService<AppDbContextInitialiser>();
    await instance.InitialiseAsync();
    await instance.RoleSeedAsync();
    await instance.UserSeedAsync();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();
////app.UseCors("AllowReactApp");
//app.UseCors();
//app.UseEndpoints(endPoints =>
//{
//    endPoints.MapHub<MyHub>("/myhub");
//});

// V2
app.UseHttpsRedirection();
app.UseCors();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization(); 

app.UseEndpoints(endPoints =>
{
    endPoints.MapHub<MyHub>("/myhub");
    endPoints.MapControllers(); // Move this inside UseEndpoints
});

app.MapControllers();

app.Run();
