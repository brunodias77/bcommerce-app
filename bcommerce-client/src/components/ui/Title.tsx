import React from 'react'

interface TitleProps {
    title: string;
    subtitle: string;
    content?: string;
    styles?: string;
    titleStyles?: string;
    contentStyles?: string;
}

const Title: React.FC<TitleProps> = ({ title, subtitle, content, styles, titleStyles, contentStyles }) => {
    return (
        <div className={`${styles} pb-1`}>
            <h2 className={`${titleStyles} text-black-primary text-[25px] leading-tight md:text-[35px] md:leading-[1.3] mb-4 font-bold`}>
                {title}
                <span className='text-black-primary !font-light underline'>{subtitle}</span>
            </h2>
            <p className={`${contentStyles}  text-gray-secondary`}>{content}</p>
        </div>
    )
}

export default Title;
