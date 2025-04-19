import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";

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
          {/* <Header /> */}
          <main className="AppBody">{children}</main>
          {/* <div>{modal}</div>
          <Footer /> */}
        </div>
      </body>
    </html>
  );
}
