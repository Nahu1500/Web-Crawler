using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Crawler.DAL;
using Web_Crawler.Models;

namespace Web_Crawler.Repositories
{
    public class ProductRepository
    {
        public Product GetProductByUrl(string url)
        {
            using (var db = new DataBaseContext())
            {
                return db.Products.FirstOrDefault(product => product.Url == url);
            }
        }

        public int GetProductCount()
        {
            using (var db = new DataBaseContext())
            {
                return db.Products.Count();
            }
        }

        internal void AddProduct(Product newProduct)
        {
            using (var db = new DataBaseContext())
            {
                db.Products.Add(newProduct);
                db.SaveChanges();
            }
        }

        internal void EditProduct(Product editedProduct)
        {
            using (var db = new DataBaseContext())
            {
                db.Entry(editedProduct).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        internal void DeleteProductByUrl(string url)
        {
            using (var db = new DataBaseContext())
            {
                var productToRemove = db.Products.First(product => product.Url == url);
                db.Products.Remove(productToRemove);
                db.SaveChanges();
            }
        }
    }
}