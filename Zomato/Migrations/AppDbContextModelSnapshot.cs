﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Zomato.Data;

#nullable disable

namespace Zomato.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Zomato.Model.Address", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<long?>("Userid")
                        .HasColumnType("bigint");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("postalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("state")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Userid");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Zomato.Model.Cart", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<long>("consumerid")
                        .HasColumnType("bigint");

                    b.Property<long>("restaurantid")
                        .HasColumnType("bigint");

                    b.Property<double>("totalPrice")
                        .HasColumnType("float");

                    b.Property<bool>("validCart")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.HasIndex("consumerid");

                    b.HasIndex("restaurantid");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("Zomato.Model.CartItem", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<long>("cartid")
                        .HasColumnType("bigint");

                    b.Property<long>("menuItemid")
                        .HasColumnType("bigint");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<double>("totalPrice")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("cartid");

                    b.HasIndex("menuItemid");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("Zomato.Model.Consumer", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<double>("rating")
                        .HasColumnType("float");

                    b.Property<long>("userid")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("userid");

                    b.ToTable("Consumer");
                });

            modelBuilder.Entity("Zomato.Model.DeliveryPartner", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<bool>("available")
                        .HasColumnType("bit");

                    b.Property<Point>("currentLocation")
                        .IsRequired()
                        .HasColumnType("geography");

                    b.Property<double>("rating")
                        .HasColumnType("float");

                    b.Property<long>("userid")
                        .HasColumnType("bigint");

                    b.Property<string>("vehicleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("userid");

                    b.ToTable("DeliveryPartner");
                });

            modelBuilder.Entity("Zomato.Model.DeliveryRequest", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<Point>("DropLocation")
                        .IsRequired()
                        .HasColumnType("geography");

                    b.Property<Point>("PickupLocation")
                        .IsRequired()
                        .HasColumnType("geography");

                    b.Property<string>("consumerOtp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("deliveryPartnerid")
                        .HasColumnType("bigint");

                    b.Property<string>("deliveryRequestStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("deliveryTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("orderid")
                        .HasColumnType("bigint");

                    b.Property<string>("restaurantOtp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("deliveryPartnerid");

                    b.HasIndex("orderid");

                    b.ToTable("DeliveryRequest", t =>
                        {
                            t.HasCheckConstraint("CHK_DeliveryRequest_deliveryRequestStatus", "[deliveryRequestStatus] IN ('ACCEPTED', 'COMPLETED', 'PENDING')");
                        });
                });

            modelBuilder.Entity("Zomato.Model.Menu", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("menuName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("restaurantid")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("restaurantid");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("Zomato.Model.MenuItem", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("dishDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("foodCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("imageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("ingredients")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAvailable")
                        .HasColumnType("bit");

                    b.Property<long>("menuid")
                        .HasColumnType("bigint");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<double>("rating")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("menuid");

                    b.ToTable("MenuItem", t =>
                        {
                            t.HasCheckConstraint("CHK_MenuItem_foodCategory", "[foodCategory] IN ('VEG', 'NONVEG', 'EGGCONTAINS')");
                        });
                });

            modelBuilder.Entity("Zomato.Model.Order", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<DateTime>("OrderCreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<long>("consumerid")
                        .HasColumnType("bigint");

                    b.Property<Point>("dropoffLocation")
                        .IsRequired()
                        .HasColumnType("geography");

                    b.Property<double>("foodAmount")
                        .HasColumnType("float");

                    b.Property<string>("orderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("paymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("paymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<Point>("pickupLocation")
                        .IsRequired()
                        .HasColumnType("geography");

                    b.Property<double>("platformFee")
                        .HasColumnType("float");

                    b.Property<long>("restaurantid")
                        .HasColumnType("bigint");

                    b.Property<double>("totalPrice")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("consumerid");

                    b.HasIndex("restaurantid");

                    b.ToTable("Order", t =>
                        {
                            t.HasCheckConstraint("CHK_Order_orderStatus", "[orderStatus] IN ('ACCEPTED', 'PREPARING', 'READY_FOR_PICKUP', 'DELIVERY_REQUEST_CREATED', 'DELIVERY_PARTNER_ASSIGNED', 'ACCEPTED_BY_DELIVERY_PARTNER', 'OUT_FOR_DELIVERY', 'DELIVERED', 'CANCELLED')");

                            t.HasCheckConstraint("CHK_Order_paymentMethod", "[paymentMethod] IN ('CASH', 'WALLET', 'UPI')");

                            t.HasCheckConstraint("CHK_Order_paymentStatus", "[paymentStatus] IN ('PENDING', 'CONFIRMED', 'REFUNDED')");
                        });
                });

            modelBuilder.Entity("Zomato.Model.OrderItem", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<long>("menuItemid")
                        .HasColumnType("bigint");

                    b.Property<long>("orderid")
                        .HasColumnType("bigint");

                    b.Property<long>("quantity")
                        .HasColumnType("bigint");

                    b.Property<double>("totalPrice")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("menuItemid");

                    b.HasIndex("orderid");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("Zomato.Model.OrderRequests", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<Point>("DropLocation")
                        .IsRequired()
                        .HasColumnType("geography");

                    b.Property<long>("cartid")
                        .HasColumnType("bigint");

                    b.Property<long>("consumerid")
                        .HasColumnType("bigint");

                    b.Property<double>("deliveryFee")
                        .HasColumnType("float");

                    b.Property<double>("foodAmount")
                        .HasColumnType("float");

                    b.Property<string>("orderRequestStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("paymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("paymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<double>("platformFee")
                        .HasColumnType("float");

                    b.Property<long>("restaurantid")
                        .HasColumnType("bigint");

                    b.Property<double>("totalPrice")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("cartid");

                    b.HasIndex("consumerid");

                    b.HasIndex("restaurantid");

                    b.ToTable("OrderRequest", t =>
                        {
                            t.HasCheckConstraint("CHK_OrderRequests_orderRequestStatus", "[orderRequestStatus] IN ('ACCEPTED', 'CANCELLED', 'PENDING')");

                            t.HasCheckConstraint("CHK_OrderRequests_paymentMethod", "[paymentMethod] IN ('CASH', 'WALLET', 'UPI')");

                            t.HasCheckConstraint("CHK_OrderRequests_paymentStatus", "[paymentStatus] IN ('PENDING', 'CONFIRMED', 'REFUNDED')");
                        });
                });

            modelBuilder.Entity("Zomato.Model.Payment", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<double>("amount")
                        .HasColumnType("float");

                    b.Property<long>("orderid")
                        .HasColumnType("bigint");

                    b.Property<string>("paymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("paymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("paymentTime")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("orderid");

                    b.ToTable("Payment", t =>
                        {
                            t.HasCheckConstraint("CHK_Payment_paymentMethod", "[paymentMethod] IN ('CASH', 'WALLET', 'UPI')");

                            t.HasCheckConstraint("CHK_Payment_paymentStatus", "[paymentStatus] IN ('PENDING', 'CONFIRMED', 'REFUNDED')");
                        });
                });

            modelBuilder.Entity("Zomato.Model.Restaurant", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("gstNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAvailable")
                        .HasColumnType("bit");

                    b.Property<bool>("isVarified")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("rating")
                        .HasColumnType("float");

                    b.Property<Point>("restaurantLocation")
                        .IsRequired()
                        .HasColumnType("geography");

                    b.Property<long>("restaurantPartnerid")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("restaurantPartnerid");

                    b.ToTable("Restaurant");
                });

            modelBuilder.Entity("Zomato.Model.RestaurantPartner", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<long>("aadharNo")
                        .HasColumnType("bigint");

                    b.Property<long>("userid")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("userid");

                    b.ToTable("RestaurantPartner");
                });

            modelBuilder.Entity("Zomato.Model.User", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("contact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("id");

                    b.ToTable("User", t =>
                        {
                            t.HasCheckConstraint("CHK_User_role", "[role] IN ('ADMIN', 'CONSUMER', 'DELIVERY_PARTNER', 'RESTAURENT_PARTNER')");
                        });
                });

            modelBuilder.Entity("Zomato.Model.Wallet", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<long>("userid")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("userid");

                    b.ToTable("Wallet");
                });

            modelBuilder.Entity("Zomato.Model.WalletTransaction", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("TransactionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("orderid")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("timeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("transactionMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("transactionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<long>("walletid")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("orderid");

                    b.HasIndex("walletid");

                    b.ToTable("WalletTransaction", t =>
                        {
                            t.HasCheckConstraint("CHK_WalletTransaction_transactionMethod", "[transactionMethod] IN ('BANKING', 'ORDER')");

                            t.HasCheckConstraint("CHK_WalletTransaction_transactionType", "[transactionType] IN ('CREDIT', 'DEBIT')");
                        });
                });

            modelBuilder.Entity("Zomato.Model.Address", b =>
                {
                    b.HasOne("Zomato.Model.User", null)
                        .WithMany("addresses")
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Zomato.Model.Cart", b =>
                {
                    b.HasOne("Zomato.Model.Consumer", "consumer")
                        .WithMany()
                        .HasForeignKey("consumerid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zomato.Model.Restaurant", "restaurant")
                        .WithMany()
                        .HasForeignKey("restaurantid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("consumer");

                    b.Navigation("restaurant");
                });

            modelBuilder.Entity("Zomato.Model.CartItem", b =>
                {
                    b.HasOne("Zomato.Model.Cart", "cart")
                        .WithMany("cartItems")
                        .HasForeignKey("cartid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zomato.Model.MenuItem", "menuItem")
                        .WithMany()
                        .HasForeignKey("menuItemid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("cart");

                    b.Navigation("menuItem");
                });

            modelBuilder.Entity("Zomato.Model.Consumer", b =>
                {
                    b.HasOne("Zomato.Model.User", "user")
                        .WithMany()
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Zomato.Model.DeliveryPartner", b =>
                {
                    b.HasOne("Zomato.Model.User", "user")
                        .WithMany()
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Zomato.Model.DeliveryRequest", b =>
                {
                    b.HasOne("Zomato.Model.DeliveryPartner", "deliveryPartner")
                        .WithMany("deliveryRequest")
                        .HasForeignKey("deliveryPartnerid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zomato.Model.Order", "order")
                        .WithMany()
                        .HasForeignKey("orderid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("deliveryPartner");

                    b.Navigation("order");
                });

            modelBuilder.Entity("Zomato.Model.Menu", b =>
                {
                    b.HasOne("Zomato.Model.Restaurant", "restaurant")
                        .WithMany()
                        .HasForeignKey("restaurantid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("restaurant");
                });

            modelBuilder.Entity("Zomato.Model.MenuItem", b =>
                {
                    b.HasOne("Zomato.Model.Menu", "menu")
                        .WithMany("menuItems")
                        .HasForeignKey("menuid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("menu");
                });

            modelBuilder.Entity("Zomato.Model.Order", b =>
                {
                    b.HasOne("Zomato.Model.Consumer", "consumer")
                        .WithMany()
                        .HasForeignKey("consumerid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zomato.Model.Restaurant", "restaurant")
                        .WithMany("Orders")
                        .HasForeignKey("restaurantid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("consumer");

                    b.Navigation("restaurant");
                });

            modelBuilder.Entity("Zomato.Model.OrderItem", b =>
                {
                    b.HasOne("Zomato.Model.MenuItem", "menuItem")
                        .WithMany()
                        .HasForeignKey("menuItemid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zomato.Model.Order", "order")
                        .WithMany("orderItems")
                        .HasForeignKey("orderid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("menuItem");

                    b.Navigation("order");
                });

            modelBuilder.Entity("Zomato.Model.OrderRequests", b =>
                {
                    b.HasOne("Zomato.Model.Cart", "cart")
                        .WithMany()
                        .HasForeignKey("cartid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zomato.Model.Consumer", "consumer")
                        .WithMany()
                        .HasForeignKey("consumerid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zomato.Model.Restaurant", "restaurant")
                        .WithMany("orderRequests")
                        .HasForeignKey("restaurantid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("cart");

                    b.Navigation("consumer");

                    b.Navigation("restaurant");
                });

            modelBuilder.Entity("Zomato.Model.Payment", b =>
                {
                    b.HasOne("Zomato.Model.Order", "order")
                        .WithMany()
                        .HasForeignKey("orderid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("order");
                });

            modelBuilder.Entity("Zomato.Model.Restaurant", b =>
                {
                    b.HasOne("Zomato.Model.RestaurantPartner", "restaurantPartner")
                        .WithMany()
                        .HasForeignKey("restaurantPartnerid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("restaurantPartner");
                });

            modelBuilder.Entity("Zomato.Model.RestaurantPartner", b =>
                {
                    b.HasOne("Zomato.Model.User", "user")
                        .WithMany()
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Zomato.Model.Wallet", b =>
                {
                    b.HasOne("Zomato.Model.User", "user")
                        .WithMany()
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Zomato.Model.WalletTransaction", b =>
                {
                    b.HasOne("Zomato.Model.Order", "order")
                        .WithMany()
                        .HasForeignKey("orderid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zomato.Model.Wallet", "wallet")
                        .WithMany("WalletTransaction")
                        .HasForeignKey("walletid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("order");

                    b.Navigation("wallet");
                });

            modelBuilder.Entity("Zomato.Model.Cart", b =>
                {
                    b.Navigation("cartItems");
                });

            modelBuilder.Entity("Zomato.Model.DeliveryPartner", b =>
                {
                    b.Navigation("deliveryRequest");
                });

            modelBuilder.Entity("Zomato.Model.Menu", b =>
                {
                    b.Navigation("menuItems");
                });

            modelBuilder.Entity("Zomato.Model.Order", b =>
                {
                    b.Navigation("orderItems");
                });

            modelBuilder.Entity("Zomato.Model.Restaurant", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("orderRequests");
                });

            modelBuilder.Entity("Zomato.Model.User", b =>
                {
                    b.Navigation("addresses");
                });

            modelBuilder.Entity("Zomato.Model.Wallet", b =>
                {
                    b.Navigation("WalletTransaction");
                });
#pragma warning restore 612, 618
        }
    }
}
