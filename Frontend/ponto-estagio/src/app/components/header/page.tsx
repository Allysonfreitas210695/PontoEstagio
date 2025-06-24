import Image from "next/image";
import Link from "next/link";
import Logo from "../../../../public/assets/image/logo.png";

export default function Header() {
  return (
    <header className="absolute top-8 left-8 z-10">
      <Link href="/">
        <Image src={Logo} alt="Logo" width={150} height={40} />
      </Link>
    </header>
  );
}
