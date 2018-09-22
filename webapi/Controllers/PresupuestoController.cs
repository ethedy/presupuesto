using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Datos;
using webapi.DTO;

namespace webapi.Controllers
{
  public class PresupuestoController : ApiController
  {
    private const string SQL = @"
    select 
      analisis.nombre as Analisis, 
      precio.precio, 
      obrasocial.nombre as ObraSocial
    from 
      (analisis inner join precio on analisis.id = precio.idanalisis) inner join obrasocial on precio.idObraSocial=obrasocial.id
    where 
      obrasocial.id = @p0 
      and analisis.Activo and obrasocial.Activo;
";

    [HttpGet]
    public string Ping()
    {
      return "PONG";
    }

    [HttpGet]
    public List<PrecioAnalisis> ConsultaPrecio(int id)
    {
      PresupuestoContext ctx = PresupuestoContext.DB;

      var lista = ctx.Database.SqlQuery<PrecioAnalisis>(SQL, id);
      return lista.ToList();
    }
  }
}
