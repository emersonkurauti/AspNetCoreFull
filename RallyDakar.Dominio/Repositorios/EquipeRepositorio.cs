using RallyDakar.Dominio.DbContexto;
using RallyDakar.Dominio.Entidades;
using RallyDakar.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RallyDakar.Dominio.Repositorios
{
    public class EquipeRepositorio : IEquipeRepositorio
    {
        private readonly RallyDbContexto _rallyDbContexto;

        public EquipeRepositorio(RallyDbContexto rallyDbContexto)
        {
            _rallyDbContexto = rallyDbContexto;
        }

        public void Adicionar(Equipe equipe)
        {
            _rallyDbContexto.Equipes.Add(equipe);
            _rallyDbContexto.SaveChanges();
        }

        public IEnumerable<Equipe> ObterTodos()
        {
            return _rallyDbContexto.Equipes.ToList();
        }

        public Equipe ObterPorNome(string nome)
        {
            return _rallyDbContexto.Equipes.FirstOrDefault(p => p.Nome == nome);
        }

        public Equipe Obter(int equipeId)
        {
            return _rallyDbContexto.Equipes.FirstOrDefault(p => p.Id == equipeId);
        }

        public bool Existe(int equipeId)
        {
            return _rallyDbContexto.Equipes.Any(p => p.Id == equipeId);
        }

        public bool Existe(Equipe equipe)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Equipe equipe)
        {
            if (_rallyDbContexto.Entry(equipe).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                _rallyDbContexto.Attach(equipe);
                _rallyDbContexto.Entry(equipe).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                _rallyDbContexto.Update(equipe);
            }
            _rallyDbContexto.SaveChanges();
        }

        public void Deletar(Equipe equipe)
        {
            _rallyDbContexto.Equipes.Remove(equipe);
            _rallyDbContexto.SaveChanges();
        }
    }
}
