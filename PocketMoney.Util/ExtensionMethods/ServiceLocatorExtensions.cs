using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace PocketMoney.Util.ExtensionMethods
{
    public static class ServiceLocatorExtensions
    {
        public static void BuildUp(this IServiceLocator locator, object target)
        {
            target.GetType()
                .GetProperties()
                .Where(property => property.CanRead && property.CanWrite && !property.GetIndexParameters().Any())
                .Where(property => property.GetValue(target, null) == null)
                .Each(property =>
                          {
                              try
                              {
                                  property.SetValue(target, locator.GetInstance(property.PropertyType), null);
                              }
                              catch
                              {
                                  // Eat the exception
                              }
                          });
        }
    }
}