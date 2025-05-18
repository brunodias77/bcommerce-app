import React from "react";

type AlertIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const AlertIcon: React.FC<AlertIconProps> = ({
    width = 21,
    height = 21,
    color = "#191C1F",
}) => {
    return (
        < svg width={width} height={height} viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg" >
            <path d="M10 19C14.9706 19 19 14.9706 19 10C19 5.02944 14.9706 1 10 1C5.02944 1 1 5.02944 1 10C1 14.9706 5.02944 19 10 19Z" stroke={color} strokeWidth="1.5" strokeMiterlimit="10" />
            <path d="M10 5.5V10.75" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M10 15.25C10.6213 15.25 11.125 14.7463 11.125 14.125C11.125 13.5037 10.6213 13 10 13C9.37868 13 8.875 13.5037 8.875 14.125C8.875 14.7463 9.37868 15.25 10 15.25Z" fill={color} />
        </svg >
    );
};

export default AlertIcon;


