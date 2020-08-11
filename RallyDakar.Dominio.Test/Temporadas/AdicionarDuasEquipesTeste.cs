using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyDakar.Dominio.Entidades;

namespace RallyDakar.Dominio.Teste.Temporadas
{
    [TestClass]
    public class AdicionarDuasEquipesTeste
    {
        Temporada temporada;
        Equipe equipe1;
        Equipe equipe2;
        Equipe equipe3;
        Equipe equipe4;

        [TestInitialize]
        public void Inicializacao()
        {
            temporada = new Temporada();
            temporada.Id = 1;
            temporada.Nome = "Temporada 1";

            equipe1 = new Equipe();
            equipe1.Id = 1;
            equipe1.Nome = "Teste1";

            equipe2 = new Equipe();
            equipe2.Id = 2;
            equipe2.Nome = "Teste2";

            equipe3 = null;

            equipe4 = new Equipe();
            equipe4.Id = 4;

            temporada.AdicionarEquipe(equipe1);
            temporada.AdicionarEquipe(equipe2);
            temporada.AdicionarEquipe(equipe3);
            temporada.AdicionarEquipe(equipe4);
        }

        [TestMethod]
        public void DuasEquipesAdicionadasCorretamente()
        {
            Assert.IsTrue(temporada.Equipes.Count == 2);
        }
    }
}
