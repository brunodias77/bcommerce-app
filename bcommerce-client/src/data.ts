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

export const products: Product[] = [
  {
    id: "1",
    name: "Bluetooth Headset Pro",
    description: "Superior sound with noise cancellation.",
    price: { amount: 15 },
    oldPrice: { amount: 20 },
    images: [{ url: img1.src }],
    category: { name: "Headphones" },
    colors: [{ value: "Black" }, { value: "Red" }, { value: "White" }],
    stock: { quantity: 100 },
    sold: 12,
    isActive: true,
    popular: false,
    createdAt: new Date(1716634345448).toISOString(),
    updatedAt: new Date(1716634345448).toISOString(),
    isNew: true,
    sale: true,
  },
  {
    id: "2",
    name: "Noise Cancelling Headphones",
    description: "Premium wireless headset with crystal-clear audio.",
    price: { amount: 22 },
    images: [
      { url: img2_1.src },
      { url: img2_2.src },
      { url: img2_3.src },
      { url: img2_4.src },
    ],
    category: { name: "Headphones" },
    colors: [
      { value: "Black" },
      { value: "Red" },
      { value: "White" },
      { value: "Blue" },
    ],
    stock: { quantity: 80 },
    sold: 20,
    isActive: true,
    popular: false,
    createdAt: new Date(1716621345448).toISOString(),
    updatedAt: new Date(1716621345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "3",
    name: "Over-Ear Wireless Headphones",
    description: "Comfortable and advanced sound for music lovers.",
    price: { amount: 20 },
    oldPrice: { amount: 30 },
    images: [{ url: img3.src }],
    category: { name: "Headphones" },
    colors: [{ value: "Black" }, { value: "White" }, { value: "Blue" }],
    stock: { quantity: 50 },
    sold: 18,
    isActive: true,
    popular: true,
    createdAt: new Date(1716234545448).toISOString(),
    updatedAt: new Date(1716234545448).toISOString(),
    isNew: false,
    sale: true,
  },
  {
    id: "4",
    name: "Wireless Noise Cancelling Headphones",
    description: "Lightweight headphones for immersive listening.",
    price: { amount: 80 },
    images: [{ url: img4.src }],
    category: { name: "Headphones" },
    colors: [{ value: "Black" }, { value: "Red" }, { value: "Blue" }],
    stock: { quantity: 40 },
    sold: 10,
    isActive: true,
    popular: false,
    createdAt: new Date(1716621345448).toISOString(),
    updatedAt: new Date(1716621345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "5",
    name: "Gaming Headphones with Mic",
    description: "Immersive gaming headphones with built-in mic.",
    price: { amount: 40 },
    images: [{ url: img5.src }],
    category: { name: "Headphones" },
    colors: [{ value: "Red" }, { value: "White" }, { value: "Blue" }],
    stock: { quantity: 30 },
    sold: 5,
    isActive: true,
    popular: false,
    createdAt: new Date(1716622345448).toISOString(),
    updatedAt: new Date(1716622345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "6",
    name: "Sports Bluetooth Earphones",
    description: "Sweat-resistant earphones for active users.",
    price: { amount: 60 },
    oldPrice: { amount: 75 },
    images: [{ url: img6.src }],
    category: { name: "Headphones" },
    colors: [{ value: "XS" }, { value: "Black" }, { value: "Red" }],
    stock: { quantity: 60 },
    sold: 9,
    isActive: true,
    popular: false,
    createdAt: new Date(1716623345448).toISOString(),
    updatedAt: new Date(1716623345448).toISOString(),
    isNew: true,
    sale: true,
  },
  {
    id: "7",
    name: "Foldable Wireless Headphones",
    description: "Portable and comfortable headphones.",
    price: { amount: 20 },
    images: [{ url: img7.src }],
    category: { name: "Headphones" },
    colors: [
      { value: "Black" },
      { value: "Red" },
      { value: "White" },
      { value: "Blue" },
    ],
    stock: { quantity: 70 },
    sold: 12,
    isActive: true,
    popular: false,
    createdAt: new Date(1716624345448).toISOString(),
    updatedAt: new Date(1716624345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "8",
    name: "Digital Camera Pro",
    description: "Professional-grade digital camera.",
    price: { amount: 40 },
    oldPrice: { amount: 49 },
    images: [
      { url: img8_1.src },
      { url: img8_2.src },
      { url: img8_3.src },
      { url: img8_4.src },
    ],
    category: { name: "Cameras" },
    colors: [{ value: "Black" }, { value: "Red" }],
    stock: { quantity: 25 },
    sold: 7,
    isActive: true,
    popular: false,
    createdAt: new Date(1716625345448).toISOString(),
    updatedAt: new Date(1716625345448).toISOString(),
    isNew: true,
    sale: true,
  },
  {
    id: "9",
    name: "4K DSLR Camera",
    description: "High-res DSLR camera for pros.",
    price: { amount: 20 },
    images: [{ url: img9.src }],
    category: { name: "Cameras" },
    colors: [{ value: "Black" }, { value: "Red" }],
    stock: { quantity: 90 },
    sold: 22,
    isActive: true,
    popular: false,
    createdAt: new Date(1716626345448).toISOString(),
    updatedAt: new Date(1716626345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "10",
    name: "Compact Digital Camera",
    description: "Lightweight and easy to use.",
    price: { amount: 20 },
    images: [{ url: img10.src }],
    category: { name: "Cameras" },
    colors: [{ value: "Black" }, { value: "Red" }],
    stock: { quantity: 35 },
    sold: 14,
    isActive: true,
    popular: false,
    createdAt: new Date(1716627345448).toISOString(),
    updatedAt: new Date(1716627345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "11",
    name: "Outdoor Action Camera",
    description: "Waterproof action camera built for adventurers.",
    price: { amount: 30 },
    images: [{ url: img11.src }],
    category: { name: "Cameras" },
    colors: [{ value: "Red" }, { value: "Red" }],
    stock: { quantity: 60 },
    sold: 11,
    isActive: true,
    popular: false,
    createdAt: new Date(1716628345448).toISOString(),
    updatedAt: new Date(1716628345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "12",
    name: "Professional Mirrorless Camera",
    description: "4K mirrorless camera with stabilization.",
    price: { amount: 10 },
    oldPrice: { amount: 19 },
    images: [{ url: img12.src }],
    category: { name: "Cameras" },
    colors: [{ value: "Black" }, { value: "Red" }],
    stock: { quantity: 50 },
    sold: 33,
    isActive: true,
    popular: true,
    createdAt: new Date(1716629345448).toISOString(),
    updatedAt: new Date(1716629345448).toISOString(),
    isNew: true,
    sale: true,
  },
  {
    id: "13",
    name: "Camera Lens Kit",
    description: "Versatile lens kit for professionals.",
    price: { amount: 20 },
    images: [{ url: img13.src }],
    category: { name: "Cameras" },
    colors: [{ value: "Black" }, { value: "Red" }],
    stock: { quantity: 75 },
    sold: 12,
    isActive: true,
    popular: false,
    createdAt: new Date(1716630345448).toISOString(),
    updatedAt: new Date(1716630345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "14",
    name: "Camera Tripod Stand",
    description: "Stable tripod stand for indoor/outdoor shots.",
    price: { amount: 20 },
    images: [{ url: img14.src }],
    category: { name: "Cameras" },
    colors: [{ value: "Black" }, { value: "Red" }],
    stock: { quantity: 90 },
    sold: 9,
    isActive: true,
    popular: false,
    createdAt: new Date(1716631345448).toISOString(),
    updatedAt: new Date(1716631345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "15",
    name: "Camera Flash Light",
    description: "Bright and powerful flash light for photography.",
    price: { amount: 15 },
    images: [{ url: img15.src }],
    category: { name: "Mobiles" },
    colors: [{ value: "XS" }, { value: "Black" }, { value: "Red" }],
    stock: { quantity: 40 },
    sold: 17,
    isActive: true,
    popular: true,
    createdAt: new Date(1716632345448).toISOString(),
    updatedAt: new Date(1716632345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "16",
    name: "5G Tecno Mobile",
    description: "Durable 5G phone with excellent storage.",
    price: { amount: 20 },
    oldPrice: { amount: 28 },
    images: [{ url: img16.src }],
    category: { name: "Mobiles" },
    colors: [{ value: "Black" }, { value: "Red" }, { value: "White" }],
    stock: { quantity: 120 },
    sold: 31,
    isActive: true,
    popular: false,
    createdAt: new Date(1716633345448).toISOString(),
    updatedAt: new Date(1716633345448).toISOString(),
    isNew: true,
    sale: true,
  },
  {
    id: "17",
    name: "Smartphone Camera Lens Kit",
    description: "Enhance mobile photography with lens kit.",
    price: { amount: 30 },
    images: [{ url: img17.src }],
    category: { name: "Mobiles" },
    colors: [
      { value: "Black" },
      { value: "Red" },
      { value: "White" },
      { value: "Blue" },
    ],
    stock: { quantity: 60 },
    sold: 23,
    isActive: true,
    popular: false,
    createdAt: new Date(1716634345448).toISOString(),
    updatedAt: new Date(1716634345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "18",
    name: "Mobile Phone 4G",
    description: "Fast and efficient mobile phone.",
    price: { amount: 10 },
    images: [{ url: img18.src }],
    category: { name: "Mobiles" },
    colors: [
      { value: "Black" },
      { value: "Red" },
      { value: "White" },
      { value: "Blue" },
    ],
    stock: { quantity: 100 },
    sold: 12,
    isActive: true,
    popular: false,
    createdAt: new Date(1716635345448).toISOString(),
    updatedAt: new Date(1716635345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "19",
    name: "5G Smartphone",
    description: "Ultra-fast smartphone with vibrant display.",
    price: { amount: 30 },
    images: [{ url: img19.src }],
    category: { name: "Mobiles" },
    colors: [{ value: "Black" }, { value: "Red" }, { value: "White" }],
    stock: { quantity: 90 },
    sold: 19,
    isActive: true,
    popular: false,
    createdAt: new Date(1716636345448).toISOString(),
    updatedAt: new Date(1716636345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "20",
    name: "Mobile Phone Case",
    description: "Shock-resistant premium phone case.",
    price: { amount: 20 },
    oldPrice: { amount: 25 },
    images: [{ url: img20.src }],
    category: { name: "Mobiles" },
    colors: [{ value: "Black" }, { value: "Red" }, { value: "White" }],
    stock: { quantity: 70 },
    sold: 27,
    isActive: true,
    popular: false,
    createdAt: new Date(1716637345448).toISOString(),
    updatedAt: new Date(1716637345448).toISOString(),
    isNew: true,
    sale: true,
  },
  {
    id: "21",
    name: "Mobile Charger",
    description: "High-speed mobile charging cable and adapter.",
    price: { amount: 30 },
    images: [{ url: img21.src }],
    category: { name: "Mobiles" },
    colors: [
      { value: "Black" },
      { value: "Red" },
      { value: "White" },
      { value: "Blue" },
    ],
    stock: { quantity: 120 },
    sold: 33,
    isActive: true,
    popular: false,
    createdAt: new Date(1716638345448).toISOString(),
    updatedAt: new Date(1716638345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "22",
    name: "Smartwatch Phone",
    description: "Smartwatch with phone connectivity & notifications.",
    price: { amount: 400 },
    oldPrice: { amount: 460 },
    images: [{ url: img22.src }],
    category: { name: "Speakers" },
    colors: [{ value: "Red" }, { value: "White" }, { value: "Blue" }],
    stock: { quantity: 35 },
    sold: 8,
    isActive: true,
    popular: true,
    createdAt: new Date(1716639345448).toISOString(),
    updatedAt: new Date(1716639345448).toISOString(),
    isNew: true,
    sale: true,
  },
  {
    id: "23",
    name: "Bluetooth Mobile Speaker",
    description: "Compact speaker with rich Bluetooth sound.",
    price: { amount: 190 },
    images: [{ url: img23.src }],
    category: { name: "Speakers" },
    colors: [{ value: "Black" }, { value: "Red" }, { value: "White" }],
    stock: { quantity: 70 },
    sold: 22,
    isActive: true,
    popular: false,
    createdAt: new Date(1716640345448).toISOString(),
    updatedAt: new Date(1716640345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "24",
    name: "Portable Bluetooth Speaker",
    description: "Deep bass speaker for parties and events.",
    price: { amount: 250 },
    oldPrice: { amount: 280 },
    images: [{ url: img24.src }],
    category: { name: "Speakers" },
    colors: [{ value: "Black" }, { value: "Red" }, { value: "White" }],
    stock: { quantity: 55 },
    sold: 29,
    isActive: true,
    popular: false,
    createdAt: new Date(1716641345448).toISOString(),
    updatedAt: new Date(1716641345448).toISOString(),
    isNew: true,
    sale: true,
  },
  {
    id: "25",
    name: "Smart Bluetooth Speaker",
    description: "Voice recognition smart speaker.",
    price: { amount: 20 },
    images: [{ url: img25.src }],
    category: { name: "Speakers" },
    colors: [{ value: "Red" }, { value: "White" }, { value: "Blue" }],
    stock: { quantity: 80 },
    sold: 16,
    isActive: true,
    popular: false,
    createdAt: new Date(1716642345448).toISOString(),
    updatedAt: new Date(1716642345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "26",
    name: "Portable Mini Bluetooth Speaker",
    description: "Mini speaker with powerful on-the-go sound.",
    price: { amount: 22 },
    images: [{ url: img26.src }],
    category: { name: "Speakers" },
    colors: [{ value: "Black" }, { value: "Red" }, { value: "White" }],
    stock: { quantity: 90 },
    sold: 13,
    isActive: true,
    popular: false,
    createdAt: new Date(1716643345448).toISOString(),
    updatedAt: new Date(1716643345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "27",
    name: "Wireless Home Speaker",
    description: "Home theater-quality wireless speaker.",
    price: { amount: 30 },
    images: [{ url: img27.src }],
    category: { name: "Speakers" },
    colors: [{ value: "Black" }, { value: "White" }],
    stock: { quantity: 40 },
    sold: 18,
    isActive: true,
    popular: true,
    createdAt: new Date(1716644345448).toISOString(),
    updatedAt: new Date(1716644345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "28",
    name: "Surround Sound Speaker",
    description: "Cinematic surround sound experience.",
    price: { amount: 530 },
    oldPrice: { amount: 600 },
    images: [{ url: img28.src }],
    category: { name: "Speakers" },
    colors: [{ value: "Black" }, { value: "Red" }, { value: "White" }],
    stock: { quantity: 25 },
    sold: 14,
    isActive: true,
    popular: false,
    createdAt: new Date(1716645345448).toISOString(),
    updatedAt: new Date(1716645345448).toISOString(),
    isNew: true,
    sale: true,
  },
  {
    id: "29",
    name: "Wireless Gaming Mouse",
    description: "Ergonomic mouse with low-latency.",
    price: { amount: 120 },
    images: [{ url: img29.src }],
    category: { name: "Mouse" },
    colors: [
      { value: "Black" },
      { value: "Red" },
      { value: "White" },
      { value: "Blue" },
    ],
    stock: { quantity: 100 },
    sold: 40,
    isActive: true,
    popular: true,
    createdAt: new Date(1716646345448).toISOString(),
    updatedAt: new Date(1716646345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "30",
    name: "Ergonomic Wireless Mouse",
    description: "Comfortable design for extended use.",
    price: { amount: 90 },
    oldPrice: { amount: 110 },
    images: [{ url: img30.src }],
    category: { name: "Mouse" },
    colors: [{ value: "Black" }, { value: "Red" }, { value: "Blue" }],
    stock: { quantity: 85 },
    sold: 31,
    isActive: true,
    popular: false,
    createdAt: new Date(1716647345448).toISOString(),
    updatedAt: new Date(1716647345448).toISOString(),
    isNew: true,
    sale: true,
  },
  {
    id: "31",
    name: "RGB Gaming Mouse",
    description: "Customizable mouse with RGB and sensors.",
    price: { amount: 40 },
    oldPrice: { amount: 50 },
    images: [{ url: img31.src }],
    category: { name: "Mouse" },
    colors: [
      { value: "Black" },
      { value: "Red" },
      { value: "White" },
      { value: "Blue" },
    ],
    stock: { quantity: 110 },
    sold: 25,
    isActive: true,
    popular: true,
    createdAt: new Date(1716648345448).toISOString(),
    updatedAt: new Date(1716648345448).toISOString(),
    isNew: true,
    sale: true,
  },
  {
    id: "32",
    name: "Wireless Mouse with USB Receiver",
    description: "Reliable wireless mouse for everyday use.",
    price: { amount: 40 },
    images: [{ url: img32.src }],
    category: { name: "Mouse" },
    colors: [{ value: "Black" }, { value: "Red" }, { value: "White" }],
    stock: { quantity: 60 },
    sold: 13,
    isActive: true,
    popular: false,
    createdAt: new Date(1716649345448).toISOString(),
    updatedAt: new Date(1716649345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "33",
    name: "Bluetooth Multi-Device Mouse",
    description: "Switch between devices easily via Bluetooth.",
    price: { amount: 80 },
    images: [{ url: img33.src }],
    category: { name: "Mouse" },
    colors: [
      { value: "Black" },
      { value: "Red" },
      { value: "White" },
      { value: "Blue" },
    ],
    stock: { quantity: 90 },
    sold: 17,
    isActive: true,
    popular: false,
    createdAt: new Date(1716650345448).toISOString(),
    updatedAt: new Date(1716650345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "34",
    name: "Compact Wireless Mouse",
    description: "Portable and travel-friendly mouse.",
    price: { amount: 30 },
    images: [{ url: img34.src }],
    category: { name: "Mouse" },
    colors: [{ value: "Black" }, { value: "Red" }, { value: "Blue" }],
    stock: { quantity: 100 },
    sold: 20,
    isActive: true,
    popular: false,
    createdAt: new Date(1716651345448).toISOString(),
    updatedAt: new Date(1716651345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "35",
    name: "Gaming Mouse with Customizable Weights",
    description: "Performance mouse with adjustable balance.",
    price: { amount: 15 },
    images: [{ url: img35.src }],
    category: { name: "Mouse" },
    colors: [
      { value: "Black" },
      { value: "Red" },
      { value: "White" },
      { value: "Blue" },
    ],
    stock: { quantity: 75 },
    sold: 19,
    isActive: true,
    popular: true,
    createdAt: new Date(1716652345448).toISOString(),
    updatedAt: new Date(1716652345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "36",
    name: "Smart Fitness Watch",
    description: "All-in-one watch for fitness tracking.",
    price: { amount: 20 },
    oldPrice: { amount: 28 },
    images: [{ url: img36.src }],
    category: { name: "Watches" },
    colors: [
      { value: "Black" },
      { value: "Red" },
      { value: "White" },
      { value: "Blue" },
    ],
    stock: { quantity: 60 },
    sold: 27,
    isActive: true,
    popular: true,
    createdAt: new Date(1716653345448).toISOString(),
    updatedAt: new Date(1716653345448).toISOString(),
    isNew: true,
    sale: true,
  },
  {
    id: "37",
    name: "Luxury Smartwatch",
    description: "Elegant smartwatch with premium features.",
    price: { amount: 450 },
    images: [{ url: img37.src }],
    category: { name: "Watches" },
    colors: [{ value: "Gold" }, { value: "Silver" }, { value: "Black" }],
    stock: { quantity: 30 },
    sold: 10,
    isActive: true,
    popular: false,
    createdAt: new Date(1716654345448).toISOString(),
    updatedAt: new Date(1716654345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "38",
    name: "Sports Smartwatch",
    description: "Rugged smartwatch with health tracking.",
    price: { amount: 270 },
    images: [{ url: img38.src }],
    category: { name: "Watches" },
    colors: [{ value: "Red" }, { value: "Blue" }, { value: "Black" }],
    stock: { quantity: 42 },
    sold: 21,
    isActive: true,
    popular: false,
    createdAt: new Date(1716655345448).toISOString(),
    updatedAt: new Date(1716655345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "39",
    name: "Android Smartwatch",
    description: "Smartwatch with seamless Android sync.",
    price: { amount: 20 },
    images: [{ url: img39.src }],
    category: { name: "Watches" },
    colors: [
      { value: "Black" },
      { value: "Red" },
      { value: "White" },
      { value: "Blue" },
    ],
    stock: { quantity: 88 },
    sold: 14,
    isActive: true,
    popular: false,
    createdAt: new Date(1716656345448).toISOString(),
    updatedAt: new Date(1716656345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "40",
    name: "Round Dial Smartwatch",
    description: "Stylish round dial, full touchscreen.",
    price: { amount: 350 },
    images: [{ url: img40.src }],
    category: { name: "Watches" },
    colors: [
      { value: "Gold" },
      { value: "Silver" },
      { value: "Black" },
      { value: "White" },
    ],
    stock: { quantity: 33 },
    sold: 19,
    isActive: true,
    popular: false,
    createdAt: new Date(1716657345448).toISOString(),
    updatedAt: new Date(1716657345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "41",
    name: "Smartwatch with Heart Rate Monitor",
    description: "Monitor heart rate, sleep, and fitness.",
    price: { amount: 22 },
    images: [{ url: img41.src }],
    category: { name: "Watches" },
    colors: [
      { value: "Black" },
      { value: "Red" },
      { value: "White" },
      { value: "Blue" },
    ],
    stock: { quantity: 70 },
    sold: 16,
    isActive: true,
    popular: false,
    createdAt: new Date(1716658345448).toISOString(),
    updatedAt: new Date(1716658345448).toISOString(),
    isNew: true,
    sale: false,
  },
  {
    id: "42",
    name: "Smartwatch for Kids",
    description: "Kid-friendly smartwatch with fun features.",
    price: { amount: 120 },
    oldPrice: { amount: 140 },
    images: [{ url: img42.src }],
    category: { name: "Watches" },
    colors: [{ value: "Pink" }, { value: "Blue" }, { value: "Red" }],
    stock: { quantity: 58 },
    sold: 22,
    isActive: true,
    popular: false,
    createdAt: new Date(1716659345448).toISOString(),
    updatedAt: new Date(1716659345448).toISOString(),
    isNew: true,
    sale: true,
  },
];
