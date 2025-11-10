using GameOnlineStore.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GameOnlineStore.Helpers
{
    public class EnumHelper
    {
        public static string GetDisplayName(OrderStatusViewModel status)
        {
            var field = status.GetType().GetField(status.ToString());
            var attribute = field?.GetCustomAttribute<DisplayAttribute>();
            return attribute?.Name ?? status.ToString();
        }
    }
}