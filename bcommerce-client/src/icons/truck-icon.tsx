import React from "react";

type TruckIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const TruckIcon: React.FC<TruckIconProps> = ({
    width = 20,
    height = 20,
    color = "#111827",
}) => {
    return (
        <svg width={width} height={height} viewBox="0 0 20 16" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M11 12V2C11 1.44772 10.5523 1 10 1H2C1.44772 1 1 1.44772 1 2V12C1 12.5523 1.44772 13 2 13H3M11 12C11 12.5523 10.5523 13 10 13H7M11 12L11 4C11 3.44772 11.4477 3 12 3H14.5858C14.851 3 15.1054 3.10536 15.2929 3.29289L18.7071 6.70711C18.8946 6.89464 19 7.149 19 7.41421V12C19 12.5523 18.5523 13 18 13H17M11 12C11 12.5523 11.4477 13 12 13H13M3 13C3 14.1046 3.89543 15 5 15C6.10457 15 7 14.1046 7 13M3 13C3 11.8954 3.89543 11 5 11C6.10457 11 7 11.8954 7 13M13 13C13 14.1046 13.8954 15 15 15C16.1046 15 17 14.1046 17 13M13 13C13 11.8954 13.8954 11 15 11C16.1046 11 17 11.8954 17 13" stroke={color} strokeWidth="2" />
        </svg>
    );
};

export default TruckIcon;