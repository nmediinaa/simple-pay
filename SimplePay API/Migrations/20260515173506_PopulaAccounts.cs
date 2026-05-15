using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplePay_API.Migrations
{
    /// <inheritdoc />
    public partial class PopulaAccounts : Migration
    {
        protected override void Up(MigrationBuilder mB)
        {
            mB.Sql("INSERT INTO ACCOUNTS(UserId, AccountBalance) VALUES(1, 20000.00)");
            mB.Sql("INSERT INTO ACCOUNTS(UserId, AccountBalance) VALUES(2, 40000.00)");
            mB.Sql("INSERT INTO ACCOUNTS(UserId, AccountBalance) VALUES(3, 150000.00)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mB)
        {
            mB.Sql("DELETE FROM ACCOUNTS");
        }
    }
}
