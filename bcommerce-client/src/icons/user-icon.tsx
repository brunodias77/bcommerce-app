
import React from "react";

type UserIconProps = {
  width?: number;
  height?: number;
  color?: string;
};

const UserIcon: React.FC<UserIconProps> = ({
  width = 24,
  height = 24,
  color = "#111827",
}) => {
  return (
    <svg width={width} height={height} viewBox="0 0 20 19" fill="none" xmlns="http://www.w3.org/2000/svg">
      <path d="M10 13C13.3137 13 16 10.3137 16 7C16 3.68629 13.3137 1 10 1C6.68629 1 4 3.68629 4 7C4 10.3137 6.68629 13 10 13Z" stroke={color} strokeWidth="1.5" strokeMiterlimit="10" />
      <path d="M0.90625 18.2501C1.82775 16.6537 3.15328 15.328 4.74958 14.4062C6.34588 13.4845 8.1567 12.9993 10 12.9993C11.8433 12.9993 13.6541 13.4845 15.2504 14.4062C16.8467 15.328 18.1722 16.6537 19.0938 18.2501" stroke={color} strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
    </svg>

  );
};

export default UserIcon;





// import React from "react";

// type UserIconProps = {
//   width?: number;
//   height?: number;
//   color?: string;
// };

// const UserIcon: React.FC<UserIconProps> = ({
//   width = 24,
//   height = 24,
//   color = "#111827",
// }) => {
//   return (
//     <svg
//       width={width}
//       height={height}
//       viewBox="0 0 24 24"
//       fill="none"
//       xmlns="http://www.w3.org/2000/svg"
//     >
//       <path
//         d="M16 7C16 9.20914 14.2091 11 12 11C9.79086 11 8 9.20914 8 7C8 4.79086 9.79086 3 12 3C14.2091 3 16 4.79086 16 7Z"
//         stroke={color}
//         strokeWidth="2"
//         strokeLinecap="round"
//         strokeLinejoin="round"
//       />
//       <path
//         d="M12 14C8.13401 14 5 17.134 5 21H19C19 17.134 15.866 14 12 14Z"
//         stroke={color}
//         strokeWidth="2"
//         strokeLinecap="round"
//         strokeLinejoin="round"
//       />
//     </svg>
//   );
// };

// export default UserIcon;