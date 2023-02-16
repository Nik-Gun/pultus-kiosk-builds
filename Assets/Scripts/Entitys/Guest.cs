using System;
using System.Collections.Generic;

public class Guest
{
    public List<ProductItem> selected_products = new List<ProductItem>();
    private int productItemLimit = 4;

    public int getProductsCount()
    {
        return selected_products.Count;
    }

    public List<ProductItem> getProducts()
    {
        return selected_products;
    }

    public bool addProduct(Film f, string lang)
    {
        if (getProductsCount() >= productItemLimit) return false;
        ProductItem productItem = new ProductItem(f, lang);
        selected_products.Add(productItem);
        return true;
    }

    public bool removeProduct(int index)
    {
        if (getProductsCount() <= index || index < 0)
            return false;

        selected_products.RemoveAt(index);
        return true;
    }
}