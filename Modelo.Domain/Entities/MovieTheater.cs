using System.Collections.Generic;

namespace Modelo.Domain.Entities
{
    /// <summary>
    /// classe representante da Sala de Cinema
    /// </summary>
    public class MovieTheater : BaseEntity
    {
        /// <summary>
        /// Numero da sala
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Descrição da sala
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// uma sala pode ter varios filmes
        /// </summary>
        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
