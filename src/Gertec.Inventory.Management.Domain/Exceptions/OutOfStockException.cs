using Gertec.Inventory.Management.Domain.Abstractions;

namespace Gertec.Inventory.Management.Domain.Exceptions;

public class OutOfStockException :  DefaultException
{

   public OutOfStockException()
   {
      Message = "Item out of stock.";
   }
}