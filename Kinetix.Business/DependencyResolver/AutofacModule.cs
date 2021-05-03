using Autofac;
using Kinetix.Business.Abstract;
using Kinetix.Business.Concrete;
using Kinetix.DataAccess.Abstract;
using Kinetix.DataAccess.Concrete;
using Kinetix.Dto;

namespace Kinetix.Business.DependencyResolver
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrderManager>().As<IOrderManager>();
            builder.RegisterType<OrderDal>().As<IOrderDal>();

          
            builder.RegisterType<ArticleDal>().As<IArticleDal>();

            builder.RegisterType<StockDal>().As<IStockDal>();

            builder.RegisterType<NotificationManager>().As<INotificationManager>();
            builder.RegisterType<OrderManagementClient>().As<IOrderManagementClient>();
            

            base.Load(builder);
        }
    }
}
