export function getProducts(filters) {
  const url = new URL("https://localhost:7220/api/Products");

  if (filters) {
    Object.keys(filters).forEach((key) =>
      url.searchParams.append(key, filters[key])
    );
  }

  return fetch(url, { method: "GET" })
    .then((response) => response.json())
    .then((data) => data);
}
