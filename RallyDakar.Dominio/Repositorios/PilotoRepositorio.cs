﻿using RallyDakar.Dominio.DbContexto;
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

        public Piloto ObterPorNome(string nome)
        {
            return _rallyDbContexto.Pilotos.FirstOrDefault(p => p.Nome == nome);
        }

        public Piloto Obter(int pilotoId)
        {
            return _rallyDbContexto.Pilotos.FirstOrDefault(p => p.Id == pilotoId);
        }

        public bool Existe(int pilotoId)
        {
            return _rallyDbContexto.Pilotos.Any(p => p.Id == pilotoId);
        }

        public bool Existe(Piloto piloto)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Piloto piloto)
        {
            _rallyDbContexto.Attach(piloto);
            _rallyDbContexto.Entry(piloto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _rallyDbContexto.SaveChanges();
        }

        public void Deletar(Piloto piloto)
        {
            _rallyDbContexto.Pilotos.Remove(piloto);
            _rallyDbContexto.SaveChanges();
        }
    }
}
