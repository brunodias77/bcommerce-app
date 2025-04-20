import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import Header from "@/components/header/header";
import HeaderInfo from "@/components/header/header-info";
import Footer from "@/components/ui/footer";
import { ToastContainer } from "react-toastify";

const inter = Inter({
  variable: "--font-inter",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "Bcommerce | Home",
  description: "Bem Vindo ao Bcommerce",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="pt-BR">
      <body
        className={`${inter.variable}  antialiased`}
      >
        <div className="App">
          <HeaderInfo />
          <Header />
          <main className="AppBody">{children}</main>
          {/* <div>{modal}</div>
          <Footer /> */}
          <Footer />
          <ToastContainer aria-label={undefined} />
        </div>
      </body>
    </html>
  );
}
