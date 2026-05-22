# 🪓 ORK – Open Regedit Keys

![ORK Banner](assets/ork-banner.png)

### Make sure to download the latest installer file and run it to install the ORK helper  
[![Download ORK](https://img.shields.io/badge/Download-ORK-blue?style=for-the-badge&logo=github&logoColor=white)](https://github.com/deviceargent/ork/releases/latest)   &emsp;   👈

---

ORK is a lightweight Windows + browser integration tool that lets you instantly open any Windows Registry key directly in Regedit from your browser.  
Select a registry path on any webpage, right-click, and ORK will jump Regedit straight to that key.

&ensp;

🚀 <ins>What it does</ins>

> ORK connects your browser with Regedit using a secure Native Messaging Host.  
> You select a registry path like:  
> `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows`  
> Right-click → **Open with ORK**  
> Regedit opens exactly at that key.

No copying.  
No pasting.  
No mistakes.

&ensp;

🧠 <ins>Who is it for?</ins>

• Windows power users  
• Sysadmins  
• Developers  
• Anyone following technical tutorials or documentation  

&ensp;

🔐 <ins>Security & Privacy</ins>

• The browser extension does **not** read your browsing data  
• It only sends the selected text (the registry path) to a local helper app  
• Everything runs locally on your machine  

&ensp;

📦 <ins>Installation</ins>

### 1️⃣ Install the browser extension  
From Edge Add-ons / Chrome Web Store (link coming soon)

### 2️⃣ Install the Native Host (Windows)

• Download the latest installer from **Releases**  
• Run the `ORK_setup.exe` installer  
• Make sure you have **.NET 8 Runtime** installed  

👉 https://dotnet.microsoft.com/download/dotnet/8.0

&ensp;

⚙️ <ins>Requirements</ins>

• Windows 10 / 11  
• Microsoft Edge or Google Chrome  
• .NET 8 Runtime  

&ensp;

🛠 <ins>How it works (technical)</ins>

ORK uses Chrome/Edge Native Messaging:

`Browser Extension → Native Host (C#) → Regedit`

The native host sets Regedit’s **LastKey** and launches Regedit automatically.

&ensp;

📁 <ins>Repository structure</ins>

/extension → Browser extension source  
/native-host → C# native messaging host  
/installer → Inno Setup installer files  

&ensp;

📄 <ins>License</ins>

MIT

&ensp;

👤 <ins>Author</ins>

DeviceArgent 

![ArgentinaFlorkGIF](https://github.com/user-attachments/assets/1564ac6d-7b0b-4c0b-8f82-5bd3a9b69edb)
