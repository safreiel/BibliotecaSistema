using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    public class EmprestimoService 
    {
        public void Inserir(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Emprestimo.Add(e);
                bc.SaveChanges();
            }
        }

        public void Atualizar(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Emprestimo emprestimo = bc.Emprestimo.Find(e.Id);
                emprestimo.NomeUsuario = e.NomeUsuario;
                emprestimo.Telefone = e.Telefone;
                emprestimo.LivroId = e.LivroId;
                emprestimo.DataEmprestimo = e.DataEmprestimo;
                emprestimo.DataDevolucao = e.DataDevolucao;
                emprestimo.Devolvido = e.Devolvido;


                bc.SaveChanges();
            }
        }

        public ICollection<Emprestimo> ListarTodos(FiltrosEmprestimos filtro)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                IQueryable<Emprestimo> consulta;

                if(filtro!=null)
                {
                    switch(filtro.TipoFiltro)
                    {
                        case "Usuario" :
                            consulta = bc.Emprestimo.Where(e => e.NomeUsuario.Contains(filtro.Filtro));
                        break;

                        case "Livro" :

                            List<livros> LivrosFiltrados = bc.Livros.Where(l => l.Titulo.Contains(filtro.Filtro)).ToList();

                            List<int>LivroIds = new List<int>();
                            for (int i = 0; i < LivrosFiltrados.Count; i++)
                            {LivroIds.Add(LivrosFiltrados[i].Id);}

                            consulta = bc.Emprestimo.Where(e => LivroIds.Contains(e.LivroId));
                            var debug = consulta.ToList();
                            break;
                            
                            default :
                                consulta = bc.Emprestimo;
                            break;
                    }
                }
                else
                {
                    consulta = bc.Emprestimo;
                }

                List<Emprestimo>ListaConsulta = consulta.OrderBy(e => e.DataEmprestimo).ToList();

                for (int i = 0; i < ListaConsulta.Count; i++)
                {
                    ListaConsulta[i].Livro = bc.Livros.Find(ListaConsulta[i].LivroId);
                }

                return ListaConsulta;
            }
        }

        public Emprestimo ObterPorId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimo.Find(id);
            }
        }
    }
}