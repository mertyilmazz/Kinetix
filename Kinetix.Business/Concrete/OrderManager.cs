using AutoMapper;
using Kinetix.Business.Abstract;
using Kinetix.Business.Constant;
using Kinetix.Business.ExceptionResult;
using Kinetix.DataAccess.Abstract;
using Kinetix.Dto;
using Kinetix.Dto.Request;
using Kinetix.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Kinetix.Business.Concrete
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderDal _orderDal;
        private readonly IArticleDal _articleDal;
        private readonly INotificationManager _notificationManager;
        private readonly IMapper _mapper;
        private readonly IOrderManagementClient _orderManagementClient;


        public OrderManager(IOrderDal orderDal, IArticleDal articleDal, INotificationManager notificationManager, IMapper mapper, IOrderManagementClient orderManagementClient)
        {
            _orderDal = orderDal;
            _articleDal = articleDal;
            _notificationManager = notificationManager;
            _mapper = mapper;
            _orderManagementClient = orderManagementClient;
        }

        public async Task AddAsync(OrderRequest model)
        {
            if (model.OrderFile.Length <= 0)
                throw new NotFoundException("There is no file");

            var orderModel = await ReadFile(model.OrderFile);

            await PriceControl(orderModel.Articles);

            await StockControl(orderModel.Articles);

            var orderEntity = _mapper.Map<Order>(orderModel);

            await _orderDal.AddOrderAsync(orderEntity);

            await _orderManagementClient.AddOrder(orderModel);
        }


        private async Task<OrderModelDto> ReadFile(IFormFile formFile)
        {
            var model = new OrderModelDto();
            string fileContents;
            using (var stream = formFile.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                fileContents = await reader.ReadToEndAsync();
                string[] lines = fileContents.Split(Environment.NewLine);

                var firstLine = lines[0];
                model.FileType = firstLine.Substring(0, OrderLenghtConstants.FileTypeLenght);

                int orderId;
                if (!Int32.TryParse(firstLine.Substring(3, OrderLenghtConstants.OrderIdLength), out orderId))
                    throw new FormatException("OrderId format is not correct");
                model.Id = orderId;

                long buyerCode;
                if (!Int64.TryParse(firstLine.Substring(36, OrderLenghtConstants.BuyerCodeLength), out buyerCode))
                    throw new FormatException("BuyerCode format is not correct");
                model.BuyerCode = buyerCode;

                long supplierCode;
                if (!Int64.TryParse(firstLine.Substring(49, OrderLenghtConstants.SupplierCodeLength), out supplierCode))
                    throw new FormatException("SupplierCode format is not correct");
                model.SupplierCode = supplierCode;

                model.OrderDate = firstLine.Substring(23, OrderLenghtConstants.OrderDateLength);
                model.FreeTextComment = firstLine.Substring(62, OrderLenghtConstants.TextCommentLength).TrimStart().TrimEnd();


                for (int i = 1; i < lines.Length; i++)
                {
                    var article = new ArticleModelDto();

                    long eanCode;
                    if (!Int64.TryParse(lines[i].Substring(0, OrderLenghtConstants.ArticleCodeLenght), out eanCode))
                        throw new FormatException("EanCode format is not correct for this line={i}");
                    article.EanCode = eanCode;

                    short quantity;
                    if (!Int16.TryParse(lines[i].Substring(78, OrderLenghtConstants.ArticleQuantity), out quantity))
                        throw new FormatException("Quantity format is not correct for this line={i}");
                    article.Quantity = quantity;

                    decimal unitPrice;
                    if (!decimal.TryParse(lines[i].Substring(88, OrderLenghtConstants.ArticleUnitPrice), out unitPrice))
                        throw new FormatException($"UnitPrice format is not correct for this line={i}");
                    article.UnitPrice = unitPrice;

                    article.Description = lines[i].Substring(13, OrderLenghtConstants.ArticleDescriptionLenght);
                    model.Articles.Add(article);
                }
            }
            return model;
        }

        private async Task StockControl(List<ArticleModelDto> articles)
        {
            foreach (var item in articles)
            {
                var stocks = await _articleDal.GetAsync(f => f.EanCode == item.EanCode);
                if (stocks == null)
                {
                    throw new NotFoundException("There is no data");
                }

                if (stocks.Quantity < item.Quantity)
                {
                    _notificationManager.SendNotificationManager($"there is no stock for this product={item.Description}", "Stock");
                    throw new NoStockException($"there is no stock for this product={item.Description}");
                }
            }
        }

        private async Task PriceControl(List<ArticleModelDto> articles)
        {
            foreach (var item in articles)
            {
                var data = await _articleDal.GetAsync(f => f.EanCode == item.EanCode);
                if (data == null)
                {
                    throw new NotFoundException($"Article not found EanCode={item.EanCode}");
                }

                if (data.UnitPrice != item.UnitPrice)
                {
                    item.UnitPrice = data.UnitPrice;
                    _notificationManager.SendNotificationManager($"Price did not match product={item.Description}", "Price-Article");
                }
            }
        }
    }
}
