using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RallyDakar.Dominio.Entidades;
using System;

namespace RallyDakar.Dominio.DbContexto
{
    public class BaseDados
    {
        public static void CargaInicial(IServiceProvider serviceProvider)
        {
            using (var context = new RallyDbContexto(serviceProvider.GetRequiredService<DbContextOptions<RallyDbContexto>>()))
            {
                var temporada = new Temporada();
                temporada.Id = 1;
                temporada.Nome = "Temporada 2020";
                temporada.DataInicio = DateTime.Now;

                var equipe = new Equipe();
                equipe.Id = 1;
                equipe.Nome = "Tarvos";
                equipe.CodigoIdentificador = "TAR";

                var piloto = new Piloto();
                piloto.Id = 1;
                piloto.Nome = "Emerson";
                piloto.SobreNome = "Kurauti";

                var piloto2 = new Piloto();
                piloto2.Id = 2;
                piloto2.Nome = "Ayrton";
                piloto2.SobreNome = "Gota";

                equipe.AdicionarPiloto(piloto);
                equipe.AdicionarPiloto(piloto2);

                temporada.AdicionarEquipe(equipe);

                context.Temporadas.Add(temporada);
                context.SaveChanges();
            }
        }
    }
}
