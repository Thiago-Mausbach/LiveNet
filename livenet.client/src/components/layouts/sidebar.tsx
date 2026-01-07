import { Users, Star, User } from "lucide-react";
import { NavLink } from "react-router-dom";

const items = [
    { label: "Contatos", icon: Users, to: "/contatos" },
    { label: "Favoritos", icon: Star, to: "/favoritos" },
    { label: "Conta", icon: User, to: "/conta" },
];

export default function Sidebar() {
    return (
        <aside className="w-64 border-r bg-muted/40 h-screen p-4">
            <div className="mb-8 text-xl font-bold">
                LiveNet
            </div>

            <nav className="space-y-1">
                {items.map(item => (
                    <NavLink
                        key={item.to}
                        to={item.to}
                        className={({ isActive }) =>
                            `flex items-center gap-3 rounded-md px-3 py-2 text-sm transition
               ${isActive ? "bg-primary text-primary-foreground"
                                : "hover:bg-muted"}`
                        }
                    >
                        <item.icon className="h-4 w-4" />
                        {item.label}
                    </NavLink>
                ))}
            </nav>
        </aside>
    );
}
