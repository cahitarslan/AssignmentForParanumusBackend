namespace Business.Utilities.Consts;

public static class ValidationMessages
{
    public struct Product
    {
        public const string NameNotEmpty = "Product name cannot be empty";
        public const string NameMinimumLength = "Product name must be at least two characters long";
        public const string NameMaximumLength = "Product name cannot exceed one hundred characters";
        public const string DescriptionMaximumLength = "Product description cannot exceed one thousand characters";
        public const string PriceNotEmpty = "Product price cannot be empty";
        public const string PriceGreaterThan = "Product price cannot be zero or less than zero";
    }
}
