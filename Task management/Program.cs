using QuestPDF.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// إضافة خدمات إلى الحاوية
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ViewmMODeElMASTER>(); // خدمة مخصصة

// إضافة DBContext
builder.Services.AddDbContext<MasterDbcontext>(options => {
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MasterConnection"),
        sqlOptions => sqlOptions.CommandTimeout(180) // تحديد مهلة الاتصال بـ 180 ثانية
    );
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// إضافة المصادقة باستخدام JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// إضافة التفويض (Authorization)
builder.Services.AddAuthorization();

// إضافة Swagger لتوثيق الـ API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        { securityScheme, new[] { "Bearer" } }
    };

    c.AddSecurityRequirement(securityRequirement);
});

// إضافة خدمات Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<MasterDbcontext>()
.AddDefaultTokenProviders();

// تكوين ملفات تعريف الارتباط (Cookies)

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/Accounts/Login";
    options.AccessDeniedPath = "/Admin/Home/Denied";
    options.Cookie.Name = "Cookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;

    // هنا نضيف OnValidatePrincipal لجعل المستخدم Offline عند انتهاء الجلسة
    //options.Events = new CookieAuthenticationEvents
    //{
    //    OnValidatePrincipal = async context =>
    //    {
    //        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
    //        var dbContext = context.HttpContext.RequestServices.GetRequiredService<MasterDbcontext>();

    //        var userId = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //        if (userId != null)
    //        {
    //            var user = await userManager.FindByIdAsync(userId);
    //            if (user != null)
    //            {
    //                var lastLoginTime = context.Properties?.IssuedUtc;
    //                var sessionExpired = lastLoginTime.HasValue && DateTime.UtcNow > lastLoginTime.Value.Add(options.ExpireTimeSpan);

    //                if (sessionExpired)
    //                {
    //                    // تحديث حالة المستخدم إلى Offline
    //                    user.IsOnline = false;
    //                    dbContext.Users.Update(user);
    //                    await dbContext.SaveChangesAsync();

    //                    // تسجيل خروج المستخدم
    //                    context.RejectPrincipal();
    //                    await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    //                }
    //            }
    //        }
    //    }
    //};

    options.Events = new CookieAuthenticationEvents
    {
        OnValidatePrincipal = async context =>
        {
            var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
            var dbContext = context.HttpContext.RequestServices.GetRequiredService<MasterDbcontext>();

            var userId = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var lastLoginTime = context.Properties?.IssuedUtc;

                    if (lastLoginTime.HasValue)
                    {
                        var sessionExpired = DateTime.UtcNow > lastLoginTime.Value.Add(options.ExpireTimeSpan);

                        if (sessionExpired)
                        {
                            // تحديث حالة المستخدم إلى Offline
                            user.IsOnline = false;
                            dbContext.Users.Update(user);
                            await dbContext.SaveChangesAsync();

                            // تسجيل خروج المستخدم
                            context.RejectPrincipal();
                            await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        }
                    }
                }
            }
        }
    };

});


