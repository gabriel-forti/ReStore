import { Product } from "../../app/models/product"
import ProductList from "./ProductList";
import { useState, useEffect } from "react";

export default function Catalog() {
    const [products, setProducts] = useState<Product[]>([]);

    // this happens everytime the component renders itself
    useEffect(() => {
        fetch('http://localhost:5000/api/products')
            .then(response => response.json())
            .then(data => setProducts(data)) // causes a re-render
    }, []); // empty array dependency so it runs just once!!    

    return (  
        <>        
            <ProductList products={products} />               
        </>      
    )
}