import { useEffect, useState } from "react";
import { getContatos, Contato } from "../api/contatos";
import FavoritoToggle from "../components/FavoritoToggle";

export default function Contatos() {
    const [contatos, setContatos] = useState<Contato[]>([]);

    useEffect(() => {
        carregarContatos();
    }, []);

    function carregarContatos() {
        getContatos().then(res => setContatos(res.data));
    }

    return (
        <div>
            <h1>Contatos</h1>

            {contatos.map(c => (
                <div
                    key={c.id}
                    style={{
                        display: "flex",
                        alignItems: "center",
                        gap: "12px",
                        marginBottom: "8px"
                    }}
                >
                    {/* ? TOGGLE DE FAVORITO */}
                    <FavoritoToggle
                        contatoId={c.id}
                        /* 
                          Se no futuro o backend mandar `ehFavorito`,
                          basta trocar para:
                          favoritoInicial={c.ehFavorito}
                        */
                        onToggle={carregarContatos}
                    />

                    {/* DADOS DO CONTATO */}
                    <div>
                        <p><strong>{c.nome}</strong></p>
                        <p>{c.email}</p>
                    </div>
                </div>
            ))}
        </div>
    );
}
