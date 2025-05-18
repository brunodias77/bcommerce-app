import React from "react";

type ArrowDownIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const ArrowDownIcon: React.FC<ArrowDownIconProps> = ({
    width = 21,
    height = 21,
    color = "#191C1F",
}) => {
    return (
        <svg width={width} height={height} viewBox="0 0 18 10" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M16.5 1L9 8.5L1.5 1" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
        </svg>
    );
};

export default ArrowDownIcon;


