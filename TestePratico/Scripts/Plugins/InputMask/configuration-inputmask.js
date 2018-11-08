$(document).ready(function () {
    $('.cnpj-mask').inputmask({ "mask": "99.999.999/9999-99", 'autoUnmask': false, 'removeMaskOnSubmit': false });
    $('.phone-mask').inputmask({ "mask": "(99) 9999-9999"});
    $('.celphone-mask').inputmask({ "mask": "(99) 99999-9999" });
    $('.agencia-mask').inputmask({ "mask": "999-9", 'autoUnmask': false, 'removeMaskOnSubmit': false });
    $('.conta-mask').inputmask({ "mask": "99.999-9", 'autoUnmask': false, 'removeMaskOnSubmit': false });
});

$("form").submit(function () {
    $(".percentage-mask").inputmask("remove");    
    $(".ccnumber-mask").inputmask("remove");
});

jQuery.validator.methods.date = function (value, element) {
    var isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor);
    if (isChrome) {
        var d = new Date();
        return this.optional(element) || !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));
    } else {
        return this.optional(element) || !/Invalid|NaN/.test(new Date(value));
    }
};
$.validator.methods.range = function (value, element, param) { var globalizedValue = value.replace(".", ""); globalizedValue = globalizedValue.replace(",", "."); return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]); }; $.validator.methods.number = function (value, element) { return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/ .test(value); };

