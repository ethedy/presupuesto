using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura
{
  public class Logger
  {
    private static Logger _logBase;

    public static Logger Log
    {
      get { return _logBase ?? (_logBase = new Logger());  }
    }

    private StreamWriter _writer;

    /*
     * Consultar libreria log4net
     */
    private Logger()
    {
      _writer = File.CreateText($@"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\Presupuesto.LOG");
    }

    public void Info(string info)
    {
      //  CrearLogSiNoExisteOCambioDeFecha();
      _writer.WriteLine(info);
    }

    public void Warning(string warn)
    {
      //  CrearLogSiNoExisteOCambioDeFecha();
      _writer.Write(">>>> WARNING >>>> ");
      _writer.WriteLine(warn);
    }

    public Action<string> LogExterno
    {
      get { return _writer.WriteLine;  }
    }

    public void Close()
    {
      _writer.Close();
    }
  }
}
