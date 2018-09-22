using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
  public class Precio
  {
    public int Id { get; set; }

    public decimal Valor { get; set; }

    public bool Activo { get; set; }

    public virtual ObraSocial ObraSocial { get; set; }

    public virtual Analisis Analisis { get; set; }
  }
}
