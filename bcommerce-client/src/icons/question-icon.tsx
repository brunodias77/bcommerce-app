



import React from "react";

type QuestionIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const QuestionIcon: React.FC<QuestionIconProps> = ({
    width = 21,
    height = 21,
    color = "#191C1F",
}) => {
    return (
        <svg width={width} height={height} viewBox="0 0 20 19" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M4.7125 10.5L1 13.5V1.5C1 1.30109 1.07902 1.11032 1.21967 0.96967C1.36032 0.829018 1.55109 0.75 1.75 0.75H13.75C13.9489 0.75 14.1397 0.829018 14.2803 0.96967C14.421 1.11032 14.5 1.30109 14.5 1.5V9.75C14.5 9.94891 14.421 10.1397 14.2803 10.2803C14.1397 10.421 13.9489 10.5 13.75 10.5H4.7125Z" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M5.5 10.5V14.25C5.5 14.4489 5.57902 14.6397 5.71967 14.7803C5.86032 14.921 6.05109 15 6.25 15H15.2875L19 18V6C19 5.80109 18.921 5.61032 18.7803 5.46967C18.6397 5.32902 18.4489 5.25 18.25 5.25H14.5" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
        </svg>
    );
};

export default QuestionIcon;
