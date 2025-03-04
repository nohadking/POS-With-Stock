



    function showProducts(categoryId) {
            // إخفاء جميع منتجات الفئات
            var allProducts = document.querySelectorAll('.tab_content');
    allProducts.forEach(function (content) {
        content.style.display = 'none';
            });

    // إظهار منتجات الفئة المحددة
    var selectedCategory = document.getElementById('products-' + categoryId);
    selectedCategory.style.display = 'block';

    // تغيير الفئة النشطة
    var allCategories = document.querySelectorAll('.category-item');
    allCategories.forEach(function (category) {
        category.classList.remove('active');
            });

    var selectedCategoryItem = document.getElementById('category-' + categoryId);
    selectedCategoryItem.classList.add('active');
        }





        // تحديد كل المنتجات القابلة للإضافة
    document.querySelectorAll(".product-info.default-cover").forEach(function (productCard) {
        productCard.addEventListener("click", function () {
            // استخراج بيانات المنتج
            const idForProduct = productCard.querySelector(".product-IIdd").textContent.trim();
            const imgSrc = productCard.querySelector("img").getAttribute("src");
            const productName = productCard.querySelector(".product-name a").textContent.trim();
            const price = parseFloat(productCard.querySelector(".price p").textContent.trim().replace(/[^0-9.]/g, '')); // استخراج السعر كرقم

            // التحقق إذا كان المنتج موجودًا بالفعل
            const productWrap = document.querySelector(".product-added .product-wrap");
            const existingProduct = productWrap.querySelector(`.product-list[data-product-name="${productName}"]`);
            if (existingProduct) {
                // المنتج موجود - تحديث الكمية والسعر
                const qtyInput = existingProduct.querySelector('input[name="qty"]');
                const currentQty = parseInt(qtyInput.value);
                const newQty = currentQty + 1;
                qtyInput.value = newQty;
                // تحديث السعر (السعر الكلي = الكمية × السعر الفردي)
                const priceElement = existingProduct.querySelector('.info p');
                const totalPrice = newQty * price;
                priceElement.textContent = `$${totalPrice.toFixed(2)}`; // تنسيق السعر

            } else {
                // المنتج غير موجود - إضافته إلى القائمة
                const newProductHTML = `
                            <div class="product-list d-flex align-items-center justify-content-between" data-product-name="${productName}">
                                <div class="d-flex align-items-center product-info">
                                    <a href="javascript:void(0);" class="img-bg">
                                        <img src="${imgSrc}" alt="Products">
                                    </a>
                                            <p hidden class="iiddPro">${idForProduct}</p>
                                    <div class="info">
                                        <h6><a href="javascript:void(0);">${productName}</a></h6>
                                        <p>$${price.toFixed(2)}</p>
                                        <h5>$${price.toFixed(2)}</h5>
                                    </div>
                                </div>
                                <div class="qty-item text-center">
                                    <a href="javascript:void(0);" class="dec d-flex justify-content-center align-items-center" data-bs-toggle="tooltip" title="minus" onclick="changeQuantity('${productName}', -1)">
                                        <i data-feather="minus-circle" class="feather-14"></i>
                                    </a>
                                            <input type="text" class="form-control text-center qutOfPro" name="qty" value="1">
                                    <a href="javascript:void(0);" class="inc d-flex justify-content-center align-items-center" data-bs-toggle="tooltip" title="plus" onclick="changeQuantity('${productName}', 1)">
                                        <i data-feather="plus-circle" class="feather-14"></i>
                                    </a>
                                </div>
                                <div class="d-flex align-items-center action">
                                    <a class="btn-icon delete-icon confirm-text" href="javascript:void(0);" onclick="removeProduct('${productName}')">
                                        <i data-feather="trash-2" class="feather-14"></i>
                                    </a>
                                </div>
                            </div>`;

                // إضافة المنتج
                productWrap.insertAdjacentHTML("beforeend", newProductHTML);
            }
            // تحديث العدد (count)
            const countElement = document.querySelector(".product-added .count");
            const newCount = productWrap.querySelectorAll(".product-list").length;
            countElement.textContent = newCount;
            // إعادة تفعيل أيقونات Feather (لإعادة تفعيل الأيقونات)
            feather.replace();
            // تحديث الإجماليات
            updateOrderTotal();
        });
        });
// تحديث الكمية في السلة




    function changeQuantity(productName, change) {
            const productWrap = document.querySelector(".product-added .product-wrap");
    const product = productWrap.querySelector(`.product-list[data-product-name="${productName}"]`);
    const qtyInput = product.querySelector('input[name="qty"]');
    const currentQty = parseInt(qtyInput.value);
    const newQty = currentQty + change;

    const productElement = Array.from(document.querySelectorAll('.product-name a'))
                .find(el => el.textContent.trim() === productName);
    const priceElement = productElement.closest('.product-name').nextElementSibling.querySelector('p');
    const nativePrice = parseFloat(priceElement.textContent);



            if (newQty >= 1) {
        qtyInput.value = newQty;
    // تحديث السعر الكلي
    const price = parseFloat(product.querySelector('.info p').textContent.replace(/[^0-9.]/g, ''));
    const totalPrice = newQty * nativePrice;
    product.querySelector('.info p').textContent = `$${totalPrice.toFixed(2)}`;
    // تحديث الإجماليات
    updateOrderTotal();
            }
        }
