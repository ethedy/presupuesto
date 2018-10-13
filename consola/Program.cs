using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;
using MySql.Data.EntityFramework;
using Infraestructura;

namespace consola
{
  class Program
  {
    static void Main(string[] args)
    {
      AppDomain.CurrentDomain.UnhandledException += (o, e) =>
      {
        PresupuestoContext.DB.Dispose();
        Logger.Log.Close();
        Console.WriteLine("Excepcion");
      };

      Logger.Log.Info("Inicializando...");

      using (PresupuestoContext ctx = PresupuestoContext.DB) 
      {
        if (ctx.Database.Exists())
        {
          Console.WriteLine("La base esta...!!");
          if (!ctx.ModoSeguro)
            Console.WriteLine("MODO DE ESCRITURA INDISCRIMINADA...");
        }
        else
        {
          Console.WriteLine("La base no esta....");
          Console.ReadLine();
          Environment.Exit(-1);
        }

        var lista = ctx.Database.SqlQuery<PrecioCuidado>("select * from PreciosCuidados");

        foreach (var x in lista)
          Console.WriteLine($"{x.Analisis} -- {x.Precio}");

        var os = ctx.ObrasSociales.First();
        //var os1 = ctx.ObrasSociales.Select();
        /*
              var ing = ctx.Pacientes.Where(p => p.DNI == 18339577).Single().Ingresos.Count;

              Listado resultado = new Listado { DNI = 18339577, Ingresos = ing};

              var pp = ctx.Database.SqlQuery<Listado>("select DNI, count(*) as Ingresos from ....where DNI=@p0 group by DNI", 18339577).ToList();
        */
        if (!ctx.ModoSeguro)
        {
          bool fValorPrevio = os.Activo;
          os.Activo = !os.Activo;
          ctx.SaveChanges();

          Console.WriteLine($"==>  Cambios realizados...{os.Nombre} paso de {fValorPrevio} a {os.Activo}");
        }

        Console.WriteLine(os.Nombre);

        Console.ReadLine();
      }
      Logger.Log.Close();
    }

    private class PrecioCuidado
    {
      //  ojo tiene que ser una propiedad, no puede ser un campo!!!
     
 //  si es un campo el EF no le asigna un valor
      //  en este caso, aunque la fila traiga mas de una columna, se asigna esta sola al conjunto
      //  de datos (el resto se ignora por supuesto...)
      public string Analisis { get; set; }

      public decimal Precio { get; set; }
    }

/*
    private class Listado
    {
      public int DNI { get; set; }

      public int Ingresos { get; set; }
    }
*/
  }
}
