using ShoppingApp2.UnitOfWork;
using ShoppingApp2.Data.Models;
using ShoppingApp2.Data.DTO.Request;
using ShoppingApp2.Data.DTO.Response;

namespace ShoppingApp2.Service
{
    public class ShopManagement
    {
        private readonly IUnitOfWork _unitOfWork;
    //    private readonly ILogger _logger;

        public ShopManagement(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
   //         _logger = logger;
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            var items = await _unitOfWork.ItemRepo.All();
  //          _logger.LogInformation("Show list of items.");
            return items;
        }

        public async Task<PurchaseResponse> PurchaseItems(Cart cart)
        {
            bool key = await CreateNewReceipt(cart);
            if (key)
            {

                await _unitOfWork.CompleteAsync();
                return new PurchaseResponse() { Success = true, Message = "Your cart was ordered successfully.", Total = await TotalSum(cart) };
            }
            else
            {
                return new PurchaseResponse() { Success = false, Message = "Sorry something strange occurs when we create the receipt." };
            }
        }

        private async Task<bool> TakeOutItems(Cart cart)
        {
            bool key;
            foreach (PurchasingItem purchased_item in cart.Items)
            {
                key = await _unitOfWork.ItemRepo.TakeOutItem(purchased_item.ItemId, purchased_item.Number);
                if (!key)
                {
                    return false;
                }
            }
            return true;
        }

        private async Task<double> TotalSum(Cart cart)
        {
            double sum = 0;
            foreach (PurchasingItem item in cart.Items)
            {
                var item_in_store = await _unitOfWork.ItemRepo.GetById(item.ItemId);
                if (item_in_store != null) sum = sum + item_in_store.Price * item.Number;
            }
            return sum;
        }

        private async Task<bool> CreateNewReceipt(Cart cart)
        {
            Receipt receipt = new Receipt()
            {
                UserName = cart.UserName,
                TotalPurchased = await TotalSum(cart),
            };
            try
            {
                bool key1 = _unitOfWork.ReceiptRepo.NewReceipt(receipt);
                bool key2 = await TakeOutItems(cart);
                if (key1 && key2)
                {
           //         _logger.LogInformation("New receipt was created");
                    await _unitOfWork.CompleteAsync();
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
           //     _logger.LogError(ex, "Something wrong has happened", typeof(ShopManagement));
                return false;
            }
        }
    }
}
