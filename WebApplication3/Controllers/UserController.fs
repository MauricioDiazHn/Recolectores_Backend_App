namespace WebApplication3.Controllers

open Microsoft.AspNetCore.Mvc
open WebApplication.Data

[<ApiController>]
[<Route("[controller]")>]
type UsersController(connectionString : string) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.Get() =
        UserRepository.getUsers connectionString |> this.Ok