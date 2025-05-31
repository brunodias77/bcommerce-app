import React from "react";

type PlusIconProps = {
    width?: number;
    height?: number;
    color?: string;
    isActive?: boolean;
};

const PlusIcon: React.FC<PlusIconProps> = ({
    width = 21,
    height = 21,
    color = "#191C1F",
    isActive = true,
}) => {
    return (
        <svg xmlns="http://www.w3.org/2000/svg" width={width} height={height} fill={isActive ? color : "#9CA3AF"} viewBox="0 0 256 256"><path d="M224,128a8,8,0,0,1-8,8H136v80a8,8,0,0,1-16,0V136H40a8,8,0,0,1,0-16h80V40a8,8,0,0,1,16,0v80h80A8,8,0,0,1,224,128Z"></path></svg>
    );
};

export default PlusIcon;





