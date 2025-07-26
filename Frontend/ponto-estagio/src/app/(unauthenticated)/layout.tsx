import { Footer } from "@/components/footer";
import { Header } from "@/components/header";

export default function UnauthenticatedLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <div className="unauthenticated-layout">
      <Header />
      <main>{children}</main>
      <Footer />
    </div>
  );
}
