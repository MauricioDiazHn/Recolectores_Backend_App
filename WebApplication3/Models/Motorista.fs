namespace WebApplication.Models

open System

type Motorista = {
    MotoristaId: int
    Nombre: string
    Apellido: string
    NumeroLicencia: string
    Telefono: string option
    Direccion: string option
    FechaContratacion: DateTime
    FechaNacimiento: DateTime option
    Estado: string
    FechaRegistro: DateTime option
    PasswordHash: string
    Username: string
}

type LoginCredentials = {
    Username: string
    Password: string
}