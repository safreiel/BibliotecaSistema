using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    public class EmprestimoService 
    {
        public void Inserir(Emprestimos e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Emprestimos.Add(e);
                bc.SaveChanges();
            }
        }

        public void Atualizar(Emprestimos e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Emprestimos emprestimo = bc.Emprestimos.Find(e.Id);
                emprestimo.NomeUsuario = e.NomeUsuario;
                emprestimo.Telefone = e.Telefone;
                emprestimo.LivroId = e.LivroId;
                emprestimo.DataEmprestimo = e.DataEmprestimo;
                emprestimo.DataDevolucao = e.DataDevolucao;
                emprestimo.Devolvido = e.Devolvido;


                bc.SaveChanges();
            }
        }

        public ICollection<Emprestimos> ListarTodos(FiltrosEmprestimos filtro)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                IQueryable<Emprestimos> consulta;

                if(filtro!=null)
                {
                    switch(filtro.TipoFiltro)
                    {
                        case "Usuario" :
                            consulta = bc.Emprestimos.Where(e => e.NomeUsuario.Contains(filtro.Filtro));
                        break;

                        case "Livro" :

                            List<livros> LivrosFiltrados = bc.Livros.Where(l => l.Titulo.Contains(filtro.Filtro)).ToList();

                            List<int>LivroIds = new List<int>();
                            for (int i = 0; i < LivrosFiltrados.Count; i++)
                            {LivroIds.Add(LivrosFiltrados[i].Id);}

                            consulta = bc.Emprestimos.Where(e => LivroIds.Contains(e.LivroId));
                            var debug = consulta.ToList();
                            break;
                            
                            default :
                                consulta = bc.Emprestimos;
                            break;
                    }
                }
                else
                {
                    consulta = bc.Emprestimos;
                }

                List<Emprestimos>ListaConsulta = consulta.OrderBy(e => e.DataEmprestimo).ToList();

                for (int i = 0; i < ListaConsulta.Count; i++)
                {
                    ListaConsulta[i].Livro = bc.Livros.Find(ListaConsulta[i].LivroId);
                }

                return ListaConsulta;
            }
        }

        public Emprestimos ObterPorId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimos.Find(id);
            }
        }
    }
}