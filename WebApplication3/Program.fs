namespace WebApplication3
#nowarn "20"
open ParametrosJwt

open System.Text

open WebApplication.Data

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.IdentityModel.Tokens

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddControllers()

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(fun options ->
                options.TokenValidationParameters <- TokenValidationParameters(
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtConfig.issuer,
                    ValidAudience = JwtConfig.audience,
                    IssuerSigningKey = SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.secretKey))
                )
            ) |> ignore

        let connectionString = "Server=192.168.6.3;Database=SERP_ENERCOM;User Id=b1UserAPI;Password=Eterno.2020$$;TrustServerCertificate=True"
        
        builder.Services.AddSingleton<string>(connectionString)

        let app = builder.Build()

        app.UseHttpsRedirection()

        app.UseAuthentication()
        app.UseAuthorization()
        app.MapControllers()

        app.Run()

        exitCode
