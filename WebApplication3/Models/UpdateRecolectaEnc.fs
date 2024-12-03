namespace WebApplication.Models

open System

type UpdateRecolectaEnc = {
    FechaAceptacion: DateTime option
    Comentario: string
    Estado: string
}
