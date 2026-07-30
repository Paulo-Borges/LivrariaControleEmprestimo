using LivrariaControleEmprestimo.DATA.Interfaces;
using LivrariaControleEmprestimo.DATA.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaControleEmprestimo.DATA.Repositories
{
    public class RepositoryBase<T> : IRepositoryModel<T>, IDisposable where T : class
    {
        protected readonly ControleEmprestimoLivroContext _Contexto;
        public bool _SaveChanges = true;
        public RepositoryBase(ControleEmprestimoLivroContext contexto, bool saveChanges = true) 
        { 
            _Contexto = contexto;
            _SaveChanges = saveChanges;
        }

        public List<T> SelecionarTodos()
        {
            return _Contexto.Set<T>().ToList();
        }

        public T SelecionarPK(params object[] variavel)
        {
     
            return _Contexto.Set<T>().Find(variavel);  

        }

        public T Incluir(T objeto)    //_Contexto.Add(objeto)---x-- NOVO --x--
        {
            _Contexto.Set<T>().Add(objeto);
            if (_SaveChanges)
            {
                _Contexto.SaveChanges();
            }
            return objeto;
        }

        public T Alterar(T objeto)     //_Contexto.Update(objeto)---x-- NOVO --x--
        {
            _Contexto.Entry(objeto).State = EntityState.Modified; 
            if (_SaveChanges)
            {
                _Contexto.SaveChanges();
            }
            return objeto;
        }

        public void Excluir(T objeto)    //_Contexto.Remove(objeto)---x-- NOVO --x--
        {
            _Contexto.Set<T>().Remove(objeto);
            if (_SaveChanges)
            {
                _Contexto.SaveChanges();
            }
        }

        public void Excluir(params object[] variavel)
        {
            var objeto = SelecionarPK(variavel);
            if (objeto != null)
            {
                Excluir(objeto);
            }
        }

        public void SaveChages()
        {
            _Contexto.SaveChanges();
        }

        public void Dispose()
        {
            _Contexto.Dispose();
        }
    }
}
