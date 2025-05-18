import React from 'react';

type DividerVariant = 'button-icon';

interface DividerProps {
    variant?: DividerVariant;
    icon?: React.FC<{ width?: number; height?: number; color?: string }>;
    label?: string;
    onClick?: () => void;
    iconSize?: number;
    iconColor?: string;
    dashedLine?: boolean;
    gradientEdge?: boolean;
}

const Divider: React.FC<DividerProps> = ({
    variant = 'button-icon',
    icon: Icon,
    label = '',
    onClick,
    iconSize = 16,
    iconColor = '#FFF',
    dashedLine = true,
    gradientEdge = true,
}) => {
    const lineStyles = gradientEdge
        ? {
            maskImage:
                'linear-gradient(to right, transparent, white 30%, white 70%, transparent)',
            WebkitMaskImage:
                'linear-gradient(to right, transparent, white 30%, white 70%, transparent)',
            maskRepeat: 'no-repeat',
            maskSize: '100% 100%',
        }
        : {};

    return (
        <div className="">
            <div className="relative  h-[1px]">
                {/* Linha central */}
                {/* <div className="absolute top-0 left-[5%] right-[5%] h-[1px]"> */}
                <div className=" top-0 left-[5%] right-[5%] h-[1px]">
                    <div
                        className={`w-full h-full border-t ${dashedLine ? 'border-dashed' : 'border-solid'} border-gray-300`}
                        style={lineStyles}
                    />
                </div>
                {/* Ícone central com botão */}
                {variant === 'button-icon' && Icon && (
                    <div className="absolute left-1/2 top-[-11px] transform -translate-x-1/2 z-10 flex flex-col items-center gap-1">
                        <button
                            onClick={onClick}
                            className="w-6 h-6 rounded-full bg-[#111827] flex items-center justify-center shadow-lg cursor-pointer focus:outline-none transition transform active:scale-95"
                        >
                            <Icon width={iconSize} height={iconSize} color={iconColor} />
                        </button>
                        {label && (
                            <span className="text-xs text-center text-gray-700">{label}</span>
                        )}
                    </div>
                )}
            </div>
        </div>
    );
};

export default Divider;

// import React from 'react';

// type DividerVariant = 'button-icon';

// interface DividerProps {
//     variant?: DividerVariant;
//     icon?: React.FC<{ width?: number; height?: number; color?: string }>;
//     label?: string;
//     onClick?: () => void;
//     iconSize?: number;
//     iconColor?: string;
//     dashedLine?: boolean;
// }

// const Divider: React.FC<DividerProps> = ({
//     variant = 'button-icon',
//     icon: Icon,
//     label = '',
//     onClick,
//     iconSize = 16,
//     iconColor = '#FFF',
//     dashedLine = true,
// }) => {
//     return (
//         <div className="">
//             <div className="relative  h-[1px]">
//                 {/* Linha central */}
//                 <div className="absolute top-0 left-[5%] right-[5%] h-[1px]">
//                     <div
//                         className={`w-full h-full border-t ${dashedLine ? 'border-dashed' : 'border-solid'} border-gray-300`}
//                         style={{
//                             maskImage:
//                                 'linear-gradient(to right, transparent, white 30%, white 70%, transparent)',
//                             WebkitMaskImage:
//                                 'linear-gradient(to right, transparent, white 30%, white 70%, transparent)',
//                             maskRepeat: 'no-repeat',
//                             maskSize: '100% 100%',
//                         }}
//                     />
//                 </div>

//                 {/* Ícone central com botão */}
//                 {variant === 'button-icon' && Icon && (
//                     <div className="absolute left-1/2 top-[-11px] transform -translate-x-1/2 z-10 flex flex-col items-center gap-1">
//                         <button
//                             onClick={onClick}
//                             className="w-6 h-6 rounded-full bg-[#111827] flex items-center justify-center shadow-lg cursor-pointer focus:outline-none transition transform active:scale-95"
//                         >
//                             <Icon width={iconSize} height={iconSize} color={iconColor} />
//                         </button>
//                         {label && (
//                             <span className="text-xs text-center text-gray-700">{label}</span>
//                         )}
//                     </div>
//                 )}
//             </div>
//         </div>
//     );
// };

// export default Divider;


// import React from 'react';

// type DividerVariant = 'button-icon';

// interface DividerProps {
//     variant?: DividerVariant;
//     icon?: React.FC<{ width?: number; height?: number; color?: string }>;
//     label?: string;
//     onClick?: () => void;
//     iconSize?: number;
//     iconColor?: string;
// }

// const Divider: React.FC<DividerProps> = ({
//     variant = 'button-icon',
//     icon: Icon,
//     label = '',
//     onClick,
//     iconSize = 16,
//     iconColor = '#FFF',
// }) => {
//     return (
//         <div className="pb-[90px]">
//             <div className="relative mt-[90px] h-[1px]">
//                 {/* Linha com degradê nas pontas */}
//                 <div className="absolute top-0 left-[5%] right-[5%] h-[1px]">
//                     <div
//                         className="w-full h-full border-t border-dashed border-gray-300"
//                         style={{
//                             maskImage:
//                                 'linear-gradient(to right, transparent, white 30%, white 70%, transparent)',
//                             WebkitMaskImage:
//                                 'linear-gradient(to right, transparent, white 30%, white 70%, transparent)',
//                             maskRepeat: 'no-repeat',
//                             maskSize: '100% 100%',
//                         }}
//                     />
//                 </div>

//                 {/* Ícone central com botão */}
//                 {variant === 'button-icon' && Icon && (
//                     <div className="absolute left-1/2 top-[-11px] transform -translate-x-1/2 z-10 flex flex-col items-center gap-1">
//                         <button
//                             onClick={onClick}
//                             className="w-6 h-6 rounded-full bg-[#111827] flex items-center justify-center shadow-lg cursor-pointer focus:outline-none transition transform active:scale-95"
//                         >
//                             <Icon width={iconSize} height={iconSize} color={iconColor} />
//                         </button>
//                         {label && (
//                             <span className="text-xs text-center text-gray-700">{label}</span>
//                         )}
//                     </div>
//                 )}
//             </div>
//         </div>
//     );
// };

// export default Divider;
