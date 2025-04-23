import React from "react";

type DevolutionIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const DevolutionIcon: React.FC<DevolutionIconProps> = ({
    width = 20,
    height = 20,
    color = "#111827",
}) => {
    return (
        <svg width={width} height={height} viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M22.25 15.2217C22.25 19.0917 19.12 22.2217 15.25 22.2217L16.3 20.4717" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M2.25 9.22168C2.25 5.35168 5.38 2.22168 9.25 2.22168L8.2 3.97168" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M13.9492 4.67188L17.9291 6.97186L21.8691 4.68188" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M17.9297 11.0419V6.96191" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M16.9896 2.43168L14.5896 3.76164C14.0496 4.06164 13.5996 4.82163 13.5996 5.44163V7.98167C13.5996 8.60167 14.0396 9.36166 14.5896 9.66166L16.9896 10.9917C17.4996 11.2817 18.3396 11.2817 18.8596 10.9917L21.2596 9.66166C21.7996 9.36166 22.2496 8.60167 22.2496 7.98167V5.44163C22.2496 4.82163 21.8096 4.06164 21.2596 3.76164L18.8596 2.43168C18.3496 2.15168 17.5096 2.15168 16.9896 2.43168Z" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M2.59961 15.6719L6.56961 17.9718L10.5196 15.6819" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M6.57031 22.0419V17.9619" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M5.64 13.4317L3.24001 14.7617C2.70001 15.0617 2.25 15.8216 2.25 16.4416V18.9817C2.25 19.6017 2.69001 20.3617 3.24001 20.6617L5.64 21.9917C6.15 22.2817 6.98999 22.2817 7.50999 21.9917L9.91 20.6617C10.45 20.3617 10.9 19.6017 10.9 18.9817V16.4416C10.9 15.8216 10.46 15.0617 9.91 14.7617L7.50999 13.4317C6.98999 13.1517 6.15 13.1517 5.64 13.4317Z" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
        </svg>
    );
};

export default DevolutionIcon;



