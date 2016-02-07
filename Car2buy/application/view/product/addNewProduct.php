		<?php
		require APP . 'view/_templates/header.php';
         require APP . 'view/_templates/menu.php';
     
         ?>
<link href='http://fonts.googleapis.com/css?family=Roboto+Condensed|Open+Sans+Condensed:300' rel='stylesheet' type='text/css'>
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>

	<div class="banner-top">
			<div class="header-bottom">
				 <div class="">
				 	
		    <div class="section group">
				<div class="col span_2_of_c">
				  <div class="contact-form">
				  	<div id="image_preview"><img id="previewing"  /></div>
				  	<form enctype="multipart/form-data" action="<?php echo URL; ?>product/addNewProduct"method="POST" >
<input type="hidden" name="MAX_FILE_SIZE" value="100000" />
				  	<?php
				  		if(isset($_POST["submit"])) {
				  	echo "<div>".$validator->getData('file_error')."</div>"	;

				  	echo "<div>
				  		 	
                   <select class='form-control' name='categoryId'>  
                   <option value=0>Select Model</option>";
				  				foreach ($categories as $category) {
				  				echo"	<option value=".$category->categoryId.">".$category->categoryName."</option>'";
				  			}
				  		echo"	</select>
				  		</div>
				  		<div class='error'> ".$validator->getData('error_categoryId')." </div>
				  		<div>
				  		<span><label>Product Name</label></span><span>
				  		<input name='productName' id='productName' type='text' class='form-control' value ='".$validator->getData('productName')."' /></span>

						   
						    </div>
						     <div class='error'> ".$validator->getData('error_productName')." </div>
						    <div>
				  		<span><label>Description</label></span><span>
				  		<input name='productDescription' id='productDescription' value ='".$validator->getData('productDescription')."' type='text' class='textbox' /></span>
				  			
						    </div>
						      <div class='error'> ".$validator->getData('error_productDescription')." </div>
						    <div>
						    <div>
						   
    <input type='file' name='fileToUpload' id='fileToUpload' >
				  	 
    <input type='submit' value='Save' name='submit' >
					</div>
					";
				  		}
				  	else{
				  		echo "<div>
				  		 	
                   <select class='form-control' name ='categoryId'>  
                   <option value=''>Select Model</option>";
				  				foreach ($categories as $category) {
				  				echo"	<option value=".$category->categoryId.">".$category->categoryName."</option>'";
				  			}
				  		echo"	</select>
				  		</div>
				  		<div>
				  		<span><label>Product Name</label></span><span>
				  		<input name='productName' id='productName' type='text' class='form-control' /></span>

						    </div>
						    <div>
				  		<span><label>Description</label></span><span>
				  		<input name='productDescription' id='productDescription' type='text' class='textbox'/></span>
				  			
						    </div>
						    <div>
						   
    <input type='file' name='fileToUpload' id='fileToUpload' >
				  	 
    <input type='submit' value='Save' name='submit' >
					</div>";
				}
				  			?>

				  			
				  		
				  	
				  		
					    </form>
				  </div>
  				</div><div class="clear"></div>
			</div>
		</div>
<?php
   require APP . 'view/_templates/footer.php';
?>

<script type="text/javascript">
	$(document).ready(function (e) {


// Function to preview image after validation
$(function() {
$("#fileToUpload").change(function() {

var file = this.files[0];
var imagefile = file.type;
var match= ["image/jpeg","image/png","image/jpg","image/gif"];
if(!((imagefile==match[0]) || (imagefile==match[1]) || (imagefile==match[2])
	|| (imagefile==match[3])))
{
alert("invalid image");
return false;
}
else
{
var reader = new FileReader();
reader.onload = imageIsLoaded;
reader.readAsDataURL(this.files[0]);
}
});
});
function imageIsLoaded(e) {
$("#file").css("color","green");
$('#image_preview').css("display", "block");
$('#previewing').attr('src', e.target.result);
$('#previewing').attr('width', '250px');
$('#previewing').attr('height', '230px');
};
});
</script>
