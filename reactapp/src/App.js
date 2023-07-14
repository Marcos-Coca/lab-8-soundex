import { Box, Typography } from "@mui/material";
import Table from "./Table";
import { useEffect, useState } from "react";
import { getProducts } from "./service";
import TextField from "@mui/material/TextField";

// Show products table

export default function App() {
  const [products, setProducts] = useState([]);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [searchQuery, setSearch] = useState("");

  useEffect(() => {
    getProducts({ page, pageSize, searchQuery })
      .then((data) => {
        setProducts([...products, ...data]);
      })
      .catch((error) => {
        console.log(error);
      });
  }, [page, pageSize]);

  useEffect(() => {
    getProducts({ page, pageSize, searchQuery })
      .then((data) => {
        setProducts([...data]);
        setPage(1);
      })
      .catch((error) => {
        console.log(error);
      });
  }, [searchQuery]);

  return (
    <Box sx={{ padding: 4 }}>
      <Typography marginBottom={3} textAlign="center" variant="h3">
        Productos Soundex
      </Typography>

      <TextField
        id="standard-basic"
        label="Buscar..."
        variant="standard"
        sx={{ marginBottom: 4 }}
        onChange={(e) => {
          setSearch(e.target.value);
        }}
      />
      <Table
        data={products}
        onPageChange={(page) => {
          setPage(page);
        }}
        onPageSizeChange={(pageSize) => {
          setPageSize(pageSize);
        }}
      />
    </Box>
  );
}
