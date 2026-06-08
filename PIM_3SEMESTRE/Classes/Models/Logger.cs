using System;
using System.IO;
using System.Web;

public class Logger
{
    public static void Log(string acao, string detalhes)
    {
        try
        {
            string pasta = HttpContext.Current.Server.MapPath("~/Logs");

            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);

            string arquivo = Path.Combine(pasta, "log.txt");

            File.AppendAllText(
                arquivo,
                $"[{DateTime.Now}] {acao} | {detalhes}{Environment.NewLine}"
            );
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }
}