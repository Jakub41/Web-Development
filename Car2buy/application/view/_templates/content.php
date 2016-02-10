<link rel="stylesheet" type="text/css" href="http://fonts.googleapis.com/css?family=Bitter">
 
  <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.2/themes/smoothness/jquery-ui.css" />
 <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">

<div class="banner-top">
			<div class="header-bottom">
				 <div class="header_bottom_right_images">
				     	<div id="slideshow">
							<ul class="slides">
						    	<li><a href="details.html"><canvas ></canvas><img src="<?php echo URL;?>images/banner4.jpg" alt="Marsa Alam underawter close up" ></a></li>
						        <li><a href="details.html"><canvas></canvas><img src="<?php echo URL;?>images/banner2.jpg" alt="Turrimetta Beach - Dawn" ></a></li>
						        <li><a href="details.html"><canvas></canvas><img src="<?php echo URL;?>images/banner3.jpg" alt="Power Station" ></a></li>
						        <li><a href="details.html"><canvas></canvas><img src="<?php echo URL;?>images/banner1.jpg" alt="Colors of Nature" ></a></li>
						    </ul>
						    <span class="arrow previous"></span>
						    <span class="arrow next"></span>
				  	</div>
		  			<div class="content-wrapper">	
		  			
						<div class="content-top">
							  	<div class="box_wrapper"><h1>New Products For July</h1>
								</div>
							 <div class="text"> 	
								<div class="row">
				  	<?php $counter=0;
				  	foreach ($Products as $product) {
		  			
		  				 if ($counter != 0 && $counter % 3 == 0)
            			{
            				echo '</div> <div class="row">';
            			}
				  		echo ' <div class="">
				  		 	
				  	<div class="grid_1_of_3 images_1_of_3">
									<div class="grid_1">
										<a href="single.html"><img src="'.URL.'images/'.$product->imagePath.'"  alt="car"></a>
											<div class="grid_desc">
												<p class="title">Lorem ipsum dolor sitconsectetuer adipiscing elit</p>
												<p class="title1">Lorem ipsum dolor sitconsectetuer adipiscing elit</p>
													 <div class="price" >
													 	 <span class="reducedfrom">$6600</span>
								        				<span class="actual">$6000</span>
													</div>
													<div class="cart-button">
														<div class="cart">
															<a href="#"><img src="'.URL.'images/cart.png" alt=""/></a>
														</div>
														<a href="'.URL.'product/productDetails?id='.$product->productId.'" class="button">Details</a>
														
													<div class="clear"></div>
												</div>
											</div>
								</div>
								<div class="clear"></div>
							</div>	
				  		 </div>';
				  		 $counter++;
				  		 }
				  		 ?>
				  	</div>

									
								</div>
						</div>
						<div class="content-top">
							  
							 
						</div>
				</div>
		</div>
