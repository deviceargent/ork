let nativePort = null;

function getNativePort() {
  if (!nativePort) {
    nativePort = chrome.runtime.connectNative('com.microsoft.ork');

    nativePort.onMessage.addListener((response) => {
      if (response.status === "ok") {
        chrome.notifications.create({
          type: 'basic',
          iconUrl: chrome.runtime.getURL('assets/icon128.png'),
          title: ' ',
          message: ' ',
          priority: 2
        });
        console.log("Éxito confirmado por el host nativo.");
      }
    });

    nativePort.onDisconnect.addListener(() => {
      if (chrome.runtime.lastError) {
        console.error("Native Host Error:", chrome.runtime.lastError.message);
      } else {
        console.log("Host Nativo Desconectado limpiamente.");
      }
      nativePort = null;
    });
  }
  return nativePort;
}

chrome.runtime.onInstalled.addListener(() => {
  chrome.contextMenus.create({
    id: "ork-jump",
    title: chrome.i18n.getMessage("menuTitle", ["%s"]),
    contexts: ["selection"]
  });
});

chrome.contextMenus.onClicked.addListener((info) => {
  if (info.menuItemId === "ork-jump") {
    const port = getNativePort();
    port.postMessage({ path: info.selectionText.trim() });
  }
});
