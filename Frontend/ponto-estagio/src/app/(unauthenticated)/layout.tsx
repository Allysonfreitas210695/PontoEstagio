export default function UnauthenticatedLayout({ children }: { children: React.ReactNode }) {
    return (
      <div className="unauthenticated-layout">
        <main>{children}</main>
      </div>
    );
  }