export const showToast = (message, type = "success", duration = 3000) => {
  const container = document.getElementById("toast-container");
  const toast = document.createElement("div");

  toast.className = `toast ${type}`;
  toast.textContent = message;

  // Adiciona a classe de exibição com um pequeno delay
  setTimeout(() => toast.classList.add("show"), 100);

  container.appendChild(toast);

  // Remove o toast depois da duração + tempo da animação
  setTimeout(() => {
    toast.classList.remove("show");
    setTimeout(() => toast.remove(), 400);
  }, duration);
};
