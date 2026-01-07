import Sidebar from "./sidebar";
import Header from "./header";

type Props = {
    children: React.ReactNode;
};

export default function AppLayout({ children }: Props) {
    return (
        <div className="flex h-screen">
            <Sidebar />

            <div className="flex flex-col flex-1">
                <Header />

                <main className="flex-1 p-6 overflow-auto">
                    {children}
                </main>
            </div>
        </div>
    );
}
