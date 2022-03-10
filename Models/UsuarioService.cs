using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public List<Usuario> Listar()
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuario.ToList();
            }
        }

        public Usuario Listar(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuario.Find(id);
            }
        }

        public void incluirUsuario(Usuario novoUser)
        {
            using(BibliotecaContext bc = new BibliotecaContext()) 
            {
                bc.Usuario.Add(novoUser);
                bc.SaveChanges();
            }  
        }

        public void editarUsuario(Usuario userEditado)
        {
            using(BibliotecaContext bc = new BibliotecaContext()) 
            {
                Usuario u = bc.Usuario.Find(userEditado.Id);

                u.login = userEditado.login;
                u.Nome = userEditado.Nome;
                u.senha = userEditado.senha;
                u.tipo = userEditado.tipo;

                bc.SaveChanges();
            }
        }

        public void excluirUsuario(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext()) 
            {
                bc.Remove(bc.Usuario.Find(id));
                bc.SaveChanges();
            }
        }
    }
}