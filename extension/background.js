chrome.runtime.onInstalled.addListener(() => {
  chrome.contextMenus.create({
    id: "regJump",
    title: "Saltar a '%s' en Regedit",
    contexts: ["selection"]
  });
});

chrome.contextMenus.onClicked.addListener((info) => {
  if (info.menuItemId === "regJump") {
    // Enviamos el texto al Host C#
    chrome.runtime.sendNativeMessage('com.tu.regopener', { path: info.selectionText }, (response) => {
      if (chrome.runtime.lastError) {
        console.error("Error:", chrome.runtime.lastError.message);
      }
    });
  }
});
