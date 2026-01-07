import { createContext, useContext, useState } from "react";
import type { ReactNode } from "react";

export type Usuario = {
    id: string;
    nome: string;
    email: string;
};

type AuthContextType = {
    usuario: Usuario | null;
    setUsuario: (usuario: Usuario | null) => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
    const [usuario, setUsuario] = useState<Usuario | null>(null);

    return (
        <AuthContext.Provider value={{ usuario, setUsuario }}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth() {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error("useAuth deve ser usado dentro de AuthProvider");
    }
    return context;
}
