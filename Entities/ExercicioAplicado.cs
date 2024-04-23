namespace ModuleAPI.Entities
{
    public class ExercicioAplicado
    {
        public int Id { get; set; }

        public int FichaId { get; set; }

        public int ExercicioId { get; set; }

        public int Repeticoes { get; set; }

        public int Series { get; set; }

        public double Carga { get; set; }

        public string Observacao { get; set; }
    }
}
