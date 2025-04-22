// components/StarRating.tsx
"use client";

import React, { useState } from "react";

interface StarRatingProps {
    totalStars?: number;
    onRate?: (rating: number) => void;
    className?: string;
    starClassName?: string;
    size?: string;
}

const StarRating: React.FC<StarRatingProps> = ({
    totalStars = 5,
    onRate,
    className = "",
    starClassName = "",
    size = "text-2xl"
}) => {
    const [rating, setRating] = useState(0);
    const [hover, setHover] = useState(0);

    const handleRate = (value: number) => {
        setRating(value);
        onRate?.(value);
    };

    return (
        <div className={`flex space-x-1 cursor-pointer ${className}`} role="radiogroup" aria-label="Star Rating">
            {Array.from({ length: totalStars }, (_, i) => {
                const starValue = i + 1;
                const isActive = starValue <= (hover || rating);

                return (
                    <span
                        key={i}
                        role="radio"
                        aria-checked={isActive}
                        tabIndex={0}
                        className={`transition-colors duration-300 select-none ${size} ${isActive ? "text-yellow-400" : "text-gray-300"
                            } ${starClassName}`}
                        onClick={() => handleRate(starValue)}
                        onMouseEnter={() => setHover(starValue)}
                        onMouseLeave={() => setHover(0)}
                        onKeyDown={(e) => (e.key === "Enter" || e.key === " ") && handleRate(starValue)}
                    >
                        ★
                    </span>
                );
            })}
        </div>
    );
};

export default StarRating;
