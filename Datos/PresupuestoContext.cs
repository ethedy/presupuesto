using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Infraestructura;

namespace Datos
{
  public class PresupuestoContext : DbContext
  {
    private readonly List<Variable> _variablesServer;

    //  private readonly StreamWriter _writer;

    private static PresupuestoContext _ctxPresupuesto;

    //  public static PresupuestoContext DB => _ctxPresupuesto ?? (_ctxPresupuesto = new PresupuestoContext());
    public static PresupuestoContext DB
    {
      get
      {
        return _ctxPresupuesto ?? (_ctxPresupuesto = new PresupuestoContext());
      }
    }
    //Contexto.Current.GetProperContextName("Presupuesto"))

    private PresupuestoContext() // : base("PresupuestoContextRemoto")
    {
      //  Ajustamos el log para que escriba en disco...

      //  writer = File.CreateText($@"{Environment.CurrentDirectory}\{this.GetType().Name}.LOG");
      //  writer = File.CreateText($@"C:\Users\Enrique\Documents\DESARROLLO\{this.GetType().Name}.LOG");
      //  writer = File.CreateText(string.Format(@"{0}\{1}.LOG", "F:\\", this.GetType().Name));


      //  _writer = File.CreateText($@"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\{this.GetType().Name}.LOG");

      //  Database.Log = _writer.WriteLine;
      Database.Log = Logger.Log.LogExterno;

      //  Lamentablemente no funciona con tuplas aunque seria maravilloso...
      //
      //var listaxx = ctx.Database.SqlQuery<(string Variable_Name, string Value)>("show variables").ToList();
      _variablesServer = Database.SqlQuery<Variable>("show variables").ToList();

      foreach (var x in _variablesServer.Where(x => x.Variable_Name == "version" || x.Variable_Name == "hostname"))
        Console.WriteLine($"{x.Variable_Name} --> {x.Value}");
    }

    public DbSet<ObraSocial> ObrasSociales { get; set; }

    public DbSet<Precio> Precios { get; set; }

    public DbSet<Analisis> Analisis { get; set; }

    //public DbSet<Paciente> Pacientes { get; set; }

    /// <summary>
    /// Permite definir si estamos en el servidor local (SEGURO) o remoto (ESCRITURA CONTROLADA)
    /// </summary>
    public bool ModoSeguro
    {
      get
      {
        return !_variablesServer.Any(x =>
          x.Variable_Name == "hostname" && x.Value == "VALIS" || 
          x.Variable_Name == "version" && x.Value == "8.0.12");
      }
    }

    public void Log(string info)
    {
      //_writer.WriteLine("LOG PROPIO");
      //_writer.WriteLine(info);

      Logger.Log.Info("LOG PROPIO");
      Logger.Log.Info(info);
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Configurations.Add(new ConfigurarObrasSociales());
      modelBuilder.Configurations.Add(new ConfigurarPrecios());
      modelBuilder.Configurations.Add(new ConfigurarAnalisis());

      //  base.OnModelCreating(modelBuilder);
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      //_writer.Close();
    }

    private class Variable
    {
      public string Variable_Name { get; set; }
      public string Value { get; set; }
    }


    private class ConfigurarObrasSociales : EntityTypeConfiguration<ObraSocial>
    {
      public ConfigurarObrasSociales()
      {
        this.ToTable("obrasocial");
      }
    }

    private class ConfigurarAnalisis : EntityTypeConfiguration<Analisis>
    {
      public ConfigurarAnalisis()
      {
        this.ToTable("analisis");
        this.Property(an => an.Codigo)
          .HasColumnName("CodAna");
      }
    }

    private class ConfigurarPrecios : EntityTypeConfiguration<Precio>
    {
      public ConfigurarPrecios()
      {
        this.ToTable("precio");
        this.Property(p => p.Valor).HasColumnName("Precio");

        this.HasRequired(p => p.ObraSocial)
          .WithMany(os => os.Precios)
          .Map(cfg => cfg.MapKey("IdObraSocial"));

        this.HasRequired(p => p.Analisis)
          .WithMany()
          .Map(cfg => cfg.MapKey("IdAnalisis"));
      }
    }
  }
}
