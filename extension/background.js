chrome.runtime.onInstalled.addListener(() => {
  chrome.contextMenus.create({
    id: "ork-jump",
    title: chrome.i18n.getMessage("menuTitle", ["%s"]),
    contexts: ["selection"]
  });
});

chrome.contextMenus.onClicked.addListener((info, tab) => {
  if (info.menuItemId === "ork-jump") {
    const port = chrome.runtime.connectNative('com.microsoft.ork');
    
    port.postMessage({ path: info.selectionText });

    port.onMessage.addListener((response) => {
        if (response.status === "ok") {
            chrome.notifications.create({
                type: 'basic',
                // Usamos la imagen del orco que reemplazaste en assets/icon128.png
                iconUrl: 'assets/icon128.png', 
                title: ' ', // Título con un espacio en blanco (invisible)
                message: ' ', // Mensaje con un espacio en blanco (invisible)
                priority: 2
            });
            console.log("Éxito confirmado por el host nativo.");
        }
    });

    port.onDisconnect.addListener(() => {
      if (chrome.runtime.lastError) {
        // Esto solo debería ocurrir si el registro o el .exe no existen en absoluto
        console.error("Native Host Error:", chrome.runtime.lastError.message);
      } else {
        console.log("Host Nativo Desconectado limpiamente.");
      }
    });
  }
});
