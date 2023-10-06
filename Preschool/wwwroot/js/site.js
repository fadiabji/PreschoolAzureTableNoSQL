// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function printBill(subId) {

    const min = 10000; // Minimum 5-digit number (inclusive)
    const max = 99999; // Maximum 5-digit number (inclusive)
    const random5DigitNumber = Math.floor(Math.random() * (max - min + 1)) + min;

    // Get the HTML content of the order summary
    var childName = document.getElementById("childName").innerHTML;

    var info = document.getElementById(subId).innerHTML;

    // Create a new window for printing
    var printWindow = window.open('', '', 'height=600,width=800');
    

    // Create a new Date object for the current date
    var currentDate = new Date();

    // Define an array of month names
    var monthNames = [
        "January", "February", "March", "April",
        "May", "June", "July", "August",
        "September", "October", "November", "December"
    ];

    // Get the current month and year
    var currentMonth = currentDate.getMonth();
    var currentYear = currentDate.getFullYear();
    // Calculate the next month
    var nextMonth = currentMonth + 1;
    var nextYear = currentYear;
    // If the current month is December, adjust for the next year
    if (nextMonth === 12) {
        nextMonth = 0; // January
        nextYear++;
    }
    // Create a Date object for one month from now
    var oneMonthFromNow = new Date(nextYear, nextMonth, currentDate.getDate());

    // Format the current date and one month from now
    var formattedCurrentDate = monthNames[currentMonth] + " " + currentDate.getDate() + ", " + currentYear;
    var formattedOneMonthFromNow = monthNames[nextMonth] + " " + oneMonthFromNow.getDate() + ", " + nextYear;


    var baseUrl = '@Url.Content("~/")';

    var billContent = '<html><head><title>'+childName+'_Bill_Nr:'+ random5DigitNumber +' </title><style>' +
        'body {font-family: Arial, sans-serif;}' +
        '.bill-container {width: 80%; margin: 0 auto;}' +
        '.bill-header {text-align: center; background-color: #007BFF; color: white; padding: 10px;}' +
        '.company-logo {max-width: 150px; max-height: 150px; margin: 0 auto; display: block;}' +
        '.company-info {text-align: center; margin-top: 10px;}' +
        '.invoice-details {margin: 20px; padding: 20px; border: 1px solid #ccc;}' +
        '.invoice-footer {text-align: center; margin-top: 20px;}' +
        '.qr-code {margin-top: 10px; max-width: 150px; max-height: 150px;}' +
        '</style></head><body>' +

        '<div class="invoice-header">' +
        '<img src="/images/logo.png" alt="Company Logo" class="company-logo">' +
        '<h1>The English Kindergarten - TEK</h1>' +
        '<p>Villa No.19 Mansour Bin Talha St 652 - Doha, Qatar</p>' +
        '<p>Phone: 44829505</p>' +
        '<p>Mobile: 55116312</p>' +
        '<p>Email: info@tek.com.qa</p>' +
        '</div>' +

        '<div class="invoice-details">' +
        '<h2>Invoice Details</h2>' +
        '<p>Invoice Number: ' + random5DigitNumber +'</p>' +
        '<p>Invoice Date: ' + formattedCurrentDate + '</p>' +
        '<p>Due Date: ' + formattedOneMonthFromNow + '</p>' +
        '<p>Child Name: ' + childName + ' </p>' +
        info +
        '</div>' +

        '<div class="invoice-footer">' +
        '<p>Thank you for choosing our daycare/preschool services.</p>' +
        '<!-- Add any additional footer information here -->' +
        '<img src="/documentscopies/qr_code.png" alt="QR Code" class="qr-code"/>' +
        '</div></body></html>';
         
    printWindow.document.write(billContent);

    // Print the window
    printWindow.print();

    // Close the window
    printWindow.close();
}




function printPage() {
    // Apply print styles
    var link = document.createElement('link');
    link.rel = 'stylesheet';
    link.type = 'text/css';
    link.href = '/css/print-styles.css'; // Adjust the path to your CSS file
    document.getElementsByTagName('head')[0].appendChild(link);

    // Remove the print button from the printed page
    var printButton = document.querySelector('.btn');
    if (printButton) {
        printButton.style.display = 'none';
    }

    // Remove the print button from the printed page
    var printButton = document.querySelector('.btn');
    if (printButton) {
        printButton.style.display = 'none';
    }

    var allbuttons = document.getElementById("SkippTh");
    if (allbuttons) {
        allbuttons.style.display = 'none';
    }

    // Trigger the print dialog
    window.print();

    // Restore the original styles and show the print button
    link.parentNode.removeChild(link);
    if (printButton && allbuttons) {
        printButton.style.display = 'block';
        allbuttons.style.display = 'block';
    }
}


