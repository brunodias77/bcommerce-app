import React from "react";

type MoneyIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const MoneyIcon: React.FC<MoneyIconProps> = ({
    width = 20,
    height = 20,
    color = "#191C1F",
}) => {
    return (
        <svg
            width={width}
            height={height}
            viewBox="0 0 20 20"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
        >
            <path
                d="M10 4.75V6.25"
                stroke={color}
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
            />
            <path
                d="M10 13.75V15.25"
                stroke={color}
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
            />
            <path
                d="M10 19C14.9706 19 19 14.9706 19 10C19 5.02944 14.9706 1 10 1C5.02944 1 1 5.02944 1 10C1 14.9706 5.02944 19 10 19Z"
                stroke={color}
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
            />
            <path
                d="M7.75 13.75H11.125C11.6223 13.75 12.0992 13.5525 12.4508 13.2008C12.8025 12.8492 13 12.3723 13 11.875C13 11.3777 12.8025 10.9008 12.4508 10.5492C12.0992 10.1975 11.6223 10 11.125 10H8.875C8.37772 10 7.90081 9.80246 7.54917 9.45083C7.19754 9.09919 7 8.62228 7 8.125C7 7.62772 7.19754 7.15081 7.54917 6.79917C7.90081 6.44754 8.37772 6.25 8.875 6.25H12.25"
                stroke={color}
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
            />
        </svg>
    );
};

export default MoneyIcon;





