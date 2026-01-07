import { Card, CardContent, CardHeader } from "@/components/ui/card";
import FavoritoToggle from "./FavoritoToggle";
import { Contato } from "@/api/contatos";

type Props = {
    contato: Contato;
    onToggleFavorito: () => void;
};

export default function ContatoCard({ contato, onToggleFavorito }: Props) {
    return (
        <Card>
            <CardHeader className="flex flex-row items-center justify-between">
                <div>
                    <p className="font-semibold">{contato.nome}</p>
                    <p className="text-sm text-muted-foreground">{contato.email}</p>
                </div>

                <FavoritoToggle
                    contatoId={contato.id}
                    favorito={contato.isFavorito}
                    onToggle={onToggleFavorito}
                />
            </CardHeader>

            <CardContent>
                {/* espaço futuro: telefone, tags, ações */}
            </CardContent>
        </Card>
    );
}
