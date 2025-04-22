type FireIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const FireIcon: React.FC<FireIconProps> = ({ color, height, width }) => {
    return (
        <svg width={width} height={height} viewBox="0 0 18 20" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M14.6569 16.6568C11.5327 19.781 6.46734 19.781 3.34315 16.6568C1.78105 15.0947 1 13.0474 1 11C0.999997 8.95262 1.78105 6.90523 3.34315 5.34313C3.34315 5.34313 4.00004 6.99995 6.00004 7.99995C6.00004 5.99995 6.50004 2.99996 8.98588 1C11 3 13.0912 3.77745 14.6569 5.34313C16.219 6.90523 17 8.95262 17 11C17 13.0474 16.2189 15.0947 14.6569 16.6568Z" stroke={color} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
            <path d="M6.87868 14.1213C8.05025 15.2928 9.94975 15.2928 11.1213 14.1213C11.7071 13.5355 12 12.7677 12 12C12 11.2322 11.7071 10.4644 11.1213 9.87863C10.5392 9.29655 9.77747 9.00367 9.01456 8.99999L7.99995 11.9999L6 12C6.00001 12.7677 6.2929 13.5355 6.87868 14.1213Z" stroke={color} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
        </svg>

    );
}

export default FireIcon;