// -----------------------------------------------------------------------
// <copyright file="ModelStateDictionaryExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Web.Mvc;

namespace PocketMoney.Util.ExtensionMethods
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ModelStateExtensions
    {
        public static void ClearErrors(this ModelState modelStateDictionary)
        {
            if ((modelStateDictionary != null) && (modelStateDictionary.Errors != null))
                modelStateDictionary.Errors.Clear();
        }
    }
}