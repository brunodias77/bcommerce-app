import React from "react";

type CreditCardIconProps = {
    width?: number;
    height?: number;
    color?: string;
    isActive?: boolean;
};

const CreditCardIcon: React.FC<CreditCardIconProps> = ({
    width = 21,
    height = 21,
    color = "#191C1F",
    isActive = true,
}) => {
    return (
        <svg width={width} height={height} viewBox="0 0 22 16" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M20 1.25H2C1.58579 1.25 1.25 1.58579 1.25 2V14C1.25 14.4142 1.58579 14.75 2 14.75H20C20.4142 14.75 20.75 14.4142 20.75 14V2C20.75 1.58579 20.4142 1.25 20 1.25Z" stroke={isActive ? color : "#9CA3AF"} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M14.75 11.75H17.75" stroke={isActive ? color : "#9CA3AF"} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M10.25 11.75H11.75" stroke={isActive ? color : "#9CA3AF"} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M1.25 5.08447H20.75" stroke={isActive ? color : "#9CA3AF"} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
        </svg>
    );
};

export default CreditCardIcon;



