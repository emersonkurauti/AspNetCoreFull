using RallyDakar.Dominio.DbContexto;
using RallyDakar.Dominio.Entidades;
using RallyDakar.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RallyDakar.Dominio.Repositorios
{
    public class PilotoRepositorio : IPilotoRepositorio
    {
        private readonly RallyDbContexto _rallyDbContexto;

        public PilotoRepositorio(RallyDbContexto rallyDbContexto)
        {
            _rallyDbContexto = rallyDbContexto;
        }

        public void Adicionar(Piloto piloto)
        {
            _rallyDbContexto.Pilotos.Add(piloto);
            _rallyDbContexto.SaveChanges();
        }

        public IEnumerable<Piloto> ObterTodos()
        {
            return _rallyDbContexto.Pilotos.ToList();
        }

        public IEnumerable<Piloto> ObterPorNome(string nome)
        {
            return _rallyDbContexto.Pilotos.Where(p => p.Nome == nome).ToList();
        }
    }
}
