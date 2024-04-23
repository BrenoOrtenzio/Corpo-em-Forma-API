namespace ModuleAPI.Entities
{
    public class Ficha
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        
        public int UsuarioId { get; set; }

        public ICollection<ExercicioAplicado> Exercicios { get; set; }
    }
}
