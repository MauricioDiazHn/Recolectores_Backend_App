namespace WebApplication.Models

open System

type RecolectaEnc = {
    IdRecolecta: int
    OrdenCompraId: int
    Proveedor: string
    Direccion: string
    FechaRecolecta: DateTime
    HoraRecolecta: TimeSpan
    FechaAsignacion: DateTime option
    FechaAceptacion: DateTime option
    MotoristaId: int option
    IdVehiculo: int
    KmInicial: int
    KmFinal: int option
    TiempoEnSitio: int option
    EvaluacionProveedor: int option
    Comentario: string option
    Cantidad: int
    Estado: string option
    FechaRegistro: DateTime option
    TC: string option
    TituloTC: string option
}