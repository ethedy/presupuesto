using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.DTO
{
  public class PrecioAnalisis
  {
    public string Analisis { get; set; }

    public decimal Precio { get; set; }

    public string ObraSocial { get; set; }
  }
}