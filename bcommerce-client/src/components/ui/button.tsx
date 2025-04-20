import React from "react";

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
    label?: string;
    variant?: "primary" | "secondary";
    size?: "small" | "medium" | "large";
    fullWidth?: boolean;
    iconLeft?: React.ReactNode;
    isLoading?: boolean;
    children?: React.ReactNode;
}

const Button = ({
    children,
    label,
    variant = "primary",
    size = "medium",
    fullWidth = false,
    className = "",
    disabled = false,
    iconLeft,
    isLoading = false,
    ...props
}: ButtonProps) => {
    const base = "rounded focus:outline-none transition transform active:scale-95 flex items-center justify-center cursor-pointer";

    const variants = {
        primary: "bg-[#2D2926] text-white hover:brightness-50",
        secondary: "bg-[#FEC857] text-black hover:brightness-90",
    };

    const sizes = {
        small: "py-1 px-2 text-sm gap-1",
        medium: "py-2 px-4 text-base gap-2",
        large: "py-4 px-8 text-lg gap-3",
    };

    const combinedClass = [
        base,
        variants[variant],
        sizes[size],
        fullWidth ? "w-full" : "",
        disabled || isLoading ? "opacity-50 cursor-not-allowed" : "",
        className,
    ]
        .filter(Boolean)
        .join(" ");

    return (
        <button
            className={combinedClass}
            disabled={disabled || isLoading}
            aria-label={label}
            {...props}
        >
            {isLoading ? (
                <div className="animate-spin w-4 h-4 border-2 border-t-transparent border-white rounded-full" />
            ) : (
                <>
                    {iconLeft && <span className="mr-1">{iconLeft}</span>}
                    {children}
                </>
            )}
        </button>
    );
};

export default Button;
