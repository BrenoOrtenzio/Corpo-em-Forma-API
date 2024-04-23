namespace ModuleAPI.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Documento { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public bool Ativo { get; set; }

        public void ValidateAndThrow()
        {
            if (string.IsNullOrEmpty(this.Login))
                throw new InvalidOperationException("Login não pode ser vazio");

            if (string.IsNullOrEmpty(this.Senha))
                throw new InvalidOperationException("Senha não pode ser vazia");

            if (string.IsNullOrEmpty(this.Nome))
                throw new InvalidOperationException("Nome não pode ser vazio");

            if (string.IsNullOrEmpty(this.Documento))
                throw new InvalidOperationException("Documento não pode ser vazio");

            if (string.IsNullOrEmpty(this.Telefone))
                throw new InvalidOperationException("Telefone não pode ser vazio");

            if (string.IsNullOrEmpty(this.Email))
                throw new InvalidOperationException("Email não pode ser vazio");
        }
    }
}
