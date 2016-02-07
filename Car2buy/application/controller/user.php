<?php 


class User extends controller{
 
    private $category;
    private $product;
    private $security;
    private $user;
    public function __construct(){
           
           $this->category =$this->loadModel('CategoryModel');
           $this->product =$this->loadModel('ProductModel');
           $this->security =$this->loadModel('Security');
            $this->user =$this->loadModel('UserModel');
    }
	 
    public function Index()
    {
           $categories=$this->category->getAllCategories();
           $Products=$this->product->getAllProducts();
           require APP . 'view/_templates/header.php';
           require APP . 'view/_templates/menu.php';
           require APP . 'view/_templates/content.php';
           require APP . 'view/_templates/sidebar.php';
           require APP . 'view/_templates/footer.php';
    }
    public function login()
    {
    	   $categories=$this->category->getAllCategories();
           $Products=$this->product->getAllProducts();
           if (isset($_POST['submit']))
           {
               //$user =$this->loadModel('UserModel');
        	$validator =$this->loadModel('Validator');
                $this->security->post_secx();
                // get data
                $validator->setData('input_user',htmlentities($_POST['userName'],ENT_QUOTES) );
        	$validator->setData('input_pass', htmlentities($_POST['password'],ENT_QUOTES));
		// validate data
            if ($_POST['userName'] == '' || $_POST['password'] == '')
            {
        	// show error
		if ($_POST['userName'] == '') { $validator->setData('error_user', 'required field!'); }
		if ($_POST['password'] == '') { $validator->setData('error_pass', 'required field!'); }
        	require APP . 'view/user/login.php'; 
            }
            if (Token::checkToken($_POST['token'])==true) { 
                $result = $this->user->validateUser($validator->getData('input_user'),$validator->getData('input_pass'));
                 switch ($result) {
                        case 'valid':
                              header('location:'.URL);
                        break;
                        case 'invalid':
                              $validator->setData('login_fail', 'invalid user or password');
                              require APP . 'view/user/login.php'; 
			break;
			case 'blocked':
                              $validator->setData('login_fail', 'you are blocked for 30 minutes');
                               require APP . 'view/user/login.php'; 
			break;
                        default:
			# code...
			break;
                }
            }
	
        }
        else
        {    require APP . 'view/user/login.php';}
	}
    public function registerUser()
    {
           $categories=$this->category->getAllCategories();
    	   $Products=$this->product->getAllProducts();
	   if (isset($_POST['submit']))
           {
	       //$user =$this->loadModel('UserModel');
		$validator =$this->loadModel('Validator');
                $this->security->post_secx();
                // get data
                $validator->setData('input_userFirstName', htmlentities($_POST['userFirstName'],ENT_QUOTES));
                $validator->setData('input_userLastName', htmlentities($_POST['userLastName'],ENT_QUOTES));
                $validator->setData('input_userEmail', htmlentities($_POST['userEmail'],ENT_QUOTES));
                $validator->setData('input_userPassword', htmlentities($_POST['userPassword'],ENT_QUOTES));
                $validator->setData('input_userConfirmPassword', htmlentities($_POST['userConfirmPassword'],ENT_QUOTES));
	
                // validate data
                if ($_POST['userFirstName'] == '' || $_POST['userLastName'] == ''|| $_POST['userEmail'] == ''
                    || $_POST['userPassword'] == ''|| $_POST['userConfirmPassword'] == '')
                {
                    // show error
                    if ($_POST['userFirstName'] == '') { $validator->setData('error_userFirstName', 'required field!'); }
                    if ($_POST['userLastName'] == '') { $validator->setData('error_userLastName', 'required field!'); }
                    if ($_POST['userEmail'] == '') { $validator->setData('error_userEmail', 'required field!'); }
                    if ($_POST['userPassword'] == '') { $validator->setData('error_userPassword', 'required field!'); }
                    if ($_POST['userConfirmPassword'] == '') { $validator->setData('error_userConfirmPassword', 'required field!'); }
                    require APP . 'view/user/registeruser.php'; 
                }
                else if (!(preg_match("/^([a-zA-Z0-9])+([a-zA-Z0-9\._-])*@([a-zA-Z0-9_-])+([a-zA-Z0-9\._-]+)+$/", $_POST['userEmail']))){
                            $validator->setData('error_userEmail', 'invalid email'); 
                            require APP . 'view/user/registeruser.php';
                }
                else  if (!preg_match_all('$\S*(?=\S{8,})(?=\S*[a-z])(?=\S*[A-Z])(?=\S*[\d])(?=\S*[\W])\S*$', $_POST['userPassword'])){
                            $validator->setData('error_login', 'password should be 8 character long must include 1 lowercase letter 1 upparecase letter, 1 number and 1 speacial character '); 
                            require APP . 'view/user/registeruser.php'; 
		}
                else if ($_POST['userPassword'] != $_POST['userConfirmPassword']) { $validator->setData('error_userConfirmPassword', 'password does not match!'); 
                           require APP . 'view/user/registeruser.php'; }
                else if ($this->user->addNewUser($validator->getData('input_userFirstName'),$validator->getData('input_userLastName'),
                         $validator->getData('input_userEmail'),$validator->getData('input_userPassword')) == true)
                {          //echo 'success';
                            require APP . 'view/user/registeruser.php'; 
                }
                else
                {
                    $validator->setData('error_login', 'email is already registered'); 
                    require APP . 'view/user/registeruser.php'; 
                }
            }
            else
            {
                    require APP . 'view/user/registeruser.php'; 
            }

	}
	public function getAllUsers()
	{
	}

	public function logoutUser()
	{
	       Session::destroySession();
        }
}

?>