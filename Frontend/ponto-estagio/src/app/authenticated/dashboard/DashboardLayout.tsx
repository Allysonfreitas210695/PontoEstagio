import Sidebar from "./Sidebar";

export default function DashboardLayout({ children }: { children: React.ReactNode }) {
  return (
    <div className="">
      <Sidebar />
      <main className="ml-13 w-full p-5 bg-gray-100 min-h-screen">{children}</main>
    </div>
  );
}
