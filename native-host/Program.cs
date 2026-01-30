using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Microsoft.Win32; // Asegúrate de tener la referencia

namespace ORK
{
    class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                using (Stream stdin = Console.OpenStandardInput())
                {
                    byte[] lengthBytes = new byte[4];
                    int read = stdin.Read(lengthBytes, 0, 4);
                    if (read < 4) return;

                    int length = BitConverter.ToInt32(lengthBytes, 0);
                    byte[] buffer = new byte[length];
                    int totalRead = 0;
                    
                    // Bucle para asegurar que leemos el mensaje completo
                    while (totalRead < length)
                    {
                        int r = stdin.Read(buffer, totalRead, length - totalRead);
                        if (r <= 0) break;
                        totalRead += r;
                    }

                    var message = JsonSerializer.Deserialize<RegistryMessage>(buffer);
                    if (message != null && !string.IsNullOrEmpty(message.path))
                    {
                        OpenRegedit(message.path.Trim());
                    }
                }
            }
            catch { /* Evita popups de error en el host nativo */ }
        }

        static void OpenRegedit(string path)
        {
            string lastKeyPath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Applets\Regedit";
            
            // Usamos Registry.SetValue de Microsoft.Win32
            Registry.SetValue(lastKeyPath, "LastKey", path);
            
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "regedit.exe",
                UseShellExecute = true // Necesario en .NET 8 para archivos del sistema
            };
            
            Process.Start(startInfo);
        }
    }

    public class RegistryMessage { public string path { get; set; } }
}
