import { Routes, Route, Navigate } from "react-router-dom";
import AppLayout from "@/components/layouts/appLayout";
import Contatos from "@/pages/contatos";
import Favoritos from "@/pages/favoritos";
import Conta from "@/pages/conta";

export default function App() {
    return (
        <AppLayout>
            <Routes>
                <Route path="/" element={<Navigate to="/contatos" />} />
                <Route path="/contatos" element={<Contatos />} />
                <Route path="/favoritos" element={<Favoritos />} />
                <Route path="/conta" element={<Conta />} />
            </Routes>
        </AppLayout>
    );
}
