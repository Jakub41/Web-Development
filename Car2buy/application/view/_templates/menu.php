<?php 

if (Session::getUserName()!='' && Session::getUserType()=='u' ) {

echo '<div class="menu"> 	
			<div class="top-nav"> 
					<ul>
						<li class="active"><a href="http://46.101.127.220/">Home</a></li>
						<li><a href="#">Account</a></li>
						<li><a href="#">Specials</a></li>
						<li><a href="#">New</a></li>
						<li><a style="width:210px!important;" href="#">Contact</a></li>
					</ul>
					<div class="clear"></div> 
				</div>
		</div>';
}
else if((Session::getUserName()!='') &&(Session::getUserType()==='a') )
{ 
	echo '<div class="menu"> 	
			<div class="top-nav"> 
					<ul>
						<li class="active"><a href="http://46.101.127.220/">Home</a></li>
						<li><a href="'.URL.'product/addNewProduct">Add Product</a></li>
						
					</ul>
					<div class="clear"></div> 
				</div>
		</div>';
}
else {

echo '<div class="menu"> 	
			<div class="top-nav"> 
					<ul>
						<li class="active"><a href="http://46.101.127.220/">Home</a></li>
						<li><a href="#">About</a></li>
						<li><a href="#">Specials</a></li>
						<li><a href="#">New</a></li>
						<li><a style="width:210px!important;" href="#">Contact</a></li>
					</ul>
					<div class="clear"></div> 
				</div>
		</div>';
}	
?>


