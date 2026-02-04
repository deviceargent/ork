using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.Win32;

namespace ORK
{
    class Program
    {
        [DllImport("ucrtbase.dll", SetLastError = true)]
        static extern int _setmode(int fh, int mode);

        const int STDIN_FILENO = 0;
        const int STDOUT_FILENO = 1;
        const int _O_BINARY = 0x8000;

        static void Main(string[] args)
        {
            _setmode(STDIN_FILENO, _O_BINARY);
            _setmode(STDOUT_FILENO, _O_BINARY);

            while (true)
            {
                try
                {
                    using (Stream stdin = Console.OpenStandardInput())
                    {
                        byte[] lengthBytes = new byte[4];

                        // Si no lee 4 bytes, el navegador cerró la conexión
                        if (stdin.Read(lengthBytes, 0, 4) < 4)
                        {
                            break;
                        }

                        int length = BitConverter.ToInt32(lengthBytes, 0);
                        byte[] buffer = new byte[length];
                        int totalRead = 0;

                        while (totalRead < length)
                        {
                            int r = stdin.Read(buffer, totalRead, length - totalRead);
                            if (r <= 0) break;
                            totalRead += r;
                        }

                        var message = JsonSerializer.Deserialize<RegistryMessage>(buffer);
                        if (message?.path != null && !string.IsNullOrWhiteSpace(message.path))
                        {
                            OpenRegedit(message.path.Trim());
                            SendResponseToBrowser("{\"status\": \"ok\"}");
                        }
                    }
                }
                catch
                {
                    // Evita popups de error en el host nativo
                }
            }
        }

        static void OpenRegedit(string path)
        {
            string lastKeyPath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Applets\Regedit";

            // Guardamos la clave a abrir
            Registry.SetValue(lastKeyPath, "LastKey", path);

            // Lanza Regedit SIEMPRE en una nueva instancia
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "regedit.exe",
                Arguments = "-m",
                UseShellExecute = true
            };

            Process.Start(startInfo);
        }

        static void SendResponseToBrowser(string message)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(message);
            var lengthBytes = BitConverter.GetBytes(bytes.Length);

            using (var stdout = Console.OpenStandardOutput())
            {
                stdout.Write(lengthBytes, 0, lengthBytes.Length);
                stdout.Write(bytes, 0, bytes.Length);
                stdout.Flush();
            }
        }
    }

    public class RegistryMessage
    {
        public string path { get; set; } = string.Empty;
    }
}
