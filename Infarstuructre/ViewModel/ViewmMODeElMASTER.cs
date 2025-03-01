using Domin.Entity;
using Domin.Resource;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.ViewModel
{
    public class ViewmMODeElMASTER
    {
        public returnUrl returnUrl { get; set; }
        public IEnumerable<IdentityRole> ListIdentityRole { get; set; }
        public IdentityRole? sIdentityRole { get; set; }
        public IEnumerable<VwUser> ListVwUser { get; set; }
        public IEnumerable<ApplicationUser> ListlicationUser { get; set; }
        public int CustomerCount { get; set; } // إضافة الخاصية هنا
        public VwUser sVwUser { get; set; }
        public ApplicationUser sUser { get; set; }
        public RegisterViewModel ruser { get; set; }
        public NewRegister SNewRegister { get; set; }
        public IEnumerable<RegisterViewModel> ListRegisterViewModel { get; set; }
        public IEnumerable<NewRegister> ListNewRegister { get; set; }
        public ChangePasswordViewModel SChangePassword { get; set; }
        public bool Rememberme { get; set; }
        public List<SelectListItem> Roles1 { get; set; }
        public string SelectedRoleId { get; set; }
        public IEnumerable<Product> Product1 { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string UserImage { get; set; }
        public string Name { get; set; }
        public string UserRole { get; set; }
        public NewRegister NewRegister { get; set; }
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string? ImageUser { get; set; }
        public bool ActiveUser { get; set; }
        public string Password { get; set; }
        public string ComparePassword { get; set; }
        public string userName { get; set; }
        public string PhoneNumber { get; set; }
        public int IdCategory { get; set; }
        public string Photo { get; set; }
        public string ProductNameEn { get; set; }
        public decimal price { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public List<VwUser> Users { get; set; }
        public List<TBEmailAlartSetting> ListEmailAlartSetting { get; set; }
        public TBEmailAlartSetting EmailAlartSetting { get; set; }
        public List<TBCategory> ListCategory { get; set; }
        public TBCategory Category { get; set; }
        public List<TBViewProduct> ListViewProduct { get; set; }
        public TBProduct Product { get; set; }
        public TBViewProduct Productsng { get; set; }





        public List<TBCustomerCategorie> ListCustomerCategorie { get; set; }
        public TBCustomerCategorie CustomerCategorie { get; set; }
        public List<TBViewInvoseHeder> ListViewInvoseHede { get; set; }
        public TBInvoseHeder InvoseHeder { get; set; }
        public List<TBPaymentMethod> ListPaymentMethod { get; set; }
        public TBPaymentMethod PaymentMethod { get; set; }
        public List<TBHomeSliderContent> ListHomeSliderContent { get; set; }
        public TBHomeSliderContent HomeSliderContent { get; set; }
        public List<TBViewPhotoHomeSliderContent> ListViewPhotoHomeSliderContent { get; set; }
        public TBPhotoHomeSliderContent PhotoHomeSliderContent { get; set; }
        public List<TBServiceSectionStartHomeContent> ListServiceSectionStartHomeContent { get; set; }
        public TBServiceSectionStartHomeContent ServiceSectionStartHomeContent { get; set; }
        public List<TBAboutSectionStartHomeContent> ListAboutSectionStartHomeContent { get; set; }
        public TBAboutSectionStartHomeContent AboutSectionStartHomeContent { get; set; }
        public List<TBCategoryServic> ListCategoryServic { get; set; }
        public TBCategoryServic CategoryServic { get; set; }
        public List<TBBrandProduct> ListBrandProduct { get; set; }
        public TBBrandProduct BrandProduct { get; set; }
        public List<TBViewInvose> ListViewInvose { get; set; }
        public TBInvose Invoic { get; set; }
        public List<TBCompanyInformation> ListCompanyInformation { get; set; }
        public TBCompanyInformation CompanyInformation { get; set; }
        public List<TBExpenseCategory> ListExpenseCategory { get; set; }
        public TBExpenseCategory ExpenseCategory { get; set; }
        public List<TBViewExpense> ListViewExpense { get; set; }
        public TBExpense Expense { get; set; }
        public List<TBViewSupplier> ListViewSupplier { get; set; }
        public TBSupplier Supplier { get; set; }
        public List<TBUnit> ListUnit { get; set; }
        public TBUnit Unit { get; set; }
        public List<TBViewClassCard> ListViewClassCard { get; set; }
        public TBClassCard ClassCard { get; set; }
        public List<TBAccountingRestriction> ListAccountingRestriction { get; set; }
        public TBAccountingRestriction AccountingRestriction { get; set; }
        public List<TBViewPurchase> ListViewPurchase { get; set; }
        public TBPurchase Purchase { get; set; }
        public List<TBBestSellingProductsHomeContent> ListBestSellingProductsHomeContent { get; set; }
        public TBBestSellingProductsHomeContent BestSellingProductsHomeContent { get; set; }
        public List<TBHomeBackgroundimage> ListHomeBackgroundimage { get; set; }
        public TBHomeBackgroundimage HomeBackgroundimage { get; set; }
        public List<TBHomeImageProdact> ListHomeImageProdact { get; set; }
        public TBHomeImageProdact HomeImageProdact { get; set; }
        public List<TBViewOrderProductsFromSupplier> ListViewOrderProductsFromSupplier { get; set; }
        public TBOrderProductsFromSupplier OrderProductsFromSupplier { get; set; }
        public List<TBMainAccount> ListMainAccount { get; set; }
        public TBMainAccount MainAccount { get; set; }
        public List<TBViewLevelTwoAccount> ListViewLevelTwoAccount { get; set; }
        public TBLevelTwoAccount LevelTwoAccount { get; set; }
        public List<TBViewLevelThreeAccount> ListViewViewLevelThreeAccount { get; set; }
        public TBLevelThreeAccount LevelThreeAccount { get; set; }
        public List<TBViewLevelForeAccount> ListViewLevelForeAccount { get; set; }
        public TBLevelForeAccount LevelForeAccount { get; set; }  
        public List<TBStaff> ListStaff { get; set; }
        public TBStaff Staff { get; set; } 
        public List<TBAboutSectionStartShopContent> ListAboutSectionStartShopContent { get; set; }
        public TBAboutSectionStartShopContent AboutSectionStartShopContent { get; set; }    
        public List<TBPhotoShopLiftSaide> ListPhotoShopLiftSaide { get; set; }
        public TBPhotoShopLiftSaide PhotoShopLiftSaide { get; set; }
        public List<TBViewPhotoAddProdact> ListViewPhotoAddProdact { get; set; }
        public TBPhotoAddProdact PhotoAddProdact { get; set; }
        public List<TBCustomerMessage> ListCustomerMessage { get; set; }
        public TBCustomerMessage CustomerMessage { get; set; }   
        public List<TBDeliveryCompanie> ListDeliveryCompanie { get; set; }
        public TBDeliveryCompanie DeliveryCompanie { get; set; } 
        public List<TBCitie> ListCitie { get; set; }
        public TBCitie Citie { get; set; }
        public List<TBViewArea> ListViewArea { get; set; }
        public TBArea Area { get; set; }    
        public List<TBViewDeliveryCompanyPricing> ListViewDeliveryCompanyPricing { get; set; }
        public TBDeliveryCompanyPricing DeliveryCompanyPricing { get; set; } 
        public List<TBTypeOrder> ListTypeOrder { get; set; }
        public TBTypeOrder TypeOrder { get; set; }    
        public List<TBViewWarehouse> ListViewWarehouse { get; set; }
        public TBWarehouse Warehouse { get; set; }   
        public List<TBViewStock> ListViewStock { get; set; }
        public TBStock Stock { get; set; }


    }
}

