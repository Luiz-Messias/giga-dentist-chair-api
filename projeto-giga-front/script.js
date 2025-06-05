import {
  getAllChairs,
  createChair,
  updateChair,
  deleteChair,
  getChairById,
} from "./api/chairsApi.js";

import { showToast } from "./utils/alertUtils.js";

const container = document.getElementById("chairs-container");
const formSection = document.getElementById("form-section");
const btnNova = document.getElementById("btn-nova");
const formTemplate = document.getElementById("form-template");
const btnAlocar = document.getElementById("btn-alocar");

btnNova.addEventListener("click", () => showForm());

btnAlocar.addEventListener("click", () => {
  window.location.href = "allocation.html";
});

const showForm = (chair = null) => {
  if (!formTemplate || !formSection) return;

  formSection.classList.remove("hidden");
  formSection.innerHTML = formTemplate.innerHTML;

  const form = document.getElementById("chair-form");
  const idField = document.getElementById("chair-id");
  const number = document.getElementById("chair-number");
  const desc = document.getElementById("chair-description");
  const active = document.getElementById("chair-active");
  const cancelBtn = document.getElementById("cancel-button");

  if (chair) {
    idField.value = chair.id;
    number.value = chair.number;
    desc.value = chair.description;
    active.checked = chair.isActive;
  }

  form.onsubmit = async (e) => {
    e.preventDefault();
    try {
      const dto = {
        number: +number.value,
        description: desc.value,
        isActive: active.checked,
      };

      if (idField.value) {
        await updateChair(idField.value, dto);
        showToast("Cadeira salva com sucesso!", "success");
      } else {
        await createChair(dto);
        showToast("Cadeira salva com sucesso!", "success");
      }

      await renderChairs();
      formSection.classList.add("hidden");
    } catch (err) {
      showToast("Erro ao salvar cadeira: " + err.message, "error", 5000);
      alert(err.message);
    }
  };

  cancelBtn.onclick = () => formSection.classList.add("hidden");
};

const renderChairs = async () => {
  container.innerHTML = "<p>Carregando cadeiras...</p>";
  try {
    const response = await getAllChairs();
    const data = response.data;

    if (!data.length) {
      container.innerHTML = "<p>Nenhuma cadeira cadastrada.</p>";
      return;
    }

    container.innerHTML = "";
    data.forEach((chair) => {
      const card = document.createElement("div");
      card.className = "card";
      card.innerHTML = `
        <h2>Cadeira N°${chair.number}</h2>
        <p>${chair.description}</p>
        <p class="status">${chair.isActive ? "Ativa" : "Inativa"}</p>
        <button onclick="editChair(${chair.id})">Editar</button>
        <button onclick="deleteChairAction(${
          chair.id
        })" style="margin-left: 10px; color: red;">Excluir</button>
      `;
      container.appendChild(card);
    });
  } catch (err) {
    container.innerHTML = `<p style="color:red;">${err.message}</p>`;
  }
};

window.editChair = async (id) => {
  try {
    const response = await getChairById(id);
    const chair = response.data;
    showForm(chair);
  } catch (err) {
    showToast("Erro ao carregar cadeira: " + err.message, "error", 5000);
  }
};

window.deleteChairAction = async (id) => {
  if (confirm("Tem certeza que deseja deletar essa cadeira?")) {
    try {
      await deleteChair(id);
      showToast("Cadeira deletada com sucesso!", "success");
      await renderChairs();
    } catch (err) {
      showToast("Erro ao deletar cadeira: " + err.message, "error", 5000);
    }
  }
};

renderChairs();
