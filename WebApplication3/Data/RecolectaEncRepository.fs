
namespace WebApplication.Data

open WebApplication.Models
open Microsoft.Data.SqlClient
open System
open System.Data

module RecolectaEncRepository =
    let getRecolectaEnc (connectionString: string) : RecolectaEnc list =
        use connection = new SqlConnection(connectionString)
        use command = new SqlCommand("SELECT * FROM RecolectaEnc", connection)
        connection.Open()
        use reader = command.ExecuteReader()
        [
            while reader.Read() do
                {
                    IdRecolecta = reader.GetInt32 0
                    OrdenCompraId = reader.GetInt32 1
                    Proveedor = reader.GetString 2
                    Direccion = reader.GetString 3
                    FechaRecolecta = reader.GetDateTime 4
                    HoraRecolecta = reader.GetTimeSpan 5
                    FechaAsignacion = if reader.IsDBNull 6 then None else Some (reader.GetDateTime 6)
                    FechaAceptacion = if reader.IsDBNull 7 then None else Some (reader.GetDateTime 7)
                    MotoristaId = if reader.IsDBNull 8 then None else Some (reader.GetInt32 8)
                    IdVehiculo = reader.GetInt32 9
                    KmInicial = reader.GetInt32 10
                    KmFinal = if reader.IsDBNull 11 then None else Some (reader.GetInt32 11)
                    TiempoEnSitio = if reader.IsDBNull 12 then None else Some (reader.GetInt32 12)
                    EvaluacionProveedor = if reader.IsDBNull 13 then None else Some (reader.GetInt32 13)
                    Comentario = if reader.IsDBNull 14 then None else Some (reader.GetString 14)
                    Cantidad = reader.GetInt32 15
                    Estado = if reader.IsDBNull 16 then None else Some (reader.GetString 16)
                    FechaRegistro = if reader.IsDBNull 17 then None else Some (reader.GetDateTime 17)
                    TC = if reader.IsDBNull 18 then None else Some (reader.GetString 18)
                    TituloTC = if reader.IsDBNull 19 then None else Some (reader.GetString 19)
                }
        ]

    let getRecolectaEncByMotoristaId (connectionString: string) (motoristaId: int) : RecolectaEnc list =
        use connection = new SqlConnection(connectionString)
        use command = new SqlCommand("SELECT * FROM RecolectaEnc WHERE MotoristaId = @MotoristaId", connection)
        command.Parameters.AddWithValue("@MotoristaId", motoristaId)
        connection.Open()
        use reader = command.ExecuteReader()
        [
            while reader.Read() do
                {
                    IdRecolecta = reader.GetInt32 0
                    OrdenCompraId = reader.GetInt32 1
                    Proveedor = reader.GetString 2
                    Direccion = reader.GetString 3
                    FechaRecolecta = reader.GetDateTime 4
                    HoraRecolecta = reader.GetTimeSpan 5
                    FechaAsignacion = if reader.IsDBNull 6 then None else Some (reader.GetDateTime 6)
                    FechaAceptacion = if reader.IsDBNull 7 then None else Some (reader.GetDateTime 7)
                    MotoristaId = if reader.IsDBNull 8 then None else Some (reader.GetInt32 8)
                    IdVehiculo = reader.GetInt32 9
                    KmInicial = reader.GetInt32 10
                    KmFinal = if reader.IsDBNull 11 then None else Some (reader.GetInt32 11)
                    TiempoEnSitio = if reader.IsDBNull 12 then None else Some (reader.GetInt32 12)
                    EvaluacionProveedor = if reader.IsDBNull 13 then None else Some (reader.GetInt32 13)
                    Comentario = if reader.IsDBNull 14 then None else Some (reader.GetString 14)
                    Cantidad = reader.GetInt32 15
                    Estado = if reader.IsDBNull 16 then None else Some (reader.GetString 16)
                    FechaRegistro = if reader.IsDBNull 17 then None else Some (reader.GetDateTime 17)
                    TC = if reader.IsDBNull 18 then None else Some (reader.GetString 18)
                    TituloTC = if reader.IsDBNull 19 then None else Some (reader.GetString 19)
                }
        ]

    let updateRecolectaEnc (connectionString: string) (idRecolecta: int) (fechaAceptacion: DateTime option) (comentario: string) (estado: string) =
        use connection = new SqlConnection(connectionString)
        use command = new SqlCommand("""
            UPDATE RecolectaEnc 
            SET FechaAceptacion = @FechaAceptacion, Comentario = @Comentario, Estado = @Estado
            WHERE IdRecolecta = @IdRecolecta
            """, connection)

        command.Parameters.AddWithValue("@IdRecolecta", idRecolecta)
        let parameterFechaAceptacion = new SqlParameter("@FechaAceptacion", SqlDbType.DateTime)
        parameterFechaAceptacion.Value <-
            match fechaAceptacion with
            | Some fecha -> fecha :> obj
            | None -> DBNull.Value
        command.Parameters.Add(parameterFechaAceptacion)
        command.Parameters.AddWithValue("@Comentario", comentario)
        command.Parameters.AddWithValue("@Estado", estado)

        connection.Open()
        command.ExecuteNonQuery() |> ignore