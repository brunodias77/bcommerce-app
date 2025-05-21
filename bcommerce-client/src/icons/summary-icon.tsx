import React from "react";

type SummaryIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const SummaryIcon: React.FC<SummaryIconProps> = ({
    width = 21,
    height = 21,
    color = "#191C1F",
}) => {
    return (
        <svg width={width} height={height} viewBox="0 0 18 21" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M6 13.25H12" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M6 10.25H12" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M12 2.75H15.75C15.9489 2.75 16.1397 2.82902 16.2803 2.96967C16.421 3.11032 16.5 3.30109 16.5 3.5V19.25C16.5 19.4489 16.421 19.6397 16.2803 19.7803C16.1397 19.921 15.9489 20 15.75 20H2.25C2.05109 20 1.86032 19.921 1.71967 19.7803C1.57902 19.6397 1.5 19.4489 1.5 19.25V3.5C1.5 3.30109 1.57902 3.11032 1.71967 2.96967C1.86032 2.82902 2.05109 2.75 2.25 2.75H6" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M5.25 5.75V5C5.25 4.00544 5.64509 3.05161 6.34835 2.34835C7.05161 1.64509 8.00544 1.25 9 1.25C9.99456 1.25 10.9484 1.64509 11.6517 2.34835C12.3549 3.05161 12.75 4.00544 12.75 5V5.75H5.25Z" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
        </svg>
    );
};

export default SummaryIcon;





