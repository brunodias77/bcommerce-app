import { Product, Blog } from "@/types/product";

// Importação de imagens dos produtos
import img1 from "../public/assets/products-images/product_1.png";
import img2_1 from "../public/assets/products-images/product_2_1.png";
import img2_2 from "../public/assets/products-images/product_2_2.png";
import img2_3 from "../public/assets/products-images/product_2_3.png";
import img2_4 from "../public/assets/products-images/product_2_4.png";
import img3 from "../public/assets/products-images/product_3.png";
import img4 from "../public/assets/products-images/product_4.png";
import img5 from "../public/assets/products-images/product_5.png";
import img6 from "../public/assets/products-images/product_6.png";
import img7 from "../public/assets/products-images/product_7.png";
import img8_1 from "../public/assets/products-images/product_8_1.png";
import img8_2 from "../public/assets/products-images/product_8_2.png";
import img8_3 from "../public/assets/products-images/product_8_3.png";
import img8_4 from "../public/assets/products-images/product_8_4.png";
import img9 from "../public/assets/products-images/product_9.png";
import img10 from "../public/assets/products-images/product_10.png";
import img11 from "../public/assets/products-images/product_11.png";
import img12 from "../public/assets/products-images/product_12.png";
import img13 from "../public/assets/products-images/product_13.png";
import img14 from "../public/assets/products-images/product_14.png";
import img15 from "../public/assets/products-images/product_15.png";
import img16 from "../public/assets/products-images/product_16.png";
import img17 from "../public/assets/products-images/product_17.png";
import img18 from "../public/assets/products-images/product_18.png";
import img19 from "../public/assets/products-images/product_19.png";
import img20 from "../public/assets/products-images/product_20.png";
import img21 from "../public/assets/products-images/product_21.png";
import img22 from "../public/assets/products-images/product_22.png";
import img23 from "../public/assets/products-images/product_23.png";
import img24 from "../public/assets/products-images/product_24.png";
import img25 from "../public/assets/products-images/product_25.png";
import img26 from "../public/assets/products-images/product_26.png";
import img27 from "../public/assets/products-images/product_27.png";
import img28 from "../public/assets/products-images/product_28.png";
import img29 from "../public/assets/products-images/product_29.png";
import img30 from "../public/assets/products-images/product_30.png";
import img31 from "../public/assets/products-images/product_31.png";
import img32 from "../public/assets/products-images/product_32.png";
import img33 from "../public/assets/products-images/product_33.png";
import img34 from "../public/assets/products-images/product_34.png";
import img35 from "../public/assets/products-images/product_35.png";
import img36 from "../public/assets/products-images/product_36.png";
import img37 from "../public/assets/products-images/product_37.png";
import img38 from "../public/assets/products-images/product_38.png";
import img39 from "../public/assets/products-images/product_39.png";
import img40 from "../public/assets/products-images/product_40.png";
import img41 from "../public/assets/products-images/product_41.png";
import img42 from "../public/assets/products-images/product_42.png";

// import blog1 from "/src/assets/blogs/blog-1.png";
// import blog2 from "/src/assets/blogs/blog-2.png";
// import blog3 from "/src/assets/blogs/blog-3.png";
// import blog4 from "/src/assets/blogs/blog-4.png";
// import blog5 from "/src/assets/blogs/blog-5.png";
// import blog6 from "/src/assets/blogs/blog-6.png";
// import blog7 from "/src/assets/blogs/blog-7.png";
// import blog8 from "/src/assets/blogs/blog-8.png";

