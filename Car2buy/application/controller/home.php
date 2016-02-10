<?php


class Home extends Controller
{
    
    public function index()
    {
        $category =$this->loadModel('CategoryModel');
  
    $categories=$category->getAllCategories();
    
    $Product =$this->loadModel('ProductModel');
   
    $Products=$Product->getAllProducts();
        // load views
        require APP . 'view/_templates/header.php';
         require APP . 'view/_templates/menu.php';
          require APP . 'view/_templates/content.php';
           require APP . 'view/_templates/sidebar.php';
           require APP . 'view/_templates/footer.php';
     
    }

    
}
