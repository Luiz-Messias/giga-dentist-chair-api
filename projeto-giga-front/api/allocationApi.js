const BASE_URL_ALLOCATION = "https://localhost:7001/api/allocation";

export const createAllocation = async (dto) => {
  const response = await fetch(`${BASE_URL_ALLOCATION}/allocate`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(dto),
  });

  const result = await response.json();

  if (!response.ok || result.success === false) {
    throw new Error(result.message || "Erro ao criar alocação");
  }

  return result;
};

export const getAllAllocations = async () => {
  const response = await fetch(`${BASE_URL_ALLOCATION}/allocations`);

  const result = await response.json();

  if (!response.ok || result.success === false) {
    throw new Error(result.message || "Erro ao buscar alocações");
  }

  return result;
};
