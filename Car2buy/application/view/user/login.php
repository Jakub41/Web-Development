<script>

function validateUserName()
{
	var str = document.getElementById('userName').value;
	var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
	var re2 = /^[a-zA-Z0-9- ]*$/;



	if(re.test(str) == false && re2.test(str) == false) 
	{
	    	alert('Your Username is invalid.');
		document.getElementById("userName").focus();
		return false;
	}
return true;
}
function validatePass()
{
	var str = document.getElementById('password').value;
	var re3 = /\([^\)]*\)/;
	if(re3.test(str) == true) 
	{
	    	alert('Your Password contains invalid characters');
		document.getElementById('password').focus();
		return false;
	}
return true;
}
</script>

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
				  	<h3>Login</h3>
				  		<form onsubmit="if(!validateUserName() && !validatePass())return false;" action="<?php echo URL; ?>user/login" method="POST">
                                                     <input type='hidden' name='token' value="<?php echo Token::generateToken();?>"/>
				  	<?php if (!isset($_POST['submit']))
				  		{

				  		echo '<div>
				  		<span><label>User Name</label></span><span>
				  		<input name="userName" id="userName" type="text" class="textbox" onblur="validateUserName();" /></span>

						    </div>
						    <div>
						    	<span><label>Password</label></span>
						    	<span><input name="password" id="password" type="password" class="textbox" onblur="validatePass();" /></span>
						    </div>
						    
						   <div>
						   		<span><input type="submit" name="submit" value="Submit" /></span>
						  </div>
					    ';
					}
					else{ echo "<div>".$validator->getData('login_fail')."</div>
						<div>
				  		<span><label>User Name</label></span><span>
				  		<input name='userName' type='text' class='textbox' 
				  		value=".$validator->getData('input_user')."></span>

						    </div>
						    <div class='error'> ".$validator->getData('error_user')." </div>
						    <div>
						    	<span><label>Password</label></span>
						    	<span><input name='password' type='password' class='textbox' 
						    	value=".$validator->getData('input_pass').">
						    	</span>
						    </div>
						     <div class='error'> ".$validator->getData('error_pass')." </div>
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