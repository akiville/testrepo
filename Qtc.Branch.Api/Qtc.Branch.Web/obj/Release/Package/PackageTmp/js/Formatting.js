function numberWithCommas(x) {
    return Number(x).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function getDecimalValue(x) {
    return parseFloat($(this).find(".mAmount").text().replace(',', ''));
}