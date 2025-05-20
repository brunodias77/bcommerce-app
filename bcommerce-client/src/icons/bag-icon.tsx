import React from "react";

type BagIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const BagIcon: React.FC<BagIconProps> = ({
    width = 21,
    height = 21,
    color = "#191C1F",
}) => {
    return (
        <svg width={width} height={height} viewBox="0 0 20 19" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M18.25 4.75H1.75C1.33579 4.75 1 5.08579 1 5.5V17.5C1 17.9142 1.33579 18.25 1.75 18.25H18.25C18.6642 18.25 19 17.9142 19 17.5V5.5C19 5.08579 18.6642 4.75 18.25 4.75Z" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M6.25 7.75V4.75C6.25 3.75544 6.64509 2.80161 7.34835 2.09835C8.05161 1.39509 9.00544 1 10 1C10.9946 1 11.9484 1.39509 12.6517 2.09835C13.3549 2.80161 13.75 3.75544 13.75 4.75V7.75" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
        </svg>
    );
};

export default BagIcon;




