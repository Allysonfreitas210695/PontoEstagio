export default function AuthenticatedLayout({ children }: { children: React.ReactNode }) {
    return (
      <div className="authenticated-layout">
        <main>{children}</main>
      </div>
    );
  }