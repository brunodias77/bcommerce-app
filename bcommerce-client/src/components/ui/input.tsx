import React from 'react';

interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
    id: string;
    label?: string;
    icon?: React.ReactNode; // ✅ Novo: prop opcional para ícone
}

const Input: React.FC<InputProps> = ({ id, label, icon, ...props }) => {
    return (
        <div>
            {label && (
                <label htmlFor={id} className="block text-sm font-medium text-gray-700 mb-2">
                    {label}
                </label>
            )}

            <div className="relative">
                {icon && (
                    <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                        {icon}
                    </div>
                )}
                <input
                    id={id}
                    {...props}
                    className={`w-full p-2.5 text-sm text-gray-900 border border-gray-300 rounded-lg bg-white focus:ring-blue-500 focus:border-blue-500 ${icon ? 'pl-10' : ''
                        }`}
                />
            </div>
        </div>
    );
};

export default Input;


// import React from 'react';

// interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
//     id: string;
//     label?: string;
// }

// const Input: React.FC<InputProps> = ({ id, label, ...props }) => {
//     return (
//         <div>
//             {label && (<label htmlFor={id} className="block text-sm font-medium text-gray-700 mb-2">
//                 {label}
//             </label>)}

//             <input
//                 id={id}
//                 {...props} // 👈 Repassa tudo (name, type, placeholder, required, etc)
//                 className="w-full p-2.5 text-sm text-gray-900 border border-gray-300 rounded-lg bg-white focus:ring-blue-500 focus:border-blue-500"
//             />
//         </div>
//     );
// };

// export default Input;


