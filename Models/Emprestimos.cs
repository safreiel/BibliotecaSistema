using System;

namespace Biblioteca.Models
{
    public class Emprestimos
    {
        public int Id { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public string NomeUsuario { get; set; }
        public string Telefone { get; set; }
        public bool Devolvido { get; set; }
        
        public int LivroId { get; set; }
        public livros Livro { get; set; }
    }
}