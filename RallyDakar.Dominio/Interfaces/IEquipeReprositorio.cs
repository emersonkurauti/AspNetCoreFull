using RallyDakar.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Dominio.Interfaces
{
    public interface IEquipeRepositorio
    {
        void Adicionar(Equipe equipe);
        IEnumerable<Equipe> ObterTodos();
        Equipe ObterPorNome(string nome);
        Equipe Obter(int equipeId);
        bool Existe(int equipeId);
        bool Existe(Equipe equipe);
        void Atualizar(Equipe equipe);
        void Deletar(Equipe equipe);
    }
}
