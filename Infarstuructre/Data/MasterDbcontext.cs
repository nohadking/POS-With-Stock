using Domin.Entity;

using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
namespace Infarstuructre.Data
{
    public class MasterDbcontext : IdentityDbContext<ApplicationUser>
    {
        public MasterDbcontext(DbContextOptions<MasterDbcontext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //***********************************************************


            builder.Entity<VwUser>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwUsers");
            });


            //*********************************************************  


            builder.Entity<TBViewProduct>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewProduct");
            });


            //*********************************************************  


            builder.Entity<TBViewInvoseHeder>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewInvoseHeder");
            });


            //*********************************************************  

            builder.Entity<TBViewInvose>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewInvose");
            });


            //*********************************************************  



            //*********************************************************
            builder.Entity<TBViewPhotoHomeSliderContent>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewPhotoHomeSliderContent");
            });


            //*********************************************************

               
            builder.Entity<TBViewExpense>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewExpense");
            });


            //*********************************************************

             //*********************************************************

               
            builder.Entity<TBViewSupplier>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewSupplier");
            });


            //*********************************************************


               //*********************************************************

               
            builder.Entity<TBViewClassCard>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewClassCard");
            });


            //*********************************************************

                  //*********************************************************

               
            builder.Entity<TBViewPurchase>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewPurchase");
            });


            //*********************************************************  
            //*********************************************************

               
            builder.Entity<TBViewOrderProductsFromSupplier>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewOrderProductsFromSupplier");
            });


            //*********************************************************

              //*********************************************************

               
            builder.Entity<TBViewLevelTwoAccount>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewLevelTwoAccount");
            });


            //*********************************************************


                 //*********************************************************

               
            builder.Entity<TBViewLevelThreeAccount>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewLevelThreeAccount");
            });


            //*********************************************************      
            //*********************************************************

               
            builder.Entity<TBViewLevelForeAccount>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewLevelForeAccount");
            });


            //*********************************************************      
            //*********************************************************

               
            builder.Entity<TBViewPhotoAddProdact>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewPhotoAddProdact");
            });


            //*********************************************************   
            //*********************************************************

               
            builder.Entity<TBViewArea>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewArea");
            });


            //*********************************************************    
            //*********************************************************

               
            builder.Entity<TBViewDeliveryCompanyPricing>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewDeliveryCompanyPricing");
            });


            //*********************************************************    
            //*********************************************************

               
            builder.Entity<TBViewStock>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewStock");
            });


            //*********************************************************  
            //*********************************************************

               
            builder.Entity<TBViewWarehouse>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewWarehouse");
            });


            //*********************************************************   
            //*********************************************************

               
            builder.Entity<TBViewOrderOnline>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewOrderOnline");
            });


            //*********************************************************
            //---------------------------------
            builder.Entity<TBEmailAlartSetting>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBEmailAlartSetting>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBEmailAlartSetting>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //---------------------------------   
            //---------------------------------
            builder.Entity<TBCategory>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBCategory>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBCategory>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //---------------------------------   
            //---------------------------------
            builder.Entity<TBProduct>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBProduct>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBProduct>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //---------------------------------  
            //---------------------------------
            builder.Entity<TBCustomerCategorie>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBCustomerCategorie>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBCustomerCategorie>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //---------------------------------  
            //---------------------------------
            builder.Entity<TBInvoseHeder>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBInvoseHeder>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBInvoseHeder>()
           .Property(b => b.OutstandingBill)
           .HasDefaultValueSql("((0))");
            //---------------------------------  
            //---------------------------------
            builder.Entity<TBPaymentMethod>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBPaymentMethod>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBPaymentMethod>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //---------------------------------
            builder.Entity<TBInvose>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBInvose>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");

            //--------------------------------- 
            //*********************************************************
            //---------------------------------
            builder.Entity<TBHomeSliderContent>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBHomeSliderContent>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            //---------------------------------  
            //---------------------------------
            builder.Entity<TBPhotoHomeSliderContent>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBPhotoHomeSliderContent>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            //---------------------------------  
            //---------------------------------
            builder.Entity<TBServiceSectionStartHomeContent>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBServiceSectionStartHomeContent>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            //---------------------------------  
            //---------------------------------
            builder.Entity<TBAboutSectionStartHomeContent>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBAboutSectionStartHomeContent>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            //--------------------------------- 
            //---------------------------------
            builder.Entity<TBCategoryServic>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBCategoryServic>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBCategoryServic>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //---------------------------------
            //---------------------------------
            builder.Entity<TBCompanyInformation>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBCompanyInformation>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");

            //---------------------------------
            //---------------------------------
            builder.Entity<TBExpenseCategory>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBExpenseCategory>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBExpenseCategory>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //--------------------------------- 
            //---------------------------------
            builder.Entity<TBExpense>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBExpense>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");  

            //--------------------------------- 
            //---------------------------------
            builder.Entity<TBSupplier>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBSupplier>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");   
            builder.Entity<TBSupplier>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //---------------------------------
            //---------------------------------
            builder.Entity<TBUnit>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBUnit>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");

            //---------------------------------  
            //---------------------------------
            builder.Entity<TBClassCard>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBClassCard>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");   
            builder.Entity<TBClassCard>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");

            //---------------------------------  
            //---------------------------------
            builder.Entity<TBPurchase>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBPurchase>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");   
      

            //--------------------------------- 
            //---------------------------------
            builder.Entity<TBAccountingRestriction>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBAccountingRestriction>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");   
            //---------------------------------
            //---------------------------------
            builder.Entity<TBBestSellingProductsHomeContent>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBBestSellingProductsHomeContent>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");   
            //---------------------------------  
            //---------------------------------
            //---------------------------------
            builder.Entity<TBHomeBackgroundimage>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBHomeBackgroundimage>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");   
            //---------------------------------  
            //---------------------------------
            builder.Entity<TBHomeImageProdact>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBHomeImageProdact>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");   
            //---------------------------------   
            //---------------------------------
            builder.Entity<TBOrderProductsFromSupplier>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBOrderProductsFromSupplier>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");   
            //---------------------------------  
            //---------------------------------
            builder.Entity<TBMainAccount>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBMainAccount>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");  
            builder.Entity<TBMainAccount>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");   
            //---------------------------------  
            //---------------------------------
            builder.Entity<TBLevelTwoAccount>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBLevelTwoAccount>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");  
            builder.Entity<TBLevelTwoAccount>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");   
            //--------------------------------- 
            //---------------------------------
            builder.Entity<TBLevelThreeAccount>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBLevelThreeAccount>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");  
            builder.Entity<TBLevelThreeAccount>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");   
            //---------------------------------   
            //---------------------------------
            builder.Entity<TBLevelForeAccount>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBLevelForeAccount>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");  
            builder.Entity<TBLevelForeAccount>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");   
            //---------------------------------   
            //---------------------------------
            builder.Entity<TBStaff>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBStaff>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");  
            builder.Entity<TBStaff>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");   
            //---------------------------------    
            //---------------------------------
            builder.Entity<TBAboutSectionStartShopContent>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBAboutSectionStartShopContent>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");  
       
            //---------------------------------   
            //---------------------------------
            builder.Entity<TBPhotoShopLiftSaide>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBPhotoShopLiftSaide>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");  
       
            //---------------------------------  
            //---------------------------------
            builder.Entity<TBPhotoAddProdact>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBPhotoAddProdact>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");  
       
            //---------------------------------   
            //---------------------------------
            builder.Entity<TBCustomerMessage>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBCustomerMessage>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");  
       
            //---------------------------------   
            //---------------------------------
            builder.Entity<TBDeliveryCompanie>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBDeliveryCompanie>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");   
            builder.Entity<TBDeliveryCompanie>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");  
       
            //---------------------------------      
            //---------------------------------
            builder.Entity<TBCitie>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBCitie>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");   
            builder.Entity<TBCitie>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");  
       
            //---------------------------------    
            //---------------------------------
            builder.Entity<TBArea>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBArea>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");   
            builder.Entity<TBArea>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");  
       
            //---------------------------------     
            //---------------------------------
            builder.Entity<TBDeliveryCompanyPricing>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBDeliveryCompanyPricing>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");   
 
       
            //---------------------------------   
            //---------------------------------
            builder.Entity<TBTypeOrder>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBTypeOrder>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");   
            builder.Entity<TBTypeOrder>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");   
 
       
            //--------------------------------- 
            //---------------------------------
            builder.Entity<TBOrderOnline>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBOrderOnline>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");  
            //---------------------------------   
            //---------------------------------
            builder.Entity<TBWarehouse>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBWarehouse>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");  
            builder.Entity<TBWarehouse>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");  
            //---------------------------------  
            //---------------------------------
            builder.Entity<TBStock>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBStock>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");  
       
            //--------------------------------- 
        }
        //***********************************
        public DbSet<VwUser> VwUsers { get; set; }
        public DbSet<TBEmailAlartSetting> TBEmailAlartSettings { get; set; }
        public DbSet<TBCategory> TBCategorys { get; set; }
        public DbSet<TBProduct> TBProducts { get; set; }
        public DbSet<TBViewProduct> ViewProduct { get; set; }
        public DbSet<TBCustomerCategorie> TBCustomerCategories { get; set; }
        public DbSet<TBInvoseHeder> TBInvoseHeders { get; set; }
        public DbSet<TBViewInvoseHeder> ViewInvoseHeder { get; set; }
        public DbSet<TBPaymentMethod> TBPaymentMethods { get; set; }
        public DbSet<TBInvose> TBInvoses { get; set; }
        public DbSet<TBViewInvose> ViewInvose { get; set; }
        public DbSet<TBHomeSliderContent> TBHomeSliderContents { get; set; }
        public DbSet<TBPhotoHomeSliderContent> TBPhotoHomeSliderContents { get; set; }
        public DbSet<TBViewPhotoHomeSliderContent> ViewPhotoHomeSliderContent { get; set; }
        public DbSet<TBServiceSectionStartHomeContent> TBServiceSectionStartHomeContents { get; set; }
        public DbSet<TBAboutSectionStartHomeContent> TBAboutSectionStartHomeContents { get; set; }
        public DbSet<TBCategoryServic> TBCategoryServics { get; set; }
        public DbSet<TBBrandProduct> TBBrandProducts { get; set; }
        public DbSet<TBCompanyInformation> TBCompanyInformations { get; set; }
        public DbSet<TBExpenseCategory> TBExpenseCategorys { get; set; }
        public DbSet<TBExpense> TBExpenses { get; set; }
        public DbSet<TBViewExpense> ViewExpense { get; set; }
        public DbSet<TBSupplier> TBSuppliers { get; set; }
        public DbSet<TBViewSupplier> ViewSupplier { get; set; }
        public DbSet<TBUnit> TBUnits { get; set; }
        public DbSet<TBClassCard> TBClassCards { get; set; }
        public DbSet<TBViewClassCard> ViewClassCard { get; set; }
        public DbSet<TBPurchase> TBPurchases { get; set; }
        public DbSet<TBAccountingRestriction> TBAccountingRestrictions { get; set; }
        public DbSet<TBViewPurchase> ViewPurchase { get; set; }
        public DbSet<TBBestSellingProductsHomeContent> TBBestSellingProductsHomeContents { get; set; }
        public DbSet<TBHomeBackgroundimage> TBHomeBackgroundimages { get; set; }
        public DbSet<TBHomeImageProdact> TBHomeImageProdacts { get; set; }
        public DbSet<TBOrderProductsFromSupplier> TBOrderProductsFromSuppliers { get; set; }
        public DbSet<TBViewOrderProductsFromSupplier> ViewOrderProductsFromSupplier { get; set; }
        public DbSet<TBMainAccount> TBMainAccounts { get; set; }
        public DbSet<TBLevelTwoAccount> TBLevelTwoAccounts { get; set; }
        public DbSet<TBViewLevelTwoAccount> ViewLevelTwoAccount { get; set; }
        public DbSet<TBLevelThreeAccount> TBLevelThreeAccounts { get; set; }
        public DbSet<TBViewLevelThreeAccount> ViewLevelThreeAccount { get; set; }
        public DbSet<TBLevelForeAccount> TBLevelForeAccounts { get; set; }
        public DbSet<TBViewLevelForeAccount> ViewLevelForeAccount { get; set; }
        public DbSet<TBStaff> TBStaffs { get; set; }
        public DbSet<TBAboutSectionStartShopContent> TBAboutSectionStartShopContents { get; set; }
        public DbSet<TBPhotoShopLiftSaide> TBPhotoShopLiftSaides { get; set; }
        public DbSet<TBPhotoAddProdact> TBPhotoAddProdacts { get; set; }
        public DbSet<TBViewPhotoAddProdact> ViewPhotoAddProdact { get; set; }
        public DbSet<TBCustomerMessage> TBCustomerMessages { get; set; }
        public DbSet<TBDeliveryCompanie> TBDeliveryCompanies { get; set; }
        public DbSet<TBCitie> TBCities { get; set; }
        public DbSet<TBArea> TBAreas { get; set; }
        public DbSet<TBViewArea> ViewArea { get; set; }
        public DbSet<TBDeliveryCompanyPricing> TBDeliveryCompanyPricings { get; set; }
        public DbSet<TBViewDeliveryCompanyPricing> ViewDeliveryCompanyPricing { get; set; }
        public DbSet<TBTypeOrder> TBTypeOrders { get; set; }
        public DbSet<TBOrderOnline> TBOrderOnlines { get; set; }
        public DbSet<TBViewOrderOnline> ViewOrderOnline { get; set; }
        public DbSet<TBWarehouse> TBWarehouses { get; set; }
        public DbSet<TBViewWarehouse> ViewWarehouse { get; set; }
        public DbSet<TBStock> TBStocks { get; set; }
        public DbSet<TBViewStock> ViewStock { get; set; }


    }
}
