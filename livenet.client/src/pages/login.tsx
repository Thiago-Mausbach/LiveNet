import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";

export default function Login() {
    return (
        <div className="min-h-screen flex items-center justify-center">
            <Card className="w-full max-w-sm">
                <CardHeader>
                    <CardTitle className="text-center">Faça seu login</CardTitle>
                </CardHeader>

                <CardContent className="space-y-4">
                    <div className="space-y-1">
                        <Label htmlFor="email">Email</Label>
                        <Input id="email" type="email" placeholder="seu@email.com" />
                    </div>

                    <div className="space-y-1">
                        <Label htmlFor="senha">Senha</Label>
                        <Input id="senha" type="password" />
                    </div>

                    <Button className="w-full">
                        Login
                    </Button>
                </CardContent>
            </Card>
        </div>
    );
}
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
