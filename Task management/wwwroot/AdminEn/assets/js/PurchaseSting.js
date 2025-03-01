$(document).ready(function () {
    $('#supplierSelect').change(function () {
        var supplierId = $(this).val();

        if (supplierId) {
            $.ajax({
                url: '/Admin/Purchase/GetSupplierImage',
                type: 'GET',
                data: { id: supplierId },
                success: function (response) {
                    if (response && response.imageUrl) {
                        $('#supplierImage').attr('src', response.imageUrl).show();
                    } else {
                        $('#supplierImage').hide();
                    }
                },
                error: function () {
                    alert('Error fetching supplier image.');
                    $('#supplierImage').hide();
                }
            });
        } else {
            $('#supplierImage').hide();
        }
    });

    $('#supplierSelectEdit').change(function () {
        var supplierId = $(this).val();

        if (supplierId) {
            $.ajax({
                url: '/Admin/Purchase/GetSupplierImage',
                type: 'GET',
                data: { id: supplierId },
                success: function (response) {
                    if (response && response.imageUrl) {
                        $('#supplierImage').attr('src', response.imageUrl).show();
                    } else {
                        $('#supplierImageEdit').hide();
                    }
                },
                error: function () {
                    alert('Error fetching supplier image.');
                    $('#supplierImageEdit').hide();
                }
            });
        } else {
            $('#supplierImageEdit').hide();
        }
    });

});

$(document).ready(function () {
    $('#productSelect').change(function () {
        var productId = $(this).val();

        if (productId) {
            $.ajax({
                url: '/Admin/Purchase/GetLastPurchasePrice',
                type: 'GET',
                data: { id: productId },
                success: function (response) {
                    if (response && response.lastPrice !== undefined) {
                        $('#lastPurchasePrice').val(response.lastPrice);
                    } else {
                        $('#lastPurchasePrice').val(0);
                    }
                },
                error: function () {
                    alert('حدث خطأ أثناء جلب آخر سعر شراء.');
                    $('#lastPurchasePrice').val(0);
                }
            });
        } else {
            $('#lastPurchasePrice').val(0);
        }
    });

    $('#productSelectEdit').change(function () {
        var productId = $(this).val();

        if (productId) {
            $.ajax({
                url: '/Admin/Purchase/GetLastPurchasePrice',
                type: 'GET',
                data: { id: productId },
                success: function (response) {
                    if (response && response.lastPrice !== undefined) {
                        $('#lastPurchasePriceEdit').val(response.lastPrice);
                    } else {
                        $('#lastPurchasePrice').val(0);
                    }
                },
                error: function () {
                    alert('حدث خطأ أثناء جلب آخر سعر شراء.');
                    $('#lastPurchasePrice').val(0);
                }
            });
        } else {
            $('#lastPurchasePrice').val(0);
        }
    });
});

