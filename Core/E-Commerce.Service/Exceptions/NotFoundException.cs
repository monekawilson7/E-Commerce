namespace E_Commerce.Service.Exceptions;
public class NotFoundException(string message) : Exception(message);

public sealed class ProductNotFoundException(int id)
    : NotFoundException($"Product with Id {id} Not Found");

public sealed class BasketNotFoundException(String id)
    : NotFoundException($"Basket with Id {id} Not Found");
