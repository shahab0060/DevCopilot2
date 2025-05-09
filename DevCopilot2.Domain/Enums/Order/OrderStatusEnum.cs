using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Order;

namespace DevCopilot2.Domain.Enums.Order
{
    public enum OrderStatusEnum
    {
        [Display(ResourceType = typeof(OrderStatusEnumResources), Name = nameof(OrderStatusEnumResources.WaitingForPayment))]
        WaitingForPayment = 0,
        [Display(ResourceType = typeof(OrderStatusEnumResources), Name = nameof(OrderStatusEnumResources.PaymentFailed))]
        PaymentFailed = 1,
        [Display(ResourceType = typeof(OrderStatusEnumResources), Name = nameof(OrderStatusEnumResources.Preparing))]
        Preparing = 2,
        [Display(ResourceType = typeof(OrderStatusEnumResources), Name = nameof(OrderStatusEnumResources.GivenToPost))]
        GivenToPost = 3,
    }
}
