import { useEffect, useState } from "react";
import { getFavoritos, Favorito } from "../api/favoritos";

export default function Favoritos() {
    const [favoritos, setFavoritos] = useState<Favorito[]>([]);

    useEffect(() => {
        carregarFavoritos();
    }, []);

    function carregarFavoritos() {
        getFavoritos().then(res => setFavoritos(res.data));
    }

    return (
        <div>
            <h1>Favoritos</h1>

            {favoritos.length === 0 && <p>Nenhum favorito ainda</p>}

            {favoritos.map(f => (
                <div key={f.id}>
                    <p>{f.contato.nome} - {f.contato.email}</p>
                </div>
            ))}
        </div>
    );
}
