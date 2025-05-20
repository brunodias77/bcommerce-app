import React from "react";

type PencilIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const PencilIcon: React.FC<PencilIconProps> = ({
    width = 21,
    height = 21,
    color = "#191C1F",
}) => {
    return (
        <svg width={width} height={height} viewBox="0 0 19 19" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M5.69063 18.25H1.5C1.30109 18.25 1.11033 18.171 0.969675 18.0304C0.829022 17.8897 0.750005 17.699 0.750005 17.5V13.3094C0.749663 13.212 0.768533 13.1155 0.805532 13.0254C0.842532 12.9354 0.896936 12.8534 0.96563 12.7844L12.2156 1.53441C12.2854 1.46355 12.3686 1.40728 12.4603 1.36886C12.5521 1.33045 12.6505 1.31067 12.75 1.31067C12.8495 1.31067 12.9479 1.33045 13.0397 1.36886C13.1314 1.40728 13.2146 1.46355 13.2844 1.53441L17.4656 5.71566C17.5365 5.78545 17.5928 5.86864 17.6312 5.96038C17.6696 6.05212 17.6894 6.15058 17.6894 6.25004C17.6894 6.3495 17.6696 6.44796 17.6312 6.5397C17.5928 6.63144 17.5365 6.71463 17.4656 6.78441L6.21563 18.0344C6.1466 18.1031 6.06469 18.1575 5.9746 18.1945C5.88452 18.2315 5.78802 18.2504 5.69063 18.25V18.25Z" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M9.75 4L15 9.25" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
        </svg>
    );
};

export default PencilIcon;





