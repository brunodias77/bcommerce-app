import React from "react";

type ArrowUpIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const ArrowUpIcon: React.FC<ArrowUpIconProps> = ({
    width = 21,
    height = 21,
    color = "#191C1F",
}) => {
    return (
        <svg width={width} height={height} viewBox="0 0 18 10" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M1.5 9L9 1.5L16.5 9" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
        </svg>

    );
};

export default ArrowUpIcon;


