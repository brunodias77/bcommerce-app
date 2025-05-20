import React from "react";

type ArrowRigthIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const ArrowRigthIcon: React.FC<ArrowRigthIconProps> = ({
    width = 21,
    height = 21,
    color = "#191C1F",
}) => {
    return (
        <svg xmlns="http://www.w3.org/2000/svg" width={width} height={height} fill={color} viewBox="0 0 256 256"><path d="M181.66,133.66l-80,80a8,8,0,0,1-11.32-11.32L164.69,128,90.34,53.66a8,8,0,0,1,11.32-11.32l80,80A8,8,0,0,1,181.66,133.66Z"></path></svg>
    );
};

export default ArrowRigthIcon;




