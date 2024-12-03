namespace WebApplication.Data

open WebApplication.Models
open Microsoft.Data.SqlClient
open System
open BCrypt.Net

module MotoristaRepository =    

    let createMotorista (connectionString: string) (motorista: Motorista) =
        use connection = new SqlConnection(connectionString)
        use command = new SqlCommand("""
            INSERT INTO Motorista (Nombre, Apellido, NumeroLicencia, Telefono, Direccion, FechaContratacion, FechaNacimiento, Estado, FechaRegistro, PasswordHash, Username)
            OUTPUT INSERTED.MotoristaId
            VALUES (@Nombre, @Apellido, @NumeroLicencia, @Telefono, @Direccion, @FechaContratacion, @FechaNacimiento, @Estado, @FechaRegistro, @PasswordHash, @Username)
            """, connection)
        let hashedPassword = BCrypt.HashPassword(motorista.PasswordHash)
        command.Parameters.AddWithValue("@Nombre", motorista.Nombre)
        command.Parameters.AddWithValue("@Apellido", motorista.Apellido)
        command.Parameters.AddWithValue("@NumeroLicencia", motorista.NumeroLicencia)

        match motorista.Telefono with
        | Some telefono -> command.Parameters.AddWithValue("@Telefono", telefono)
        | None -> command.Parameters.AddWithValue("@Telefono", DBNull.Value)

        match motorista.Direccion with
        | Some direccion -> command.Parameters.AddWithValue("@Direccion", direccion)
        | None -> command.Parameters.AddWithValue("@Direccion", DBNull.Value)

        command.Parameters.AddWithValue("@FechaContratacion", motorista.FechaContratacion)

        match motorista.FechaNacimiento with
        | Some fechaNacimiento -> command.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento)
        | None -> command.Parameters.AddWithValue("@FechaNacimiento", DBNull.Value)

        command.Parameters.AddWithValue("@Estado", motorista.Estado)

        match motorista.FechaRegistro with
        | Some fechaRegistro -> command.Parameters.AddWithValue("@FechaRegistro", fechaRegistro)
        | None -> command.Parameters.AddWithValue("@FechaRegistro", DBNull.Value)


        command.Parameters.AddWithValue("@PasswordHash", hashedPassword) 
        command.Parameters.AddWithValue("@Username", motorista.Username)

        connection.Open()
        command.ExecuteScalar() :?> int

    let validateCredentials (connectionString: string) (username: string) (password: string) : bool =
        use connection = new SqlConnection(connectionString)
        use command = new SqlCommand("""
            SELECT PasswordHash 
            FROM Motorista 
            WHERE Username = @Username
            """, connection)

        command.Parameters.AddWithValue("@Username", username)

        connection.Open()
        let passwordHash = command.ExecuteScalar()
        match passwordHash with
        | :? string as hash -> BCrypt.Verify(password, hash)
        | _ -> false