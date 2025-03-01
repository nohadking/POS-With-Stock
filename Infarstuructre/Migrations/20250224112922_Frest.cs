using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class Frest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveUser = table.Column<bool>(type: "bit", nullable: false),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBAboutSectionStartHomeContents",
                columns: table => new
                {
                    IdAboutSectionStartHomeContent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleOneAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleOneEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleTwoAr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    TitleTwoEn = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAboutSectionStartHomeContents", x => x.IdAboutSectionStartHomeContent);
                });

            migrationBuilder.CreateTable(
                name: "TBAboutSectionStartShopContents",
                columns: table => new
                {
                    IdAboutSectionStartShopContent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleOneAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleOneEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleTwoAr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    TitleTwoEn = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAboutSectionStartShopContents", x => x.IdAboutSectionStartShopContent);
                });

            migrationBuilder.CreateTable(
                name: "TBAccountingRestrictions",
                columns: table => new
                {
                    IdaccountingRestrictions = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberaccountingRestrictions = table.Column<int>(type: "int", nullable: false),
                    AccountingName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BondType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BondNumber = table.Column<int>(type: "int", nullable: false),
                    Debtor = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    creditor = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Statement = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Nouts = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAccountingRestrictions", x => x.IdaccountingRestrictions);
                });

            migrationBuilder.CreateTable(
                name: "TBAreas",
                columns: table => new
                {
                    IdArea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCitie = table.Column<int>(type: "int", nullable: false),
                    AreaName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAreas", x => x.IdArea);
                });

            migrationBuilder.CreateTable(
                name: "TBBestSellingProductsHomeContents",
                columns: table => new
                {
                    IdBestSellingProductsHomeContent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleOneAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleOneEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleTwoAr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    TitleTwoEn = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBBestSellingProductsHomeContents", x => x.IdBestSellingProductsHomeContent);
                });

            migrationBuilder.CreateTable(
                name: "TBBrandProducts",
                columns: table => new
                {
                    IdBrandProduct = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitelOneAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitelOneEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    URlButtonEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    URlButtonAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBBrandProducts", x => x.IdBrandProduct);
                });

            migrationBuilder.CreateTable(
                name: "TBCategorys",
                columns: table => new
                {
                    IdCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryNameAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CategoryNameEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCategorys", x => x.IdCategory);
                });

            migrationBuilder.CreateTable(
                name: "TBCategoryServics",
                columns: table => new
                {
                    IdCategoryServic = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleOneAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleOneEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleTwoAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleTwoEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleThreAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleThreEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleForAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleForEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitlefiveAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitlefiveEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleButtonAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleButtonEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    UrlButtonAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UrlButtonEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCategoryServics", x => x.IdCategoryServic);
                });

            migrationBuilder.CreateTable(
                name: "TBCities",
                columns: table => new
                {
                    IdCitie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitieName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCities", x => x.IdCitie);
                });

            migrationBuilder.CreateTable(
                name: "TBClassCards",
                columns: table => new
                {
                    IdClassCard = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUnit = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeItem = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ProductionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBClassCards", x => x.IdClassCard);
                });

            migrationBuilder.CreateTable(
                name: "TBCompanyInformations",
                columns: table => new
                {
                    IdCompanyInformation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameCompanyAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    NameCompanyEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NameOner = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PhoneOner = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmailOner = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    EmailCompany = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ShortDescriptionAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ShortDescriptionEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    AddressAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AddressEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    FaceBoock = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Instagram = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    YouTube = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Mabs = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCompanyInformations", x => x.IdCompanyInformation);
                });

            migrationBuilder.CreateTable(
                name: "TBCustomerCategories",
                columns: table => new
                {
                    IdCustomerCategorie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryNameAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CategoryNameEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCustomerCategories", x => x.IdCustomerCategorie);
                });

            migrationBuilder.CreateTable(
                name: "TBCustomerMessages",
                columns: table => new
                {
                    IdCustomerMessage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    MessageTitle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmailMessage = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCustomerMessages", x => x.IdCustomerMessage);
                });

            migrationBuilder.CreateTable(
                name: "TBDeliveryCompanies",
                columns: table => new
                {
                    IdDeliveryCompanie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryCompanie = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OnerName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    OnerPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmilCompanie = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    EmilOner = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DeliveryCompanieAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBDeliveryCompanies", x => x.IdDeliveryCompanie);
                });

            migrationBuilder.CreateTable(
                name: "TBDeliveryCompanyPricings",
                columns: table => new
                {
                    IdDeliveryCompanyPricing = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDeliveryCompanie = table.Column<int>(type: "int", nullable: false),
                    IdArea = table.Column<int>(type: "int", nullable: false),
                    Pricing = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBDeliveryCompanyPricings", x => x.IdDeliveryCompanyPricing);
                });

            migrationBuilder.CreateTable(
                name: "TBEmailAlartSettings",
                columns: table => new
                {
                    IdEmailAlartSetting = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MailSender = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    SmtpServer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PortServer = table.Column<int>(type: "int", nullable: false),
                    PasswordEmail = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Ssl_validity = table.Column<bool>(type: "bit", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBEmailAlartSettings", x => x.IdEmailAlartSetting);
                });

            migrationBuilder.CreateTable(
                name: "TBExpenseCategorys",
                columns: table => new
                {
                    IdExpenseCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseCategory = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBExpenseCategorys", x => x.IdExpenseCategory);
                });

            migrationBuilder.CreateTable(
                name: "TBExpenses",
                columns: table => new
                {
                    IdExpense = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdExpenseCategory = table.Column<int>(type: "int", nullable: false),
                    IdLevelForeAccount = table.Column<int>(type: "int", nullable: false),
                    BondNumber = table.Column<int>(type: "int", nullable: false),
                    DateBond = table.Column<DateOnly>(type: "date", nullable: false),
                    Statement = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBExpenses", x => x.IdExpense);
                });

            migrationBuilder.CreateTable(
                name: "TBHomeBackgroundimages",
                columns: table => new
                {
                    IdHomeBackgroundimage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBHomeBackgroundimages", x => x.IdHomeBackgroundimage);
                });

            migrationBuilder.CreateTable(
                name: "TBHomeImageProdacts",
                columns: table => new
                {
                    IdHomeImageProdact = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBHomeImageProdacts", x => x.IdHomeImageProdact);
                });

            migrationBuilder.CreateTable(
                name: "TBHomeSliderContents",
                columns: table => new
                {
                    IdHomeSliderContent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleOneAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleOneEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleTwoAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleTwoEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleButtonAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleButtonEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    UrlButtonAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UrlButtonEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBHomeSliderContents", x => x.IdHomeSliderContent);
                });

            migrationBuilder.CreateTable(
                name: "TBInvoseHeders",
                columns: table => new
                {
                    IdInvoseHeder = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    IdPaymentMethod = table.Column<int>(type: "int", nullable: false),
                    DateInvos = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    OutstandingBill = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBInvoseHeders", x => x.IdInvoseHeder);
                });

            migrationBuilder.CreateTable(
                name: "TBInvoses",
                columns: table => new
                {
                    IdInvose = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdInvoseHeder = table.Column<int>(type: "int", nullable: false),
                    IdProduct = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBInvoses", x => x.IdInvose);
                });

            migrationBuilder.CreateTable(
                name: "TBLevelForeAccounts",
                columns: table => new
                {
                    IdLevelForeAccount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLevelThreeAccount = table.Column<int>(type: "int", nullable: false),
                    IdLevelTwoAccount = table.Column<int>(type: "int", nullable: false),
                    IdMainAccount = table.Column<int>(type: "int", nullable: false),
                    AccountNumberlivl = table.Column<long>(type: "bigint", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBLevelForeAccounts", x => x.IdLevelForeAccount);
                });

            migrationBuilder.CreateTable(
                name: "TBLevelThreeAccounts",
                columns: table => new
                {
                    IdLevelThreeAccount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMainAccount = table.Column<int>(type: "int", nullable: false),
                    IdLevelTwoAccount = table.Column<int>(type: "int", nullable: false),
                    NumberAccount = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBLevelThreeAccounts", x => x.IdLevelThreeAccount);
                });

            migrationBuilder.CreateTable(
                name: "TBLevelTwoAccounts",
                columns: table => new
                {
                    IdLevelTwoAccount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMainAccount = table.Column<int>(type: "int", nullable: false),
                    NumberAccount = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBLevelTwoAccounts", x => x.IdLevelTwoAccount);
                });

            migrationBuilder.CreateTable(
                name: "TBMainAccounts",
                columns: table => new
                {
                    IdMainAccount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberAccount = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMainAccounts", x => x.IdMainAccount);
                });

            migrationBuilder.CreateTable(
                name: "TBOrderOnlines",
                columns: table => new
                {
                    IdOrderOnline = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdInvose = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTypeOrder = table.Column<int>(type: "int", nullable: false),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Nouts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBOrderOnlines", x => x.IdOrderOnline);
                });

            migrationBuilder.CreateTable(
                name: "TBOrderProductsFromSuppliers",
                columns: table => new
                {
                    IdOrderProductsFromSupplier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSupplier = table.Column<int>(type: "int", nullable: false),
                    NumberOrderProducts = table.Column<int>(type: "int", nullable: false),
                    DateOrderProducts = table.Column<DateOnly>(type: "date", nullable: false),
                    IdProduct = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IdUnit = table.Column<int>(type: "int", nullable: false),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    Nouts = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBOrderProductsFromSuppliers", x => x.IdOrderProductsFromSupplier);
                });

            migrationBuilder.CreateTable(
                name: "TBPaymentMethods",
                columns: table => new
                {
                    IdPaymentMethod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethodAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaymentMethodEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBPaymentMethods", x => x.IdPaymentMethod);
                });

            migrationBuilder.CreateTable(
                name: "TBPhotoAddProdacts",
                columns: table => new
                {
                    IdPhotoAddProdact = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProduct = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBPhotoAddProdacts", x => x.IdPhotoAddProdact);
                });

            migrationBuilder.CreateTable(
                name: "TBPhotoHomeSliderContents",
                columns: table => new
                {
                    IdPhotoHomeSliderContent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdHomeSliderContent = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBPhotoHomeSliderContents", x => x.IdPhotoHomeSliderContent);
                });

            migrationBuilder.CreateTable(
                name: "TBPhotoShopLiftSaides",
                columns: table => new
                {
                    IdPhotoShopLiftSaide = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBPhotoShopLiftSaides", x => x.IdPhotoShopLiftSaide);
                });

            migrationBuilder.CreateTable(
                name: "TBProducts",
                columns: table => new
                {
                    IdProduct = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCategory = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductNameAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ProductNameEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBProducts", x => x.IdProduct);
                });

            migrationBuilder.CreateTable(
                name: "TBPurchases",
                columns: table => new
                {
                    IdPurchase = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSupplier = table.Column<int>(type: "int", nullable: false),
                    IdPaymentMethod = table.Column<int>(type: "int", nullable: false),
                    Statement = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PurchaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PurchaseNumber = table.Column<int>(type: "int", nullable: false),
                    PurchaseSubNumber = table.Column<int>(type: "int", nullable: true),
                    IdProduct = table.Column<int>(type: "int", nullable: false),
                    IdUnit = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    FreeQuantity = table.Column<int>(type: "int", nullable: true),
                    AllQuantity = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    SingleDiscount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    shipping = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Nouts = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    TotalAll = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBPurchases", x => x.IdPurchase);
                });

            migrationBuilder.CreateTable(
                name: "TBServiceSectionStartHomeContents",
                columns: table => new
                {
                    IServiceSectionStartContent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleOneAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleOneEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleTwoAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleTwoEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleThreAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleThreEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleForAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleForEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitlefiveAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitlefiveEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitlesixAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitlesixEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleSevenAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleSevenEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleEightAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleEightEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBServiceSectionStartHomeContents", x => x.IServiceSectionStartContent);
                });

            migrationBuilder.CreateTable(
                name: "TBStaffs",
                columns: table => new
                {
                    IdStaff = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeFullname = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    EmployeePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmployeeEmail = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AccountNumber = table.Column<long>(type: "bigint", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    EmployeeAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBStaffs", x => x.IdStaff);
                });

            migrationBuilder.CreateTable(
                name: "TBSuppliers",
                columns: table => new
                {
                    IdSupplier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPaymentMethod = table.Column<int>(type: "int", nullable: false),
                    NumberAccount = table.Column<long>(type: "bigint", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NameOner = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PhoneOner = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmailOner = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    EmailCompany = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Debtlimit = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    GracePeriod = table.Column<int>(type: "int", nullable: false),
                    website = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSuppliers", x => x.IdSupplier);
                });

            migrationBuilder.CreateTable(
                name: "TBTypeOrders",
                columns: table => new
                {
                    IdTypeOrder = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOrderEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TypeOrderAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBTypeOrders", x => x.IdTypeOrder);
                });

            migrationBuilder.CreateTable(
                name: "TBUnits",
                columns: table => new
                {
                    IdUnit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Unit = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBUnits", x => x.IdUnit);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TBAboutSectionStartHomeContents");

            migrationBuilder.DropTable(
                name: "TBAboutSectionStartShopContents");

            migrationBuilder.DropTable(
                name: "TBAccountingRestrictions");

            migrationBuilder.DropTable(
                name: "TBAreas");

            migrationBuilder.DropTable(
                name: "TBBestSellingProductsHomeContents");

            migrationBuilder.DropTable(
                name: "TBBrandProducts");

            migrationBuilder.DropTable(
                name: "TBCategorys");

            migrationBuilder.DropTable(
                name: "TBCategoryServics");

            migrationBuilder.DropTable(
                name: "TBCities");

            migrationBuilder.DropTable(
                name: "TBClassCards");

            migrationBuilder.DropTable(
                name: "TBCompanyInformations");

            migrationBuilder.DropTable(
                name: "TBCustomerCategories");

            migrationBuilder.DropTable(
                name: "TBCustomerMessages");

            migrationBuilder.DropTable(
                name: "TBDeliveryCompanies");

            migrationBuilder.DropTable(
                name: "TBDeliveryCompanyPricings");

            migrationBuilder.DropTable(
                name: "TBEmailAlartSettings");

            migrationBuilder.DropTable(
                name: "TBExpenseCategorys");

            migrationBuilder.DropTable(
                name: "TBExpenses");

            migrationBuilder.DropTable(
                name: "TBHomeBackgroundimages");

            migrationBuilder.DropTable(
                name: "TBHomeImageProdacts");

            migrationBuilder.DropTable(
                name: "TBHomeSliderContents");

            migrationBuilder.DropTable(
                name: "TBInvoseHeders");

            migrationBuilder.DropTable(
                name: "TBInvoses");

            migrationBuilder.DropTable(
                name: "TBLevelForeAccounts");

            migrationBuilder.DropTable(
                name: "TBLevelThreeAccounts");

            migrationBuilder.DropTable(
                name: "TBLevelTwoAccounts");

            migrationBuilder.DropTable(
                name: "TBMainAccounts");

            migrationBuilder.DropTable(
                name: "TBOrderOnlines");

            migrationBuilder.DropTable(
                name: "TBOrderProductsFromSuppliers");

            migrationBuilder.DropTable(
                name: "TBPaymentMethods");

            migrationBuilder.DropTable(
                name: "TBPhotoAddProdacts");

            migrationBuilder.DropTable(
                name: "TBPhotoHomeSliderContents");

            migrationBuilder.DropTable(
                name: "TBPhotoShopLiftSaides");

            migrationBuilder.DropTable(
                name: "TBProducts");

            migrationBuilder.DropTable(
                name: "TBPurchases");

            migrationBuilder.DropTable(
                name: "TBServiceSectionStartHomeContents");

            migrationBuilder.DropTable(
                name: "TBStaffs");

            migrationBuilder.DropTable(
                name: "TBSuppliers");

            migrationBuilder.DropTable(
                name: "TBTypeOrders");

            migrationBuilder.DropTable(
                name: "TBUnits");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
