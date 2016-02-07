$(document).on('click', '.category', function () {
    
   
    var categoryId = $(this).attr('data-cateogry-id');
    alert(categoryId);
    var url1      = window.location.href
    url = url1+"Product/productByCategory?id="+categoryId;
    console.log(url); $("#product").empty();
    $.get(url, function (data) {
      alert(data);
        $("#product").append(data);


    });
  

});





