﻿using SimpleApplication.Domain.Models;
using SimplePurchase.Application.Interfaces;
using SimplePurchase.Service.Automapper;
using SimplePurchase.Service.Interfaces;
using SimplePurchase.Service.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplePurchase.Service.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ILineItemRepository _lineItemRepository;

        public PurchaseService(IProductRepository productRepository,
            IPurchaseRepository purchaseRepository,
            ILineItemRepository lineItemRepository)
        {
            _productRepository = productRepository;
            _purchaseRepository = purchaseRepository;
            _lineItemRepository = lineItemRepository;
        }

        public bool AddPurchase(IEnumerable<ProductModel> orderedProducts, string userId)
        {
            var lineitemsEntities = Mapping.Mapper.Map<IEnumerable<ProductEntity>>(orderedProducts);
            var fullProducts = _productRepository.GetPurchaseProducts(lineitemsEntities.Select(t => t.Id).Distinct().ToArray());

            var newPurchaseId = GeneratePurchaseId();
            var totalCalculated = GetTotal(fullProducts, orderedProducts);


            var newPurchase = new PurchaseModel()
            {
                Id = newPurchaseId,
                UserId = userId,
                CreationDate = DateTime.UtcNow,
                IsConfirmed = false,
                Total = totalCalculated,
                TotalCount = orderedProducts.Sum(t => t.Count)
            };

            var purchaseEntity = Mapping.Mapper.Map<PurchaseEntity>(newPurchase);

            var result = _purchaseRepository.AddPurchase(purchaseEntity);
            int lineItemResult = 0;
            if(result > 0)
            {
                var lineItems = GetLineItems(fullProducts, orderedProducts, newPurchaseId);
                lineItemResult = _lineItemRepository.AddLineItems(lineItems);
            }

            return result > 0 && lineItemResult > 0;
        }

        private IEnumerable<LineItemEntity> GetLineItems(IEnumerable<ProductEntity> fullProducts, IEnumerable<ProductModel> orderedProducts, string newPurchaseId)
        {
            var lineItems = new List<LineItemEntity>();

            foreach(var item in orderedProducts)
            {
                lineItems.Add(new LineItemEntity()
                {
                    ProductId = item.ProductId,
                    PurchaseId = newPurchaseId,
                    Count = item.Count,
                    Price = fullProducts.FirstOrDefault(t=>t.Id == item.ProductId).Price
                });
            }

            return lineItems;
        }

        private string GeneratePurchaseId()
        {
            Guid g = Guid.NewGuid();
            return g.ToString();
        }

        private decimal GetTotal(IEnumerable<ProductEntity> products, IEnumerable<ProductModel> lineitems)
        {
            decimal total = 0;

            foreach (var item in lineitems)
            {
                total += item.Count * products.FirstOrDefault(t => t.Id == item.ProductId).Price;
            }

            return total;
        }
    }
}