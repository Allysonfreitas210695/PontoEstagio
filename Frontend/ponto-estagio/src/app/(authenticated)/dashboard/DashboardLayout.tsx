import Sidebar from "./Sidebar";

export default function DashboardLayout({ children }: { children: React.ReactNode }) {
  return (
    <div className="w-screen overflow-x-hidden">

      <Sidebar />
      <main className="ml-12 w-full min-h-screen flex flex-col bg-[#FAF9F6] ">{children}</main>
    </div>
  );
}
