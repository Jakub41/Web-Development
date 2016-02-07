
<!DOCTYPE HTML>
<html>
<head>
<title>CAR2BUY</title>
<link href="<?php echo URL; ?>css/style.css" rel="stylesheet" type="text/css" media="all">

<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href='http://fonts.googleapis.com/css?family=Playball' rel='stylesheet' type='text/css'>   
<!--slider-->
<link rel="stylesheet" type="text/css" href="http://fonts.googleapis.com/css?family=Bitter">
 
  <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.2/themes/smoothness/jquery-ui.css" />
 <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">
 <!--
<script src="<?php echo URL; ?>js/jquery.min.js"></script>
<script src="<?php echo URL; ?>js/script.js" type="text/javascript"></script>

<script src="<?php echo URL; ?>js/carScript.js"></script>
<script src="<?php echo URL; ?>js/jquery.min.js"></script>
  -->
   <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.2/jquery-ui.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.13.1/jquery.validate.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
    <script src="<?php echo URL; ?>public/js/carScript.js"></script>






</head>
<body>
<div class="header-bg">
	<div class="wrap"> 
		<div class="h-bg">
			<div class="total">
				<div class="header">
					<?php 
						//session_start();
					//$session = new Session();
					//Session::sessionStart();
					if (Session::getUserName()!='' ) {
					//if(isset($_SESSION['username'])){
					//echo $session->getUserName();
						//echo $_SESSION['username'];
						//echo $_SESSION['usertype'];
					echo '<div class="header-right">
					<div class="follow_icon">
						<ul class="user_menu">
						<li class="">
						<div class="button-t"><span>Welcome '.Session::getUserName().'</span></div></li>
						<li class="last"><a href="'.URL.'user/logoutUser"><div class="button-t"><span>Log out</span></div></a></li></ul>
					</div>';
					}
					else{
						echo '<div class="header-right">
					<div class="follow_icon">
						<ul class="user_menu">
						<li class=""><a href="'.URL.'user/registerUser"><div class="button-t"><span>Create an Account</span></div></a></li>
						<li class="last"><a href="'.URL.'user/login"><div class="button-t"><span>Log in</span></div></a></li></ul>
					</div>
						
					</div>';
					}
					?>
					
					<div class="header-bot">
						<div class="logo">
							<a href="<?php echo URL; ?>"><img src="<?php echo URL;?>images/logo.png" alt=""/></a>
						</div>
						<div class="search">
						    <input type="text" class="textbox" value="" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = '';}">
						    <button class="gray-button"><span>Search</span></button>
				       </div>
					<div class="clear"></div> 
				</div>		
		</div>	
		
				  <!---  <?php 
include_once 'content.php';
?>		
		   <?php 
include_once 'sidebar.php';
?> !-->