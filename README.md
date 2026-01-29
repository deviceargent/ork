🪓 ORK – Open Regedit Keys



ORK is a lightweight Windows + browser integration tool that lets you instantly open any Windows Registry key directly in Regedit from your browser.



Select a registry path on any webpage, right-click, and ORK will jump Regedit straight to that key.



🚀 What it does



ORK connects your browser with Regedit using a secure Native Messaging Host:



You select a registry path like:



HKEY\_CURRENT\_USER\\Software\\Microsoft\\Windows





Right-click → Open with ORK



Regedit opens exactly at that key.



No copying. No pasting. No mistakes.



🧠 Who is it for?



Windows power users



Sysadmins



Developers



Anyone following technical tutorials or documentation



🔐 Security \& Privacy



The browser extension does not read your browsing data.



It only sends the selected text (the registry path) to a local helper app.



Everything runs locally on your machine.



📦 Installation

1️⃣ Install the browser extension



From Edge Add-ons / Chrome Web Store (link coming soon)



2️⃣ Install the Native Host (Windows)



Download the latest installer from Releases



Run the .msi installer



Make sure you have .NET 8 Runtime installed

👉 https://dotnet.microsoft.com/download/dotnet/8.0



⚙️ Requirements



Windows 10 / 11



Microsoft Edge or Google Chrome



.NET 8 Runtime



🛠 How it works (technical)



ORK uses Chrome/Edge Native Messaging:



Browser Extension → Native Host (C#) → Regedit



The native host sets Regedit’s LastKey and launches Regedit automatically.



📁 Repository structure

/extension     → Browser extension source  

/native-host   → C# native messaging host  

/installer     → MSI / WiX installer files  



📄 License



MIT



👤 Author



Miguel Okstein

Argentina 🇦🇷

