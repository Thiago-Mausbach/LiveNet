import { api } from "./api";

export type Usuario = {
    id: string;
    nome: string;
    email: string;
};

/* Dados do usuário logado */
export function getMinhaConta() {
    return api.get<Usuario>("/usuarios/me");
}

/* Atualizar dados da conta */
export function atualizarConta(dados: Partial<Usuario>) {
    return api.put("/usuarios/me", dados);
}
