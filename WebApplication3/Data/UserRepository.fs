namespace WebApplication.Data

open Microsoft.Data.SqlClient
open WebApplication.Models

module UserRepository =

    let getUsers (connectionString: string) : User list =
        use connection = new SqlConnection(connectionString)
        use command = new SqlCommand("SELECT ID, Nombre FROM Usuario", connection)
        connection.Open()
        use reader = command.ExecuteReader()
        [
            while reader.Read() do
                {
                    Id = reader.GetInt32 0
                    Name = reader.GetString 1
                }
        ]