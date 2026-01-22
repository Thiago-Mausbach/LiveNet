import { Routes, Route, Navigate } from "react-router-dom";
import { useAuth } from "@/context/authContext";

import Login from "@/pages/login";
import AppLayout from "@/components/layouts/appLayout";

export default function App() {
    const { usuario, loading } = useAuth();

    if (loading) {
        return <div>Carregando...</div>;
    }

    return (
        <Routes>
            {!usuario && (
                <>
                    <Route path="/login" element={<Login />} />
                    <Route path="*" element={<Navigate to="/login" replace />} />
                </>
            )}

            {usuario && (
                <Route path="/*" element={<AppLayout/>} />
            )}
        </Routes>
    );
}
