using System.Collections.Generic;

namespace Biblioteca.Models
{
    public class CadEmprestimoViewModel
    {
        public ICollection<livros> Livros { get; set; }
        public Emprestimo Emprestimo { get; set; }
    }
}