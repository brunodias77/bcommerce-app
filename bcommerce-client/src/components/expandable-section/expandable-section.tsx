import React, { useState } from 'react';
import ArrowDownIcon from '../../icons/arrow-down-icon';
import ArrowUpIcon from '../../icons/arrow-up-icon';

type ExpandableSectionProps = {
    title: string;
    icon: React.ReactNode;
    children: React.ReactNode;
    isLast?: boolean;
};

const ExpandableSection: React.FC<ExpandableSectionProps> = ({ title, icon, children, isLast = false }) => {
    const [isExpanded, setIsExpanded] = useState(false);

    const toggleExpand = () => setIsExpanded(prev => !prev);

    return (
        <div className={`w-full bg-[#F8F8FB] z-50 pt-4 ${isLast ? 'pb-4' : ''}`}>
            <div className="w-full bg-white p-4">
                <div className="container flex flex-col gap-2">
                    <div
                        className="flex items-center gap-x-2 justify-between cursor-pointer"
                        onClick={toggleExpand}
                    >
                        <div className="flex items-center gap-x-2">
                            {icon}
                            <h2 className="text-blue-primary font-bold text-xl uppercase">{title}</h2>
                        </div>
                        {isExpanded ? <ArrowUpIcon /> : <ArrowDownIcon />}
                    </div>

                    {isExpanded && (
                        <div className="transition-all duration-300 mt-2">
                            {children}
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
};

export default ExpandableSection;
