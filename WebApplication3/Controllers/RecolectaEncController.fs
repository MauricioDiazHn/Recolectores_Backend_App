namespace WebApplication3.Controllers

open Microsoft.AspNetCore.Mvc
open WebApplication.Data
open WebApplication.Models
open Microsoft.AspNetCore.Authorization

[<ApiController>]
[<Route("[controller]")>]
[<Authorize>]
type RecolectaEncController(connectionString : string) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.GetAll() =
        RecolectaEncRepository.getRecolectaEnc connectionString |> this.Ok

    [<HttpGet("{motoristaId}")>]
    member this.GetByMotoristaId(motoristaId: int) =
        RecolectaEncRepository.getRecolectaEncByMotoristaId connectionString motoristaId |> this.Ok

    [<HttpPut("{idRecolecta}")>]
    member this.Update(idRecolecta: int, [<FromBody>] updateData: UpdateRecolectaEnc) =
        let fechaAceptacion = updateData.FechaAceptacion
        let comentario = updateData.Comentario
        let estado = updateData.Estado
        RecolectaEncRepository.updateRecolectaEnc connectionString idRecolecta fechaAceptacion comentario estado
        |> this.Ok