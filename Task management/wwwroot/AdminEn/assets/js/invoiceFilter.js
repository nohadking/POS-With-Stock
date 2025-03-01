
$('#CasherName').on('change', function () {
    console.log("CasherName change hrer");
    const cacherName = $(this).find('option:selected').text();
    const originalUrl = window.origin;

    fetch(`${originalUrl}/api/Invoice/GetByCasherName/${cacherName}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('حدث خطأ في جلب البيانات');
            }
            return response.json();
        })
        .then(data => {

            console.log("data: " + JSON.stringify(data, null, 2));

            if ($.fn.DataTable.isDataTable('#TblForResultTBL')) {
                $('#TblForResultTBL').DataTable().clear().destroy();
            }

            const tbody = $('#TblForResult');
            tbody.empty();
            let totalPrice = 0;
            $('#totalPri').val(0);

            data.forEach(item => {
                const row = `
                        <tr>
                            <td>
                                <label class="checkboxs">
                                    <input type="checkbox">
                                    <span class="checkmarks"></span>
                                </label>
                            </td>
                            <td>${item.invoiceNumber}</td>
                            <td>${item.dateInvos}</td>
                            <td>${item.paymentMethodAr}</td>
                            <td>${item.name}</td>
                            <td>${item.productNameAr}</td>
                            <td>${item.quantity}</td>
                            <td>$${item.price}</td>
                            <td>$${item.total}</td>
                            <td>${item.dataEntry}</td>
                            <td>
                                <span class="badge ${item.OutstandingBill ? 'badge-linesuccess' : 'badge-linedanger'}">
                                    ${item.OutstandingBill ? 'Active' : 'DeActive'}
                                </span>
                            </td>
                        </tr>`;
                tbody.append(row);
                totalPrice += item.total;
            });

            $('#totalPri').val(totalPrice.toFixed(2).toString() + ' $').trigger('change');

            $('#TblForResultTBL').DataTable({
                paging: true,      
                searching: false,   
                ordering: false,  
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.11.5/i18n/ar.json'
                }
            });

        })
        .catch(error => {
            console.error(`خطأ: ${error.message}`);
        });

});


$('#PayMeth').on('change', function () {

    console.log("PayMeth changed here");

    const payMeth = $(this).find('option:selected').text();
    const cacherName = $("#CasherName").find('option:selected').text();
    const originalUrl = window.origin;

    fetch(`${originalUrl}/api/Invoice/GetByCasherNameAndPayMethod/${cacherName}/${payMeth}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('حدث خطأ في جلب البيانات');
            }
            return response.json();
        })
        .then(data => {
            console.log("data: " + JSON.stringify(data, null, 2));

            if ($.fn.DataTable.isDataTable('#TblForResultTBL')) {
                $('#TblForResultTBL').DataTable().clear().destroy();
            }

            const tbody = $('#TblForResult');
            tbody.empty();
            let totalPrice = 0;
            $('#totalPri').val(0);

 
            data.forEach(item => {
                const row = `
                    <tr>
                        <td>
                            <label class="checkboxs">
                                <input type="checkbox">
                                <span class="checkmarks"></span>
                            </label>
                        </td>
                        <td>${item.invoiceNumber}</td>
                        <td>${item.dateInvos}</td>
                        <td>${item.paymentMethodAr}</td>
                        <td>${item.name}</td>
                        <td>${item.productNameAr}</td>
                        <td>${item.quantity}</td>
                        <td>$${item.price}</td>
                        <td>$${item.total}</td>
                        <td>${item.dataEntry}</td>
                        <td>
                            <span class="badge ${item.OutstandingBill ? 'badge-linesuccess' : 'badge-linedanger'}">
                                ${item.OutstandingBill ? 'Active' : 'DeActive'}
                            </span>
                        </td>
                    </tr>`;
                tbody.append(row);
                totalPrice += item.total;
            });

            $('#totalPri').val(totalPrice.toFixed(2).toString() + ' $').trigger('change');

            $('#TblForResultTBL').DataTable({
                paging: true,  
                searching: false, 
                ordering: false,  
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.11.5/i18n/ar.json'
                }
            });
        })
        .catch(error => {
            console.error(`خطأ: ${error.message}`);
        });
});





function updateTotalPrice() {
    let totalPrice = 0;

    $('#TblForResult tr').each(function () {
        let totalCell = $(this).find('td:nth-child(9)');
        if (totalCell.length > 0) {
            let totalValue = parseFloat(totalCell.text().replace('$', '').trim()) || 0;
            totalPrice += totalValue;
        }
    });

    $('#totalPri').val(totalPrice.toFixed(2).toString() + ' $').trigger('change');
}

$(document).ready(function () {
    updateTotalPrice();
});