// Lista de produtos
export const products: Product[] = [
  {
    _id: "1",
    name: "Bluetooth Headset Pro",
    description:
      "Experience superior sound quality with these noise-cancelling headphones, perfect for long listening sessions.",
    price: 15,
    image: [img1.src],
    category: "Headphones",
    colors: ["Black", "Red", "White"],
    date: 1716634345448,
    popular: false,
  },
  {
    _id: "2",
    name: "Noise Cancelling Headphones",
    description:
      "A premium wireless headset designed for crystal-clear calls and high-quality audio.",
    price: 22,
    image: [img2_1.src, img2_2.src, img2_3.src, img2_4.src],
    category: "Headphones",
    colors: ["Black", "Red", "White", "Blue"],
    date: 1716621345448,
    popular: false,
  },
  {
    _id: "3",
    name: "Over-Ear Wireless Headphones",
    description:
      "Comfortable over-ear headphones with advanced sound technology, ideal for music lovers.",
    price: 20,
    image: [img3.src],
    category: "Headphones",
    colors: ["Black", "White", "Blue"],
    date: 1716234545448,
    popular: true,
  },
  {
    _id: "4",
    name: "Wireless Noise Cancelling Headphones",
    description:
      "Lightweight and noise-cancelling, designed for immersive listening on the go.",
    price: 80,
    image: [img4.src],
    category: "Headphones",
    colors: ["Black", "Red", "Blue"],
    date: 1716621345448,
    popular: false,
  },
  {
    _id: "5",
    name: "Gaming Headphones with Mic",
    description:
      "High-quality gaming headphones with a built-in microphone for an immersive gaming experience.",
    price: 40,
    image: [img5.src],
    category: "Headphones",
    colors: ["Red", "White", "Blue"],
    date: 1716622345448,
    popular: false,
  },
  {
    _id: "6",
    name: "Sports Bluetooth Earphones",
    description:
      "Sweat-resistant Bluetooth earphones, perfect for active users who enjoy running and working out.",
    price: 60,
    image: [img6.src],
    category: "Headphones",
    colors: ["XS", "Black", "Red"],
    date: 1716623345448,
    popular: false,
  },
  {
    _id: "7",
    name: "Foldable Wireless Headphones",
    description:
      "Portable foldable headphones offering excellent sound quality and comfort for on-the-go listening.",
    price: 20,
    image: [img7.src],
    category: "Headphones",
    colors: ["Black", "Red", "White", "Blue"],
    date: 1716624345448,
    popular: false,
  },

  // Cameras (ID 8 to 14)
  {
    _id: "8",
    name: "Digital Camera Pro",
    description:
      "Capture stunning photos with this professional-grade digital camera, equipped with advanced features.",
    price: 40,
    image: [img8_1.src, img8_2.src, img8_3.src, img8_4.src],
    category: "Cameras",
    colors: ["Black", "Red"],
    date: 1716625345448,
    popular: false,
  },
  {
    _id: "9",
    name: "4K DSLR Camera",
    description:
      "A 4K resolution DSLR camera designed for professional photographers and videographers.",
    price: 20,
    image: [img9.src],
    category: "Cameras",
    colors: ["Black", "Red"],
    date: 1716626345448,
    popular: false,
  },
  {
    _id: "10",
    name: "Compact Digital Camera",
    description:
      "Compact and lightweight digital camera for everyday use, capturing high-quality images effortlessly.",
    price: 20,
    image: [img10.src],
    category: "Cameras",
    colors: ["Black", "Red"],
    date: 1716627345448,
    popular: false,
  },
  {
    _id: "11",
    name: "Outdoor Action Camera",
    description:
      "Designed for adventurers, this action camera is waterproof and built to capture every moment in high-definition.",
    price: 30,
    image: [img11.src],
    category: "Cameras",
    colors: ["Red", "Red"],
    date: 1716628345448,
    popular: false,
  },
  {
    _id: "12",
    name: "Professional Mirrorless Camera",
    description:
      "Mirrorless camera with advanced image stabilization and 4K video recording capability.",
    price: 10,
    image: [img12.src],
    category: "Cameras",
    colors: ["Black", "Red"],
    date: 1716629345448,
    popular: true,
  },
  {
    _id: "13",
    name: "Camera Lens Kit",
    description:
      "Enhance your photography with this professional camera lens kit, perfect for a variety of shoots.",
    price: 20,
    image: [img13.src],
    category: "Cameras",
    colors: ["Black", "Red"],
    date: 1716630345448,
    popular: false,
  },
  {
    _id: "14",
    name: "Camera Tripod Stand",
    description:
      "Stable tripod stand for perfect shots, whether you're shooting in the studio or outdoors.",
    price: 20,
    image: [img14.src],
    category: "Cameras",
    colors: ["Black", "Red"],
    date: 1716631345448,
    popular: false,
  },

  // Mobiles (ID 15 to 21)
  {
    _id: "15",
    name: "Camera Flash Light",
    description:
      "High-power camera flash light designed to provide excellent lighting in all situations.",
    price: 15,
    image: [img15.src],
    category: "Mobiles",
    colors: ["XS", "Black", "Red"],
    date: 1716632345448,
    popular: true,
  },
  {
    _id: "16",
    name: "5G Tecno Mobile",
    description:
      "Durable mobile designed for safety offering convenient storage space.",
    price: 20,
    image: [img16.src],
    category: "Mobiles",
    colors: ["Black", "Red", "White"],
    date: 1716633345448,
    popular: false,
  },
  {
    _id: "17",
    name: "Smartphone Camera Lens Kit",
    description:
      "Enhance your smartphone photography with this portable camera lens kit.",
    price: 30,
    image: [img17.src],
    category: "Mobiles",
    colors: ["Black", "Red", "White", "Blue"],
    date: 1716634345448,
    popular: false,
  },
  {
    _id: "18",
    name: "Mobile Phone 4G",
    description:
      "A high-performance mobile phone featuring a stunning display and powerful battery.",
    price: 10,
    image: [img18.src],
    category: "Mobiles",
    colors: ["Black", "Red", "White", "Blue"],
    date: 1716635345448,
    popular: false,
  },
  {
    _id: "19",
    name: "5G Smartphone",
    description:
      "Experience ultra-fast 5G speeds and high-definition displays with this latest smartphone.",
    price: 30,
    image: [img19.src],
    category: "Mobiles",
    colors: ["Black", "Red", "White"],
    date: 1716636345448,
    popular: false,
  },
  {
    _id: "20",
    name: "Mobile Phone Case",
    description:
      "Protect your mobile phone with this premium, shock-absorbent case.",
    price: 20,
    image: [img20.src],
    category: "Mobiles",
    colors: ["Black", "Red", "White"],
    date: 1716637345448,
    popular: false,
  },
  {
    _id: "21",
    name: "Mobile Charger",
    description:
      "High-speed charging cable and adapter for your mobile devices, built to last.",
    price: 30,
    image: [img21.src],
    category: "Mobiles",
    colors: ["Black", "Red", "White", "Blue"],
    date: 1716638345448,
    popular: false,
  },

  // Speakers (ID 22 to 28)
  {
    _id: "22",
    name: "Smartwatch Phone",
    description:
      "A smartwatch that connects seamlessly with your phone, offering notifications and more.",
    price: 400,
    image: [img22.src],
    category: "Speakers",
    colors: ["Red", "White", "Blue"],
    date: 1716639345448,
    popular: true,
  },
  {
    _id: "23",
    name: "Bluetooth Mobile Speaker",
    description:
      "Compact mobile speaker with rich sound, perfect for your phone and outdoor activities.",
    price: 190,
    image: [img23.src],
    category: "Speakers",
    colors: ["Black", "Red", "White"],
    date: 1716640345448,
    popular: false,
  },
  {
    _id: "24",
    name: "Portable Bluetooth Speaker",
    description:
      "Wireless speaker with deep bass, ideal for parties and outdoor events.",
    price: 250,
    image: [img24.src],
    category: "Speakers",
    colors: ["Black", "Red", "White"],
    date: 1716641345448,
    popular: false,
  },
  {
    _id: "25",
    name: "Smart Bluetooth Speaker",
    description:
      "Advanced smart speaker with voice recognition and superior sound quality.",
    price: 20,
    image: [img25.src],
    category: "Speakers",
    colors: ["Red", "White", "Blue"],
    date: 1716642345448,
    popular: false,
  },
  {
    _id: "26",
    name: "Portable Mini Bluetooth Speaker",
    description:
      "Compact Bluetooth speaker with powerful sound, perfect for on-the-go use.",
    price: 22,
    image: [img26.src],
    category: "Speakers",
    colors: ["Black", "Red", "White"],
    date: 1716643345448,
    popular: false,
  },
  {
    _id: "27",
    name: "Wireless Home Speaker",
    description:
      "Powerful wireless speaker with home-theater sound quality for any space.",
    price: 30,
    image: [img27.src],
    category: "Speakers",
    colors: ["Black", "White"],
    date: 1716644345448,
    popular: true,
  },
  {
    _id: "28",
    name: "Surround Sound Speaker",
    description:
      "High-quality surround sound speaker for cinematic experiences in your living room.",
    price: 530,
    image: [img28.src],
    category: "Speakers",
    colors: ["Black", "Red", "White"],
    date: 1716645345448,
    popular: false,
  },

  // Mouses (ID 29 to 35)
  {
    _id: "29",
    name: "Wireless Gaming Mouse",
    description:
      "Precision wireless mouse designed for gamers with ultra-low latency and ergonomic design.",
    price: 120,
    image: [img29.src],
    category: "Mouse",
    colors: ["Black", "Red", "White", "Blue"],
    date: 1716646345448,
    popular: true,
  },
  {
    _id: "30",
    name: "Ergonomic Wireless Mouse",
    description:
      "An ergonomic wireless mouse designed for comfort and long hours of usage.",
    price: 90,
    image: [img30.src],
    category: "Mouse",
    colors: ["Black", "Red", "Blue"],
    date: 1716647345448,
    popular: false,
  },
  {
    _id: "31",
    name: "RGB Gaming Mouse",
    description:
      "Customizable RGB gaming mouse with advanced sensor technology and programmable buttons.",
    price: 40,
    image: [img31.src],
    category: "Mouse",
    colors: ["Black", "Red", "White", "Blue"],
    date: 1716648345448,
    popular: true,
  },
  {
    _id: "32",
    name: "Wireless Mouse with USB Receiver",
    description:
      "Reliable wireless mouse with a USB receiver, ideal for everyday tasks.",
    price: 40,
    image: [img32.src],
    category: "Mouse",
    colors: ["Black", "Red", "White"],
    date: 1716649345448,
    popular: false,
  },
  {
    _id: "33",
    name: "Bluetooth Multi-Device Mouse",
    description:
      "Bluetooth mouse that can easily switch between multiple devices, perfect for multitaskers.",
    price: 80,
    image: [img33.src],
    category: "Mouse",
    colors: ["Black", "Red", "White", "Blue"],
    date: 1716650345448,
    popular: false,
  },
  {
    _id: "34",
    name: "Compact Wireless Mouse",
    description:
      "Compact and portable wireless mouse, ideal for travel and working on the go.",
    price: 30,
    image: [img34.src],
    category: "Mouse",
    colors: ["Black", "Red", "Blue"],
    date: 1716651345448,
    popular: false,
  },
  {
    _id: "35",
    name: "Gaming Mouse with Customizable Weights",
    description:
      "Gaming mouse with customizable weights for personalized performance and comfort.",
    price: 15,
    image: [img35.src],
    category: "Mouse",
    colors: ["Black", "Red", "White", "Blue"],
    date: 1716652345448,
    popular: true,
  },

  // Watches (ID 36 to 42)
  {
    _id: "36",
    name: "Smart Fitness Watch",
    description:
      "Track your workouts and monitor your health with this all-in-one smart fitness watch.",
    price: 20,
    image: [img3.src],
    category: "Watches",
    colors: ["Black", "Red", "White", "Blue"],
    date: 1716653345448,
    popular: true,
  },
  {
    _id: "37",
    name: "Luxury Smartwatch",
    description:
      "Stylish and elegant smartwatch that blends fashion with functionality, featuring health tracking and notifications.",
    price: 450,
    image: [img37.src],
    category: "Watches",
    colors: ["Gold", "Silver", "Black"],
    date: 1716654345448,
    popular: false,
  },
  {
    _id: "38",
    name: "Sports Smartwatch",
    description:
      "Perfect for athletes, this smartwatch tracks workouts, heart rate, and more with a rugged design.",
    price: 270,
    image: [img38.src],
    category: "Watches",
    colors: ["Red", "Blue", "Black"],
    date: 1716655345448,
    popular: false,
  },
  {
    _id: "39",
    name: "Android Smartwatch",
    description:
      "An Android-compatible smartwatch offering seamless integration with your mobile apps and notifications.",
    price: 20,
    image: [img39.src],
    category: "Watches",
    colors: ["Black", "Red", "White", "Blue"],
    date: 1716656345448,
    popular: false,
  },
  {
    _id: "40",
    name: "Round Dial Smartwatch",
    description:
      "Elegant round dial smartwatch featuring a sleek design and full touch-screen capabilities.",
    price: 350,
    image: [img40.src],
    category: "Watches",
    colors: ["Gold", "Silver", "Black", "White"],
    date: 1716657345448,
    popular: false,
  },
  {
    _id: "41",
    name: "Smartwatch with Heart Rate Monitor",
    description:
      "Monitor your heart rate, sleep, and fitness progress with this advanced smartwatch.",
    price: 22,
    image: [img41.src],
    category: "Watches",
    colors: ["Black", "Red", "White", "Blue"],
    date: 1716658345448,
    popular: false,
  },
  {
    _id: "42",
    name: "Smartwatch for Kids",
    description:
      "Kid-friendly smartwatch with fun features and parental control options.",
    price: 120,
    image: [img42.src],
    category: "Watches",
    colors: ["Pink", "Blue", "Red"],
    date: 1716659345448,
    popular: false,
  },
];

// Lista de blogs
// export const blogs: Blog[] = [
//   {
//     title: "Top Shopping Tips for Smart Buyers",
//     category: "Cameras",
//     image: blog1.src,
//   },
//   {
//     title: "Latest Trends in Online Shopping 2024",
//     category: "Mobiles",
//     image: blog2.src,
//   },
//   {
//     title: "How to Spot the Best Online Deals",
//     category: "Mobiles",
//     image: blog3.src,
//   },
//   {
//     title: "Why E-Commerce is the Future",
//     category: "Headphones",
//     image: blog4.src,
//   },
//   {
//     title: "Smart Buying Tips for Online Shoppers",
//     category: "Cameras",
//     image: blog5.src,
//   },
//   {
//     title: "Upcoming Trends in Shopping 2024",
//     category: "Mobiles",
//     image: blog6.src,
//   },
//   {
//     title: "Best Strategies to Find Online Discounts",
//     category: "Mobiles",
//     image: blog7.src,
//   },
//   {
//     title: "How E-Commerce is Changing",
//     category: "Headphones",
//     image: blog8.src,
//   },
// ];
