import React from "react";

type CartIconProps = {
    width?: number;
    height?: number;
    color?: string;
};

const CartIcon: React.FC<CartIconProps> = ({
    width = 21,
    height = 21,
    color = "#191C1F",
}) => {
    return (
        <svg
            width={width}
            height={height}
            viewBox="0 0 21 21"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
        >
            <path
                d="M17.25 16.25H6.54375L3.92813 1.86875C3.89752 1.69653 3.80768 1.54042 3.67415 1.42743C3.54062 1.31444 3.37179 1.25168 3.19687 1.25H1.5"
                stroke={color}
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
            />
            <path
                d="M7.5 20C8.53553 20 9.375 19.1605 9.375 18.125C9.375 17.0895 8.53553 16.25 7.5 16.25C6.46447 16.25 5.625 17.0895 5.625 18.125C5.625 19.1605 6.46447 20 7.5 20Z"
                stroke={color}
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
            />
            <path
                d="M17.25 20C18.2855 20 19.125 19.1605 19.125 18.125C19.125 17.0895 18.2855 16.25 17.25 16.25C16.2145 16.25 15.375 17.0895 15.375 18.125C15.375 19.1605 16.2145 20 17.25 20Z"
                stroke={color}
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
            />
            <path
                d="M5.85938 12.5H17.6344C17.985 12.5011 18.3247 12.3785 18.5939 12.1539C18.8631 11.9293 19.0445 11.617 19.1063 11.2719L20.25 5H4.5"
                stroke={color}
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
            />
        </svg>
    );
};

export default CartIcon;


// import React from "react";

// type CartIconProps = {
//     width?: number;
//     height?: number;
//     color?: string;
// };

// const CartIcon: React.FC<CartIconProps> = ({
//     width = 20,
//     height = 20,
//     color = "#111827",
// }) => {
//     return (
//         <svg
//             width={width}
//             height={height}
//             viewBox="0 0 20 20"
//             fill="none"
//             xmlns="http://www.w3.org/2000/svg"
//         >
//             <path
//                 d="M1 1H3L3.4 3M5 11H15L19 3H3.4M5 11L3.4 3M5 11L2.70711 13.2929C2.07714 13.9229 2.52331 15 3.41421 15H15M15 15C13.8954 15 13 15.8954 13 17C13 18.1046 13.8954 19 15 19C16.1046 19 17 18.1046 17 17C17 15.8954 16.1046 15 15 15ZM7 17C7 18.1046 6.10457 19 5 19C3.89543 19 3 18.1046 3 17C3 15.8954 3.89543 15 5 15C6.10457 15 7 15.8954 7 17Z"
//                 stroke={color}
//                 strokeWidth="2"
//                 strokeLinecap="round"
//                 strokeLinejoin="round"
//             />
//         </svg>
//     );
// };

// export default CartIcon;
