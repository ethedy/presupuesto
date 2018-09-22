using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;
using MySql.Data.EntityFramework;

namespace consola
{
  class Program
  {
    static void Main(string[] args)
    {
      //  DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
      PresupuestoContext ctx = PresupuestoContext.DB;

      AppDomain.CurrentDomain.UnhandledException += (o, e) =>
      {
        ctx.Dispose();
        Console.WriteLine("Excepcion");
      };

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

      //      var obras = ctx.Database.SqlQuery<ObraSocial>("select * from obrassociales");

      foreach (var x in lista)
        Console.WriteLine($"{x.Analisis} -- {x.Precio}");

          //  os1.Activo = false;

      var os = ctx.ObrasSociales.First();
      if (!ctx.ModoSeguro)
      {
        os.Activo = !os.Activo;
        ctx.SaveChanges();

        Console.WriteLine("==>  Cambios realizados...");
      }

      Console.WriteLine(os.Nombre);


      Console.ReadLine();
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

  }
}