// إضافة خدمات مخصصة (Scoped Services)
builder.Services.AddScoped<IIUserInformation, CLSUserInformation>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IIRolsInformation, CLSRolsInformation>();
builder.Services.AddScoped<IIEmailAlartSetting, CLSTBEmailAlartSetting>();
builder.Services.AddScoped<IICategory, CLSTBCategory>();
builder.Services.AddScoped<IIProduct, CLCTBProduct>();
builder.Services.AddScoped<IICustomerCategorie, CLSTBCustomerCategorie>();
builder.Services.AddScoped<IIInvoseHeder, CLSTBInvoseHeder>();
builder.Services.AddScoped<IIPaymentMethod, CLSTBPaymentMethod>();
builder.Services.AddScoped<IIInvose, CLSTBInvose>();
builder.Services.AddScoped<IICompanyInformation, CLSTBCompanyInformation>();
builder.Services.AddScoped<IIExpenseCategory, CLSTBExpenseCategory>();
builder.Services.AddScoped<IIExpense, CLSTBExpense>();
builder.Services.AddScoped<IISupplier, CLSTBSupplier>();
builder.Services.AddScoped<IIUnit, CLSTBUnit>();
builder.Services.AddScoped<IIClassCard, CLSTBClassCard>();
builder.Services.AddCustomServices();
builder.Services.AddScoped<IIUserInformation, CLSUserInformation>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IIRolsInformation, CLSRolsInformation>();
builder.Services.AddScoped<IIEmailAlartSetting, CLSTBEmailAlartSetting>();
builder.Services.AddScoped<IIHomeSliderContent, CLSTBHomeSliderContent>();
builder.Services.AddScoped<IIPhotoHomeSliderContent, CLSTBPhotoHomeSliderContent>();
builder.Services.AddScoped<IIServiceSectionStartHomeContent, CLSTBServiceSectionStartHomeContent>();
builder.Services.AddScoped<IIAboutSectionStartHomeContent, CLSTBAboutSectionStartHomeContent>();
builder.Services.AddScoped<IICategoryServic, CLSTBCategoryServic>();
builder.Services.AddScoped<IIBrandProduct, CLSTBBrandProduct>();
builder.Services.AddScoped<IIAccountingRestriction, CLSTBAccountingRestriction>();
builder.Services.AddScoped<IIPurchase, CLSTBPurchase>();
builder.Services.AddScoped<IIBestSellingProductsHomeContent, CLSTBBestSellingProductsHomeContent>();
builder.Services.AddScoped<IIUserService, CLSUserService>();
builder.Services.AddScoped<IIHomeBackgroundimage, CLSTBHomeBackgroundimage>();
builder.Services.AddScoped<IIHomeImageProdact, CLSTBHomeImageProdact>();
builder.Services.AddScoped<IIOrderProductsFromSupplier, CLSTBOrderProductsFromSupplier>();
builder.Services.AddScoped<IIMainAccount, CLSTBMainAccount>();
builder.Services.AddScoped<IILevelTwoAccount, CLSTBLevelTwoAccount>();
builder.Services.AddScoped<IIBLevelThreeAccount, CLSTBLevelThreeAccount>();
builder.Services.AddScoped<IILevelForeAccount, CLSTBLevelForeAccount>();
builder.Services.AddScoped<IIStaff, CLSTBStaff>();
builder.Services.AddScoped<IIAboutSectionStartShopContent,CLSTBAboutSectionStartShopContent>();
builder.Services.AddScoped<IIPhotoShopLiftSaide, CLSTBPhotoShopLiftSaide>();
builder.Services.AddScoped<IIPhotoAddProdact, CLSTBPhotoAddProdact>();
builder.Services.AddScoped<IICustomerMessage, CLSTBCustomerMessage>();
builder.Services.AddScoped<IIDeliveryCompanie, CLSTBDeliveryCompanie>();
builder.Services.AddScoped<IICitie, CLSTBCitie>();
builder.Services.AddScoped<IIArea, CLSTBArea>();
builder.Services.AddScoped<IIDeliveryCompanyPricing, CLSTBDeliveryCompanyPricing>();
builder.Services.AddScoped<IITypeOrder, CLSTBTypeOrder>();
builder.Services.AddScoped<IIWarehouse, CLSTBWarehouse>();
builder.Services.AddScoped<IIStock, CLSTBStock>();
builder.Services.AddScoped<IIOrderOnline, CLSTBOrderOnline>();


// تفعيل الترخيص لـ QuestPDF
QuestPDF.Settings.License = LicenseType.Community;

// إضافة الجلسات
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpClient();

var app = builder.Build();

// تكوين pipeline الخاص بالـ HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// استخدام ملفات ثابتة
app.UseStaticFiles();

// استخدام المصادقة والتفويض
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();
app.UseSession();

// Middleware للتحقق من تسجيل الدخول قبل الوصول إلى Swagger
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api-docs") || context.Request.Path.StartsWithSegments("/swagger"))
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Response.Redirect("/Admin/Accounts/Login");
            return;
        }
    }
    await next.Invoke();
});

// تكوين طرق الـ Controller
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Accounts}/{action=Login}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// تفعيل Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Shipping System V1");
    c.RoutePrefix = "api-docs";
});

// تشغيل التطبيق
app.Run();
