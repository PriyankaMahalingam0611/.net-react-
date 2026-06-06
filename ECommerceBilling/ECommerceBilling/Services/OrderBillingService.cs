using System;

namespace ECommerceBilling.Services{
    public class OrderBillingService{
        public decimal CalculateSubTotal(decimal productPrice, int quantity){
            if (productPrice <= 0)
                throw new ArgumentException("Product price must be greater than 0.");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0.");

            return productPrice * quantity;
        }

        public decimal CalculateDiscount(decimal subTotal){
            if (subTotal >= 5000m)
                return subTotal * 0.10m;

            if (subTotal >= 2000m)
                return subTotal * 0.05m;

            return 0m;
        }

        public decimal CalculateDeliveryCharge(decimal amountAfterDiscount){

            if (amountAfterDiscount < 1000m)
                return 100m;

            return 0m;
        }

        public decimal CalculateFinalAmount(decimal productPrice, int quantity){
            decimal subTotal = CalculateSubTotal(productPrice, quantity);
            decimal discount = CalculateDiscount(subTotal);

            decimal amountAfterDiscount = subTotal - discount;
            decimal deliveryCharge = CalculateDeliveryCharge(amountAfterDiscount);

            return amountAfterDiscount + deliveryCharge;
        }
    }
}