import { api } from "./api";

/* Tipagem básica (pode evoluir depois) */
export type Contato = {
    id: string;
    nome: string;
    email: string;
    telefone?: string;
    isFavorito: boolean;
};

/* Buscar todos os contatos */
export function getContatos() {
    return api.get<Contato[]>("/contatos");
}

/* Criar contato */
export function criarContato(dados: Omit<Contato, "id">) {
    return api.post("/contatos", dados);
}

/* Atualizar contato */
export function atualizarContato(id: string, dados: Partial<Contato>) {
    return api.put(`/contatos/${id}`, dados);
}

/* Remover contato */
export function removerContato(id: string) {
    return api.delete(`/contatos/${id}`);
}
