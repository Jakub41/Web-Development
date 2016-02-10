<?php 

if (Session::getUserName()!='') {
	
	header('location:'.URL);
}
	else{
require APP . 'view/_templates/header.php';
require APP . 'view/_templates/menu.php';
}
?>



<div class="banner-top">
			<div class="header-bottom">
				 <div class="header_bottom_right_images">
				 	<div class="about_wrapper"><h1>Long-Term Business</h1>
					</div>
		    <div class="section group">
				<div class="col span_2_of_c">
				  <div class="contact-form">
				  	<h3>Create an Account</h3>
					   <form action="<?php echo URL; ?>user/registerUser" method="POST">
                                               <input type='hidden' name='token' value="<?php echo Token::generateToken();?>"/>
				  	<?php if (!isset($_POST['submit']))
				  		{
echo "<script type='text/javascript'>
function validateFirstLastNames(id)
{

	var str = document.getElementById('userFirstName').value;
	var str2 = document.getElementById('userLastName').value;

	var check = /^[a-zA-Z0-9- ]*$/;

	if(check.test(str) == false || check.test(str2) == false) 
	{
		if(id == 1)
	   	{
		alert('Your First Name is invalid.');
		document.getElementById('userFirstName').focus();
		}else 
		{
		alert('Your Last Name is invalid.');
		document.getElementById('userLastName').focus();
		}
		return false;
	}
return true;
}
</script>";

echo "
<script>
function validateUserEmail()
{
	var str = document.getElementById('userEmail').value;
	var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
	
	if(re.test(str) == false) 
	{
	    	alert('Your Email is invalid.');
		document.getElementById('userEmail').focus();
		return false;
	}
return true;
}
</script>
";

echo "
<script>
function validatePassword()
{
	var str = document.getElementById('userPassword').value;

	var re3 = /\([^\)]*\)/;
	if(re3.test(str) == true) 
	{
	    	alert('Your Password contains invalid characters');
		document.getElementById('userPassword').focus();
		return false;
	}
return true;
}
</script>
";

echo "
<script>
function validateConfirmPassword()
{
	var str = document.getElementById('userPassword').value;
	var str2 = document.getElementById('userConfirmPassword').value;

	var re3 = /\([^\)]*\)/;
	if(str != str2) 
	{
	    	alert('Your Passwords doesn't match');
		document.getElementById('userConfirmPassword').focus();
		return false;
	}
return true;
}
</script>
";
					    	echo '<div>

						    	<span><label>First Name</label></span>
						    	<span><input name="userFirstName" id="userFirstName" onblur="validateFirstLastNames(1)" type="text" value="" class="textbox"></span>
						    </div>
						    <div>
						    	<span><label>Last Name</label></span>
						    	<span><input name="userLastName" id="userLastName" onblur="validateFirstLastNames(2)" type="text" value="" class="textbox"></span>
						    </div>
						    <div>
						    	<span><label>E-MAIL</label></span>
						    	<span><input name="userEmail" id="userEmail" onblur="validateUserEmail()" type="text" value="" class="textbox"></span>
						    </div>
						    <div>
						     	<span><label>Password</label></span>
						    	<span><input name="userPassword" id="userPassword" type="password" onblur="validatePassword()" value="" class="textbox"></span>
						    </div>
						     <div>
						     	<span><label>Confirm Password</label></span>
						    	<span><input name="userConfirmPassword" id="userConfirmPassword" onblur="validateConfirmPassword()" type="password" value="" class="textbox"></span>
						    </div>
						    
						   <div>
						   		<span><input type="submit" name="submit" value="Submit" /></span>
						  </div>
						  ';
					}
					else{ echo "<div>".$validator->getData('error_login')."</div>
								<div>
						    	<span><label>First Name</label></span>
						    	<span><input id='userFirstName' onblur='validateFirstLastNames();' name='userFirstName' type='text' class='textbox' 
						    	value =".$validator->getData('input_userFirstName')."></span>
						    </div>
						     <div class='error'> ".$validator->getData('error_userFirstName')." </div>
						    <div>
						    	<span><label>Last Name</label></span>
						    	<span><input id='userLastName' onblur='validateFirstLastNames();' name='userLastName' type='text' class='textbox' 
						    	value=".$validator->getData('input_userLastName')." ></span>
						    </div>
						     <div class='error'> ".$validator->getData('error_userLastName')." </div>
						    <div>
						    	<span><label>E-MAIL</label></span>
						    	<span><input id='userEmail' onblur='validateUserEmail();' name='userEmail' type='text' class='textbox' 
						    	value =".$validator->getData('input_userEmail')."></span>
						    </div>
						     <div class='error'> ".$validator->getData('error_userEmail')." </div>
						    <div>
						     	<span><label>Password</label></span>
						    	<span><input id='userPassword' onblur='validatePassword()' name='userPassword' type='password' class='textbox' 
						    	value =".$validator->getData('input_userPassword')." ></span>
						    </div>
						    <div class='error'> ".$validator->getData('error_userPassword')." </div>
						     <div>
						     	<span><label>Confirm Password</label></span>
						    	<span><input id='userConfirmPassword' onblur='validateConfirmPassword()' name='userConfirmPassword' type='password' class='textbox' 
						    	value =".$validator->getData('input_userConfirmPassword')." ></span>
						    </div>
						    <div class='error'> ".$validator->getData('error_userConfirmPassword')." </div>
						    
						   <div>
						   		<span><input type='submit' name='submit' value='Submit' /></span>
						  </div>
						  ";
					}
				  	?>
					    </form>
				  </div>
  				</div><div class="clear"></div>
			</div>
		</div>
	

<?php 
require APP .  'view/_templates/sidebar.php';
?>
			<?php 
require APP . 'view/_templates/footer.php';
?>