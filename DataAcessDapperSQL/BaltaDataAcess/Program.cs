using System.Data;
using BaltaDataAcess.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BaltaDataAcess;

public class Program
{
    static void Main(string[] args)
    {
        const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true";

        #region DataConnection(ADO)
        // using (var connection = new SqlConnection(connectionString))
        // {
        //     Console.WriteLine("Connected!");
        //     connection.Open();
        //     using (var command = new SqlCommand())
        //     {
        //         command.Connection = connection;
        //         command.CommandType = CommandType.Text;
        //         command.CommandText = "SELECT [Id], [Title] FROM [Category]";
        //         var reader = command.ExecuteReader();

        //         while (reader.Read())
        //         {
        //             Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
        //         }
        //     }
        // }
        #endregion

        #region DataConnection(Dapper)

        using (var connection = new SqlConnection(connectionString))
        {
            Console.WriteLine("Connected!");
            // GetCategory(connection);
            // UpdateCategory(connection);
            // CreateManyCategory(connection);
            // ListCategories(connection);
            // CreateCategory(connection);
            // DeleteCategory(connection);
            // ExecuteProcedure(connection);
            // ExecureReadProcedure(connection);
            ExecuteScalar(connection);
        }
        #endregion
    }

    static void GetCategory(SqlConnection connection)
    {
        var id = new Guid("58bbacb9-5577-4c9e-98c8-a5505aec6d6f");
        var getQuery = "SELECT [Id], [Title] FROM [Category] WHERE [Id] = @id";
        var category = connection
            .QueryFirstOrDefault<Category>(getQuery, new { id });
        Console.WriteLine($"{category.Id} - {category.Title}");
    }

    static void ListCategories(SqlConnection connection)
    {
        var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category] ORDER BY [Order] ASC");
        foreach (var item in categories)
        {
            Console.WriteLine($"{item.Id} - {item.Title}");
        }
    }

    static void CreateCategory(SqlConnection connection)
    {
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "Amazon AWS";
        category.Url = "amazon.com";
        category.Description = "Categoria destinada a serviços AWS";
        category.Order = 8;
        category.Summary = "AWS Cloud";
        category.Featured = false;

        var insertSql = @"INSERT INTO [Category] ([Id],[Title],[Url],[Summary],[Order],[Description],[Featured])
                    VALUES(
                        @Id,
                        @Title,
                        @Url,
                        @Summary,
                        @Order,
                        @Description,
                        @Featured)";

        var rows = connection.Execute(insertSql, new
        {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        });
        Console.WriteLine($"{rows} linhas inseridas.");
    }

    static void UpdateCategory(SqlConnection connection)
    {
        var updateQuery = "UPDATE [Category] SET [Title] = @title WHERE [Id] = @id";
        var rows = connection.Execute(updateQuery, new
        {
            id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
            title = "Frontend 2023"
        });
        Console.WriteLine($"{rows} registro(s) atualizado(s)");
    }

    static void DeleteCategory(SqlConnection connection)
    {
        var deleteQuery = "DELETE [Category] WHERE [Id] = @id";

        var id = new Guid("c86ec836-958c-4af8-96f8-3ff983d90803");

        var rows = connection.Execute(deleteQuery, new
        {
            id
        });
        Console.WriteLine($"Registro id {id} apagado com sucesso!");
    }

    static void CreateManyCategory(SqlConnection connection)
    {
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "Amazon AWS";
        category.Url = "amazon.com";
        category.Description = "Categoria destinada a serviços AWS";
        category.Order = 8;
        category.Summary = "AWS Cloud";
        category.Featured = false;

        var category2 = new Category();
        category2.Id = Guid.NewGuid();
        category2.Title = "Google Cloud";
        category2.Url = "google.com";
        category2.Description = "Categoria destinada a serviços Google";
        category2.Order = 9;
        category2.Summary = "Google Cloud";
        category2.Featured = false;

        var insertSql = @"INSERT INTO [Category] ([Id],[Title],[Url],[Summary],[Order],[Description],[Featured])
                    VALUES(
                        @Id,
                        @Title,
                        @Url,
                        @Summary,
                        @Order,
                        @Description,
                        @Featured)";

        var rows = connection.Execute(insertSql, new[]
        {
            new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            },
            new
            {
                category2.Id,
                category2.Title,
                category2.Url,
                category2.Summary,
                category2.Order,
                category2.Description,
                category2.Featured
            }
        });
        Console.WriteLine($"{rows} linhas inseridas.");
    }

    static void ExecuteProcedure(SqlConnection connection)
    {
        var procedure = "[spDeleteStudent]";
        var pars = new { StudentId = "378ad329-5051-469a-a0b4-5c5f3dd37181" };
        var affectedRows = connection.Execute(
            procedure,
            pars,
            commandType: CommandType.StoredProcedure
        );
        Console.WriteLine($"Linha(s) afetada(s): {affectedRows}");
    }

    static void ExecureReadProcedure(SqlConnection connection)
    {
        var procedure = "[spGetCoursesByCategory]";
        var pars = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };
        // Tipado - Quando um modelo é determinado como retorno. Ex: connection.Query<Courses>
        // Dinâmico - Quando não se determina o retorno baseado em um modelo.
        var courses = connection.Query(
           procedure,
           pars,
           commandType: CommandType.StoredProcedure
       );
        foreach (var item in courses)
        {
            System.Console.WriteLine($"{item.Id} - {item.Title}");
        }
    }

    static void ExecuteScalar (SqlConnection connection)
    {
        var category = new Category();
        category.Title = "Redhat Cloud";
        category.Url = "redhat.com";
        category.Description = "Categoria destinada a serviços Redhat";
        category.Order = 10;
        category.Summary = "RedHat Cloud";
        category.Featured = false;

        var insertSql = @"INSERT INTO 
                            [Category] ([Id],[Title],[Url],[Summary],[Order],[Description],[Featured])
                          OUTPUT inserted.[Id]
                          VALUES(
                            NEWID(),
                            @Title,
                            @Url,
                            @Summary,
                            @Order,
                            @Description,
                            @Featured)";

        var id = connection.ExecuteScalar<Guid>(insertSql, new
        {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        });
        Console.WriteLine($"A categoria inserida foi: {id}.");
    }
}