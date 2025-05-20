import React from "react";

type LikeIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const LikeIcon: React.FC<LikeIconProps> = ({
    width = 21,
    height = 21,
    color = "#191C1F",
}) => {
    return (
        <svg width={width} height={height} viewBox="0 0 22 20" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M2 8.75H6.5V18.5H2C1.80109 18.5 1.61032 18.421 1.46967 18.2803C1.32902 18.1397 1.25 17.9489 1.25 17.75V9.5C1.25 9.30109 1.32902 9.11032 1.46967 8.96967C1.61032 8.82902 1.80109 8.75 2 8.75V8.75Z" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M6.5 8.75L10.25 1.25C11.0456 1.25 11.8087 1.56607 12.3713 2.12868C12.9339 2.69129 13.25 3.45435 13.25 4.25V6.5H19.0531C19.2658 6.49947 19.4762 6.54447 19.67 6.63197C19.8639 6.71946 20.0368 6.84743 20.1771 7.00728C20.3174 7.16712 20.4219 7.35514 20.4835 7.55871C20.5451 7.76228 20.5625 7.97667 20.5344 8.1875L19.4094 17.1875C19.3639 17.5488 19.1885 17.8812 18.9159 18.1227C18.6434 18.3642 18.2923 18.4983 17.9281 18.5H6.5" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
        </svg>

    );
};

export default LikeIcon;



