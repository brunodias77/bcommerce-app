import React from "react";

type HeartIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const HeartIcon: React.FC<HeartIconProps> = ({
    width = 20,
    height = 18,
    color = "#111827",
}) => {
    return (
        <svg
            width={width}
            height={height}
            viewBox="0 0 20 18"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
        >
            <path
                d="M2.31802 2.31802C0.56066 4.07538 0.56066 6.92462 2.31802 8.68198L10.0001 16.364L17.682 8.68198C19.4393 6.92462 19.4393 4.07538 17.682 2.31802C15.9246 0.56066 13.0754 0.56066 11.318 2.31802L10.0001 3.63609L8.68198 2.31802C6.92462 0.56066 4.07538 0.56066 2.31802 2.31802Z"
                stroke={color}
                strokeWidth="2"
                strokeLinecap="round"
                strokeLinejoin="round"
            />
        </svg>
    );
};

export default HeartIcon;
