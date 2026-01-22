import { Outlet } from "react-router-dom";

export default function AppLayout() {
    return (
        <div className="flex min-h-screen bg-muted">
            {/* Sidebar depois */}
            <main className="flex-1 p-6">
                <Outlet />
            </main>
        </div>
    );
}
