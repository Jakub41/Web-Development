<?php 


class Comment extends controller{
  
	private $comment;
	private $product;
	private $category;
        private $security;
  	public function __construct(){
		$this->comment= $this->loadModel('CommentsModel');
		 $this->product =$this->loadModel('ProductModel');
		  $this->category =$this->loadModel('CategoryModel');
                   $this->security =$this->loadModel('Security');
	}
	public function Index()
	{
		
	$Products=$this->product->getAllProducts();
	$categories=$this->category->getAllCategories();
	       
        require APP . 'view/_templates/header.php';
         require APP . 'view/_templates/menu.php';
          require APP . 'view/_templates/content.php';
           require APP . 'view/_templates/sidebar.php';
           require APP . 'view/_templates/footer.php';
       	}
	
	public function addCommment()
	{
		if (isset($_POST['submit']))
		{
                    
                        $productId =$_POST['productId'];
                         $validator =$this->loadModel('Validator');
                         $Products = $this->product->getProductById($productId);
                         $Comments = $this->comment->getAllCommentsByProductId($productId);
                         $this->security->post_secx();                                                         
			$validator->setData('input_productId', htmlentities($_POST['productId'],ENT_QUOTES));
			$validator->setData('input_name', htmlentities($_POST['name'],ENT_QUOTES));
			
			$validator->setData('input_email', htmlentities($_POST['email'],ENT_QUOTES));
			$validator->setData('input_comments', htmlentities($_POST['comments'],ENT_QUOTES));
		
		if ($_POST['name'] == ''|| $_POST['email'] == ''|| $_POST['comments'] == '')
		{

		// show error
			if ($_POST['name'] == '') { $validator->setData('error_name', 'required field!'); }
			if ($_POST['email'] == '') { $validator->setData('error_email', 'required field!'); }
			if ($_POST['comments'] == '') { $validator->setData('error_comments', 'required field!'); }
			require APP . 'view/product/productDetails.php';
			//header('location:'.URL.'Product/ProductDetails?id='.$productId);
		}
		else if (!(preg_match("/^([a-zA-Z0-9])+([a-zA-Z0-9\._-])*@([a-zA-Z0-9_-])+([a-zA-Z0-9\._-]+)+$/", $_POST['email']))){
			$validator->setData('error_email', 'invalid email'); 
			
			require APP . 'view/product/productDetails.php';
		}
		else if (Token::checkToken($_POST['token'])==true) { 
			 $this->comment->addNewComment($validator->getData('input_productId'),$validator->getData('input_comments')
			 	,$validator->getData('input_name'),$validator->getData('input_email'));
			 header('location:'.URL.'product/productDetails?id='.$productId);
		}
	}
		
	
	}
	
	

	
}

?>