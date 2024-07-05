namespace Gertec.Inventory.Management.Infrastructure.Database.Abstractions;

public interface IDapperDbContext<TSqlConnection> where TSqlConnection : class
{
    public TSqlConnection GetConnection();
}