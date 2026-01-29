using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Microsoft.Win32;

namespace RegOpenerHost;

class Program {
    static void Main() {
        try {
            Stream stdin = Console.OpenStandardInput();
            byte[] lenBytes = new byte[4];
            int read = stdin.Read(lenBytes, 0, 4);
            if (read < 4) return;
            
            int len = BitConverter.ToInt32(lenBytes, 0);
            byte[] buffer = new byte[len];
            stdin.Read(buffer, 0, len);
            
            var msg = JsonSerializer.Deserialize<Dictionary<string, string>>(buffer);

            if (msg != null && msg.TryGetValue("path", out var regPath)) {
                string path = regPath.Trim().Replace("/", "\\");
                if (!path.StartsWith(@"Computer\")) path = @"Computer\" + path;
                
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Applets\Regedit", "LastKey", path);
                
                foreach (var p in Process.GetProcessesByName("regedit")) p.Kill();
                Process.Start("regedit.exe");
            }
        } catch { }
    }
}
