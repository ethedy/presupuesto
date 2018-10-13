using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
  public class ObraSocial
  {
    public int ID { get; set; }

    public string Nombre { get; set; }

    public bool Activo { get; set; }

    //  si Precio no tuviera propiedades podriamos hacer esto:
    //  public virtual HashSet<Analisis> Analisis { get; set; }

    public virtual ISet<Precio> Precios { get; set; }
  }
}
