// app/hooks/usePageName.ts
"use client"; // Necessário para hooks de navegação no App Router
import { usePathname } from "next/navigation";
import { useMemo } from "react";

const usePageName = () => {
  const pathname = usePathname();

  return useMemo(() => {
    const pathSegments = pathname
      .split("/")
      .filter((segment) => segment.length > 0);

    return pathSegments[pathSegments.length - 1] || "home";
  }, [pathname]);
};

export default usePageName;
