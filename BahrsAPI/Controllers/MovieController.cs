using Microsoft.AspNetCore.Mvc;
using Modelo.Domain.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace BahrsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly string _connectionString = "server=localhost;port=3306;database=api;user=root;";

        [HttpGet]
        public IActionResult GetMovies()
        {
            List<Movie> movies = new List<Movie>();

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM movie";

            try
            {
                connection.Open();
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Movie movie = new Movie
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        Director = reader.GetString("Director"),
                        Duration = reader.GetInt32("Duration"),
                        SalaId = reader.IsDBNull(reader.GetOrdinal("SalaId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SalaId"))
                    };
                    movies.Add(movie);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching movies: {ex.Message}");
                return StatusCode(500);
            }

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id)
        {
            Movie movie = null;

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM movie WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();
                using MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    movie = new Movie
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        Director = reader.GetString("Director"),
                        Duration = reader.GetInt32("Duration"),
                        SalaId = reader.IsDBNull(reader.GetOrdinal("SalaId")) ? null : (int?)int.Parse(reader.GetString(reader.GetOrdinal("SalaId")))
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching movie by ID: {ex.Message}");
                return StatusCode(500);
            }

            if (movie != null)
            {
                return Ok(movie);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult InsertMovie(Movie movie)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO movie (Name, Director, Duration, SalaId) VALUES (@Name, @Director, @Duration, @SalaId)";
            command.Parameters.AddWithValue("@Name", movie.Name);
            command.Parameters.AddWithValue("@Director", movie.Director);
            command.Parameters.AddWithValue("@Duration", movie.Duration);
            command.Parameters.AddWithValue("@SalaId", movie.SalaId ?? (object)DBNull.Value);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting movie: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, Movie movie)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Movie SET Name = @Name, Director = @Director, Duration = @Duration, SalaId = @SalaId WHERE Id = @Id";
            command.Parameters.AddWithValue("@Name", movie.Name);
            command.Parameters.AddWithValue("@Director", movie.Director);
            command.Parameters.AddWithValue("@Duration", movie.Duration);
            command.Parameters.AddWithValue("@SalaId", movie.SalaId ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating movie: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM movie WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting movie: {ex.Message}");
                return StatusCode(500);
            }
        }
    }
}