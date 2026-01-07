import { api } from "./api";
import { Usuario } from "@/types/usuario";

export function getUsuarioLogado() {
    return api.get<Usuario>("/usuarios/me");
}

export function atualizarUsuario(dados: Pick<Usuario, "nome" | "email">) {
    return api.put("/usuarios/me", dados);
}

export function redefinirSenha(senha: string) {
    return api.put("/usuarios/me/senha", { senha });
}
