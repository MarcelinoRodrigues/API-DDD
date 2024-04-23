using Microsoft.AspNetCore.Mvc;
using Modelo.Domain.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;


namespace BahrsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieTheatersController : ControllerBase
    {
        private readonly string _connectionString;

        public MovieTheatersController()
        {
            _connectionString = "server=localhost;port=3306;database=api;user=root;";
        }

        [HttpGet]
        public IEnumerable<MovieTheater> GetSalasDeCinema()
        {
            List<MovieTheater> salas = new List<MovieTheater>();

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM sala_de_filmes";

            try
            {
                connection.Open();
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MovieTheater sala = new MovieTheater
                    {
                        Id = reader.GetInt32("Id"),
                        Number = reader.GetInt32("Number"),
                        Description = reader.GetString("Description")
                    };
                    salas.Add(sala);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar ao banco de dados: {ex.Message}");
            }

            return salas;
        }

        [HttpPost]
        public IActionResult InserirSalaDeCinema(MovieTheater sala)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO sala_de_filmes (Number, Description) VALUES (@Number, @Description)";
            command.Parameters.AddWithValue("@Number", sala.Number);
            command.Parameters.AddWithValue("@Description", sala.Description);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                return StatusCode(201); // Retorna o código 201 (Created)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir sala de cinema: {ex.Message}");
                return StatusCode(500); // Retorna o código 500 (Internal Server Error)
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarSalaDeCinema(int id, MovieTheater sala)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE sala_de_filmes SET Number = @Number, Description = @Description WHERE Id = @Id";
            command.Parameters.AddWithValue("@Number", sala.Number);
            command.Parameters.AddWithValue("@Description", sala.Description);
            command.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok(); // Retorna o código 200 (OK)
                }
                else
                {
                    return NotFound(); // Retorna o código 404 (Not Found) se não encontrar a sala com o ID especificado
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar sala de cinema: {ex.Message}");
                return StatusCode(500); // Retorna o código 500 (Internal Server Error)
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarSalaDeCinema(int id)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM sala_de_filmes WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return NoContent(); // Retorna o código 204 (No Content) se a sala foi excluída com sucesso
                }
                else
                {
                    return NotFound(); // Retorna o código 404 (Not Found) se não encontrar a sala com o ID especificado
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar sala de cinema: {ex.Message}");
                return StatusCode(500); // Retorna o código 500 (Internal Server Error)
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM sala_de_filmes WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();
                using MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    MovieTheater sala = new MovieTheater
                    {
                        Id = reader.GetInt32("Id"),
                        Number = reader.GetInt32("Number"),
                        Description = reader.GetString("Description")
                    };
                    return Ok(sala); // Retorna o objeto SalaDeCinema encontrado
                }
                else
                {
                    return NotFound(); // Retorna o código 404 (Not Found) se não encontrar a sala com o ID especificado
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar sala de cinema por ID: {ex.Message}");
                return StatusCode(500); // Retorna o código 500 (Internal Server Error)
            }
        }
    }
}
