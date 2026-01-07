import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Contatos from "../pages/Contatos";
import Favoritos from "../pages/favoritos";
import Conta from "../pages/conta";
import * as React from "react";

export default function AppRoutes() {
    return (
        <Routes>
            <Route path="/contatos" element={<Contatos />} />
            <Route path="/favoritos" element={<Favoritos />} />
            <Route path="/conta" element={<Conta />} />
            <Route path="*" element={<Navigate to="/contatos" />} />
        </Routes>
    );
}