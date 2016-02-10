<?php 


class Product extends controller{
        private $category;
 	private $product;
 	private $comment;
        private $security;
  	
        
	public function __construct(){
		 $this->category =$this->loadModel('CategoryModel');
		 $this->product =$this->loadModel('ProductModel');
		 $this->comment =$this->loadModel('CommentsModel');
                 $this->security =$this->loadModel('Security');
	}
	 
	public function Index()
	{
		 $categories=$this->category->getAllCategories();
                  $Products=$this->product->getAllProducts();
		
    	require APP.'view/product/index.php';
	}

	public function productByCategory()
	{
		$categories=$this->category->getAllCategories();
		$categoryId =$_GET['id'];
                $this->security->get_secx();
		$Products = $this->product->getProductByCateogryId($categoryId);
		require APP . 'view/_templates/header.php';
                require APP . 'view/_templates/menu.php';
                require APP . 'view/_templates/content.php';
                require APP . 'view/_templates/sidebar.php';
                require APP . 'view/_templates/footer.php';
	}
	
	public function productDetails()
	{
		$categories=$this->category->getAllCategories();
		$productId =$_GET['id'];
		$Products = $this->product->getProductById($productId);
		$Comments = $this->comment->getAllCommentsByProductId($productId);
		
		require APP.'view/product/productDetails.php';
		
	}
	
	public function addNewProduct()
	{		
            if (isset($_POST['submit']))
            {
                $validator =$this->loadModel('Validator');
                $validator->setData('productName',htmlentities($_POST['productName'],ENT_QUOTES) );
                $validator->setData('productDescription', htmlentities($_POST['productDescription'],ENT_QUOTES));
                $validator->setData('categoryId',htmlentities($_POST['categoryId'],ENT_QUOTES) );
                if ($_POST['productName'] == '' || $_POST['productDescription'] == ''|| $_POST['categoryId'] == 0)
                {
                    // show error
                    if ($_POST['productName'] == '') { $validator->setData('error_productName', 'required field!'); }
                        if ($_POST['productDescription'] == '') { $validator->setData('error_productDescription', 'required field!'); }
                        if ($_POST['categoryId'] == 0) { $validator->setData('error_categoryId', 'required field!'); }
                        $categories=$this->category->getAllCategories();
                } 
                $target_dir = ROOT ."public/images/";
                $target_file = $target_dir . basename($_FILES["fileToUpload"]["name"]);
                $imageFileType = pathinfo($target_file,PATHINFO_EXTENSION);
                // Check if image file is a actual image or fake image
                $fileName =$_FILES["fileToUpload"]["name"];
                if ($_FILES["fileToUpload"]["name"]!="") {

                    $fileExists = false;
                    // Allow certain file formats
                    if($imageFileType != "jpg" && $imageFileType != "png" && $imageFileType != "jpeg"
                       && $imageFileType != "gif" ) {
                       $validator->setData("file_error","Invalid Image");
                       $fileExists = true;
                       $categories=$this->category->getAllCategories();
                        //require APP.'view/product/addNewProduct.php';

                    }
                    // Check if file already exists
                    if (file_exists($target_file)) {
                        $validator->setData("file_error","file already exists");
                        $fileExists = true;
                        $categories=$this->category->getAllCategories();
                        //require APP.'view/product/addNewProduct.php'; 
                    }
                    // Check file size
                    if ($_FILES["fileToUpload"]["size"] > 50000000) {
                        $validator->setData("file_error","image is to large, the image should be less than 2MB");
                        $fileExists = true;
                        $categories=$this->category->getAllCategories();
                        //require APP.'view/product/addNewProduct.php'; 
                    }
                    if ($fileExists==false && $_POST['categoryId'] != 0) {
                        if (move_uploaded_file($_FILES["fileToUpload"]["tmp_name"], $target_file))
                        {
                         
                           $fileName =$_FILES["fileToUpload"]["name"];
                           $categories=$this->category->getAllCategories();
                           if ( $this->product->addProduct(htmlentities($_POST['productName'],ENT_QUOTES),htmlentities($_POST['productDescription'],ENT_QUOTES),$fileName,htmlentities($_POST['categoryId'],ENT_QUOTES))) 
                           {
                                $validator->setData("file_error","Picture uploaded succesfully.");
                                require APP.'view/product/addNewProduct.php';
                           }


                        }
                        else{
                                $validator->setData("sorry","there was an error while uploading the image");
                                require APP.'view/product/addNewProduct.php';
                                
                        }
                    }
                    else{
                         require APP.'view/product/addNewProduct.php';
                   }
               }

               else{
                   $categories=$this->category->getAllCategories();
                    require APP.'view/product/addNewProduct.php';	
              } 

           }
           else{
                $categories=$this->category->getAllCategories();
                 require APP.'view/product/addNewProduct.php';	
           }  
		
    }

}

?>