// حذف نتج من السلة

    function removeProduct(productName) {
            const productWrap = document.querySelector(".product-added .product-wrap");
    const product = productWrap.querySelector(`.product-list[data-product-name="${productName}"]`);
    if (product) {
        product.remove();
    // تحديث العدد (count)
    const countElement = document.querySelector(".product-added .count");
    const newCount = productWrap.querySelectorAll(".product-list").length;
    countElement.textContent = newCount;

    // تحديث الإجماليات
    updateOrderTotal();
            }
}


    // حساب الإجماليات
    function updateOrderTotal() {
            const productWrap = document.querySelector(".product-added .product-wrap");
    const products = productWrap.querySelectorAll(".product-list");
        let subTotal = 0;
        let qtty = 0;
            // حساب Sub Total
            products.forEach(product => {
                const priceText = product.querySelector('.info h5').textContent.replace(/[^0-9.]/g, '');
                const qty = product.querySelector(".qutOfPro").value;
                const price = parseFloat(priceText) * parseInt(qty) || 0;
                subTotal += price;
                qtty += parseInt(qty)
            });


    // الحسابات الأخرى
    const taxRate = 0.00; // 5% ضريبة
    const discountRate = 0.0; // 10% خصم
    const shippingCost = 0.0; // تكلفة الشحن الثابتة

    const tax = subTotal * taxRate;
    const discount = subTotal * discountRate;
    const total = subTotal + tax + shippingCost - discount;
    // تحديث القيم في الجدول
        document.getElementById("Quantity-total").textContent = `${qtty}`;
        document.getElementById("totalOnceforPrintNum1").textContent = `$${subTotal.toFixed(2)}`;
        document.getElementById("TotalPriceAgane12").textContent = `$${subTotal.toFixed(2)}`;
    ////document.getElementById("tax").textContent = `$${tax.toFixed(2)}`;
    ////document.getElementById("shipping").textContent = `$${shippingCost.toFixed(2)}`;
    ////document.getElementById("discount").textContent = `-$${discount.toFixed(2)}`;
        }
        // حذف جميع المنتجات
        function clearAllProducts() {
            const productWrap = document.querySelector(".product-added .product-wrap");
            productWrap.innerHTML = '';
            updateOrderTotal();
            document.querySelector(".product-added .count").textContent = '0';
        }

        function selectMethod(item){
            console.log(item)
            document.querySelector('#PayMeth').textContent = '';
            document.querySelector('#idPayMeth1').textContent = '';
            document.querySelector('#PayMeth').textContent = item['paymentMethodAr'];
            document.querySelector('#idPayMeth1').textContent = item['idPaymentMethod'];
        }

   






        //كود الطباعة 
$('#printButton').on('click', function () {
    document.getElementById('prgraphhide1').style.display = 'none';
    document.getElementById('prgraphhide2').style.display = 'none';
    document.getElementById('prgraphhide3').style.display = 'none';
    document.getElementById('prgraphhide4').style.display = 'none';
    document.getElementById('printButton').style.display = 'none';
    document.getElementById('xhid').style.display = 'none';

    // جلب محتوى الطباعة من العنصر
    var printContent = document.getElementById('print-receipt').innerHTML;

    // إضافة تنسيق CSS خاص للطباعة الحرارية بعرض 8 سم
    var printStyle = `
    <style>
        @page {
            margin: 0; /* إزالة الهوامش */
                                }
        body {
            font - family: Arial, sans-serif;
        font-size: 10px;
        margin: 0;
        padding: 0;
        width: 8cm;
                                }
        .text-center {
            text - align: center;
                                }
        .table-borderless {
            width: 100%;
        border-collapse: collapse;
                                }
        .table-borderless th,
        .table-borderless td {
            text - align: left;
        padding: 4px 0;
                                }
        .table-borderless th {
            border - bottom: 1px solid #000;
                                }
        .invoice-bar {
            margin - top: 20px;
        text-align: center;
                                }
    </style>
    `;

    // دمج تنسيق الطباعة مع المحتوى
    var fullPrintContent = `
    ${printStyle}
    <div>${printContent}</div>
    `;

    // حفظ المحتوى الأصلي للصفحة
    var originalContent = document.body.innerHTML;

    // استبدال محتوى الصفحة بالمحتوى المراد طباعته
    document.body.innerHTML = fullPrintContent;

    // استدعاء نافذة الطباعة
    window.print();

    // إعادة الصفحة الأصلية
    document.body.innerHTML = originalContent;
    window.location.reload(); // إعادة تحميل الصفحة إذا لزم الأمر
});
