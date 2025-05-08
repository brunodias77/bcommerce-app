import React from "react";

type LoadingIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const LoadingIcon: React.FC<LoadingIconProps> = ({
    width = 22,
    height = 18,
    color = "#191C1F",
}) => {
    return (
        <svg width={width} height={height} viewBox="0 0 20 18" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M5.48126 6.34686H0.981262V1.84686" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M15.8313 3.16873C15.0659 2.40224 14.1569 1.79417 13.1563 1.37929C12.1558 0.964403 11.0832 0.750854 10 0.750854C8.91683 0.750854 7.84427 0.964403 6.84369 1.37929C5.8431 1.79417 4.93413 2.40224 4.16876 3.16873L0.981262 6.34685" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M14.5187 11.6531H19.0187V16.1531" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M4.16876 14.8313C4.93413 15.5977 5.8431 16.2058 6.84368 16.6207C7.84427 17.0356 8.91683 17.2491 10 17.2491C11.0832 17.2491 12.1558 17.0356 13.1563 16.6207C14.1569 16.2058 15.0659 15.5977 15.8313 14.8313L19.0188 11.6531" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
        </svg>

    );
};

export default LoadingIcon;
