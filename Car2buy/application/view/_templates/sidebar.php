



<div class="header-para">
				<div class="categories">
						<div class="list-categories">
						<?php

foreach ($categories as $category) {
	//echo $category->categoryId;
	
	echo '<div class="first-list">
								<div class="div_2">
								
								
								<a class="category" href="'.URL.'product/productByCategory?id='.$category->categoryId.'">'.$category->categoryName.'</a></div>
								<div class="clear"></div>
							</div>';
	# code...
}
?>
							
				</div>
				<div class="box"> 
							<div class="box-heading"><h1><a href="#">Cart:&nbsp;</a></h1></div>
							 <div class="box-content">
							Now in your cart&nbsp;<strong> 0 items</strong>
							</div>	
				</div>
				<div class="box-title">
					<h1><span class="title-icon"></span><a href="">Branches</a></h1>
				</div>
				<div class="section group example">
					<div class="col_1_of_2 span_1_of_2">
					  <img src="<?php echo URL; ?>images/pic21.jpg" alt=""/>
					   <img src="<?php echo URL; ?>images/pic24.jpg" alt=""/>
					   <img src="<?php echo URL; ?>images/pic25.jpg" alt=""/>
					   <img src="<?php echo URL; ?>images/pic27.jpg" alt=""/>
	 				</div>
					<div class="col_1_of_2 span_1_of_2">
						 <img src="<?php echo URL; ?>images/pic22.jpg" alt=""/>
					  	<img src="<?php echo URL; ?>images/pic23.jpg" alt=""/>
					  	<img src="<?php echo URL; ?>images/pic26.jpg" alt=""/>
					  	<img src="<?php echo URL; ?>images/pic28.jpg" alt=""/>
					  </div><div class="clear"></div>
		   		 </div>
				<div class="clear"></div>
				</div>
	</div>