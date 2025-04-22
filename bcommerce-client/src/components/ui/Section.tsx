import React from "react";

interface SectionProps extends React.HTMLAttributes<HTMLElement> {
    children: React.ReactNode;
    padding?: string;
}

const Section = ({
    children,
    className = "",
    ...rest
}: SectionProps) => {
    return (
        <section
            {...rest}
            className={`w-full my-8 ${className}`}
        >
            {children}
        </section>
    );
};

export default Section;
