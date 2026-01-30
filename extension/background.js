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

    port.onDisconnect.addListener(() => {
      if (chrome.runtime.lastError) {
        // Mostramos notificación visual porque el host falló o no existe
        chrome.notifications.create({
          type: 'basic',
          iconUrl: 'assets/icon128.png',
          title: 'ORK - Registry Jumper',
          message: chrome.i18n.getMessage("hostError"),
          priority: 2
        });
        console.error("Native Host Error:", chrome.runtime.lastError.message);
      }
    });
  }
});
