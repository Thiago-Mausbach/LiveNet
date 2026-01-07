import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Star } from "lucide-react";
import { toggleFavorito } from "@/api/favoritos";

type Props = {
    contatoId: string;
    favorito: boolean;
    onToggle?: () => void;
};

export default function FavoritoToggle({
    contatoId,
    favorito,
    onToggle,
}: Props) {
    const [loading, setLoading] = useState(false);

    async function handleToggle() {
        setLoading(true);
        try {
            await toggleFavorito(contatoId);
            onToggle?.();
        } finally {
            setLoading(false);
        }
    }

    return (
        <Button
            variant="ghost"
            size="icon"
            onClick={handleToggle}
            disabled={loading}
        >
            <Star
                className={`h-5 w-5 ${favorito ? "fill-yellow-400 text-yellow-400" : "text-muted-foreground"
                    }`}
            />
        </Button>
    );
}
