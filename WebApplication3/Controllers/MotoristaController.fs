namespace WebApplication3.Controllers

open Microsoft.AspNetCore.Mvc
open WebApplication.Data
open WebApplication.Models
open Microsoft.AspNetCore.Authorization

[<ApiController>]
[<Route("[controller]")>]
type MotoristaController(connectionString : string) =
    inherit ControllerBase()

    [<HttpPost>]
    member this.Create([<FromBody>] motorista: Motorista) : IActionResult =
        try
            let newMotoristaId = MotoristaRepository.createMotorista connectionString motorista
            this.CreatedAtAction(nameof(this.Create), 
                    [| "id" , newMotoristaId |], 
                    null)
        with
        | ex -> 
            this.StatusCode(500, $"Error al crear el motorista: {ex.Message}")

    [<HttpPost("login")>]
    member this.Login([<FromBody>] credentials: LoginCredentials) : IActionResult =
        try
            match MotoristaRepository.validateCredentials connectionString credentials.Username credentials.Password with
            | true -> 
                let token = JwtHelper.generateJwtToken credentials.Username
                this.Ok({| Token = token |})
            | false -> this.Unauthorized("Credenciales inválidas.")
        with
        | ex -> 
            this.StatusCode(500, $"Error al procesar la autenticación: {ex.Message}")