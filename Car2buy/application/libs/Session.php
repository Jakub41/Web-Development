<?php 
session_start();
session_regenerate_id();
class Session
{
	private  $userName;
	private $userType;
	private $tokenName;
	function ___construct()
	{
		 session_start();
	}
	public static function sessionStart()
	{
		session_start();	
	}
	public static function setToken($name,$value)
	{
		return $_SESSION[$name] =$value;
	}
	public static function getToken()
	{
		if(isset($_SESSION['tokenName']))
				  {
				  	return $tokenName= $_SESSION['tokenName'];
				  }
	}
	public static function setUserName($username)
	{
		
		$_SESSION['userName'] = $username;
	}
	public static function getUserName()
	{
		
		if(isset($_SESSION['userName']))
				  {
				  	return $userName= $_SESSION['userName'];
				  }
				  
	}
	public static function setUserType($usertype)
	{
		$_SESSION['usertype'] =$usertype;

	}
	public static function getUserType()
	{
		if(isset($_SESSION['usertype']))
				  {
				  	return $userName= $_SESSION['usertype'];
				  }
	}
	public static function destroySession($name)
	{

		
        if(isset($_SESSION['userName']))
        { 
        	$_SESSION = array();
        	if (ini_get("session.use_cookies")) {
		    $params = session_get_cookie_params();
		    setcookie(session_name(), '', time() - 3600,
		        $params["path"], $params["domain"],
		        $params["secure"], $params["httponly"]
		    );
			}
			session_destroy();
	  		header('location:'.URL);
       
        } 
        elseif (isset($_SESSION[$name])) {
        	unset($_SESSION[$name]);
        }
	}



}





