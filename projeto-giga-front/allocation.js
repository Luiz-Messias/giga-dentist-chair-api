import { createAllocation, getAllAllocations } from "./api/allocationApi.js";
import { showToast } from "./utils/alertUtils.js";

document.addEventListener("DOMContentLoaded", () => {
  const btnVoltar = document.getElementById("btn-voltar");

  if (btnVoltar) {
    btnVoltar.addEventListener("click", () => {
      window.location.href = "index.html";
    });
  }

  const allocationForm = document.getElementById("allocation-form");
  const allocationList = document.getElementById("allocations-list");

  if (allocationForm && allocationList) {
    const startInput = document.getElementById("startTime");
    const endInput = document.getElementById("endTime");

    allocationForm.addEventListener("submit", async (e) => {
      e.preventDefault();
      try {
        const dto = {
          startTime: startInput.value,
          endTime: endInput.value,
        };
        const response = await createAllocation(dto);

        if (!response.success) {
          throw new Error(response.message || "Erro ao alocar cadeira.");
        }

        showToast("Cadeira alocada com sucesso!", "success");
        await renderAllocations();
        allocationForm.reset();
      } catch (err) {
        showToast("Erro ao alocar: " + err.message, "error", 5000);
      }
    });

    const renderAllocations = async () => {
      allocationList.innerHTML = "<p>Carregando alocações...</p>";
      try {
        const response = await getAllAllocations();

        if (!response.success) {
          throw new Error(response.message || "Erro ao buscar alocações.");
        }

        const data = response.data;

        if (!data || !data.length) {
          allocationList.innerHTML = "<p>Nenhuma alocação encontrada.</p>";
          return;
        }

        allocationList.innerHTML = "";
        data.forEach((alloc) => {
          const div = document.createElement("div");
          div.className = "card";
          div.innerHTML = `
            <h3>Id da cadeira: ${alloc.chairId || "?"}</h3>
            <p><strong>Início:</strong> ${formatDateTime(alloc.startTime)}</p>
            <p><strong>Fim:</strong> ${formatDateTime(alloc.endTime)}</p>
          `;
          allocationList.appendChild(div);
        });
      } catch (err) {
        allocationList.innerHTML = `<p style="color:red;">${err.message}</p>`;
      }
    };

    const formatDateTime = (dateStr) => {
      const date = new Date(dateStr);
      return date.toLocaleString("pt-BR", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit",
      });
    };

    renderAllocations();
  }
});
