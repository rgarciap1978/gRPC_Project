using Warehouse.Client;

var clientWrapper = new WarehouseClientWrapper("https://localhost:7093");

// ADD PRODUCTS //
Console.WriteLine("\n Adding product 1...");
var product1ID = await clientWrapper.AddProductAsync("1", "Product 1", 1348);
if (!string.IsNullOrEmpty(product1ID))
    Console.WriteLine($"New product added with ID: {product1ID}");
else
    Console.WriteLine("An error occurred or the product was not added.");

Console.WriteLine("\n Adding product 2...");
var product2ID = await clientWrapper.AddProductAsync("2", "Product 2", 7245);
if (!string.IsNullOrEmpty(product2ID))
    Console.WriteLine($"New product added with ID: {product2ID}");
else
    Console.WriteLine("An error occurred or the product was not added.");

Console.WriteLine("\n Adding product 3...");
var product3ID = await clientWrapper.AddProductAsync("3", "Product 3", 1303);
if (!string.IsNullOrEmpty(product3ID))
    Console.WriteLine($"New product added with ID: {product3ID}");
else
    Console.WriteLine("An error occurred or the product was not added.");

Console.WriteLine("\n Adding product 4...");
var product4ID = await clientWrapper.AddProductAsync("4", "Product 4", 2301);
if (!string.IsNullOrEmpty(product4ID))
    Console.WriteLine($"New product added with ID: {product4ID}");
else
    Console.WriteLine("An error occurred or the product was not added.");

// GET PRODUCT BY ID //
Console.WriteLine("\n Fetching product with ID 2...");
var product1 = await clientWrapper.GetProductById("2");
if (product1 != null)
    Console.WriteLine($"Received product: {product1.Name}, Quantity: {product1.Quantity}");
else
    Console.WriteLine("Product not found or an error occurred.");

Console.WriteLine("\n Fetching product with ID 5...");
var product2 = await clientWrapper.GetProductById("5");
if (product2 != null)
    Console.WriteLine($"Received product: {product2.Name}, Quantity: {product2.Quantity}");
else
    Console.WriteLine("Product not found or an error occurred.");


// GET PRODUCT BY NAME //
Console.WriteLine("\n Fetching product with Name Product 3...");
var product3 = await clientWrapper.GetProductByName("Product 3");
if (product3 != null)
    Console.WriteLine($"Received product: {product3.Id}, Quantity: {product3.Quantity}");
else
    Console.WriteLine("Product not found or an error occurred.");

// UPDATE PRODUCT //
// GET PRODUCT BY ID //
Console.WriteLine("\n Updateing product with ID 4...");
var product4 = await clientWrapper.UpdateProductAsync("4", "Product Four", 1303);
if (product4 != null)
{
    product4 = await clientWrapper.GetProductById("4");
    if (product4 != null)
        Console.WriteLine($"Received product: {product4.Name}, Quantity: {product4.Quantity}");
    else
        Console.WriteLine("Product not found or an error occurred.");

}
else
    Console.WriteLine("Product not found or an error occurred.");


Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();