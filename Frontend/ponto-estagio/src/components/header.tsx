import Image from "next/image";
import Link from "next/link";
import React from "react";

export function Header() {
  return (
    <header className="absolute top-8 left-8 z-10">
      <Link href="/">
        <Image
          src={"/assets/image/logo.png"}
          alt="Logo"
          width={150}
          height={40}
        />
      </Link>
    </header>
  );
}
