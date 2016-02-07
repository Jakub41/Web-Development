<?php 
session_start();
class Session
{
	private  $userName;
	private $userType;
	function ___construct()
	{
		 session_start();
	}
	public static function sessionStart()
	{
		session_start();	
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
	public static function destroySession()
	{
		
        if(isset($_SESSION['userName']))
        { 
        	session_destroy();
       		header('location:'.URL);
       
        } 
	}
}





