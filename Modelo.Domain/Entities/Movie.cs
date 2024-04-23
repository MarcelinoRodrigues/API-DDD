namespace Modelo.Domain.Entities
{
    /// <summary>
    ///classe representante do Filme
    /// </summary>
    public class Movie : BaseEntity
    {
        /// <summary>
        /// Nome do filme
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Diretor do Filme
        /// </summary>
        public required string Director { get; set; }

        /// <summary>
        /// Duração do Filme
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Id da sala
        /// um filme pode existir sem uma sala
        /// </summary>
        public int? SalaId { get; set; }

        /// <summary>
        /// Sala do Filme
        /// </summary>
        public MovieTheater MovieTheater { get; set; }
    }
}
