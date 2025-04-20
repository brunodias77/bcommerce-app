import React from 'react';

type MenuOpenIconProps = {
    width?: number;
    height?: number;
    color?: string;
}

const MenuOpenIcon: React.FC<MenuOpenIconProps> = ({
    width = 20,
    height = 20,
    color = "#111827",
}) => {
    return (
        <svg width={width} height={height} viewBox="0 0 18 14" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M1 1H17M1 7H17M10 13H17" stroke={color} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
        </svg>

    );
}

export default MenuOpenIcon