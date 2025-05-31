using System;
using System.Collections.Generic;

namespace OnlineOrdering
{
    // Class to represent an Address
    public class Address
    {
        private string _street;
        private string _city;
        private string _stateOrProvince;
        private string _country;

        public Address(string street, string city, string stateOrProvince, string country)
        {
            _street = street;
            _city = city;
            _stateOrProvince = stateOrProvince;
            _country = country;
        }

        public bool IsInUSA()
        {
            return _country.ToUpper() == "USA" || _country.ToUpper() == "UNITED STATES" || _country.ToUpper() == "UNITED STATES OF AMERICA";
        }

        public string GetFullAddress()
        {
            return $"{_street}\n{_city}, {_stateOrProvince}\n{_country}";
        }
    }

    // Class to represent a Customer
    public class Customer
    {
        private string _name;
        private Address _address;

        public Customer(string name, Address address)
        {
            _name = name;
            _address = address;
        }

        public string GetName()
        {
            return _name;
        }

        public Address GetAddress()
        {
            return _address;
        }

        public bool IsInUSA()
        {
            return _address.IsInUSA();
        }
    }

    // Class to represent a Product
    public class Product
    {
        private string _name;
        private string _productId;
        private double _price;
        private int _quantity;

        public Product(string name, string productId, double price, int quantity)
        {
            _name = name;
            _productId = productId;
            _price = price;
            _quantity = quantity;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetProductId()
        {
            return _productId;
        }

        public double GetPrice()
        {
            return _price;
        }

        public int GetQuantity()
        {
            return _quantity;
        }

        public double GetTotalCost()
        {
            return _price * _quantity;
        }
    }

    // Class to represent an Order
    public class Order
    {
        private List<Product> _products;
        private Customer _customer;

        public Order(Customer customer)
        {
            _customer = customer;
            _products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public double CalculateTotalCost()
        {
            double productTotal = 0;
            foreach (var product in _products)
            {
                productTotal += product.GetTotalCost();
            }

            double shippingCost = _customer.IsInUSA() ? 5 : 35;
            return productTotal + shippingCost;
        }

        public string GetPackingLabel()
        {
            string label = "Packing Label:\n";
            foreach (var product in _products)
            {
                label += $"Product: {product.GetName()}, ID: {product.GetProductId()}\n";
            }
            return label;
        }

        public string GetShippingLabel()
        {
            return $"Shipping Label:\n{_customer.GetName()}\n{_customer.GetAddress().GetFullAddress()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create first order with a US customer
            Address address1 = new Address("123 Main St", "Springfield", "IL", "USA");
            Customer customer1 = new Customer("John Doe", address1);
            Order order1 = new Order(customer1);
            order1.AddProduct(new Product("Widget", "W123", 3.5, 5));
            order1.AddProduct(new Product("Gadget", "G456", 10.0, 2));
            order1.AddProduct(new Product("Thingamajig", "T789", 7.25, 1));

            Console.WriteLine(order1.GetPackingLabel());
            Console.WriteLine(order1.GetShippingLabel());
            Console.WriteLine($"Total Price: ${order1.CalculateTotalCost():0.00}\n");

            // Create second order with an international customer
            Address address2 = new Address("456 Elm St", "Toronto", "ON", "Canada");
            Customer customer2 = new Customer("Jane Smith", address2);
            Order order2 = new Order(customer2);
            order2.AddProduct(new Product("Doodad", "D012", 5.0, 3));
            order2.AddProduct(new Product("Whatsit", "W345", 12.5, 1));

            Console.WriteLine(order2.GetPackingLabel());
            Console.WriteLine(order2.GetShippingLabel());
            Console.WriteLine($"Total Price: ${order2.CalculateTotalCost():0.00}");
        }
    }
}
