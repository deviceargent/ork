using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices; // Necesario para DllImport
using System.Text.Json;
using Microsoft.Win32;

namespace ORK
{
    class Program
    {
        // Importación de la función _setmode de la librería ucrtbase.dll
        [DllImport("ucrtbase.dll", SetLastError = true)]
        static extern int _setmode(int fh, int mode);

        // Constantes para los descriptores de archivo estándar y el modo binario
        const int STDIN_FILENO = 0;
        const int STDOUT_FILENO = 1;
        const int _O_BINARY = 0x8000;

        static void Main(string[] args)
        {
            // CRÍTICO: Configura stdin y stdout a modo binario en Windows
            _setmode(STDIN_FILENO, _O_BINARY);
            _setmode(STDOUT_FILENO, _O_BINARY);

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
            
            Registry.SetValue(lastKeyPath, "LastKey", path);
            
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "regedit.exe",
                UseShellExecute = true
            };
            
            Process.Start(startInfo);
        }
    }

    public class RegistryMessage { public string path { get; set; } }
}


    public class RegistryMessage { public string path { get; set; } }
}
