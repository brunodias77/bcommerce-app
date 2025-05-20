'use client';
import React, { useState, useRef, useEffect } from 'react';

interface Option {
    label: string;
    value: string;
}

interface SelectProps {
    id: string;
    label?: string;
    placeholder?: string;
    options: Option[];
    value: string;
    onChange: (value: string) => void;
}

const Select: React.FC<SelectProps> = ({ id, label, placeholder = "Selecione...", options, value, onChange }) => {
    const [isOpen, setIsOpen] = useState(false);
    const ref = useRef<HTMLDivElement>(null);

    const handleOptionClick = (val: string) => {
        onChange(val);
        setIsOpen(false);
    };

    const handleClickOutside = (event: MouseEvent) => {
        if (ref.current && !ref.current.contains(event.target as Node)) {
            setIsOpen(false);
        }
    };

    useEffect(() => {
        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    }, []);

    return (
        <div>
            {label && (
                <label htmlFor={id} className="block text-sm font-medium text-gray-700 mb-2">
                    {label}
                </label>
            )}

            <div className="relative w-full" ref={ref}>
                <div
                    className="bg-white p-2 flex border border-gray-300 rounded-lg cursor-pointer items-center justify-between"
                    onClick={() => setIsOpen(!isOpen)}
                >
                    <span className="text-sm text-gray-800">{options.find(o => o.value === value)?.label || placeholder}</span>
                    <svg
                        className={`w-4 h-4 ml-2 transition-transform duration-150 ${isOpen ? 'rotate-180' : ''}`}
                        fill="none"
                        stroke="currentColor"
                        strokeWidth={2}
                        viewBox="0 0 24 24"
                    >
                        <polyline points="6 9 12 15 18 9" />
                    </svg>
                </div>

                {isOpen && (
                    <div className="absolute z-40 w-full mt-1 bg-white border border-gray-300 rounded max-h-[300px] overflow-y-auto shadow-lg">
                        {options.map((opt, idx) => (
                            <div
                                key={opt.value}
                                className={`cursor-pointer px-4 py-2 hover:bg-yellow-primary ${opt.value === value ? 'bg-yellow-secondary text-white' : ''
                                    } ${idx === 0 ? 'rounded-t' : ''} ${idx === options.length - 1 ? 'rounded-b' : ''}`}
                                onClick={() => handleOptionClick(opt.value)}
                            >
                                {opt.label}
                            </div>
                        ))}
                    </div>
                )}
            </div>
        </div>
    );
};

export default Select;
