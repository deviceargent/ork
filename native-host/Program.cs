using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using Windows.UI.Notifications;

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
                            ShowSuccessNotification();
                            SendResponseToBrowser("{\"status\": \"ok\"}");
                        }
                    }
                }
                catch
                {
                    // Silencioso: un fallo del host no debe romper el loop ni el pipe con el navegador
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

        static void ShowSuccessNotification()
        {
            try
            {
                string iconPath = Path.Combine(AppContext.BaseDirectory, "ork-icon.png");
                string heroPath = Path.Combine(AppContext.BaseDirectory, "ork-hero.png");

                var builder = new ToastContentBuilder()
                    .AddText("ARGGGGGGG!")
                    .AddText("Regedit opened successfully!");

                if (File.Exists(iconPath))
                {
                    builder.AddAppLogoOverride(
                        new Uri("file:///" + iconPath.Replace('\\', '/')),
                        ToastGenericAppLogoCrop.Circle);
                }

                if (File.Exists(heroPath))
                {
                    builder.AddInlineImage(
                        new Uri("file:///" + heroPath.Replace('\\', '/')));
                }

                var xml = builder.GetXml();
                var notifier = ToastNotificationManagerCompat.CreateToastNotifier();
                notifier.Show(new ToastNotification(xml));
            }
            catch
            {
                // Silencioso: el toast no puede romper el flujo de apertura de regedit
            }
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
