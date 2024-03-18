namespace Business.Utilities.Consts;

public static class ResultMessages
{
    public struct Success
    {
        #region ProductMessages
        public const string ProductAdd = "Product successfully added.";
        public const string ProductDelete = "Product successfully deleted.";
        public const string ProductUpdate = "Product successfully updated.";
        public const string ProductInfoReceive = "Product information successfully received.";
        public const string ProductsInfoReceive = "Products successfully received.";
        #endregion
    }

    public struct Error
    {
        #region ProductMessages
        public const string ProductAddServer = "Unable to add product: Server error!";
        public const string ProductDeleteServer = "Unable to delete product: Server error!";
        public const string ProductNotFound = "Product not found!";
        public const string ProductGetServer = "Unable to retrieve product information: Server error!";
        public const string ProductsNotFound = "No products found!";
        public const string ProductGetAllServer = "Unable to retrieve products: Server error!";
        public const string ProductUpdateServer = "Unable to update product: Server error!";
        #endregion
    }
}
