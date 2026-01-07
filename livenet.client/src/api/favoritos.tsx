import { api } from "./api";

export type Favorito = {
    id: string;
    contatoId: string;
    contato: {
        id: string;
        nome: string;
        email: string;
    };
};

/* Buscar favoritos do usuário logado */
export function getFavoritos() {
    return api.get<Favorito[]>("/favoritos");
}

/* Toggle favorito */
export function toggleFavorito(contatoId: string) {
    return api.post("/favoritos", { contatoId });
}
