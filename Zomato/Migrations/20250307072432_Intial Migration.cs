using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Zomato.Migrations
{
    /// <inheritdoc />
    public partial class IntialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    contact = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.CheckConstraint("CHK_User_role", "[role] IN ('ADMIN', 'CONSUMER', 'DELIVERY_PARTNER', 'RESTAURENT_PARTNER')");
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userLocation = table.Column<Point>(type: "geography", nullable: false),
                    userid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.id);
                    table.ForeignKey(
                        name: "FK_Address_User_userid",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Consumer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<long>(type: "bigint", nullable: false),
                    rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumer", x => x.id);
                    table.ForeignKey(
                        name: "FK_Consumer_User_userid",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryPartner",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rating = table.Column<double>(type: "float", nullable: false),
                    userid = table.Column<long>(type: "bigint", nullable: false),
                    available = table.Column<bool>(type: "bit", nullable: false),
                    vehicleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    currentLocation = table.Column<Point>(type: "geography", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPartner", x => x.id);
                    table.ForeignKey(
                        name: "FK_DeliveryPartner_User_userid",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "RestaurantPartner",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    aadharNo = table.Column<long>(type: "bigint", nullable: false),
                    userid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantPartner", x => x.id);
                    table.ForeignKey(
                        name: "FK_RestaurantPartner_User_userid",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    userid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.id);
                    table.ForeignKey(
                        name: "FK_Wallet_User_userid",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    restaurantLocation = table.Column<Point>(type: "geography", nullable: false),
                    gstNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rating = table.Column<double>(type: "float", nullable: false),
                    isAvailable = table.Column<bool>(type: "bit", nullable: false),
                    isVarified = table.Column<bool>(type: "bit", nullable: false),
                    restaurantPartnerid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.id);
                    table.ForeignKey(
                        name: "FK_Restaurant_RestaurantPartner_restaurantPartnerid",
                        column: x => x.restaurantPartnerid,
                        principalTable: "RestaurantPartner",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    consumerid = table.Column<long>(type: "bigint", nullable: false),
                    restaurantid = table.Column<long>(type: "bigint", nullable: false),
                    totalPrice = table.Column<double>(type: "float", nullable: false),
                    validCart = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cart_Consumer_consumerid",
                        column: x => x.consumerid,
                        principalTable: "Consumer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Cart_Restaurant_restaurantid",
                        column: x => x.restaurantid,
                        principalTable: "Restaurant",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    menuName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    restaurantid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.id);
                    table.ForeignKey(
                        name: "FK_Menu_Restaurant_restaurantid",
                        column: x => x.restaurantid,
                        principalTable: "Restaurant",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    consumerid = table.Column<long>(type: "bigint", nullable: false),
                    foodAmount = table.Column<double>(type: "float", nullable: false),
                    platformFee = table.Column<double>(type: "float", nullable: false),
                    totalPrice = table.Column<double>(type: "float", nullable: false),
                    pickupLocation = table.Column<Point>(type: "geography", nullable: false),
                    dropoffLocation = table.Column<Point>(type: "geography", nullable: false),
                    orderStatus = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    paymentMethod = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    paymentStatus = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    OrderCreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    restaurantid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.CheckConstraint("CHK_Order_orderStatus", "[orderStatus] IN ('ACCEPTED', 'PREPARING', 'READY_FOR_PICKUP', 'DELIVERY_REQUEST_CREATED', 'DELIVERY_PARTNER_ASSIGNED', 'ACCEPTED_BY_DELIVERY_PARTNER', 'OUT_FOR_DELIVERY', 'DELIVERED', 'CANCELLED')");
                    table.CheckConstraint("CHK_Order_paymentMethod", "[paymentMethod] IN ('CASH', 'WALLET', 'UPI')");
                    table.CheckConstraint("CHK_Order_paymentStatus", "[paymentStatus] IN ('PENDING', 'CONFIRMED', 'REFUNDED')");
                    table.ForeignKey(
                        name: "FK_Order_Consumer_consumerid",
                        column: x => x.consumerid,
                        principalTable: "Consumer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Order_Restaurant_restaurantid",
                        column: x => x.restaurantid,
                        principalTable: "Restaurant",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "OrderRequest",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cartid = table.Column<long>(type: "bigint", nullable: false),
                    foodAmount = table.Column<double>(type: "float", nullable: false),
                    platformFee = table.Column<double>(type: "float", nullable: false),
                    totalPrice = table.Column<double>(type: "float", nullable: false),
                    deliveryFee = table.Column<double>(type: "float", nullable: false),
                    orderRequestStatus = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    restaurantid = table.Column<long>(type: "bigint", nullable: false),
                    consumerid = table.Column<long>(type: "bigint", nullable: false),
                    DropLocation = table.Column<Point>(type: "geography", nullable: false),
                    paymentMethod = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    paymentStatus = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRequest", x => x.id);
                    table.CheckConstraint("CHK_OrderRequests_orderRequestStatus", "[orderRequestStatus] IN ('ACCEPTED', 'CANCELLED', 'PENDING')");
                    table.CheckConstraint("CHK_OrderRequests_paymentMethod", "[paymentMethod] IN ('CASH', 'WALLET', 'UPI')");
                    table.CheckConstraint("CHK_OrderRequests_paymentStatus", "[paymentStatus] IN ('PENDING', 'CONFIRMED', 'REFUNDED')");
                    table.ForeignKey(
                        name: "FK_OrderRequest_Cart_cartid",
                        column: x => x.cartid,
                        principalTable: "Cart",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_OrderRequest_Consumer_consumerid",
                        column: x => x.consumerid,
                        principalTable: "Consumer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_OrderRequest_Restaurant_restaurantid",
                        column: x => x.restaurantid,
                        principalTable: "Restaurant",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "MenuItem",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dishDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    foodCategory = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    ingredients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rating = table.Column<double>(type: "float", nullable: false),
                    isAvailable = table.Column<bool>(type: "bit", nullable: false),
                    menuid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.id);
                    table.CheckConstraint("CHK_MenuItem_foodCategory", "[foodCategory] IN ('VEG', 'NONVEG', 'EGGCONTAINS')");
                    table.ForeignKey(
                        name: "FK_MenuItem_Menu_menuid",
                        column: x => x.menuid,
                        principalTable: "Menu",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryRequest",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickupLocation = table.Column<Point>(type: "geography", nullable: false),
                    DropLocation = table.Column<Point>(type: "geography", nullable: false),
                    deliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deliveryRequestStatus = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    orderid = table.Column<long>(type: "bigint", nullable: false),
                    deliveryPartnerid = table.Column<long>(type: "bigint", nullable: false),
                    restaurantOtp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    consumerOtp = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryRequest", x => x.id);
                    table.CheckConstraint("CHK_DeliveryRequest_deliveryRequestStatus", "[deliveryRequestStatus] IN ('ACCEPTED', 'COMPLETED', 'PENDING')");
                    table.ForeignKey(
                        name: "FK_DeliveryRequest_DeliveryPartner_deliveryPartnerid",
                        column: x => x.deliveryPartnerid,
                        principalTable: "DeliveryPartner",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_DeliveryRequest_Order_orderid",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paymentMethod = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    orderid = table.Column<long>(type: "bigint", nullable: false),
                    amount = table.Column<double>(type: "float", nullable: false),
                    paymentStatus = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    paymentTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.id);
                    table.CheckConstraint("CHK_Payment_paymentMethod", "[paymentMethod] IN ('CASH', 'WALLET', 'UPI')");
                    table.CheckConstraint("CHK_Payment_paymentStatus", "[paymentStatus] IN ('PENDING', 'CONFIRMED', 'REFUNDED')");
                    table.ForeignKey(
                        name: "FK_Payment_Order_orderid",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "WalletTransaction",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    transactionType = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    transactionMethod = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    orderid = table.Column<long>(type: "bigint", nullable: false),
                    walletid = table.Column<long>(type: "bigint", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransaction", x => x.id);
                    table.CheckConstraint("CHK_WalletTransaction_transactionMethod", "[transactionMethod] IN ('BANKING', 'ORDER')");
                    table.CheckConstraint("CHK_WalletTransaction_transactionType", "[transactionType] IN ('CREDIT', 'DEBIT')");
                    table.ForeignKey(
                        name: "FK_WalletTransaction_Order_orderid",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_WalletTransaction_Wallet_walletid",
                        column: x => x.walletid,
                        principalTable: "Wallet",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    menuItemid = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    totalPrice = table.Column<double>(type: "float", nullable: false),
                    cartid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.id);
                    table.ForeignKey(
                        name: "FK_CartItem_Cart_cartid",
                        column: x => x.cartid,
                        principalTable: "Cart",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_CartItem_MenuItem_menuItemid",
                        column: x => x.menuItemid,
                        principalTable: "MenuItem",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    menuItemid = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false),
                    totalPrice = table.Column<double>(type: "float", nullable: false),
                    orderid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderItem_MenuItem_menuItemid",
                        column: x => x.menuItemid,
                        principalTable: "MenuItem",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_orderid",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_userid",
                table: "Address",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_consumerid",
                table: "Cart",
                column: "consumerid");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_restaurantid",
                table: "Cart",
                column: "restaurantid");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_cartid",
                table: "CartItem",
                column: "cartid");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_menuItemid",
                table: "CartItem",
                column: "menuItemid");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_userid",
                table: "Consumer",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryPartner_userid",
                table: "DeliveryPartner",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryRequest_deliveryPartnerid",
                table: "DeliveryRequest",
                column: "deliveryPartnerid");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryRequest_orderid",
                table: "DeliveryRequest",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_restaurantid",
                table: "Menu",
                column: "restaurantid");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_menuid",
                table: "MenuItem",
                column: "menuid");

            migrationBuilder.CreateIndex(
                name: "IX_Order_consumerid",
                table: "Order",
                column: "consumerid");

            migrationBuilder.CreateIndex(
                name: "IX_Order_restaurantid",
                table: "Order",
                column: "restaurantid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_menuItemid",
                table: "OrderItem",
                column: "menuItemid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_orderid",
                table: "OrderItem",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRequest_cartid",
                table: "OrderRequest",
                column: "cartid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRequest_consumerid",
                table: "OrderRequest",
                column: "consumerid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRequest_restaurantid",
                table: "OrderRequest",
                column: "restaurantid");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_orderid",
                table: "Payment",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_restaurantPartnerid",
                table: "Restaurant",
                column: "restaurantPartnerid");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantPartner_userid",
                table: "RestaurantPartner",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_userid",
                table: "Wallet",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransaction_orderid",
                table: "WalletTransaction",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransaction_walletid",
                table: "WalletTransaction",
                column: "walletid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "DeliveryRequest");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "OrderRequest");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "WalletTransaction");

            migrationBuilder.DropTable(
                name: "DeliveryPartner");

            migrationBuilder.DropTable(
                name: "MenuItem");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Consumer");

            migrationBuilder.DropTable(
                name: "Restaurant");

            migrationBuilder.DropTable(
                name: "RestaurantPartner");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
