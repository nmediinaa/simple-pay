using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplePay_API.Migrations
{
    /// <inheritdoc />
    public partial class PopulaUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mB)
        {
            mB.Sql("INSERT INTO USERS(Name, SingUpDate) " +
                "VALUES('Nicolas Medina', NOW())");

            mB.Sql("INSERT INTO USERS(Name, SingUpDate) " +
                "VALUES('Julia Menossi', NOW())");

            mB.Sql("INSERT INTO USERS(Name, SingUpDate) " +
                "VALUES('Ivan Medina', NOW())");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mB)
        {
            mB.Sql("DELETE FROM USERS");
        }
    }
}
