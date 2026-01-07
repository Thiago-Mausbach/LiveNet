import { Button } from "@/components/ui/button";
import { LogOut } from "lucide-react";

export default function Header() {
    // mock estático
    const usuario = {
        nome: "Thiago Mausbach",
    };

    return (
        <header className="h-14 border-b px-6 flex items-center justify-between">
            <div className="font-semibold">
                Dashboard
            </div>

            <div className="flex items-center gap-4">
                <span className="text-sm text-muted-foreground">
                    {usuario.nome}
                </span>

                <Button variant="ghost" size="icon">
                    <LogOut className="h-4 w-4" />
                </Button>
            </div>
        </header>
    );
}
