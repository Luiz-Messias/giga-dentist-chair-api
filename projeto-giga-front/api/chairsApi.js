const BASE_URL = "https://localhost:7001/api/dentistChairs";

async function handleResponse(response, errorMessage) {
  if (!response.ok) {
    // tenta pegar o erro vindo do backend no corpo da resposta
    let errorText;
    try {
      const errorData = await response.json();
      errorText = errorData.message || errorMessage;
    } catch {
      errorText = errorMessage;
    }
    throw new Error(errorText);
  }
  return response.json();
}

export async function getAllChairs() {
  const res = await fetch(`${BASE_URL}/retrieve/all`);
  return handleResponse(res, "Erro ao buscar cadeiras");
}

export async function createChair(dto) {
  const res = await fetch(`${BASE_URL}/create`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(dto),
  });
  await handleResponse(res, "Erro ao criar cadeira");
}

export async function updateChair(id, dto) {
  const res = await fetch(`${BASE_URL}/update/chairdentistbyid/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(dto),
  });
  await handleResponse(res, "Erro ao atualizar cadeira");
}

export async function deleteChair(id) {
  const res = await fetch(`${BASE_URL}/delete/${id}`, {
    method: "DELETE",
  });
  await handleResponse(res, "Erro ao deletar cadeira");
}

export async function getChairById(id) {
  const res = await fetch(`${BASE_URL}/retrieve/byid/${id}`);
  return handleResponse(res, "Cadeira não encontrada");
}
