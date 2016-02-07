<?php 


class Category extends controller{
 
  	public function __construct(){
		
	}
	public function Index()
	{
		$category =$this->loadModel('CategoryModel');
	
	$categories=$category->getAllCategories();
	
       
        require APP . 'view/_templates/sidebar.php';
       	
    
       

	}
	
	
	
}

?>