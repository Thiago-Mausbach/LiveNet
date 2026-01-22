import { createContext, useContext, useState } from "react";

type Usuario = {
    id: string;
    nome: string;
    email: string;
};

type AuthContextType = {
    usuario: Usuario | null;
    login: (usuario: Usuario) => void;
    logout: () => void;
};

const AuthContext = createContext<AuthContextType | null>(null);

export function AuthProvider({ children }: { children: React.ReactNode }) {
    const [usuario, setUsuario] = useState<Usuario | null>(null);

    function login(usuario: Usuario) {
        setUsuario(usuario);
    }

    function logout() {
        setUsuario(null);
    }

    return (
        <AuthContext.Provider value={{ usuario, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth() {
    const ctx = useContext(AuthContext);
    if (!ctx) throw new Error("useAuth deve estar dentro de AuthProvider");
    return ctx;
}