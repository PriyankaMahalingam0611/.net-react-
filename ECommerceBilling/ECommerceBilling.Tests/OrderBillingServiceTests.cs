using NUnit.Framework;
using System;
using ECommerceBilling.Services;

namespace ECommerceBilling.Tests{
    [TestFixture]
    public class OrderBillingServiceTests{
        private OrderBillingService _billingService;

        [SetUp]
        public void Setup() {
            _billingService = new OrderBillingService();
        }

        [TestCase(0, 5)]
        [TestCase(-10, 5)]
        public void CalculateSubTotal_PriceIsZeroOrLess_ThrowsArgumentException(decimal price, int quantity){
            var ex = Assert.Throws<ArgumentException>(() => _billingService.CalculateSubTotal(price, quantity));
            Assert.That(ex.Message, Is.EqualTo("Product price must be greater than 0."));
        }

        [TestCase(100, 0)]
        [TestCase(100, -2)]
        public void CalculateSubTotal_QuantityIsZeroOrLess_ThrowsArgumentException(decimal price, int quantity){
            var ex = Assert.Throws<ArgumentException>(() => _billingService.CalculateSubTotal(price, quantity));
            Assert.That(ex.Message, Is.EqualTo("Quantity must be greater than 0."));
        }

        [Test]
        public void CalculateSubTotal_ValidInputs_ReturnsCorrectAmount(){
            decimal result = _billingService.CalculateSubTotal(100m, 5);
            Assert.That(result, Is.EqualTo(500m));
        }

        [TestCase(5000, 500)]
        [TestCase(6000, 600)]
        [TestCase(2000, 100)]
        [TestCase(4999, 249.95)]
        [TestCase(1999, 0)]
        [TestCase(500, 0)]
        public void CalculateDiscount_CalculatesCorrectlyBasedOnSubTotal(decimal subTotal, decimal expectedDiscount){
            decimal result = _billingService.CalculateDiscount(subTotal);
            Assert.That(result, Is.EqualTo(expectedDiscount));
        }

        [TestCase(999, 100)]
        [TestCase(500, 100)]
        [TestCase(1000, 0)]
        [TestCase(1500, 0)]
        public void CalculateDeliveryCharge_CalculatesCorrectlyBasedOnAmountAfterDiscount(decimal amountAfterDiscount, decimal expectedCharge){
            decimal result = _billingService.CalculateDeliveryCharge(amountAfterDiscount);
            Assert.That(result, Is.EqualTo(expectedCharge));
        }

        [TestCase(1000, 5, 4500)]
        [TestCase(1000, 2, 1900)]
        [TestCase(500, 2, 1000)]
        [TestCase(400, 2, 900)]
        public void CalculateFinalAmount_ReturnsCorrectTotalBill(decimal price, int quantity, decimal expectedFinalAmount){
            decimal result = _billingService.CalculateFinalAmount(price, quantity);
            Assert.That(result, Is.EqualTo(expectedFinalAmount));
        }
    }
}