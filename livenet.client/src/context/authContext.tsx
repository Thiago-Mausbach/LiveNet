import {
    createContext,
    useContext,
    useEffect,
    useState,
} from "react";
import { Usuario } from "@/types/usuario";
import { getUsuarioLogado } from "@/api/usuario";

interface AuthContextData {
    usuario: Usuario | null;
    setUsuario: (usuario: Usuario | null) => void;

    // ➕ novos (não quebram nada)
    loading: boolean;
    logout: () => void;
}

const AuthContext = createContext<AuthContextData>(
    {} as AuthContextData
);

export function AuthProvider({
    children,
}: {
    children: React.ReactNode;
}) {
    const [usuario, setUsuario] = useState<Usuario | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        getUsuarioLogado()
            .then(res => setUsuario(res.data))
            .catch(() => setUsuario(null))
            .finally(() => setLoading(false));
    }, []);

    function logout() {
        setUsuario(null);
        // futuro: api.post("/auth/logout")
    }

    return (
        <AuthContext.Provider
            value={{ usuario, setUsuario, loading, logout }}
        >
            {children}
        </AuthContext.Provider>
    );
}

// 🔹 mantém compatibilidade
export function useAuth() {
    return useContext(AuthContext);
}

// 🔹 opcional: mantém export antigo
export { AuthContext };
