namespace ModuleAPI.Models
{
    public class Mensagem
    {
        public bool Sucesso { get; set; } = false;

        public string Descricao { get; set; } = string.Empty;


        public Mensagem() { }
        public Mensagem(string descricao) { this.Descricao = descricao; }
        private Mensagem(bool sucesso) { this.Sucesso = sucesso; }

        public static Mensagem MensagemSucesso => new Mensagem(true);
    }
}