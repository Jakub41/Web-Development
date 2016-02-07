		<?php
		require APP . 'view/_templates/header.php';
         require APP . 'view/_templates/menu.php';
        // require APP . 'view/_templates/sidebar.php';
         ?>


		<div class="banner-top">
			<div class="header-bottom">
				 <div class="">
				     	
		  			<div class="content-wrapper">	
		  			
						<div class="content-top">
							  
							 <div class="text"> 	
								<div class="">
				  	<?php 

				  	echo ' <div class="">
				  		 	<div class="grid images_3_of_1">
							<img src="'.URL.'images/'.$Products[0]->imagePath.'" alt=""/>
						</div>
				  <div class="grid span_2_of_3">
							<h3>Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh</h3>
							<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.</p>
							<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure.</p>
						</div>
								<div class="clear"></div>
							</div>	
				  		 </div>
				  		 
				  		 ';
				  		
				  	foreach ($Comments as $comment) {
		  			
		  				
				  		echo ' <div class="">
				  		 	
								<div class="clear"></div>
							</div>	
				  		 </div>
				  		 <div><h3>'.$comment->name.'</h3><br><p>'.$comment->comment.'</p></div>
				  		 ';
				  		
				  		 }
				  		 ?>
				  	</div>

						
						<div class="leave-comment"><a href="#" name="comment">Leave a Comment</a></div>
						<div class="comments-area">
			<form action="<?php echo URL;?>comment/addCommment" method="POST">
                            <input type='hidden' name='token' value="<?php echo Token::generateToken();?>"/>
			<input type="hidden" name="productId" value ="<?php echo $product->productId?>">
			<?php if (!isset($_POST['submit']))
				  		{
				  			echo'
				<p>
					<label>Name</label>
					<span>*</span>
					<input type="text" value="" name="name">
				</p>
				
				<p>
					<label>Email</label>
					<span>*</span>
					<input type="text" value="" name="email">
				</p>
				
				<p>
					<label>Subject</label>
					<span>*</span>
					<textarea name="comments"></textarea>
				</p>
				<p>
					<input type="submit" name="submit" value="Submit" />
				</p>';}
				else{
					echo "
						<p>
					<label>Name</label>
					<span>*</span>
					<input name='name' type='text' class='textbox'  value=".$validator->getData('input_name')." >
					<div class='error'> ".$validator->getData('error_name')." </div>
				</p>
				
				<p>
					<label>Email</label>
					<span>*</span>
					<input name='email' type='text' class='textbox'  value=".$validator->getData('input_email').">
					<div class='error'> ".$validator->getData('error_email')." </div>
				</p>
				
				<p>
					<label>Subject</label>
					<span>*</span>
					<textarea name='comments'></textarea>
					<div class='error'> ".$validator->getData('error_comments')." </div>
				</p>
				<p>
					<input type='submit' name='submit' value='Submit' />
				</p>";
				}
				?>

			</form>
		</div>
	</div>		
								</div>
						</div>
						<div class="content-top">
							  
							 
						</div>
				</div>
		</div>
<?php
          
          
           require APP . 'view/_templates/footer.php';
           ?>