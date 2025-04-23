import React from "react";

type TruckIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const TruckIcon: React.FC<TruckIconProps> = ({
    width = 24,
    height = 16,
    color = "#191C1F",
}) => {
    return (
        <svg
            width={width}
            height={height}
            viewBox="0 0 24 16"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
        >
            <path
                d="M16.5 2.50002H20.4938C20.6432 2.49904 20.7894 2.54329 20.9132 2.62695C21.037 2.71061 21.1326 2.82977 21.1875 2.96877L22.5 6.25002"
                stroke={color}
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
            />
            <path
                d="M1.5 8.5H16.5"
                stroke={color}
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
            />
            <path
                d="M17.625 15.25C18.8676 15.25 19.875 14.2426 19.875 13C19.875 11.7574 18.8676 10.75 17.625 10.75C16.3824 10.75 15.375 11.7574 15.375 13C15.375 14.2426 16.3824 15.25 17.625 15.25Z"
                stroke={color}
                strokeWidth="1.5"
                strokeMiterlimit="10"
            />
            <path
                d="M6.375 15.25C7.61764 15.25 8.625 14.2426 8.625 13C8.625 11.7574 7.61764 10.75 6.375 10.75C5.13236 10.75 4.125 11.7574 4.125 13C4.125 14.2426 5.13236 15.25 6.375 15.25Z"
                stroke={color}
                strokeWidth="1.5"
                strokeMiterlimit="10"
            />
            <path
                d="M15.375 13H8.625"
                stroke={color}
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
            />
            <path
                d="M4.125 13H2.25C2.05109 13 1.86032 12.921 1.71967 12.7803C1.57902 12.6397 1.5 12.4489 1.5 12.25V1.75C1.5 1.55109 1.57902 1.36032 1.71967 1.21967C1.86032 1.07902 2.05109 1 2.25 1H16.5V11.05"
                stroke={color}
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
            />
            <path
                d="M16.5 6.25H22.5V12.25C22.5 12.4489 22.421 12.6397 22.2803 12.7803C22.1397 12.921 21.9489 13 21.75 13H19.875"
                stroke={color}
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
            />
        </svg>
    );
};

export default TruckIcon;
