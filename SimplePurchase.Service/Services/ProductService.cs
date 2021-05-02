﻿using SimplePurchase.Application.Interfaces;
using SimplePurchase.Service.Automapper;
using SimplePurchase.Service.Interfaces;
using SimplePurchase.Service.Models;
using System.Collections.Generic;

namespace SimplePurchase.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<ProductViewModel> GetProducts()
        {
            var products = _productRepository.GetAllProducts();
            var productsMapped = Mapping.Mapper.Map<IEnumerable<ProductViewModel>>(products);

            return productsMapped;
        }
    }
}
