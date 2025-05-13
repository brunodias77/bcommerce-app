import React from "react";

type FavoriteIconProps = {
    isFavorite: boolean;
    width?: number;
    height?: number;
    color?: string;         // Cor quando favorito
    strokeColor?: string;   // Cor quando não favorito
};

const FavoriteIcon: React.FC<FavoriteIconProps> = ({
    isFavorite,
    width = 20,
    height = 20,
    color = "#2d2926",
    strokeColor = "#777777",
}) => {
    return (
        <svg
            width={width}
            height={height}
            viewBox="0 0 20 18"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
        >
            <path
                d="M19 5.25696C19 2.77168 16.9013 0.756958 14.3125 0.756958C12.3769 0.756958 10.7153 1.88324 10 3.49038C9.28472 1.88324 7.62312 0.756958 5.6875 0.756958C3.09867 0.756958 1 2.77168 1 5.25696C1 12.4775 10 17.257 10 17.257C10 17.257 19 12.4775 19 5.25696Z"
                stroke={isFavorite ? color : strokeColor}
                fill={isFavorite ? color : "none"}
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
            />
        </svg>
    );
};

export default FavoriteIcon;


// import React from "react";

// type FavoriteIconProps = {
//     isFavorite: boolean;
//     size?: number;
// };

// const FavoriteIcon: React.FC<FavoriteIconProps> = ({ isFavorite, size = 20 }) => {
//     return (
//         <svg
//             width={size}
//             height={size * 0.9}
//             viewBox="0 0 20 18"
//             fill="none"
//             xmlns="http://www.w3.org/2000/svg"
//         >
//             <path
//                 d="M19 5.25696C19 2.77168 16.9013 0.756958 14.3125 0.756958C12.3769 0.756958 10.7153 1.88324 10 3.49038C9.28472 1.88324 7.62312 0.756958 5.6875 0.756958C3.09867 0.756958 1 2.77168 1 5.25696C1 12.4775 10 17.257 10 17.257C10 17.257 19 12.4775 19 5.25696Z"
//                 stroke={isFavorite ? "#2d2926" : "#777777"}
//                 fill={isFavorite ? "#2d2926" : "none"}
//                 strokeWidth="1.5"
//                 strokeLinecap="round"
//                 strokeLinejoin="round"
//             />
//         </svg>
//     );
// };

// export default FavoriteIcon;
