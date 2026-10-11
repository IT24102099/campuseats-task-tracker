
import { useState } from "react";
import { useDebounce } from "../hooks/useDebounce";
import { useFetch } from "../hooks/useFetch";
import MenuList from "../components/MenuList";

function MenuPage() {
  const [query, setQuery] = useState("");

  const debounced = useDebounce(query, 400);

  const apiUrl = import.meta.env.VITE_API_URL?.replace(/\/$/, "");
  const menuUrl = apiUrl ? `${apiUrl}/api/menu` : "/menu.json";

  const { data: dishes, isLoading, error } = useFetch(menuUrl);

  if (isLoading) {
    return <h2>Loading menu...</h2>;
  }

  if (error) {
    return <h2>Could not load menu: {error}</h2>;
  }

  const filtered = dishes.filter((dish) =>
    dish.name.toLowerCase().includes(debounced.toLowerCase())
  );

  return (
    <div>
      <h1>CampusEats Dashboard</h1>

      <input
        type="text"
        placeholder="Search dishes..."
        value={query}
        onChange={(e) => setQuery(e.target.value)}
      />

      <br />
      <br />

      <MenuList dishes={filtered} />
    </div>
  );
}

export default MenuPage;
