using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
  public class Paciente
  {
    public int IDPaciente { get; set; }

    public int DNI { get; set; }

    public string Nombre { get; set; }

    public ISet<Ingreso> Ingresos { get; set; }
  }

  public class Ingreso
  {
  }
}
