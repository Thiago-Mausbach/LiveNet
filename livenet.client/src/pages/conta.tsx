import { useEffect, useState } from "react";
import { api } from "@/api/api";
import { Usuario } from "@/types/usuario";

import {
    Card,
    CardContent,
    CardHeader,
    CardTitle,
} from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Separator } from "@/components/ui/separator";

export default function Conta() {
    const [usuario, setUsuario] = useState<Usuario | null>(null);
    const [loading, setLoading] = useState(true);

    const [senha, setSenha] = useState("");
    const [confirmarSenha, setConfirmarSenha] = useState("");

    // 🔹 carregar usuário logado
    useEffect(() => {
        api.get<Usuario>("/usuarios/me")
            .then(res => setUsuario(res.data))
            .finally(() => setLoading(false));
    }, []);

    function atualizarCampo(
        campo: keyof Usuario,
        valor: string
    ) {
        if (!usuario) return;
        setUsuario({ ...usuario, [campo]: valor });
    }

    function salvarDados() {
        if (!usuario) return;

        api.put("/usuarios/me", {
            nome: usuario.nome,
            email: usuario.email,
        });
    }

    function redefinirSenha() {
        if (senha !== confirmarSenha) {
            alert("As senhas não coincidem");
            return;
        }

        api.put("/usuarios/me/senha", { senha })
            .then(() => {
                setSenha("");
                setConfirmarSenha("");
            });
    }

    if (loading) {
        return <div>Carregando...</div>;
    }

    if (!usuario) {
        return <div>Usuário não encontrado</div>;
    }

    return (
        <div className="space-y-6 max-w-2xl">
            <h1 className="text-2xl font-bold">Minha conta</h1>

            {/* 🔹 DADOS DO USUÁRIO */}
            <Card>
                <CardHeader>
                    <CardTitle>Informações pessoais</CardTitle>
                </CardHeader>

                <CardContent className="space-y-4">
                    <div className="space-y-2">
                        <Label htmlFor="nome">Nome</Label>
                        <Input
                            id="nome"
                            value={usuario.nome}
                            onChange={e =>
                                atualizarCampo("nome", e.target.value)
                            }
                        />
                    </div>

                    <div className="space-y-2">
                        <Label htmlFor="email">Email</Label>
                        <Input
                            id="email"
                            type="email"
                            value={usuario.email}
                            onChange={e =>
                                atualizarCampo("email", e.target.value)
                            }
                        />
                    </div>

                    <Button onClick={salvarDados}>
                        Salvar alterações
                    </Button>
                </CardContent>
            </Card>

            {/* 🔹 SEGURANÇA */}
            <Card>
                <CardHeader>
                    <CardTitle>Segurança</CardTitle>
                </CardHeader>

                <CardContent className="space-y-4">
                    <div className="space-y-2">
                        <Label htmlFor="senha">Nova senha</Label>
                        <Input
                            id="senha"
                            type="password"
                            value={senha}
                            onChange={e => setSenha(e.target.value)}
                        />
                    </div>

                    <div className="space-y-2">
                        <Label htmlFor="confirmarSenha">
                            Confirmar nova senha
                        </Label>
                        <Input
                            id="confirmarSenha"
                            type="password"
                            value={confirmarSenha}
                            onChange={e =>
                                setConfirmarSenha(e.target.value)
                            }
                        />
                    </div>

                    <Separator />

                    <Button
                        variant="destructive"
                        onClick={redefinirSenha}
                    >
                        Redefinir senha
                    </Button>
                </CardContent>
            </Card>
        </div>
    );
}
